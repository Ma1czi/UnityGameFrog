using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToGoWhenClick : MonoBehaviour
{
	public CharacterAi player;

    [Header("Player Set")]
	[Range(0,2), SerializeField] private int floorNumber;
	[SerializeField] public float playerAngle;
	public Vector3 playerPosition;
	public bool cantMove;
	
	void Start()
	{
		player = FindObjectOfType<CharacterAi>();
	}
    private void OnMouseDown()
	{
		if(player != null && !player.cantMove)
		{
			player.goTo = playerPosition;
			player.whereToLook = playerAngle;
			player.whichFloor = floorNumber;
		}
	}
}
