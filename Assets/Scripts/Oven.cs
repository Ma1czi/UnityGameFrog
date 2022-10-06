using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oven : MonoBehaviour
{
    [Header("Oven Settings")] 
    [SerializeField] private Transform player;
    [SerializeField] private ObjectToGoWhenClick clickComponent;
    [SerializeField] private GameObject panel;
    bool close;

    private void Update()
    {
        if (player.position == clickComponent.playerPosition && !close)
            panel.SetActive(true);
        else
            panel.SetActive(false);
        if (panel.active == true && Input.GetKeyDown(KeyCode.Escape))
        {
            close = true;
            panel.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        close = false;
    }
}
