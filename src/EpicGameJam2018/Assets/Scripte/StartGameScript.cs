using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour {

    private GameManager GM;

    public void StartGame()
    {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        //load next scene
        SceneManager.LoadScene(GM.GetNextScene());
    }
}
