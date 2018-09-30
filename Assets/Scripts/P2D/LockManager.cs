using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LocksName
{
    Unlock = 0,
   
}

public class LockManager
{

    public static Dictionary<LocksName, LockInfo> Locks = new Dictionary<LocksName, LockInfo>();
    public static void InitLocks()
    {
      

        CheckAllLock();
#if UNITY_EDITOR

        //LockAll();
#endif
    }

    public static void CheckAllLock()
    {
        foreach (var lockInfo in Locks)
        {
            if (Setting.lastLevel >= lockInfo.Value.levelRequierd2Unlock && lockInfo.Value.Unlock == 0)
            {
                if (Setting.lastLevel == lockInfo.Value.levelRequierd2Unlock && lockInfo.Value.levelRequierd2Unlock != 0)
                {
                    lockInfo.Value.Unlock = 1;

                }
                else
                {
                    lockInfo.Value.Unlock = 2;
                }
            }
        }

    }

    private static void LockAll()
    {
        foreach (var item in Locks)
        {
            item.Value.Unlock = 0;
        }
    }

}
