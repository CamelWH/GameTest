using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventBase : MonoBehaviour
{
    //protected abstract void Awake();
    protected virtual void Init()
    {

    }
    protected abstract void AddEvent();
    protected abstract void OnDestroy();
}