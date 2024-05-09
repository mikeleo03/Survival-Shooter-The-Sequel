﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {
	
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
	
	Canvas canvas;
    private InputAction escape;

    private void Awake()
    {
        escape = ControlRef.control.UI.Cancel;
    }

    void Start()
	{
		canvas = GetComponent<Canvas>();
	}
	
	void Update()
	{
		if (escape.WasPressedThisFrame())
		{
			canvas.enabled = !canvas.enabled;
			Pause();
		}
	}
	
	public void Pause()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		Lowpass ();
	}
	
	void Lowpass()
	{
		if (Time.timeScale == 0)
		{
			paused.TransitionTo(.01f);
		}
		
		else
			
		{
			unpaused.TransitionTo(.01f);
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
