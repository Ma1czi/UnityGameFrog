using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterStats))]
public class PlayerMovement : MonoBehaviour
{
	[Header("Floor Settings")]
	[SerializeField] private int characterFloorNumber = 0;
	[SerializeField] private float liftSpeed;
	[SerializeField] private Transform floorHolder;
	public Vector3[] floorPosition;

	[Header("Jump Settings")]
	[SerializeField] private float jumpDistance;
	[SerializeField] private float jumpHightMax;
	[SerializeField] private float bounceForce;
	[SerializeField] private float deceleratione;
	[SerializeField] private float tirednessWhenJump=1;
	float dbounceForce;
	Vector3 bouncingDirection;

	[Header("Move Settings")]
	[SerializeField] private float turnoverSpeed;
	[SerializeField] private float speed;
	[SerializeField] private float angleWhenStartToGo = 15;
	float angle;
	float targetAngle;
	Vector3 moveDiretory;

	[Header("Tongue Settings")]
	[SerializeField] private float tongueLength;
	[SerializeField] private float tongueSpeed;

	[Header("Lift Settings")]
	[SerializeField] private float liftWaitTime;
	float liftWaitTime2;
	float startLiftWaitTime;
	int startFloorNumber;

	[Header("Component")]
	Rigidbody playerRigidbody;
	CharacterStats stats;
	SphereCollider playerCollider;

	[Header("TrueOrFalse")]
	[SerializeField] private bool stopMoving;
	public bool isGrounded = false;
	bool lookAtFood;
	bool isEating;
	bool isChangingFloor;

    [Header("SitOnChair Settings")]
	[SerializeField] private bool isSitting;
	[SerializeField] private bool startStat;
	Vector3 startPoint;
	float startAngle;
	[Header("Animation Settings")]
	[SerializeField] private Animator myAnimator;

