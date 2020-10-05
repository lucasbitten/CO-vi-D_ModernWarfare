using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyData", menuName = "Data/DifficultyData")]
public class DifficultyData : ScriptableObject
{
    public float minTimeToInfect = 10;
    public float maxTimeToInfect = 5;
    public float infectionRay = 5;
    public float chanceToInfectMaskToMask = 0.2f;
    public float chanceToInfectWithoutMaskToMask = 0.5f;
    public float chanceToInfectMaskToWithoutMask = 0.4f;
    public float chanceToInfectWithoutMaskToWithoutMask = 0.75f;

}
