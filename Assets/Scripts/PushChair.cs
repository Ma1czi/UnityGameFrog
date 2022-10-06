using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushChair : MonoBehaviour
{
    [Header("Push Settings")]
	[SerializeField] private float pushDirectionX;
	[SerializeField] private float pushDirectionZ;
	[SerializeField] private float angle;
	[SerializeField] private float speed;

    [Header("Push When Sit Settings")]
	[SerializeField] private bool moveWhenSit;
	[SerializeField] private float pushDirectionXWhenSit;
	[SerializeField] private float pushDirectionZWhenSit;
	[SerializeField] private float rotationWhenSit;

    [Header("Componets")]
	[SerializeField] private ObjectToGoWhenClick click;
	[SerializeField] private Transform player;

    [Header("Start Value")]
	Vector3 direction;
	Vector3 directionWhenSit;
	Transform startSetup;

	void Start()
	{
		startSetup = transform;
		direction = new Vector3(transform.position.x + pushDirectionX, transform.position.y, transform.position.z + pushDirectionZ);
		directionWhenSit = new Vector3(transform.position.x + pushDirectionXWhenSit, transform.position.y, transform.position.z + pushDirectionZWhenSit);
	}
	void Update()
	{

			if (Vector3.Distance(click.playerPosition, player.position) < 0.1f)
			{
				transform.eulerAngles = Vector3.up * angle;
				transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
			}
			else
			{
				transform.eulerAngles = Vector3.up * click.playerAngle;
				transform.position = Vector3.MoveTowards(transform.position, startSetup.position, speed * Time.deltaTime);
			}
	}
}
