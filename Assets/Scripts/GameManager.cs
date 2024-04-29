using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
   public void startGame() {
        SceneManager.LoadScene(1);
   }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
