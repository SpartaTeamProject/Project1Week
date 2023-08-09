using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopupScore : MonoBehaviour
{
    public Text Q_TimeTxt;
    public GameObject Q_EndPanel;
    public GameObject Q_Card;
    public GameObject Q_Cards;
    public Text Q_ScoreTxt;
    public Text Q_AttemptsTxt;
    public Text Q_FinalScore;
    public Text Q_FinalAttempts;
    public Text Q_HighestScore;
    public GameObject Q_FailTxt;
    public Text Q_RealsucTxt;
    public GameObject Q_SuccessTxt;
    public float Q_Time;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager.I.timeTxt = Q_TimeTxt;
        gameManager.I.endPanel = Q_EndPanel;
        gameManager.I.card = Q_Card;
        gameManager.I.Cards = Q_Cards;
        gameManager.I.scoreTxt = Q_ScoreTxt;
        gameManager.I.attemptsTxt = Q_AttemptsTxt;
        gameManager.I.finalScore = Q_FinalScore;
        gameManager.I.finalAttempts = Q_FinalAttempts;
        gameManager.I.highestScore = Q_HighestScore;
        gameManager.I.failTxt = Q_FailTxt;
        gameManager.I.realsucTxt = Q_RealsucTxt;
        gameManager.I.successTxt = Q_SuccessTxt;

        gameManager.I.time = Q_Time;

        gameManager.I.isMainScene = true;

        gameManager.I.Start();
        //마지막줄에 상태변화 bool 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
