using UnityEngine;
using System.Collections;

public class LockInfo {

    public LocksName name;
    public int levelRequierd2Unlock;
    public string sceneName;
    public string discription;
    public int cost = 100;

    public LockInfo(LocksName myName,int levelRequierd,string scene,string myDiscription,int myCost = -1)
    {
        name = myName;
        levelRequierd2Unlock = levelRequierd;
        sceneName = scene;
        discription = myDiscription;

        if (myCost == -1)
            cost = levelRequierd * 1000;
        else
            cost = myCost;
    }


    /// <summary>
    /// 0-> lock , 1 --> firstUnlock , 2 --> Unlock
    /// </summary>
    public int Unlock
    {
        get
        {
            int unlock;
            P2DSecurety.SecureLocalLoad(name.ToString(), out unlock);

            return unlock; 
        }
        set
        {
            P2DSecurety.SecureLocalSave(name.ToString(), value);
        }
    }
}
