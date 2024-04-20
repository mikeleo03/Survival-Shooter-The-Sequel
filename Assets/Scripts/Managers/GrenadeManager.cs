using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Nightmare
{
    public class GrenadeManager : MonoBehaviour
    {
        public static int grenades;        // The player's score.


        Text gText;                      // Reference to the Text component.


        void Awake()
        {
            // Set up the reference.
            gText = GetComponent<Text>();

            // Reset the score.
            grenades = 0;
        }


        void Update()
        {
            // Set the displayed text to be the word "Score" followed by the score value.
            gText.text = "Grenades: " + grenades;
        }
    }
}