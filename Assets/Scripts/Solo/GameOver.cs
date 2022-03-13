using Common;
using UnityEngine;

public class GameOver : SceneChanger {

    private object flishlight;

    void Start() {
        GameObject.Find("Flashlight(Clone)")?.GetComponentInChildren<Flashlight>()?.onGameOver.AddListener(() => {
            this.GameOver();
        });   
    }

}
