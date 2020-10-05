using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private List<NPCController> npcControllers;

    public static LevelManager Instance;
    private int npcCount;

    // Start is called before the first frame update
    void Start()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        npcControllers = FindObjectsOfType<NPCController>().ToList();
        npcCount = npcControllers.Count;
    }
}
