using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
            return instance;
        }
    }

    public int initSpawnCount = 5;
    private List<GameObject> normalPlatformList = new List<GameObject>();
    private List<GameObject> commonPlatformList = new List<GameObject>();
    private List<GameObject> grassPlatformList = new List<GameObject>();
    private List<GameObject> winterPlatformList = new List<GameObject>();
    private List<GameObject> spikePlatformLeftList = new List<GameObject>();
    private List<GameObject> spikePlatformRightList = new List<GameObject>();
    private ManagerVars vars;
    private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        Init();
    }
    private void Init()
    {
        for (int i = 0; i < initSpawnCount; i++)
        {
            InstantiateObject(vars.normalPlatformPre, ref normalPlatformList);
            InstantiateObject(vars.spikePlatformLeft, ref spikePlatformLeftList);
            InstantiateObject(vars.spikePlatformRight, ref spikePlatformRightList);
        }
        for (int i = 0; i < initSpawnCount; i++)
        {
            for (int j = 0; j < vars.commonPlatGroup.Count; j++)
            {
                InstantiateObject(vars.commonPlatGroup[j], ref commonPlatformList);
            }
            for (int j = 0; j < vars.winterPlatGroup.Count; j++)
            {
                InstantiateObject(vars.winterPlatGroup[j], ref winterPlatformList);
            }
            for (int j = 0; j < vars.grassPlatGroup.Count; j++)
            {
                InstantiateObject(vars.grassPlatGroup[j], ref grassPlatformList);
            }
        }
    }
    private GameObject InstantiateObject(GameObject prefab, ref List<GameObject> addList)
    {
        GameObject go = Instantiate(prefab, transform);
        go.SetActive(false);
        addList.Add(go);
        return go;
    }
    public GameObject GetNormalPlatform()
    {
        for (int i = 0; i < normalPlatformList.Count; i++)
        {
            if (normalPlatformList[i].activeSelf==false)
            {
                return normalPlatformList[i];
            }
        }
        return InstantiateObject(vars.normalPlatformPre,ref normalPlatformList);
    }
    public GameObject GetCommonPlatform()
    {
        for (int i = 0; i < commonPlatformList.Count; i++)
        {
            if (commonPlatformList[i].activeSelf == false)
            {
                return commonPlatformList[i];
            }
        }
        int ran=Random.Range(0,vars.commonPlatGroup.Count);
        return InstantiateObject(vars.commonPlatGroup[ran], ref commonPlatformList);
    }
    public GameObject GetGrassPlatform()
    {
        for (int i = 0; i < grassPlatformList.Count; i++)
        {
            if (grassPlatformList[i].activeSelf == false)
            {
                return grassPlatformList[i];
            }
        }
        int ran = Random.Range(0, vars.grassPlatGroup.Count);
        return InstantiateObject(vars.grassPlatGroup[ran], ref grassPlatformList);
    }
    public GameObject GetWinterPlatform()
    {
        for (int i = 0; i < winterPlatformList.Count; i++)
        {
            if (winterPlatformList[i].activeSelf == false)
            {
                return winterPlatformList[i];
            }
        }
        int ran = Random.Range(0, vars.winterPlatGroup.Count);
        return InstantiateObject(vars.winterPlatGroup[ran], ref winterPlatformList);
    }
    public GameObject GetLeftSpikePlatform()
    {
        for (int i = 0; i < spikePlatformLeftList.Count; i++)
        {
            if (spikePlatformLeftList[i].activeSelf == false)
            {
                return spikePlatformLeftList[i];
            }
        }
        return InstantiateObject(vars.spikePlatformLeft, ref spikePlatformLeftList);
    }
    public GameObject GetRightSpikePlatform()
    {
        for (int i = 0; i < spikePlatformRightList.Count; i++)
        {
            if (spikePlatformRightList[i].activeSelf == false)
            {
                return spikePlatformRightList[i];
            }
        }
        return InstantiateObject(vars.spikePlatformRight, ref spikePlatformRightList);
    }
}