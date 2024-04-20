using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoad : MonoBehaviour
{
    public string levelToLoad;
    AsyncOperation loadSync;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine("ASyncLoad");
        }
    }

    private IEnumerator ASyncLoad()
    {
        loadSync = SceneManager.LoadSceneAsync(levelToLoad);
        loadSync.allowSceneActivation = false;
        yield return loadSync;
    }

    public void ActivateScene()
    {
        loadSync.allowSceneActivation = true;
    }
}