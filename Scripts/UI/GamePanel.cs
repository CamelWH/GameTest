using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePanel : MonoBehaviour
{
    private Button btn_Pause;
    private Button btn_Play;
    private Text txt_Score;
    private Text txt_DiamondCount;
    private int score=-1;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        AddEvent();
        btn_Pause = transform.Find("btn_Pause").GetComponent<Button>();
        btn_Play = transform.Find("btn_Play").GetComponent<Button>();
        txt_Score = transform.Find("txt_Score").GetComponent<Text>();
        txt_DiamondCount = transform.Find("Diamond/txt_DiamondCount").GetComponent<Text>();
        btn_Pause.onClick.AddListener(OnPauseButtonClick);
        btn_Play.onClick.AddListener(OnPlayButtonClick);
        gameObject.SetActive(false);
        btn_Play.gameObject.SetActive(false);
    }
    private void AddEvent()
    {
        EventCenter.AddListener(EventType.ShowGamePanel, Show);
        EventCenter.AddListener(EventType.AddScore, AddScore);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.ShowGamePanel, Show);
        EventCenter.RemoveListener(EventType.AddScore, AddScore);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void OnPauseButtonClick()
    {
        btn_Pause.gameObject.SetActive(false);
        btn_Play.gameObject.SetActive(true);
    }
    private void OnPlayButtonClick()
    {
        btn_Play.gameObject.SetActive(false);
        btn_Pause.gameObject.SetActive(true);
    }
    private void AddScore()
    {
        score++;
        txt_Score.text = score.ToString();
    }
}