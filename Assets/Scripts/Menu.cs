using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Start Stats")]
    [SerializeField] private float hunger = 100;
    [SerializeField] private float tiredness = 100;
    [SerializeField] private float boredom = 100;
    [SerializeField] private float dirty = 1;
    [SerializeField] private Vector3 position;
    [SerializeField] private int floorNumber;

    public void Game()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    public void Credits()
    {
        SceneManager.LoadScene(2);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Reset()
    {
        PlayerPrefs.SetFloat("Hunger", hunger);
        PlayerPrefs.SetFloat("Tiredness", tiredness);
        PlayerPrefs.SetFloat("Boredom", boredom);
        PlayerPrefs.SetFloat("Dirty", dirty);
        PlayerPrefs.SetFloat("PositionX", position.x);
        PlayerPrefs.SetInt("Floor", floorNumber);
        PlayerPrefs.SetFloat("PositionY", position.y);
    }
}