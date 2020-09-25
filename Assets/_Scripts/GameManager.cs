using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameDifficulty
{
    easy,
    medium,
    hard    
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameDifficulty difficulty = GameDifficulty.easy;
    

    // Start is called before the first frame update
    void Start()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