	private void Start()
	{
		characterFloorNumber = PlayerPrefs.GetInt("Floor");
		startLiftWaitTime = liftWaitTime;
		liftWaitTime2 = liftWaitTime;
		playerCollider = GetComponent<SphereCollider>();
		playerRigidbody = GetComponent<Rigidbody>();
		stats = GetComponent<CharacterStats>();
		floorPosition = new Vector3[floorHolder.childCount];
		for (int i=0; i< floorPosition.Length; i++)
		{
			floorPosition[i] = floorHolder.GetChild(i).position;
		}
	}
	private void FixedUpdate()
	{
		if (isGrounded)
		{
			Vector3 rotation = new Vector3(0, angle + 90, 0);
			playerRigidbody.MoveRotation(Quaternion.Euler(rotation));
		}
	}
    private void OnCollisionStay(Collision collision)
    {
		if (collision.gameObject.tag == "Ground")
		{
			isGrounded = true;
			dbounceForce = bounceForce;
			bouncingDirection = Vector3.forward;
		}
		else
			isGrounded = false;
    }
	public void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == ("Food"))
		{
			isEating = false;
		}

		if (collision.gameObject.tag == ("Wall"))
		{
			
			Vector3 dir = transform.forward;
			dir.Set(dir.x, 0, dir.z);
			dir = dir.normalized;
			Vector3 normal = collision.transform.forward;
			bouncingDirection = Vector3.Reflect(dir, normal);
			playerRigidbody.velocity = Vector3.zero;
			dbounceForce = bounceForce * deceleratione;
			float resultantAngle = angle + 180;
			if (!isGrounded)
			{
				transform.eulerAngles = Vector3.up * resultantAngle;
				angle = resultantAngle;

			}
			playerRigidbody.AddForce(bouncingDirection * dbounceForce, ForceMode.Impulse);
			Debug.DrawRay(transform.position + dir, normal, Color.black);
		}
	}
	public void ActionController(Vector3 point, float placeToLook, int floorNumber)
	{
		ToungeRayVision();
		if (CanMove())
		{
			//Debug.Log("a");
			MoveToPoint(point, placeToLook);
		} 
		if(CanChangeFloor(floorNumber))
		{
			//Debug.Log("b");
			ChangeFloor(point, placeToLook, floorNumber);
		}
		if (CanJump() && Input.GetKeyDown(KeyCode.Space))
		{
			//Debug.Log("c");
			//Jump();
		}
		if (transform.position.x == point.x || startStat)
        {
			//Debug.Log("d");
			stopMoving = true;
			SitOnChair(point, placeToLook);
        }
	} 
	private void ToungeRayVision()
    {
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hitInfo;
		Debug.DrawRay(transform.position, transform.forward * tongueLength, Color.red);
		lookAtFood = false;
		if (Physics.Raycast(ray, out hitInfo, tongueLength))
		{
			//Debug.Log(hitInfo.collider.gameObject.name);
			if (hitInfo.collider.gameObject.tag == "Food")
			{
				lookAtFood = true;
			}
			if (Input.GetKeyDown(KeyCode.Space) && hitInfo.collider.gameObject.tag == "Food")
			{
				if(hitInfo.collider.gameObject.TryGetComponent(out Food result))
				{
					isEating = true;
					//result.IsEaten(transform.gameObject, tongueSpeed);
				}
			}
		}

    }
	private void Jump()
    {
			stats.tiredness -= tirednessWhenJump;
			playerRigidbody.AddForce( Vector3.up * jumpHightMax, ForceMode.Impulse);
			playerRigidbody.AddForce(transform.forward * jumpDistance, ForceMode.Impulse);

    }
	private void ChangeFloor(Vector3 point, float placeToLook, int floorNumber)
	{
		stopMoving = true;
		isChangingFloor = true;
		if(floorNumber != characterFloorNumber)
			startFloorNumber = floorNumber;
		point = floorPosition[characterFloorNumber];
		placeToLook = 180;
		if (transform.position.x == floorPosition[characterFloorNumber].x)
		{
			angle = Mathf.LerpAngle(angle, placeToLook, Time.deltaTime * turnoverSpeed);
			liftWaitTime -= Time.deltaTime;
			if (ShouldMove(placeToLook, angle) && liftWaitTime <= 0)
            {
				playerCollider.isTrigger = true;
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, floorPosition[startFloorNumber].y, transform.position.z), liftSpeed * Time.deltaTime);
            }
			if (transform.position.y == floorPosition[startFloorNumber].y)
			{
				playerCollider.isTrigger = false;
				liftWaitTime2 -= Time.deltaTime;
				if(liftWaitTime2 < 0)
                {
					liftWaitTime2 = startLiftWaitTime;
					liftWaitTime = startLiftWaitTime;
					characterFloorNumber = startFloorNumber;
					stopMoving = false;
					isChangingFloor = false;
                }
			}
		}
		MoveToPoint(point, placeToLook);
	}
	private void SitOnChair(Vector3 point, float placeToLook)
	{
		if (!startStat)
        {
			startPoint = point;
			startAngle = placeToLook;
			startStat = true;
			
        }
		if (angle >= 360)
			angle -= 360;
		if (!isSitting)
        {
			angle = Mathf.LerpAngle(angle, startAngle, Time.deltaTime * turnoverSpeed);
			if (ShouldMove(startAngle, angle))
			{
				//playerCollider.isTrigger = true;
				playerRigidbody.useGravity = false;
				transform.position = Vector3.MoveTowards(transform.position, startPoint, speed * Time.deltaTime);
				if(transform.position.x == startPoint.x && transform.position.z == startPoint.z)
                {
					isSitting = true;
					myAnimator.SetBool("Move", false);
                }
			}

        }
        else
        {
			if(point != startPoint)
            {
				//playerCollider.isTrigger = false;
				playerRigidbody.useGravity = true;
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, 0), speed * Time.deltaTime);
				if (transform.position.z == 0)
				{
					myAnimator.SetBool("Move", true);
					isSitting = false;
					stopMoving = false;
					startStat = false;
				}
				
			}
        }

	}
	private void MoveToPoint(Vector3 point, float placeToLook)
    {
		Vector3 placeToGO = new Vector3(point.x, transform.position.y, transform.position.z);
		moveDiretory = (placeToGO - transform.position).normalized;
		float inputMagnitude = moveDiretory.magnitude;
		targetAngle = Mathf.Atan2(moveDiretory.x, moveDiretory.z) * Mathf.Rad2Deg;
		if (transform.position == placeToGO)
		{
			angle = Mathf.LerpAngle(angle, placeToLook, Time.deltaTime * turnoverSpeed);
		}
		angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnoverSpeed * inputMagnitude);
		if (Mathf.Abs(targetAngle) - Mathf.Abs(angle) < angleWhenStartToGo)
			transform.position = Vector3.MoveTowards(transform.position, placeToGO, speed * Time.deltaTime);
	}

	public void Die(string message)
	{
		Debug.Log(message);
		Debug.Break();
	}

	private bool CanJump()
    {
				return isGrounded && !lookAtFood && playerRigidbody.useGravity;
    }
	private bool CanMove()
    {
		return (isGrounded && !isEating && !stopMoving);
    }
	private bool CanChangeFloor(int floorNumber)
    {
		if (floorNumber != characterFloorNumber || isChangingFloor)
        {
			return !startStat;
        }
		return false;
	}
	private bool ShouldMove(float angle, float targetAngle)
    {
		return (360 - angleWhenStartToGo < Mathf.Abs((angle) - (targetAngle)) || Mathf.Abs((angle) - (targetAngle)) < angleWhenStartToGo);			
    }
}
