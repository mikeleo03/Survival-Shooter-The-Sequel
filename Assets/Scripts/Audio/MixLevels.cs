using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixLevels : MonoBehaviour {

	public AudioMixer masterMixer;
	[SerializeField] private Slider sfxSlider, musicSlider;

    private void Awake()
    {
		sfxSlider.value = PlayerPrefs.GetFloat("sfxVol", -10);
        musicSlider.value = PlayerPrefs.GetFloat("musicVol", -10);
    }

    public void SetSfxLvl(float sfxLvl)
	{
		masterMixer.SetFloat("sfxVol", sfxLvl);
		PlayerPrefs.SetFloat("sfxVol", sfxLvl);
	}

	public void SetMusicLvl (float musicLvl)
	{
		masterMixer.SetFloat ("musicVol", musicLvl);
        PlayerPrefs.SetFloat("musicVol", musicLvl);
    }
}
