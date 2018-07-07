using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;


    public enum PlayerId { P1, P2, P3, P4 }


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
        players.Add(PlayerId.P3, new Player(KeyCode.J, KeyCode.K));
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
    public void SetActionsForPlayer(PlayerId player, Action actionA, Action actionB)
    {
        players[player].KeyOneCallback = actionA;
        players[player].KeyTwoCallback = actionB;
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


    public void NextGame()
    {
        foreach (var player in players.Values)
        {
            player.ClearInputs();
        }

        //TODO start next game
    }


    //PLAYER CLASS (sotres player data and input behaviours)
    public class Player
    {
        private readonly KeyCode _keyOneCode;
        private readonly KeyCode _keyTwoCode;


        public int Score { get; set; }
        public Action KeyOneCallback { private get; set; }
        public Action KeyTwoCallback { private get; set; }


        public Player(KeyCode keyOne, KeyCode keyTwo)
        {
            _keyOneCode = keyOne;
            _keyTwoCode = keyTwo;
        }


        public void ProcessInput()
        {
            if (Input.GetKeyDown(_keyOneCode))
            {
                KeyOneCallback.Invoke();
            }

            if (Input.GetKeyDown(_keyTwoCode))
            {
                KeyTwoCallback.Invoke();
            }
        }

        public void ClearInputs()
        {
            KeyOneCallback = null;
            KeyTwoCallback = null;
        }
    }
}
