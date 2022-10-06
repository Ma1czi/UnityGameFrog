using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
	[Header("Fridge Settings")]
	[SerializeField] private Transform player;
	[SerializeField] private ObjectToGoWhenClick click;
	[SerializeField] private Food prefab;
	[SerializeField] private CharacterAi ai;
	[SerializeField] private Vector3 place;
	[SerializeField] private float angle;

	[Header("Throw Settings")]
	[SerializeField] private float throwAngle;
	[SerializeField] private float throwDistance;
	[SerializeField] private float throwSpeed;
	[SerializeField] private float waitTime;

	Food newFood;
	float startWaitTime;
	bool getFood;
	bool clickSpace;

    private void Start()
    {
		startWaitTime = waitTime;
    }
    void Update()
	{
		if (Vector3.Distance(player.position, click.playerPosition) < 0.1f)
			OpenFridgeDoor();
		else
		{
			CloseFridgeDoor();
			if (CanEat())
			{
				EatFood();
			}
		}
	}
	void OpenFridgeDoor()
	{
		//animationOpenDoor
		if (!getFood)
		{
			newFood = (Food)Instantiate(prefab, player.position + transform.forward / 3, Quaternion.Euler(0, 0, 0), player);
			ai.whereToLook = throwAngle;
			ai.goTo = place;
			getFood = true;
			ai.cantMove = true;
		}
	}
	void CloseFridgeDoor()
	{
		//AnimationCloseDoor
	}
	void EatFood()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			clickSpace = true;

		if(newFood != null && clickSpace)
        {
			waitTime -= Time.deltaTime;
			if(waitTime < 0)
				newFood.Throw(throwAngle, throwSpeed, throwDistance, player);
        }
        else if(newFood == null)
        {
			waitTime = startWaitTime;
			clickSpace = false;
			ai.cantMove = false;
			getFood = false;
        }
	}
	bool CanEat()
    {
		return (Vector3.Distance(player.position, place) < 0.1f);
    }
}
