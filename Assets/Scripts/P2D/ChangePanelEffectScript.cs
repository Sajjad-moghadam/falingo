using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChangePanelEffectScript : MonoBehaviour {

    [SerializeField]
    Animator animator;

    public void ShowChangePanelEffect(float speed =1)
    {
        animator.speed = speed;
        animator.SetTrigger("ChangeTrigger");

    }

    public void ShowStartPanelEffect(float speed = 1)
    {
        animator.speed = speed;
        animator.SetTrigger("StartTrigger");

    }



}
