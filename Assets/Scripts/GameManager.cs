using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static void startGame()
    {
        SceneManager.LoadScene("Cutscene01", LoadSceneMode.Single);
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    private void Awake()
    {
        ControlRef.control.Enable();
    }
}
