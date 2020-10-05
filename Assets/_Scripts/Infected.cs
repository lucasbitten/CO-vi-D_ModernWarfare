using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Infected : MonoBehaviour
{
    public NPCController npcController;
    [SerializeField] private float infectionRay = 10;
    [SerializeField] private float timeToInfect = 5;
    [SerializeField] private float maxTimeToInfect = 10;
    [SerializeField] private float minTimeToInfect = 5;
    [SerializeField] private float chanceToInfectMaskToMask = 0.2f;
    [SerializeField] private float chanceToInfectWithoutMaskToMask = 0.5f;
    [SerializeField] private float chanceToInfectMaskToWithoutMask = 0.4f;
    [SerializeField] private float chanceToInfectWithoutMaskToWithoutMask = 0.75f;

    private float infectTimer;
    private LayerMask personLayerMask;
    private AudioSource audioSource;
    private Animator anim;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, infectionRay);
    }

    void Start()
    {
        GetDifficultyData();

        timeToInfect = Random.Range(minTimeToInfect, maxTimeToInfect);
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        personLayerMask = LayerMask.GetMask("Person");
        infectTimer = timeToInfect;
    }

    private void GetDifficultyData()
    {
        DifficultyData difficulty = GameManager.Instance.currentDifficultyData;
        if (difficulty != null)
        {
            difficulty.infectionRay = 300;
            infectionRay = difficulty.infectionRay;
            maxTimeToInfect = difficulty.maxTimeToInfect;
            minTimeToInfect = difficulty.minTimeToInfect;
            chanceToInfectMaskToMask = difficulty.chanceToInfectMaskToMask;
            chanceToInfectWithoutMaskToMask = difficulty.chanceToInfectWithoutMaskToMask;
            chanceToInfectMaskToWithoutMask = difficulty.chanceToInfectMaskToWithoutMask;
            chanceToInfectWithoutMaskToWithoutMask = difficulty.chanceToInfectWithoutMaskToWithoutMask;
        }
    }

    public IEnumerator PlayCoughSound()
    {
        yield return new WaitForSeconds(1f);
        audioSource.clip = AudioManager.instance.GetCoughingSound().clip;
        audioSource.Play();
    }

    
    void Update()
    {
        if (infectTimer > 0)
        {
            infectTimer -= Time.deltaTime;
        }
        else
        {
            TryToInfect();
        }
    }

    private void TryToInfect()
    {
        anim.SetTrigger("Coughing");
        StartCoroutine(PlayCoughSound());
        if (timeToInfect > minTimeToInfect)
        {
            timeToInfect -= 0.5f;
        }

        infectTimer = timeToInfect;
        var coll = Physics.OverlapSphere(transform.position, infectionRay, personLayerMask,
            QueryTriggerInteraction.Collide);
        foreach (var collider in coll)
        {
            NPCController npc = collider.gameObject.GetComponent<NPCController>();
            //Debug.Log(this.name + " hitted: " + collider.name);
            if (collider.gameObject != this.gameObject && npc)
            {
                if (npc.isInfected)
                {
                    continue;
                }

                if (npcController.hasMask)
                {
                    if (npc.hasMask)
                    {
                        if (Random.Range(0, 1f) < chanceToInfectMaskToMask)
                        {
                            npc.Infected();
                            Debug.Log(this.name + "(with mask) infected: " + collider.name + " (with mask)");
                        }
                    }
                    else
                    {
                        if (Random.Range(0, 1f) < chanceToInfectMaskToWithoutMask)
                        {
                            npc.Infected();
                            Debug.Log(this.name + "(with mask) infected: " + collider.name + " (without mask)");
                        }
                    }
                }
                else
                {
                    if (npc.hasMask)
                    {
                        if (Random.Range(0, 1f) < chanceToInfectWithoutMaskToMask)
                        {
                            npc.Infected();
                            Debug.Log(this.name + "(without mask) infected: " + collider.name + " (with mask)");
                        }
                    }
                    else
                    {
                        if (Random.Range(0, 1f) < chanceToInfectWithoutMaskToWithoutMask)
                        {
                            npc.Infected();
                            Debug.Log(this.name + "(without mask) infected: " + collider.name + " (without mask)");
                        }
                    }
                }
            }
        }
    }
}
