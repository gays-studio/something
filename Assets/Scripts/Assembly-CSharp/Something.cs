using TMPro;
using UnityEngine;

public class Something : MonoBehaviour
{
	private enum GameState
	{
		NotStarted = 0,
		InProgress = 1,
		GameOver = 2
	}

	private GameState currentState;

	private float activityTime;

	private float startupPeriod = 1f;

	private float stateChangeTime;

	public TMP_Text timerText;

	private string somethingLabel = "<color=#000000>Something</color>";

	private string nothingLabel = "<color=#FFFFFF>Nothing</color>";

	protected bool didNothing
	{
		get
		{
			if (!Input.anyKeyDown && !Input.GetMouseButtonDown(0) && Input.GetAxis("Mouse X") == 0f)
			{
				return Input.GetAxis("Mouse Y") == 0f;
			}
			return false;
		}
	}

	private void Start()
	{
		timerText.text = "Press any key to start doing " + somethingLabel + "\n";
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		switch (currentState)
		{
		case GameState.NotStarted:
			if (Input.anyKeyDown)
			{
				currentState = GameState.InProgress;
				stateChangeTime = Time.time;
			}
			else if (Time.time >= startupPeriod)
			{
				timerText.text = "Press any key to start doing " + somethingLabel + "\n";
			}
			break;
		case GameState.InProgress:
			timerText.text = "You have been doing " + somethingLabel + " for\n" + TimeTextFormatted() + "\n";
			activityTime += Time.deltaTime;
			if (didNothing)
			{
				if (Time.time - stateChangeTime >= 1f)
				{
					stateChangeTime = Time.time;
					currentState = GameState.GameOver;
				}
			}
			else
			{
				stateChangeTime = Time.time;
			}
			break;
		case GameState.GameOver:
			timerText.text = "You did " + nothingLabel + ", you lost\n You did " + somethingLabel + " for " + TimeTextFormatted();
			if (Time.time - stateChangeTime >= 1f && Input.anyKeyDown)
			{
				ResetGame();
			}
			break;
		}
	}

	private void ResetGame()
	{
		currentState = GameState.NotStarted;
		activityTime = 0f;
		timerText.text = "Press any key to start " + somethingLabel + "\n";
		stateChangeTime = Time.time;
	}

	private string TimeTextFormatted()
	{
		int num = (int)activityTime;
		int num2 = num / 86400;
		num %= 86400;
		int num3 = num / 3600;
		num %= 3600;
		int num4 = num / 60;
		num %= 60;
		string text = "";
		if (num2 > 0)
		{
			text = text + num2 + ((num2 == 1) ? " day" : " days") + ", ";
		}
		if (num3 > 0)
		{
			text = text + num3 + ((num3 == 1) ? " hour" : " hours") + ", ";
		}
		if (num4 > 0)
		{
			text = text + num4 + ((num4 == 1) ? " minute" : " minutes") + ", ";
		}
		return text + num + ((num == 1) ? " second" : " seconds");
	}
}
