using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//aha
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
	[Header("Reflection Settings")]
	[Range(1,30), SerializeField] private float maxJump;
	[Range(1,10), SerializeField] private float minJump;
	[SerializeField] private float maxX;
	[SerializeField] private float maxZ;
	[SerializeField] private float distanceWhenCanPlay;

	[Header("Stats")]
	[SerializeField] private float interest;
	[Range(0.5f,1), SerializeField] private float boredomWithBall;
	[Range(0, 1), SerializeField] private float interestWithBall;
	float startInterest;

	[Header("Component")]
	Rigidbody ballRigidbody;
	CharacterStats player;
	[SerializeField] private Transform playertransform;

	private void Start()
	{
		startInterest = interest;
		ballRigidbody = GetComponent<Rigidbody>();
		Plane groundPlan = new Plane(Vector3.forward, Vector3.zero);
		player = FindObjectOfType<CharacterStats>();
	}
	private void Update()
	{
		Interest();
	}
	private void OnMouseDown()
	{
		if (Vector3.Distance(playertransform.position, transform.position) < distanceWhenCanPlay)
		{
			player.Boredom(interest);
			Vector3 randomDirection = new Vector3(Random.Range(-maxX, maxX), Random.Range(minJump, maxJump), Random.Range(-maxZ, maxZ));
			ballRigidbody.AddForce(randomDirection, ForceMode.Impulse);
			interest *= boredomWithBall;
		}
	}
	void Interest()
    {
		interest += interest * interestWithBall * Time.deltaTime;
		interest = Mathf.Clamp(interest, 0.1f, startInterest);
	}
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.gameObject.tag == ("Player"))
		{
			Vector3 dir = transform.position - collision.collider.gameObject.transform.position;
			player.boredom += interest;
			ballRigidbody.AddForce(dir.x*2, Random.Range(minJump, maxJump), dir.z*2, ForceMode.Impulse);
		}
	}
}
 
