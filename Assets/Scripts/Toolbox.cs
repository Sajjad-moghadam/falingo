using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : SingletonMahsa<Toolbox> {
    protected Toolbox() { } // guarantee this will be always a singleton only - can't use the constructor!

   // public string myGlobalVar = "whatever";

    void Awake()
    {
        // Your initialization code here
    }

    // (optional) allow runtime registration of global objects
    static public T RegisterComponent<T>() where T : Component
    {
        return Instance.GetOrAddComponent<T>();
    }

}

