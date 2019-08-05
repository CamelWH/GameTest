using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件处理中心
///     //1.添加事件
///     //2.删除事件
///     //3.广播事件
/// </summary>
public class EventCenter
{
    /// <summary>
    /// 事件表
    ///     Key:当前事件的事件码
    ///     Value:当前事件的委托,即要执行的方法
    /// </summary>
    private static Dictionary<EventType, Delegate> m_EventTable = new Dictionary<EventType, Delegate>();

    /// <summary>
    /// 正在监听
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    private static void OnListenerAdding(EventType eventType,Delegate callBack)
    {
        //判断事件池子中是否有对应的委托
        if (!m_EventTable.ContainsKey(eventType))
        {
            m_EventTable.Add(eventType, null);
        }
        Delegate d = m_EventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())
        {
            throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托,当前事件所对应的委托类型是{1},要添加的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
        }
    }
    /// <summary>
    /// 添加监听事件
    /// </summary>
    /// <param name="eventType">该事件对应的事件码</param>
    /// <param name="callBack">该事件的委托,即要执行的方法</param>
    public static void AddListener(EventType eventType,CallBack callBack)
    {
        OnListenerAdding(eventType, callBack);
        //如果没有异常,则将对应的事件添加到事件表
        m_EventTable[eventType]=(CallBack)m_EventTable[eventType]+callBack;
    }
    public static void AddListener<T>(EventType eventType,CallBack<T> callBack)
    {
        OnListenerAdding(eventType, callBack);
        //如果没有异常,则将对应的事件添加到事件表
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] + callBack;
    }
    public static void AddListener<T,X>(EventType eventType, CallBack<T,X> callBack)
    {
        OnListenerAdding(eventType, callBack);
        //如果没有异常,则将对应的事件添加到事件表
        m_EventTable[eventType] = (CallBack<T,X>)m_EventTable[eventType] + callBack;
    }
    public static void AddListener<T,X,Y>(EventType eventType, CallBack<T,X,Y> callBack)
    {
        OnListenerAdding(eventType, callBack);
        //如果没有异常,则将对应的事件添加到事件表
        m_EventTable[eventType] = (CallBack<T,X,Y>)m_EventTable[eventType] + callBack;
    }
    public static void AddListener<T, X, Y, Z>(EventType eventType, CallBack<T, X, Y, Z> callBack)
    {
        OnListenerAdding(eventType, callBack);
        //如果没有异常,则将对应的事件添加到事件表
        m_EventTable[eventType] = (CallBack<T, X, Y, Z>)m_EventTable[eventType] + callBack;
    }
    public static void AddListener<T, X, Y, Z, W>(EventType eventType, CallBack<T, X, Y, Z, W> callBack)
    {
        OnListenerAdding(eventType, callBack);
        //如果没有异常,则将对应的事件添加到事件表
        m_EventTable[eventType] = (CallBack<T, X, Y, Z, W>)m_EventTable[eventType] + callBack;
    }
    private static void OnListenerRemoving(EventType eventType,Delegate callBack)
    {
        //判断当前事件表中是否有对应的事件码
        if (m_EventTable.ContainsKey(eventType))
        {
            //获取该事件码下的委托
            Delegate d = m_EventTable[eventType];
            //判断当前委托是否为空
            if (d == null)
            {
                throw new Exception(string.Format("移除监听错误:事件{0}没有对应的委托,委托为空", eventType));
            }
            //判断当前委托的类型和要移除的委托类型是否一致
            else if (d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("移除监听错误:尝试为事件{0}移除不同类型的委托,当前委托类型为{1},要移除的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("移除监听错误:事件表中没有对应的事件码{0}", eventType));
        }
    }
    private static void OnListenerRemoved(EventType eventType)
    {
        //如果删除之后,对应事件的委托为空,则将该事件删除
        if (m_EventTable[eventType] == null)
        {
            m_EventTable.Remove(eventType);
        }
    }
    /// <summary>
    /// 移除监听事件
    /// </summary>
    /// <param name="eventType">事件码</param>
    /// <param name="callBack">事件委托</param>
    public static void RemoveListener(EventType eventType,CallBack callBack)
    {
        OnListenerRemoving(eventType, callBack);
        //如果没有异常,则将对应的事件从事件表中删除
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }
    public static void RemoveListener<T>(EventType eventType, CallBack<T> callBack)
    {
        OnListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }
    public static void RemoveListener<T, X>(EventType eventType, CallBack<T, X> callBack)
    {
        OnListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X>)m_EventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }
    public static void RemoveListener<T, X, Y>(EventType eventType, CallBack<T, X, Y> callBack)
    {
        OnListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y>)m_EventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }
    public static void RemoveListener<T, X, Y, Z>(EventType eventType, CallBack<T, X, Y, Z> callBack)
    {
        OnListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y, Z>)m_EventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }
    public static void RemoveListener<T, X, Y, Z, W>(EventType eventType, CallBack<T, X, Y, Z, W> callBack)
    {
        OnListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y, Z, W>)m_EventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }
    /// <summary>
    /// 广播事件
    /// </summary>
    /// <param name="eventType"></param>
    public static void Broadcast(EventType eventType)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType,out d))
        {
            Debug.Log(string.Format("执行事件{0}", eventType));
            //将获得委托d转换成为CallBack型
            CallBack callBack = d as CallBack;
            //如果callBack不为空,则执行
            if (callBack!=null)
            {
                callBack();
            }
                //如果callBack为空,则抛出异常
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应委托具有不同的类型,可能没有传递对应的参数",eventType));
            }
        }
    }
    public static void Broadcast<T>(EventType eventType,T arg)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            //将获得委托d转换成为CallBack型
            CallBack<T> callBack = d as CallBack<T>;
            //如果callBack不为空,则执行
            if (callBack != null)
            {
                callBack(arg);
            }
            //如果callBack为空,则抛出异常
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T,X>(EventType eventType, T arg1, X arg2)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            //将获得委托d转换成为CallBack型
            CallBack<T,X> callBack = d as CallBack<T,X>;
            //如果callBack不为空,则执行
            if (callBack != null)
            {
                callBack(arg1,arg2);
            }
            //如果callBack为空,则抛出异常
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T, X,Y>(EventType eventType, T arg1, X arg2,Y arg3)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            //将获得委托d转换成为CallBack型
            CallBack<T, X,Y> callBack = d as CallBack<T, X,Y>;
            //如果callBack不为空,则执行
            if (callBack != null)
            {
                callBack(arg1, arg2,arg3);
            }
            //如果callBack为空,则抛出异常
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T, X, Y,Z>(EventType eventType, T arg1, X arg2, Y arg3,Z arg4)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            //将获得委托d转换成为CallBack型
            CallBack<T, X, Y,Z> callBack = d as CallBack<T, X, Y,Z>;
            //如果callBack不为空,则执行
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3,arg4);
            }
            //如果callBack为空,则抛出异常
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T, X, Y, Z,W>(EventType eventType, T arg1, X arg2, Y arg3, Z arg4,W arg5)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            //将获得委托d转换成为CallBack型
            CallBack<T, X, Y, Z,W> callBack = d as CallBack<T, X, Y, Z,W>;
            //如果callBack不为空,则执行
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3, arg4,arg5);
            }
            //如果callBack为空,则抛出异常
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    public static Dictionary<Type, Delegate> EventPool = new Dictionary<Type, Delegate>();
    public static Delegate GetDelegate(Type str)
    {
        if (!EventPool.ContainsKey(str.GetType()))
        {
            EventPool.Add(str.GetType(), null);
        }
        return EventPool[str.GetType()];
    }
}