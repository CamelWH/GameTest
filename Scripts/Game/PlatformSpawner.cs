using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformGroupType
{
    Grass,
    Winter
}
public class PlatformSpawner : EventBase
{
    public Vector3 startSpawnPos;//初始位置
    private ManagerVars managerVars;
    /// <summary>
    /// 生成平台数量
    /// </summary>
    private int spawnPlatformCount;
    private Vector3 platformSpawnPosition;//平台生成位置
    private bool isLeftSpawn = false;
    private int initPlatformCount = 5;//初始生成平台的数量
    /// <summary>
    /// 当前选择的平台图片
    /// </summary>
    private Sprite selectPlatformSprite;
    /// <summary>
    /// 组合平台的类型
    /// </summary>
    private PlatformGroupType groupType;
    /// <summary>
    /// 钉子平台是否生成在左边
    /// </summary>
    private bool IsSpikeSpawnLeft = false;
    /// <summary>
    /// 钉子方向平台的位置
    /// </summary>
    private Vector3 spikeDirPlatformPos;
    /// <summary>
    /// 钉子方向生成的平台数量
    /// </summary>
    private int afterSpawnSpikeSpawnCount;
    private bool IsSpawnSpike=false;

    private void Awake()
    {
        AddEvent();
    }
    protected override void AddEvent()
    {
        EventCenter.AddListener(EventType.DecidePath, DecidePath);
    }
    protected override void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.DecidePath, DecidePath);
    }
    private void Start()
    {
        platformSpawnPosition = startSpawnPos;
        managerVars = ManagerVars.GetManagerVars();

        RandomPlatformTheme();
        spawnPlatformCount = initPlatformCount;
        for (int i = 0; i < initPlatformCount; i++)
        {
            DecidePath();
        }
        //生成人物
        GameObject go=Instantiate(managerVars.characterPre);
        go.transform.position = startSpawnPos+Vector3.up*0.5f;

    }
    /// <summary>
    /// 确定平台主题(风格)
    /// </summary>
    private void RandomPlatformTheme()
    {
        //随机一个主题
        int ran = Random.Range(0, managerVars.platformThemeSpriteList.Count);
        selectPlatformSprite = managerVars.platformThemeSpriteList[ran];
        if (ran==2)
        {
            groupType = PlatformGroupType.Winter;
        }
        else
        {
            groupType = PlatformGroupType.Grass;
        }
    }
    /// <summary>
    /// 确定路径
    /// </summary>
    private void DecidePath()
    {
        if (IsSpawnSpike)
        {
            AfterSpawnSpike();
            return;
        }
        if (spawnPlatformCount>0)
        {
            spawnPlatformCount--;
            SpawnPlatform();
        }
        else
        {
            isLeftSpawn = !isLeftSpawn;
            spawnPlatformCount = Random.Range(1, 4);
            SpawnPlatform();
        }
    }
    /// <summary>
    /// 生成平台
    /// </summary>
    private void SpawnPlatform()
    {
        //生成单个平台
        if (spawnPlatformCount>=1)
        {
            SpawnNormalPlatform();
        }
            //在最后拐角生成组合平台
        else if (spawnPlatformCount==0)
        {
            int ran = Random.Range(0, 3);
            if (ran==0)//生成通用组合平台
            {
                SpawnCommonPlatform();
            }
            else if (ran==1)//生成主体组合平台
            {
                switch (groupType)
                {
                    case PlatformGroupType.Grass:
                        SpawnGrassPlatform();
                        break;
                    case PlatformGroupType.Winter:
                        SpawnWinterPlatform();
                        break;
                    default:
                        break;
                }
            }
            else//生成钉子组合平台
            {
                int value=-1;
                if (isLeftSpawn)
                {
                    value = 0;//生成右边
                }
                else
                {
                    value = 1;//生成左边
                }
                SpawnSpikePlatform(value);
                IsSpawnSpike = true;
                afterSpawnSpikeSpawnCount = Random.Range(4,6);
                //确定钉子平台的位置
                if (IsSpikeSpawnLeft)
                {
                    spikeDirPlatformPos = new Vector3(platformSpawnPosition.x - 1.65f, platformSpawnPosition.y+managerVars.nextYPos, 0);
                }
                else
                {
                    spikeDirPlatformPos = new Vector3(platformSpawnPosition.x + 1.65f, platformSpawnPosition.y + managerVars.nextYPos, 0);
                }
            }
        }
        if (isLeftSpawn)
        {
            platformSpawnPosition = new Vector3(platformSpawnPosition.x - managerVars.nextXPos, platformSpawnPosition .y+ managerVars.nextYPos,0);
        }
        else
        {
            platformSpawnPosition = new Vector3(platformSpawnPosition.x + managerVars.nextXPos, platformSpawnPosition.y + managerVars.nextYPos, 0);
        }
    }
    /// <summary>
    /// 生成普通平台
    /// </summary>
    private void SpawnNormalPlatform()
    {
        //CreatePlatform(managerVars.normalPlatformPre);
        CreatePlatform(ObjectPool.Instance.GetNormalPlatform());
    }
    /// <summary>
    /// 生成通用组合平台
    /// </summary>
    private void SpawnCommonPlatform()
    {
        //int ran = Random.Range(0, managerVars.commonPlatGroup.Count);
        //CreatePlatform(managerVars.commonPlatGroup[ran]); 
        CreatePlatform(ObjectPool.Instance.GetCommonPlatform());
    }
    /// <summary>
    /// 生成草地主题平台
    /// </summary>
    private void SpawnGrassPlatform()
    {
        //int ran = Random.Range(0, managerVars.grassPlatGroup.Count);
        //CreatePlatform(managerVars.grassPlatGroup[ran]);
        CreatePlatform(ObjectPool.Instance.GetGrassPlatform());
    }
    /// <summary>
    /// 生成冬季主题平台
    /// </summary>
    private void SpawnWinterPlatform()
    {
        //int ran = Random.Range(0, managerVars.winterPlatGroup.Count);
        //CreatePlatform(managerVars.winterPlatGroup[ran]);
        CreatePlatform(ObjectPool.Instance.GetWinterPlatform());
    }
    /// <summary>
    /// 生成钉子组合平台
    /// </summary>
    private void SpawnSpikePlatform(int dir)
    {
        if (dir==0)//生成在右边
        {
            IsSpikeSpawnLeft = false;
            //CreatePlatform(managerVars.spikePlatformRight);
            CreatePlatform(ObjectPool.Instance.GetRightSpikePlatform());
        }
        else if(dir==1)//生成在左边
        {
            IsSpikeSpawnLeft = true;
            //CreatePlatform(managerVars.spikePlatformLeft);
            CreatePlatform(ObjectPool.Instance.GetLeftSpikePlatform());
        }
        //确定当前钉子平台的位置
        spikeDirPlatformPos = platformSpawnPosition;
    }
    /// <summary>
    /// 生成钉子方向的平台
    /// 包括钉子方向,也包括正常路线的平台
    /// </summary>
    private void AfterSpawnSpike()
    {
        if (afterSpawnSpikeSpawnCount>0)
        {
            afterSpawnSpikeSpawnCount--;
            //根据钉子的方向生成两个方向的平台
            //1.钉子在左边,钉子生成的平台在左边,正常路径生成的平台在右边
            if (IsSpikeSpawnLeft)
            {
                SpawnNormalPlatform();
                CreateAfterSpikePlatform(managerVars.normalPlatformPre);
                platformSpawnPosition = GetRightNextPlatform(platformSpawnPosition);
                spikeDirPlatformPos = GetLeftNextPlatform(spikeDirPlatformPos);
            }
            else
            {
                SpawnNormalPlatform();
                CreateAfterSpikePlatform(managerVars.normalPlatformPre);
                platformSpawnPosition = GetLeftNextPlatform(platformSpawnPosition);
                spikeDirPlatformPos = GetRightNextPlatform(spikeDirPlatformPos);
            }
        }
        else
        {
            IsSpawnSpike = false;
            DecidePath();
        }
    }
    /// <summary>
    /// 生成平台
    /// </summary>
    private void CreatePlatform(GameObject go)
    {
        int ranObstacleDir = Random.Range(0, 2);
        go.transform.position = platformSpawnPosition;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite, ranObstacleDir);
        go.SetActive(true);
    }
    private void CreateAfterSpikePlatform(GameObject go)
    {
        //GameObject go = Instantiate(temp, transform);
        go.transform.position = spikeDirPlatformPos;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite, 1);
        go.SetActive(true);
    }
    #region 获取一下平台的位置
    /// <summary>
    /// 获取左边生成的位置
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    private Vector3 GetLeftNextPlatform(Vector3 v)
    {
        return new Vector3(v.x - managerVars.nextXPos, v.y + managerVars.nextYPos, 0);
    }
    private Vector3 GetRightNextPlatform(Vector3 v)
    {
        return new Vector3(v.x + managerVars.nextXPos, v.y + managerVars.nextYPos, 0);
    }
    #endregion
}