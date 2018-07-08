using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntermediateScript : MonoBehaviour {

    private const int SecondsUntilNextScreen = 15;

    private GameManager GM;

    public Text P1Score;
    public Text P2Score;
    public Text P3Score;
    public Text P4Score;

    public Text SecondsCounter;

    // Use this for initialization
    void Start () {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        P1Score.text = GM.GetScore(PlayerId.P1).ToString();
        P2Score.text = GM.GetScore(PlayerId.P2).ToString();
        P3Score.text = GM.GetScore(PlayerId.P3).ToString();
        P4Score.text = GM.GetScore(PlayerId.P4).ToString();

        StartCoroutine(SwitchSceneAfter(SecondsUntilNextScreen));
    }

    IEnumerator SwitchSceneAfter(int seconds)
    {
        int secondsLeft = seconds;

        do
        {
            SecondsCounter.text = secondsLeft.ToString();
            yield return new WaitForSeconds(1);
        } while (--secondsLeft > 0);
        
        SceneManager.LoadScene(GM.GetNextScene());
    }
}
