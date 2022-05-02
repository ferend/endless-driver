using System;
using AmazingAssets.CurvedWorld;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour
{
    public CurvedWorldController curvedWorldController;
    private  float myFloat;
    private int[] endValues =
    {
        3,0,-3
    } ;
    public static GameFlowManager Instance;

    public Text timerText;
    public Text playerScoreText;
    public float targetTime;
    public int playerScore;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    public void Start()
    {
        targetTime = 0;
        InvokeRepeating(nameof(TimerEnded), 1f,1f);
    }

    public void Update()
    {
        targetTime += 1*Time.deltaTime;
        timerText.text = targetTime.ToString();

        if (targetTime == 0)
        {
            CancelInvoke();
        }

    }

    public void TimerEnded()
    {
        playerScore += 1;
        playerScoreText.text = playerScore.ToString();
    }

    public void WorldCurveTweener(int carCount)
    {
        DOTween.To(()=> myFloat, x=>
        {
            myFloat = x;
            curvedWorldController.SetBendCurvatureSize(myFloat);
            curvedWorldController.SetBendHorizontalSize(myFloat);
        }, endValues[carCount], 10).SetId("curve");

    }
    
    
}
