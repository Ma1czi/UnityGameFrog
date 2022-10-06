using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectToGoWhenClick))]
public class Bath : MonoBehaviour
{
	[Header("Bath Settings")]
	[SerializeField] private CharacterStats stats;
	[SerializeField] private ObjectToGoWhenClick clickComponent;
	[SerializeField] private Transform player;
	[SerializeField] private GameObject water;
	[SerializeField] private float howLongFillBath;
	[SerializeField] private float howLongBathe;
    [SerializeField] private Vector3 bathposition;
	[SerializeField] private CharacterAi ai;
	[SerializeField] private Tap tap;


	[Header("Bath Control")]
	float startHowLongFillBath;
	float StartHowLongBathe;
	public bool isFilling;
	public bool canBathe;
	public bool isBathe;


	private void Start()
    {
		startHowLongFillBath = howLongFillBath;
		StartHowLongBathe = howLongBathe;
    }

    void Update()
	{
		if(IsInDistance()) {
			isFilling = true;
			if (canBathe)
				Bathe();
		}
        if (isBathe && isFilling)
        {
			FillingBath();
		}
	}
	void FillingBath()
	{
		//StartAnimation
		water.SetActive(tap.xInput > 0);
		howLongFillBath -= Time.deltaTime * tap.xInput;
		if (howLongFillBath < 0)
        {
			//Stop Animation
			tap.xInput = 0;
			isFilling = false;
			canBathe = true;
        }
	}
	void Bathe()
	{
		ai.goTo = bathposition;
		ai.cantMove = true;
		howLongBathe -= Time.deltaTime;
		if(howLongBathe < 0)
        {
			stats.Dirty(-100);
			ai.goTo = clickComponent.playerPosition;
			howLongFillBath = startHowLongFillBath;
			howLongBathe = StartHowLongBathe;
			ai.cantMove = false;
			isBathe = false;
			canBathe = false;	
		}
	}
	bool IsInDistance()
    {
		return (Vector3.Distance(player.position, clickComponent.playerPosition) < 0.1f || Vector3.Distance(player.position, bathposition) < 0.1f);

	}
    private void OnMouseDown()
    {
		if (!isBathe)
			isFilling = false;
		isBathe = true;
    }
}
