using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private Animator anim;
    public bool hasMask;
    public MaskPositionScript maskPosition;
    public GameObject indicator;

    [Header("Animation Fields")]
    [SerializeField] private bool running;
    [SerializeField] private bool walking;
    [SerializeField] private bool sitting;
    [SerializeField] private bool talking;
    [SerializeField] private bool idling;
    [SerializeField] private bool listening;

    public void GetMaskPosition(MaskPositionScript maskPositionScript)
    {
        maskPosition = maskPositionScript;
    }

    void Start()
    {
        anim = GetComponent<Animator>();

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


        if (sitting)
        {
            anim.SetLayerWeight(1,1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Walking", walking);
        anim.SetBool("Running", running);
        anim.SetBool("Sitting", sitting);
        anim.SetBool("Talking", talking);
        anim.SetBool("Idling", idling);
        anim.SetBool("Listening", listening);


    }
}
