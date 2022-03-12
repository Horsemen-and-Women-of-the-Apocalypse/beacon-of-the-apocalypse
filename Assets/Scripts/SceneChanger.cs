using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public void Solo() {
         SceneManager.LoadScene("Solo");
    }

    public void Multi() {
        SceneManager.LoadScene("Multi");
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame() {
#if UNITY_EDITOR
        Debug.Log("Exit game");
#endif

        Application.Quit();
    }
}