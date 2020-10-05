using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private NPCController npcController;

    public void GetNPC(NPCController npc)
    {
        npcController = npc;
    }

    private Renderer renderer;
    private MaterialPropertyBlock propBlock;
    private int colorID;

    private void Start()
    {
        SetIndicatorType();
    }

    public void SetIndicatorType()
    {
        if (npcController != null)
        {
            renderer = GetComponent<Renderer>();
            propBlock = new MaterialPropertyBlock();
            colorID = Shader.PropertyToID("_BaseColor");
            renderer.GetPropertyBlock(propBlock);

            if (GameManager.Instance.difficulty == GameDifficulty.easy)
            {
                if (npcController.isInfected && !npcController.hasMask)
                {
                    propBlock.SetColor(colorID, Color.red);
                    gameObject.SetActive(true);

                }
                else if (npcController.isInfected && npcController.hasMask)
                {
                    propBlock.SetColor(colorID, Color.white);
                    gameObject.SetActive(true);
                }
                else if (!npcController.hasMask)
                {
                    propBlock.SetColor(colorID, Color.yellow);
                    gameObject.SetActive(true);

                } 
                else
                {
                    gameObject.SetActive(false);
                }

            }
            else if (GameManager.Instance.difficulty == GameDifficulty.medium)
            {
                if (!npcController.hasMask)
                {
                    propBlock.SetColor(colorID, Color.yellow);
                    gameObject.SetActive(true);

                }
                else
                {
                    gameObject.SetActive(false);
                }

            }
            else
            {
                gameObject.SetActive(false);

            }
            renderer.SetPropertyBlock(propBlock);

        }
    }

}
