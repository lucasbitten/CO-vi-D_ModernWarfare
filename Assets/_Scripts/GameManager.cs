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
    public DifficultyData easyData;
    public DifficultyData normalData;
    public DifficultyData hardData;
    public DifficultyData currentDifficultyData;

    // Start is called before the first frame update
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        
    }


}
