using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour
{
    [Header("Camera Settings")]
	[SerializeField] private Transform zoom;
	[SerializeField] private MainCamera mainCamera;
	bool zoomOn;

    [Header("Mouse Settings")]
	[SerializeField] private float mouseSensivity;
	public float xInput;

    [Header("Tap Settings")]
	[SerializeField] private float maxAngle;

    private void Update()
    {
		Zoom();
    }
    private void OnMouseDown()
    {
		zoomOn = true;
    }
    private void OnMouseDrag()
	{
		xInput += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensivity;
		xInput = Mathf.Clamp(xInput, 0, 1);
		transform.eulerAngles = Vector3.back * xInput * maxAngle;
	}
	void Zoom()
	{
		if (Input.GetKeyDown(KeyCode.E) && zoomOn)
		{
			zoomOn = false;
			mainCamera.isZoom = false;
			mainCamera.followPlayer = true;
		}
		if (zoomOn)
			mainCamera.Zoom(zoom.position);
	}
}
