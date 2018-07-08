using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{
    private const int SecondsUntilNextScreen = 3;
    private readonly string[] _screens = { "Final screen 2", "Final screen 3" };
    private int currscreen = 0;
    private bool activateLeaveGame = false;

    public Image ForegroundImage;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SwitchImageAfter(SecondsUntilNextScreen));
    }

    IEnumerator SwitchImageAfter(int seconds)
    {
        do
        {
            int secondsLeft = seconds;

            do
            {
                yield return new WaitForSeconds(1);
            } while (--secondsLeft > 0);

            if (currscreen < _screens.Length)
            {
                ForegroundImage.sprite = Resources.Load<Sprite>(_screens[currscreen]);
                var color = ForegroundImage.color;
                color.a = 1;
                ForegroundImage.color = color;
            }
            else
            {
                activateLeaveGame = true;
            }

            currscreen++;
        } while (!activateLeaveGame);
    }

    // Update is called once per frame
    void Update()
    {
        if(activateLeaveGame && Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Goodbye Caroline!");
        }
    }
}
