using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laptop : MonoBehaviour
{
    [Header("TikTakToe Settings")]
	[SerializeField] private Transform tikTakToeHolder;
	[SerializeField] private Transform prefabsHolder;
	[SerializeField] private Transform xPrefab;
	[SerializeField] private Transform oPrefab;
	[SerializeField] private Text win;
	[SerializeField] private GameObject endGamePanel;
	int[] arr = {0,1,2,3,4,5,6,7,8};
	List<int> usedButtons = new();
	Vector3[] buttonPosition;
	Transform[] prefabs;
	int player = 1;
	bool endWindow;

	[Header("Open Laptop Settings")]
	[SerializeField] private Transform playerPosition;
	[SerializeField] private CharacterAi ai;
	[SerializeField] private CharacterStats stat;
 	[SerializeField] private GameObject panel;
	[SerializeField] private float distance;
	[SerializeField] private float timeHowLongHaveInterest;
	[SerializeField] private float interest;
	[SerializeField] private float howSlowToCharge;
	float startTimeHowLongHaveInterest;

	void Start()
	{
		startTimeHowLongHaveInterest = timeHowLongHaveInterest;
		buttonPosition = new Vector3[tikTakToeHolder.childCount];
		for (int i = 0; i < buttonPosition.Length; i++)
		{
			buttonPosition[i] = tikTakToeHolder.GetChild(i).position;
		}
	}
    private void Update()
    {
        if (panel.active && timeHowLongHaveInterest != 0)
        {
			timeHowLongHaveInterest -= Time.deltaTime;
			timeHowLongHaveInterest = Mathf.Clamp(timeHowLongHaveInterest, 0, startTimeHowLongHaveInterest);
			if (timeHowLongHaveInterest > 0)
				stat.Boredom(interest * Time.deltaTime);
        }
        else if(!panel.active && timeHowLongHaveInterest != startTimeHowLongHaveInterest)
        {
			timeHowLongHaveInterest += Time.deltaTime/ howSlowToCharge;
			timeHowLongHaveInterest = Mathf.Clamp(timeHowLongHaveInterest, 0, startTimeHowLongHaveInterest);
		}
        if (endWindow && Input.GetKeyDown(KeyCode.Space))
        {
			GameReset();
        }
    }
    public void TikTakToeButton(int buttonId)
	{

		if (!usedButtons.Contains(buttonId) && !endWindow)
		{

			if(player%2 == 0)
			{
				Debug.Log("Player 2");
				Instantiate(xPrefab, buttonPosition[buttonId], Quaternion.Euler(Vector3.zero), prefabsHolder);
				arr[buttonId] = 11;
			}
			else
			{
				Debug.Log("Player 1");
				Instantiate(oPrefab, buttonPosition[buttonId], Quaternion.Euler(Vector3.zero), prefabsHolder);
				arr[buttonId] = 10;
			}
			player++;
			usedButtons.Add(buttonId);
			if(CheckWin() == 1)
			{
				Debug.Log("Win");
				PlayerWin(player % 2);

			}
			if(CheckWin() == -1)
			{
				Debug.Log("Draw");
				Draw();
			}
		}
	}
	int CheckWin()
	{
		#region Horzontal Winning Condtion
		//Winning Condition For First Row
		if (arr[0] == arr[1] && arr[1] == arr[2])
		{
			return 1;
		}
		//Winning Condition For Second Row
		else if (arr[3] == arr[4] && arr[4] == arr[5])
		{
			return 1;
		}
		//Winning Condition For Third Row
		else if (arr[6] == arr[7] && arr[7] == arr[8])
		{
			return 1;
		}
		#endregion
		#region vertical Winning Condtion
		//Winning Condition For First Column
		else if (arr[0] == arr[3] && arr[3] == arr[6])
		{
			return 1;
		}
		//Winning Condition For Second Column
		else if (arr[1] == arr[4] && arr[4] == arr[7])
		{
			return 1;
		}
		//Winning Condition For Third Column
		else if (arr[2] == arr[5] && arr[5] == arr[8])
		{
			return 1;
		}
		#endregion
		#region Diagonal Winning Condition
		else if (arr[0] == arr[4] && arr[4] == arr[8])
		{
			return 1;
		}
		else if (arr[2] == arr[4] && arr[4] == arr[6])
		{
			return 1;
		}
		#endregion
		#region Checking For Draw
		// If all the cells or values filled with X or O then any player has won the match
		else if (arr[0] != 0 && arr[1] != 1 && arr[2] != 2 && arr[3] != 3 && arr[4] != 4 && arr[5] != 5 && arr[6] != 6 && arr[7] != 7 && arr[8] != 8)
		{
			return -1;
		}
		#endregion
		else
		{
			return 0;
		}
	}
	void GameReset()
	{
		endWindow = false;
		endGamePanel.SetActive(false);
		player = 1;
		arr = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
		usedButtons.Clear();
		prefabs = new Transform[prefabsHolder.childCount];
		for (int i = 0; i < prefabs.Length; i++)
		{
			prefabs[i] = prefabsHolder.GetChild(i);
			Destroy(prefabs[i].gameObject);
		}
	}
	public void Exit()
    {
		GameReset();
		panel.SetActive(false);
		ai.cantMove = false;
    }
    private void OnMouseDown()
    {
		if (Vector3.Distance(playerPosition.position, transform.position) < distance)
		{
			panel.SetActive(true);
			ai.cantMove = true;
        }
    }
	void PlayerWin(int playerNumber)
    {
		endWindow = true;
		endGamePanel.SetActive(true);
		if(playerNumber == 0)
			win.text = $"Player One Win";
		else
			win.text = $"Player Two Win";
	}
	void Draw()
    {
		endWindow = true;
		endGamePanel.SetActive(true);
		win.text = "Draw";
		
	}
}
