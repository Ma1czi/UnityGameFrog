using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("Object Transform")]
	[SerializeField] private Transform player;
	[SerializeField] private Transform fullView;

	[Header("DistanceFromPlayer")]
	[SerializeField] private float distanceX = 10;
	[SerializeField] private float distanceY = 3;

    [Header("Camera Movement Settings")]
	[SerializeField] private float speed = 20;
	public bool followPlayer = true;
	public bool isZoom;

	
	void Update()
	{
        if (!isZoom)
        {
			if (Input.GetKeyDown(KeyCode.E))
			{
				if (followPlayer)
					followPlayer = false;
				else
					followPlayer = true;
			}
			if (followPlayer)
			{
				LookAtPlayer();
			}
			if (!followPlayer)
				AllLook();

        }
	}
	public void Zoom(Vector3 position)
    {
		isZoom = true;
		transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
	}
	void LookAtPlayer()
    {
		Vector3 direction = new Vector3(player.position.x, player.position.y + distanceY, player.position.z - distanceX);
		transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
	}
	void AllLook()
    {
		transform.position = Vector3.MoveTowards(transform.position, fullView.position, speed * Time.deltaTime);
	}
}
