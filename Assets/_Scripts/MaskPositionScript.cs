using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPositionScript : MonoBehaviour
{
    public GameObject mask;
    public Animator animator;
    public NPCController npcController;

    private void Awake()
    {
        npcController.GetMaskPosition(this);
    }
}
