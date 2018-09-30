using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup),typeof(Animator),typeof(Text))]
public class P2DNotification : MonoBehaviour {

    Animator animator;
    Text text;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<Text>();
	}
	
	
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Messege">faConvertor</param>
    public void Show(string Messege)
    {
        text.text = Messege;
        animator.SetTrigger("Show");
    }
}
