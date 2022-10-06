using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    [Header("Zoom Settings")]
	[SerializeField] private Transform zoom;
	[SerializeField] private Transform player;
	bool zoomon;

    [Header("Componets Settings")]
	[SerializeField] private MainCamera mainCamera;
	[SerializeField] private ObjectToGoWhenClick click;
	[SerializeField] private CharacterAi ai;

    private void OnMouseDown()
    {
		zoomon = true;
    }
    void Update()
	{
        if (IsPlayerAtPlace())
        {
			mainCamera.Zoom(zoom.position);
			CanPlayOnPiano();
        }
        if (Input.GetKeyDown(KeyCode.E) && zoomon)
        {
			zoomon = false;
			mainCamera.isZoom = false;
			mainCamera.followPlayer = true;
		}
	}
	bool IsPlayerAtPlace()
	{
		return (Vector3.Distance(player.position, click.playerPosition) < 0.1f && zoomon);	
	}
	void CanPlayOnPiano()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
			//Sound1
        }
		if (Input.GetKeyDown(KeyCode.S))
		{
			//Sound2
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			//Sound3
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			//Sound4
		}
	}
}
