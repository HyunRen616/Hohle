using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable] 
public class SaveData 
{ 
    public int orbCount; 
    public int deathCount;
    public bool Upgrade1 = false;
    public bool Upgrade2 = false;
    public bool IsWhiteUnlocked;
    public bool IsBlackUnlocked;
    public bool IsLevel1;
    public bool IsLevel2;
    public bool postProcessing = true;
    public int levelCounter;
    public int levelweapon1 = 1;
    public int levelweapon2 = 0;
    public int levelweapon3 = 0;
    public int levelweapon4 = 0;
    public int levelweapon5 = 0;
    public int levelweapon6 = 0;
}

