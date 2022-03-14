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
        this._FateToScene("Solo");
    }

    public void Multi() {
        this._FateToScene("Multi");
    }

    public void MultiStartup() {
        this._FateToScene("Multi Startup");
    }

    public void Menu() {
        this._FateToScene("Menu");
    }
    
    public void GameOver() {
        this._FateToScene("Game Over");
    }

    public void ExitGame() {
#if UNITY_EDITOR
        Debug.Log("Exit game");
#endif

        Application.Quit();
    }
}