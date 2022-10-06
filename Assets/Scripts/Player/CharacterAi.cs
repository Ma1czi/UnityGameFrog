using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class CharacterAi : MonoBehaviour
{
	[Header("PointToGo")]
    [SerializeField] private float playerRadius;
	public Vector3 goTo;
	public float whereToLook;
	public int whichFloor = 0;
	public bool cantMove;
	public PlayerMovement pMovement;
	[SerializeField] private FastMenu menu;

    [Header("Camera")]
	Camera mainCamera;
	
	public void Start()
	{
		mainCamera = Camera.main;
		transform.position = new Vector3(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"), 0);
		goTo = transform.position;
		whichFloor = PlayerPrefs.GetInt("Floor");
	}
	public void Update()
	{
		if(!menu.openMenu)
			pMovement.ActionController(goTo, whereToLook, whichFloor);
		if(Input.GetKeyDown(KeyCode.Mouse0))
			GoToPlaceWhereClick();
	}
	void GoToPlaceWhereClick()
    {
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;

		if(Physics.Raycast(ray, out hitInfo) && !cantMove)
        {
			Vector3 point = ray.GetPoint(hitInfo.distance);
            if (hitInfo.collider.tag == "Ground")
            {
				whereToLook = 180;
				if (hitInfo.collider.name == "First")
                {
					goTo = new Vector3(point.x, point.y + playerRadius, 0);
					whichFloor = 0;
                }
					
				if(hitInfo.collider.name == "Second")
                {
					goTo = new Vector3(point.x, point.y+ playerRadius, 0);
					whichFloor = 1;
				}

				if(hitInfo.collider.name == "Third")
                {
					goTo = new Vector3(point.x, point.y+ playerRadius, 0);
					whichFloor = 2;
				}

            }
        }
    }
}
