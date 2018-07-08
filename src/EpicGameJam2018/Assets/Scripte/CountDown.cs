using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

	public int SecondsUntilNextScreen = 60;
	public GameObject CounterTextNode;
	private Text _SecondsCounter;

	private GameManager GM;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();

		_SecondsCounter = CounterTextNode.GetComponent<Text>();
		_SecondsCounter.text = SecondsUntilNextScreen.ToString();

		StartCoroutine(SwitchSceneAfter(SecondsUntilNextScreen));
	}

	IEnumerator SwitchSceneAfter(int seconds)
    {
        int secondsLeft = seconds;

        do
        {
            _SecondsCounter.text = secondsLeft.ToString();
            yield return new WaitForSeconds(1);
        } while (--secondsLeft > 0);
        
        GM.NextGame();
    }
}
