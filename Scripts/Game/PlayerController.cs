using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour 
{
    /// <summary>
    /// 是否向左移动,反之向右
    /// </summary>
    private bool isMoveLeft = false;
    /// <summary>
    /// 是否正在跳跃
    /// </summary>
    private bool isJumping = true;
    private Vector3 nextPlatformLeft, nextPlatformRight;
    private ManagerVars vars;
    public GameObject collisionObject;
    /// <summary>
    /// 当前所在的平台
    /// </summary>
    private GameObject currentPlatform;
    private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
    }
    private void Update()
    {
        if (GameManager.Instance.IsGameStarted == false || GameManager.Instance.IsGameOver == true)
            return;
        if (Input.GetMouseButtonDown(0)&&isJumping==false)
        {
            EventCenter.Broadcast(EventType.DecidePath);
            isJumping = true;
            Vector3 mousePos = Input.mousePosition;
            //点击屏幕左边
            if (mousePos.x<=Screen.width/2)
            {
                isMoveLeft = true;
            }
            else//点击屏幕右边
            {
                isMoveLeft = false;
            }
            Jump();
        }
    }

    /// <summary>
    /// 人物跳跃
    /// </summary>
    private void Jump()
    {
        if (isMoveLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.DOMoveX(nextPlatformLeft.x, 0.2f);
            transform.DOMoveY(nextPlatformLeft.y+0.7f, 0.15f);
        }else
        {
            transform.localScale = Vector3.one;
            transform.DOMoveX(nextPlatformRight.x, 0.2f);
            transform.DOMoveY(nextPlatformRight.y + 0.7f, 0.15f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            collisionObject = collision.gameObject;
            //if (currentPlatform == collision.gameObject)
            //    return;
            currentPlatform = collision.gameObject;
            isJumping = false;
            Vector3 currentPlatformPos = collision.transform.position;
            nextPlatformLeft = currentPlatformPos + new Vector3(-vars.nextXPos, vars.nextYPos, 0);
            nextPlatformRight = currentPlatformPos + new Vector3(vars.nextXPos, vars.nextYPos, 0);
            //广播加分
            EventCenter.Broadcast(EventType.AddScore);
        }
        else
        {
            print("游戏失败");
        }
    }
}