using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    Running,
    Walking,
    Sitting,
    Talking,
    Idling,
    Listening
}

public class NPCController : MonoBehaviour
{
    public bool hasMask;
    public bool isInfected;
    public Indicator indicator;
    public AnimationType animation = AnimationType.Idling;

    private Animator animator;
    private MaskPositionScript maskPosition;

    public void GetMaskPosition(MaskPositionScript maskPositionScript)
    {
        maskPosition = maskPositionScript;
    }



    public void Infected()
    {
        Infected infected = gameObject.AddComponent<Infected>();
        isInfected = true;
        infected.npcController = this;
        indicator.SetIndicatorType();
    }

    void Start()
    {
        indicator.GetNPC(this);
        indicator.SetIndicatorType();

        if (isInfected)
        {
            Infected();
        }

        animator = GetComponent<Animator>();

        if (maskPosition != null)
        {
            maskPosition.mask.SetActive(maskPosition.npcController.hasMask);
        }

        animator.SetBool(animation.ToString(), true);


        animator.SetLayerWeight(animation == AnimationType.Sitting ? 1 : 2, 1);
    }
    
}
