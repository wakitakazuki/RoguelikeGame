using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject floor;
    public GameObject wall;
    public GameObject line;

    private Array2D map;
    private const float oneTile = 1.0f;
    private const float floorSize = 10.0f / oneTile;
    public static float ToWorldX(int xgrid)
    {
        return xgrid * oneTile;
    }

    public static float ToWorldZ(int zgrid)
    {
        return zgrid * oneTile;
    }

    public static int ToGridX(float xworld)
    {
        return Mathf.FloorToInt(xworld / oneTile);
    }

    public static int ToGridZ(float zworld)
    {
        return Mathf.FloorToInt(zworld / oneTile);
    }
    //マップデータの生成
    public void Create(Array2D mapdata)
    {
        map = mapdata;
        float floorw = map.width / floorSize;
        float floorh = map.height / floorSize;
        floor.transform.localScale = new Vector3(floorw, 1, floorh);
        float floorx = (map.width - 1) / 2.0f * oneTile;
        float floorz = (map.height - 1) / 2.0f * oneTile;
        floor.transform.position = new Vector3(floorx, 0, floorz);
        for(int z = 0; z < map.height; z++)
        {
            for(int x = 0; x < map.width; x++)
            {
                if (map.Get(x, z) > 0)
                {
                    GameObject block = Instantiate(wall);
                    float xblock = ToWorldX(x);
                    float zblock = ToWorldZ(z);
                    block.transform.localScale = new Vector3(oneTile, 2, oneTile);
                    block.transform.position = new Vector3(xblock, 1, zblock);
                    block.transform.SetParent(floor.transform.GetChild(0));
                }
            }
        }
        ShowGridEffects();
    }
    //生成したマップのリセット
    public void Reset()
    {
        Transform walls = floor.transform.GetChild(0);
        for(int i = 0; i < walls.childCount; i++)
        {
            Destroy(walls.GetChild(i).gameObject);
        }
        Transform effects = floor.transform.GetChild(1);
        for(int i = 0; i < effects.childCount; i++)
        {
            Destroy(effects.GetChild(i).gameObject);
        }
    }
    //指定の座標が壁かどうかのチェック
    public bool IsCollide(int xgrid,int zgrid)
    {
        return map.Get(xgrid, zgrid) != 0;
    }
    //仮記載したスタート文　不要かもしれないが残しておく
    /*private void Start()
    {
        Array2D mapdata = new Array2D(10, 10);
        mapdata.Set(1, 1, 1);
        Create(mapdata);
    }*/
    //升目のエフェクトを表示する
    private void ShowGridEffects()
    {
        for(int x = 1; x < map.width; x++)
        {
            GameObject obj = Instantiate(line, floor.transform.GetChild(1));
            obj.transform.position = new Vector3(x * oneTile - oneTile / 2, 0.1f, -oneTile / 2);
            obj.transform.localScale = new Vector3(1, 1, floorSize * oneTile);
        }
        for(int z = 1; z < map.height; z++)
        {
            GameObject obj = Instantiate(line, floor.transform.GetChild(1));
            obj.transform.position = new Vector3(-oneTile / 2, 0.1f, z * oneTile - oneTile / 2);
            obj.transform.rotation = Quaternion.Euler(0, 90, 0);
            obj.transform.localScale = new Vector3(1, 1, floorSize * oneTile);
        }
    }
    //マップデータを返す
    public Array2D GetMapData()
    {
        Array2D mapdata = new Array2D(map.width, map.height);
        for(int z = 0; z < map.height; z++)
        {
            for(int x = 0; x < map.width; x++)
            {
                mapdata.Set(x, z, map.Get(x, z));
            }
        }
        return mapdata;
    }
   
}
