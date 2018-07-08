using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheek : MonoBehaviour {

	private Color _startColor = Color.white;
	private SpriteRenderer _sr;

	[Range(0.0f, 1.0f)]
	public float CurrentRedValue = 0.0f;
	public float ColoringDelta = 0.05f;

	private GameManager _gameManager;

	public GameObject CounterTextNode;
	private Text _SecondsCounter;
	public GameObject WinTextNode;
	private Text _winText;

	void OnTriggerEnter2D(Collider2D col)
	{
		CurrentRedValue += ColoringDelta;
		_sr.material.color = new Color(CurrentRedValue, 0f, 0f, 1f);
		if (CurrentRedValue >= 1.0f)
		{
			DeclareWinners(col.gameObject.GetComponentInChildren<Brush>().TeamMember1,
			col.gameObject.GetComponentInChildren<Brush>().TeamMember2);
		}
	}

	// Use this for initialization
	void Start () {
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		_winText = WinTextNode.GetComponent<Text>();
		_winText.text = "";
		_SecondsCounter = CounterTextNode.GetComponent<Text>();
		_SecondsCounter.text = "";

		CurrentRedValue = 0f;
		_sr = GetComponentInChildren<SpriteRenderer>();
		_sr.material.color = _startColor;
	}
	
	private void DeclareWinners(PlayerId p1, PlayerId p2)
	{
		_gameManager.AddScore(p1);
		_gameManager.AddScore(p2);
		string winMsg = "Team " + p1 + " and " + p2 + " managed to apply rouge to the panda!!";
		_winText.text = winMsg;
		StartCoroutine(SwitchSceneAfter(3));
	}

	IEnumerator SwitchSceneAfter(int seconds)
    {
        int secondsLeft = seconds;

        do
        {
            _SecondsCounter.text = secondsLeft.ToString();
            yield return new WaitForSeconds(1);
        } while (--secondsLeft > 0);
        
        _gameManager.NextGame();
    }
}
