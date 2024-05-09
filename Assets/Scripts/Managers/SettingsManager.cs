using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] Slider effectSlider, musicSlider;
    [SerializeField] Toggle[] toggles;
    [SerializeField] Toggle arToggle;

    private void Awake()
    {
        ControlRef.control.Enable();
        nameInput.text = PlayerPrefs.GetString("PlayerName", "");
        toggles[PlayerPrefs.GetInt("Difficulty", 0)].isOn = true;

#if MOBILE_INPUT
        arToggle.isOn = PlayerPrefs.GetInt("isAR", 0) != 0;
#else 
        arToggle.gameObject.SetActive(false);
#endif
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

    public void SetIsAR(bool isTrue)
    {
        PlayerPrefs.SetInt("isAR", isTrue ?  1 : 0);
    }
}
