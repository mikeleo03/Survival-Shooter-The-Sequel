using System;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GlobalSaveState
{
    public bool[] cheats;

    public GlobalSaveState(bool[] cheats)
    {
        this.cheats = cheats;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

[Serializable]
public class SaveState
{
    public GlobalSaveState globalSaveState;

    public SaveState(GlobalSaveState globalSaveState)
    {
        this.globalSaveState = globalSaveState;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
