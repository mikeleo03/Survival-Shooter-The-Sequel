using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    static GameManager instance;
    public void startGame() {
        if (PlayerPrefs.GetInt("isAR", 0) != 0)
        {
            SceneManager.LoadScene("AR", LoadSceneMode.Single);
        } else
        {
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
    }

    public void goToMenu() {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    private void Awake() {
        // DontDestroyOnLoad(gameObject);
    }
}
