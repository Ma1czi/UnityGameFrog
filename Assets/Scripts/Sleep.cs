using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    [Header("Sleep Settings")]
    [SerializeField] private float energyRecovery;
    [SerializeField] private Vector3 getUp;
    [SerializeField] private Transform player;

    [Header("Componets")]
    [SerializeField] private CharacterStats stats;
    [SerializeField] private ObjectToGoWhenClick click;
 
    void Update()
    {
        if (Vector3.Distance(player.position, click.playerPosition) < 0.1f)
        {
            Sleeping();
        }
        else
            stats.offTirednessFactor = false;

    }
    void Sleeping()
    {
        stats.offTirednessFactor = true;
        stats.Tiredness(energyRecovery * Time.deltaTime);
        if(stats.tiredness > 99.8)
        {
            click.player.goTo = getUp;
        }
    }
}
