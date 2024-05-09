using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {
	
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
    [SerializeField] private Canvas canvas;
	
	void Start()
	{
		canvas = GetComponent<Canvas>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (canvas.isActiveAndEnabled == true)
            {
                canvas.enabled = false;
				Time.timeScale = 1;
                paused.TransitionTo(.01f);
            }
            else if (canvas.isActiveAndEnabled == false)
            {
                canvas.enabled = true;
                Time.timeScale = 0;
                unpaused.TransitionTo(.01f);
            }
		}
	}
	
	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
