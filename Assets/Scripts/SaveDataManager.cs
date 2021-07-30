using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    public Field field;
    public GameObject player;
    private SaveData saveData;
    private const string saveKey = "GameData";
    private const string saveKey2 = "testdata";
    // Start is called before the first frame update
    void Start()
    {
        saveData = new SaveData();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown("s"))
            {
                Save();
                Message.add("セーブしました");
            }
            if (Input.GetKeyDown("l"))
            {
                Load();
                Message.add("ロードしました");
            }
        }
    }

    //データを保存する
    private void Save()
    {
        PlayerMovement playermove = player.GetComponent<PlayerMovement>();
        ActorSaveData playerSaveData = new ActorSaveData();
        playerSaveData.grid = new Pos2D();
        playerSaveData.grid.x = playermove.grid.x;
        playerSaveData.grid.z = playermove.grid.z;
        playerSaveData.direction = playermove.direction;
        saveData.playerData = playerSaveData;
        MapSaveData mapSaveData = new MapSaveData();
        mapSaveData.map = field.GetMapData();
        saveData.mapData = mapSaveData;
       
        
        PlayerPrefs.SetString(saveKey, JsonUtility.ToJson(saveData));

        // Debug.Log(saveData.mapData.map.Get(0,0));
        PlayerPrefs.SetString(saveKey2, JsonUtility.ToJson(mapSaveData.map));
    }
    //データを読み込む
    private void Load()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            var data = PlayerPrefs.GetString(saveKey);
            JsonUtility.FromJsonOverwrite(data, saveData);
            field.Reset();
            field.Create(saveData.mapData.map);
            PlayerMovement playermove = player.GetComponent<PlayerMovement>();
            playermove.SetPosition(saveData.playerData.grid.x, saveData.playerData.grid.z);
            playermove.SetDirection(saveData.playerData.direction);
        }
    }
}
