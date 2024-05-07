using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARWorld : MonoBehaviour
{
    [SerializeField] GameObject walls;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("isAR", 0) != 0)
        {
            walls.SetActive(false);
        }
    }
}
