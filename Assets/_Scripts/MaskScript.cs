using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider != null && other.collider.CompareTag("MaskPosition"))
        {
            MaskPositionScript maskPosition = other.collider.gameObject.GetComponent<MaskPositionScript>();
            if (!maskPosition.mask.activeSelf)
            {
                maskPosition.npcController.hasMask = true;
                maskPosition.animator.SetTrigger("HitMask");
                maskPosition.mask.SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }
}
