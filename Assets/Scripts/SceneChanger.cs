using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  

public class SceneChanger: MonoBehaviour {  
    public void Solo() {  
        SceneManager.LoadScene("Solo");  
    }  
    public void Multi() {  
        SceneManager.LoadScene("Multi");  
    }
    public void Menu() {  
        SceneManager.LoadScene("Menu");  
    }
    public void exitgame() {  
        Debug.Log("exitgame");  
        Application.Quit();  
    }  
}   