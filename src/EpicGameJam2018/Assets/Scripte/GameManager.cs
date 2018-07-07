using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerId { P1, P2, P3, P4 }
public enum KeyCallback { KeyOneDown, KeyOnePressed, KeyOneUp, KeyTwoDown, KeyTwoPressed, KeyTwoUp }

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    private readonly string[] _minigames = { "CatchThePresents", "Pong" }; //TODO add panda CatchThePresents
    private int _nextGame = 0;

    private Dictionary<PlayerId, Player> players = new Dictionary<PlayerId, Player>();

	// Use this for initialization
	void Start () {
	    if (_instance == null)
	    {
	        _instance = this;
	    } else if (_instance != this)
	    {
            Destroy(gameObject);
	    }

        DontDestroyOnLoad(gameObject); //don't destroy parent

        // DO FUN STUFF BELOW ;)
        Init();
	}

    private void Init()
    {
        // create 4 players
        players.Add(PlayerId.P1, new Player(KeyCode.Q, KeyCode.W));
        players.Add(PlayerId.P2, new Player(KeyCode.X, KeyCode.C));
        players.Add(PlayerId.P3, new Player(KeyCode.O, KeyCode.P));
        players.Add(PlayerId.P4, new Player(KeyCode.LeftArrow, KeyCode.RightArrow));
    }
	
	// Update is called once per frame
	void Update ()
	{
        //do actions
	    foreach (var player in players.Values)
	    {
	        player.ProcessInput();
	    }
	}

    //METHODS
    public void SetActionsForPlayer(PlayerId player, Dictionary<KeyCallback, Action> callbacks)
    {
        players[player].Callbacks = callbacks;
    }


    public int GetScore(PlayerId player)
    {
        return players[player].Score;
    }

    public int AddScore(PlayerId player)
    {
        return ++players[player].Score;
    }

    public int SubScore(PlayerId player)
    {
        return --players[player].Score;
    }

    /// <summary>
    /// Call this method at the end of each minigame
    /// </summary>
    public void NextGame()
    {
        foreach (var player in players.Values)
        {
            player.ClearInputs();
        }

        //1. if nextGame is last game -> vic screen
        if (_nextGame == _minigames.Length) //we finished the list, show final score
        {
            //TODO switch to final score
        }
        else //2. if next game is just another game -> first intermediate score
        {
            SceneManager.LoadScene("IntermediateScore");
        }

        _nextGame++;
    }

    public string GetNextScene()
    {
        return _minigames[_nextGame];
    }


    //PLAYER CLASS (sotres player data and input behaviours)
    public class Player
    {
        private readonly KeyCode _keyOneCode;
        private readonly KeyCode _keyTwoCode;

        public Dictionary<KeyCallback, Action> Callbacks { private get; set; }
        public int Score { get; set; }

        public Player(KeyCode keyOne, KeyCode keyTwo)
        {
            _keyOneCode = keyOne;
            _keyTwoCode = keyTwo;
        }

        public void ProcessInput()
        {
            Action action;

            if (Input.GetKeyDown(_keyOneCode)
            && Callbacks.TryGetValue(KeyCallback.KeyOneDown, out action))
            {
                action.Invoke();
            }

            if (Input.GetKey(_keyOneCode)
            && Callbacks.TryGetValue(KeyCallback.KeyOnePressed, out action))
            {
                action.Invoke();
            }

            if (Input.GetKeyUp(_keyOneCode)
            && Callbacks.TryGetValue(KeyCallback.KeyOneUp, out action))
            {
                action.Invoke();
            }

            if (Input.GetKeyDown(_keyTwoCode)
            && Callbacks.TryGetValue(KeyCallback.KeyTwoDown, out action))
            {
                action.Invoke();
            }

            if (Input.GetKey(_keyTwoCode)
            && Callbacks.TryGetValue(KeyCallback.KeyTwoPressed, out action))
            {
                action.Invoke();
            }

            if (Input.GetKeyUp(_keyTwoCode)
            && Callbacks.TryGetValue(KeyCallback.KeyTwoUp, out action))
            {
                action.Invoke();
            }
        }

        public void AddInputs(Dictionary<KeyCallback, Action> callbacks)
        {
            foreach (KeyCallback key in callbacks.Keys)
            {
                this.Callbacks[key] = callbacks[key];
            }
        }

        public void ClearInputs()
        {
            Callbacks.Clear();
        }
    }
}
