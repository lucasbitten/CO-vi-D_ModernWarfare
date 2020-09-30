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
    public GameObject indicator;
    public AnimationType animation = AnimationType.Idling;

    private Animator animator;
    private MaskPositionScript maskPosition;

    public void GetMaskPosition(MaskPositionScript maskPositionScript)
    {
        maskPosition = maskPositionScript;
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        if (GameManager.Instance.difficulty == GameDifficulty.easy)
        {
            indicator.SetActive(true);
        }
        else
        {
            indicator.SetActive(false);

        }

        if (maskPosition != null)
        {
            if (maskPosition.npcController.hasMask)
            {
                maskPosition.mask.SetActive(true);
            }
            else
            {
                maskPosition.mask.SetActive(false);
            }
        }

        animator.SetBool(animation.ToString(), true);


        if (animation == AnimationType.Sitting)
        {
            animator.SetLayerWeight(1,1);
        }
        else
        {
            animator.SetLayerWeight(2, 1);

        }
    }
    
}
