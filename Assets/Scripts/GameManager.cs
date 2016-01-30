using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private GameObject canvasPause;
	[SerializeField]
	private GameObject canvasSelection;
	[SerializeField]
	private GameObject canvasHUD;
	[SerializeField]
	private GameObject canvasWin;
	[SerializeField]
	private GameObject canvasWinText;
	[SerializeField]
	private GameObject background;

	[SerializeField]
	private Text timer;
	[SerializeField]
	private int waitTime = 30;
	private int remainingTime;
	private float decreaseTime = 0;

	[SerializeField]
	private Text roundTimer;
	[SerializeField]
	private int roundTime = 66;
	private int roundRemainingTime;
	private float roundDecreaseTime = 0;

	PlayerStats player1 = new PlayerStats();
	PlayerStats player2 = new PlayerStats();

	public GameObject player1Prefab;
	public GameObject player2Prefab;

	[HideInInspector]
	public GameState state;
	private GameState lastState;

	// Enum for current game state
	public enum GameState
	{
		SELECTION,
		PLAYING,
		PAUSE,
		WIN
	}

    void Awake()
    {
        player1.items.Add((int)PART.HEAD, new List<Part>());
        player1.items.Add((int)PART.BODY, new List<Part>());
        player1.items.Add((int)PART.LEFTARM, new List<Part>());
        player1.items.Add((int)PART.RIGHTARM, new List<Part>());
        player1.items.Add((int)PART.LEFTLEG, new List<Part>());
        player1.items.Add((int)PART.RIGHTLEG, new List<Part>());

        player2.items.Add((int)PART.HEAD, new List<Part>());
        player2.items.Add((int)PART.BODY, new List<Part>());
        player2.items.Add((int)PART.LEFTARM, new List<Part>());
        player2.items.Add((int)PART.RIGHTARM, new List<Part>());
        player2.items.Add((int)PART.LEFTLEG, new List<Part>());
        player2.items.Add((int)PART.RIGHTLEG, new List<Part>());
    }

	void Start ()
	{
		state = GameState.SELECTION;
		lastState = state;

		remainingTime = waitTime;
		roundRemainingTime = roundTime;

		player1.playerObj = GameObject.FindGameObjectWithTag ("Player1");
		player2.playerObj = GameObject.FindGameObjectWithTag ("Player2");

		SetSelection ();
    }

    void Update ()
	{
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                Debug.Log("Button " + i + " pressed !");
            }
        }

        // fait le décompte sur la selection
        if (state == GameState.SELECTION)
		{
			if (remainingTime > 0)
			{
				remainingTime = waitTime - (int)decreaseTime;
				decreaseTime += Time.deltaTime;
				timer.text = remainingTime.ToString ();
			}
			else
			{
				decreaseTime = 0;
				timer.text = "Go !";
				StartGame ();
			}
		}

		// décompte sur la partie, trigger des events
		if (state == GameState.PLAYING)
		{
			if (roundRemainingTime > 0)
			{
				roundRemainingTime = roundTime - (int)roundDecreaseTime;
				roundDecreaseTime += Time.deltaTime;
				roundTimer.text = roundRemainingTime.ToString ();
			}
			else
			{
				roundDecreaseTime = 0;
				SetWin (null);
				// TODO: fire gaspar event
			}
		}

		// relance la partie après un win
		if (state == GameState.WIN)
		{
			if (Input.GetKeyDown ("joystick 1 button 0") || Input.GetKeyDown(KeyCode.Return))
			{
				SetSelection ();
			}
		}

		// Sets pause with start
		if (Input.GetKeyDown ("joystick 1 button 7"))
		{
			SetPause ();
		}

		if (player1 == null)
		{
			player1.playerObj = GameObject.FindGameObjectWithTag ("Player1");
		}

		if (player2 == null)
		{
			player2.playerObj = GameObject.FindGameObjectWithTag ("Player2");
		}

		if (Input.GetKeyDown (KeyCode.A))
		{
			SetWin (player1.playerObj);
		}
	}

	public void StartGame()
	{
		canvasSelection.SetActive (false);
		canvasHUD.SetActive (true);
		background.SetActive (true);
		player1.playerObj = (GameObject)Instantiate (player1Prefab);
		player2.playerObj = (GameObject)Instantiate (player2Prefab);
		roundRemainingTime = roundTime;
		state = GameState.PLAYING;
	}

	void SetPause ()
	{
		if (canvasPause.activeSelf)
		{
			canvasPause.SetActive (false);
			Time.timeScale = 1;
			state = lastState;
		}
		else
		{
			canvasPause.SetActive (true);
			Time.timeScale = 0;
			lastState = state;
			state = GameState.PAUSE;
		}
	}

	public void SetWin(GameObject player)
	{
		lastState = state;
		state = GameState.WIN;
		canvasHUD.SetActive (false);
		canvasWin.SetActive (true);

		if (player == null)
		{
			player1.nbCandies += 50;
			player2.nbCandies += 50;
		}
		else if (player == player1.playerObj)
		{
			player2.nbVictories++;
			player2.nbCandies += 75;
			player1.nbCandies += 25;
		}
		else
		{
			player1.nbVictories++;
			player1.nbCandies += 75;
			player2.nbCandies += 25;
		}

		Destroy (player1.playerObj);
		Destroy (player2.playerObj);
	}

	public void SetSelection()
	{
		state = GameState.SELECTION;
		remainingTime = waitTime;
		decreaseTime = 0;
		canvasHUD.SetActive (false);
		canvasPause.SetActive (false);
		canvasWin.SetActive (false);
		background.SetActive (false);
		canvasSelection.SetActive (true);
	}

	public void SetPlayerParts(int playerID, Dictionary<int, List<Part>> _items)
	{
		if (playerID == 1)
		{
			player1.items = _items;
		}
		else if (playerID == 2)
		{
			player2.items = _items;
		}
	}

	public Dictionary<int, List<Part>> GetPlayerParts(int playerID)
	{
		if (playerID == 1)
		{
			return player1.items;
		}
		else if (playerID == 2)
		{
			return player2.items;
		}
		return null;
	}

	public int GetNbCandies(int playerID)
	{
		if (playerID == 1)
		{
			return player1.nbCandies;
		}
		else if (playerID == 2)
		{
			return player2.nbCandies;
		}
		return 0;
	}

	public void SetPlayerPart(int playerID, int partType, Part part)
	{
		switch (partType)
		{
		case (int)PART.HEAD:
			if (playerID == 1)
			{
				player1.Head = part;
			}
			else
			{
				player2.Head = part;
			}
			break;
		case (int)PART.BODY:
			if (playerID == 1)
			{
				player1.Torso = part;
			}
			else
			{
				player2.Torso = part;
			}
			break;
		case (int)PART.LEFTARM:
			if (playerID == 1)
			{
				player1.ArmL = part;
			}
			else
			{
				player2.ArmL = part;
			}
			break;
		case (int)PART.LEFTLEG:
			if (playerID == 1)
			{
				player1.LegL = part;
			}
			else
			{
				player2.LegL = part;
			}
			break;
		case (int)PART.RIGHTARM:
			if (playerID == 1)
			{
				player1.ArmR = part;
			}
			else
			{
				player2.ArmR = part;
			}
			break;
		case (int)PART.RIGHTLEG:
			if (playerID == 1)
			{
				player1.LegR = part;
			}
			else
			{
				player2.LegR = part;
			}
			break;
		default:
			break;
		}
	}

    public Part GetPlayerPart(int playerID, int partType)
    {
        switch (partType)
        {
            case (int)PART.HEAD:
                if (playerID == 1)
                    return player1.Head;
                else
                    return player2.Head;
            case (int)PART.BODY:
                if (playerID == 1)
                    return player1.Torso;
                else
                    return player2.Torso;
            case (int)PART.LEFTARM:
                if (playerID == 1)
                    return player1.ArmL;
                else
                    return player2.ArmL;
            case (int)PART.LEFTLEG:
                if (playerID == 1)
                    return player1.LegL;
                else
                    return player2.LegL;
            case (int)PART.RIGHTARM:
                if (playerID == 1)
                    return player1.ArmR;
                else
                    return player2.ArmR;
            case (int)PART.RIGHTLEG:
                if (playerID == 1)
                    return player1.LegR;
                else
                    return player2.LegR;
            default:
                break;
        }
        return null;
    }

	public int GetPlayerVictories(int playerID)
	{
		if (playerID == 1)
			return player1.nbVictories;
		return player2.nbVictories;
	}

    class PlayerStats
	{
		GameObject _playerObj = null;
		int _nbVictories = 0;
		int _nbCandies = 0;

		// All the character active items
		Part head = new Part();
		Part armL = new Part();
		Part armR = new Part();
		Part torso = new Part();
		Part legL = new Part();
		Part legR = new Part();

		Dictionary<int, List<Part>> _items = new Dictionary<int, List<Part>>();

		public Part LegR
		{
			get { return legR; }
			set { legR = value; }
		}

		public Part LegL
		{
			get { return legL; }
			set { legL = value; }
		}

		public Part Torso
		{
			get { return torso;	}
			set { torso = value; }
		}

		public Part ArmR
		{
			get { return armR; }
			set { armR = value; }
		}

		public Part Head
		{
			get { return head; }
			set { head = value;	}
		}

		public Part ArmL {
			get { return armL; }
			set { armL = value;	}
		}

		public GameObject playerObj
		{
			get { return _playerObj; }
			set { _playerObj = value; }
		}

		public int nbVictories
		{
			get { return _nbVictories; }
			set { _nbVictories = value; }
		}

		public int nbCandies
		{
			get { return _nbCandies; }
			set { _nbCandies = value; }
		}

		public Dictionary<int, List<Part>> items
		{
			get { return _items; }
			set { _items = value; }
		}
	}
}
