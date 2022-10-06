using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimney : MonoBehaviour
{
    [Header("Chimney Settings")]
	[SerializeField] private bool burningChimney;
	[SerializeField] private float howLongShouldBurning;
	[SerializeField] private GameObject fire;
	[SerializeField] private float dirty;
	float startHowLongShouldBurning;
	bool isBurning;

    [Header("Components")]
	[SerializeField] private Transform player;
	[SerializeField] private ObjectToGoWhenClick click;
	[SerializeField] private CharacterStats stat;

	void Start()
	{
		startHowLongShouldBurning = howLongShouldBurning;
	}

	void Update()
	{
		if(CanBurningChimney())
			BurningChimney();
	}
	void BurningChimney()
    {
		fire.SetActive(true);
		howLongShouldBurning -= Time.deltaTime;
		if(howLongShouldBurning < 0)
        {
			isBurning = false;
			fire.SetActive(false);
			howLongShouldBurning = startHowLongShouldBurning;
        }
    }
	bool CanBurningChimney()
    {
		if(Vector3.Distance(player.position, click.playerPosition) < 0.2f)
        {
			howLongShouldBurning = startHowLongShouldBurning;
			isBurning = true;

			stat.Dirty(dirty * Time.deltaTime);

		}
		return isBurning;

	}
}
