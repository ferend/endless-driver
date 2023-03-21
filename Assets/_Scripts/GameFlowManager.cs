using System;
using _Scripts.Network;
using AmazingAssets.CurvedWorld;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour
{
    public CurvedWorldController curvedWorldController;
    private  float myFloat;
    private int[] endValues = {3,0,-3} ;
    public Text timerText;
    public Text playerScoreText;
    public Text highScoreText;
    public float targetTime;
    public int playerScore;
    
    private PlayerProfile _playerProfile;
    public static GameFlowManager Instance;
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

    public void OnEnable()
    {
        _playerProfile = RealmManager.Instance.GetPlayerProfile();
        highScoreText.text =  _playerProfile.HighScore.ToString();
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
        RealmManager.Instance.IncreaseScore();
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
