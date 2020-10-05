using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class SaveLoad : MonoBehaviour
{
    private int maxLevel;
    private string path;
    private void Awake() {

        path = Application.persistentDataPath + "/playerInfo.dat";

    }

    public void Save(){

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);

        PlayerData data = new PlayerData();
        data.levelMax = maxLevel;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load(){
        if (File.Exists(path)){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            maxLevel = data.levelMax;
        }
    }

}

[Serializable]
class PlayerData{

    public int levelMax;



}

