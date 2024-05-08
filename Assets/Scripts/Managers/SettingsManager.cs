using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] Slider effectSlider, musicSlider;
    [SerializeField] Toggle[] toggles;

    private void Awake()
    {
        nameInput.text = PlayerPrefs.GetString("PlayerName", "");
        toggles[PlayerPrefs.GetInt("Difficulty", 0)].isOn = true;
    }

    public void SetPlayerName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }

    public void SetDiffEasy(bool isTrue)
    {
        if (isTrue)
        {
            PlayerPrefs.SetInt("Difficulty", 0);
        }
    }

    public void SetDiffMedium(bool isTrue)
    {
        if (isTrue)
        {
            PlayerPrefs.SetInt("Difficulty", 1);
        }
    }

    public void SetDiffHard(bool isTrue)
    {
        if (isTrue)
        {
            PlayerPrefs.SetInt("Difficulty", 2);
        }
    }
}
