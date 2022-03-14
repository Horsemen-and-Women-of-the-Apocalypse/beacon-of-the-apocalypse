using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public Animator animator;
    private string sceneToLoad;
    private void _FateToScene(string sceneName)
    {
        animator.SetTrigger("Fade Out Trigger");
        sceneToLoad = sceneName;
    }

    private void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Solo() {
        SceneManager.LoadScene("Solo");
    }

    public void Multi() {
        SceneManager.LoadScene("Multi");
    }

    public void Menu() {
        this._FateToScene("Menu");
    }
    
    public void GameOver() {
        SceneManager.LoadScene("Game Over");
    }

    public void ExitGame() {
#if UNITY_EDITOR
        Debug.Log("Exit game");
#endif

        Application.Quit();
    }
}