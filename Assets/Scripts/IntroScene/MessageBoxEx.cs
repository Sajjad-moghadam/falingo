using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoxEx : MonoBehaviour {

    public Text _mainText;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
    public void SetMessage(string mes)
    {
        _mainText.text = mes;
        Showwindow();
    }

    public void Showwindow()
    {

        gameObject.SetActive(true);
    }
}
