using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene1Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
#if MOBILE_INPUT
        if (PlayerPrefs.GetInt("isAR", 0) != 0)
        {
            SceneManager.LoadScene("AR", LoadSceneMode.Single);
        } else
        {
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
#else
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
#endif
    }
}
