using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntermediateScript : MonoBehaviour {

    private const int SecondsUntilNextScreen = 15;
    private const string CrownSprite = "Crown 1st";
    private const string ClownSprite = "Jesters cap";

    private GameManager GM;

    public Text P1Score;
    public Text P2Score;
    public Text P3Score;
    public Text P4Score;

    public Image P1Hat;
    public Image P2Hat;
    public Image P3Hat;
    public Image P4Hat;

    public Text SecondsCounter;

    // Use this for initialization
    void Start () {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        P1Score.text = GM.GetScore(PlayerId.P1).ToString();
        P2Score.text = GM.GetScore(PlayerId.P2).ToString();
        P3Score.text = GM.GetScore(PlayerId.P3).ToString();
        P4Score.text = GM.GetScore(PlayerId.P4).ToString();

        //scores
        int p1Score = GM.GetScore(PlayerId.P1);
        int p2Score = GM.GetScore(PlayerId.P2);
        int p3Score = GM.GetScore(PlayerId.P3);
        int p4Score = GM.GetScore(PlayerId.P4);
        int[] scores = {p1Score, p2Score, p3Score, p4Score};
        int maxScore = scores.Max();
        int minScore = scores.Min();

        SetHat(p1Score, maxScore, minScore, P1Hat);
        SetHat(p2Score, maxScore, minScore, P2Hat);
        SetHat(p3Score, maxScore, minScore, P3Hat);
        SetHat(p4Score, maxScore, minScore, P4Hat);

        StartCoroutine(SwitchSceneAfter(SecondsUntilNextScreen));
    }

    private void SetHat(int score, int max, int min, Image hatHolder)
    {
        if (score == max)
        {
            hatHolder.sprite = Resources.Load<Sprite>(CrownSprite);
            var color = hatHolder.color;
            color.a = 1;
            hatHolder.color = color;
        }
        else if (score == min)
        {
            hatHolder.sprite = Resources.Load<Sprite>(ClownSprite);
            var color = hatHolder.color;
            color.a = 1;
            hatHolder.color = color;
        }
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
