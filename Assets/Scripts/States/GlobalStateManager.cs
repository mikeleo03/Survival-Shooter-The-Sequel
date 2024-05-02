using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    // Create Singleton
    public static GlobalStateManager Instance { get; private set; }

    private CheatManager cheatManager;

    private void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
            {
                Destroy(this);
            }
        }
        else
        {
            Instance = this;
            Refresh();
        }
    }

    private void Refresh()
    {
        cheatManager = FindObjectOfType<CheatManager>();
    }

    public void SetState(SaveState state)
    {
        Refresh();

        cheatManager.LoadCheat(state.globalSaveState.cheats);
    }
}
