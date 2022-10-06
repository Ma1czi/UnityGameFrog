using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FastMenu : MonoBehaviour
{
    [Header("Menu Settings")]
    [SerializeField] private GameObject panel;
    [SerializeField] private CharacterStats stats;
    [SerializeField] private CharacterAi ai;
    public bool openMenu;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (openMenu)
            {
                openMenu = false;
                panel.SetActive(false);
            }
            else
            {
                openMenu = true;
                panel.SetActive(true);
            }
        }
    }
    public void Return()
    {
        openMenu = false;
        panel.SetActive(false);
    }
    public void Menu()
    {
        PlayerPrefs.SetFloat("Hunger", stats.hunger);
        PlayerPrefs.SetFloat("Tiredness", stats.tiredness);
        PlayerPrefs.SetFloat("Boredom", stats.boredom);
        PlayerPrefs.SetFloat("Dirty", stats.dirty);
        PlayerPrefs.SetFloat("PositionX", ai.goTo.x);
        PlayerPrefs.SetInt("Floor", ai.whichFloor);
        PlayerPrefs.SetFloat("PositionY", ai.pMovement.floorPosition[ai.whichFloor].y);

        SceneManager.LoadScene(0);
    }
}
