using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgTheme : EventBase
{
    private SpriteRenderer m_SpriteRenderer;
    private ManagerVars vars;
    private void Awake()
    {
        Init();
    }
    protected override void Init()
    {
        vars = ManagerVars.GetManagerVars();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        int ranValue = Random.Range(0, vars.bgThemeSpriteList.Count);
        m_SpriteRenderer.sprite = vars.bgThemeSpriteList[ranValue];
    }
    protected override void AddEvent()
    {

    }
    protected override void OnDestroy()
    {

    }
}