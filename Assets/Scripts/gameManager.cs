using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.UIElements;
using System;

public class gameManager : MonoBehaviour
{
    public static gameManager I;

    private void Awake()
    {
        I = this;
    }

    public Text timeTxt;
    public GameObject endPanel;
    public GameObject card;
    public float time = 30;

    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject Cards;

    public AudioClip mach;
    public AudioSource audioSource;

    public Text scoreTxt;
    public Text attemptsTxt;
    public Text finalScore;
    public Text finalAttempts;
    public Text highestScore;

    public GameObject failTxt;  
    public Text realsucTxt;        
    public GameObject Tpenalty;  
    public GameObject successTxt;
    
    public int currentStage = 0;
    public int maxSize = 5;
    public int shuffleCnt = 10;


    int score = 0;
    int attempts = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        highestScore.text = PlayerPrefs.GetInt("bestScore" + currentStage.ToString()).ToString();

        //=============== MakeBoard(currentStage)
        //* input: 0<currentStage<4
        // stageSize[currentStage] = 스테이지에 따른 정사각형 크기
        // cardSpace = 카드끼리의 간격
        // cardScale = 카드 스케일이 maxSize 내에서 자동 스케일링되도록 하는 계산식
        // cardIndex = 기존 코드의 rtans 역할
        
        // 1. cardIndex 배열 초기화 후 셔플
        // 2. card n*n개 배치
        // 3. Cards 위치 조정.

        int[] stageSize = { 2, 3, 4, 4 };
        float cardSpace = 0.15f;
        float cardScale = (maxSize - (cardSpace * (stageSize[currentStage] -1)))/ stageSize[currentStage];

        int cardIndexSize = Convert.ToInt32(Math.Pow(stageSize[currentStage], 2));
        int[] cardIndex = new int[cardIndexSize];

        //0   2   4   6   8   10   12   14
        //00, 11, 22, 33, 44, 5 5, 6 6, 7 7
        /*
        for (int i = 0, cnt = 0; i < cardIndexSize; i += 2, ++cnt)
            for (int j = 0; j < 2; ++j)
                cardIndex[i + j] = cnt;
        */

        for (int i = 0, cnt=0; i < cardIndex.Length; ++i, ++cnt)
        {
            if (cnt>7) cnt = 0;
            cardIndex[i] = cnt;
        }

        foreach (int i in cardIndex)
            Debug.Log(i);

        //Shuffle
        for (int i = cardIndex.Length-1; i>0; --i)
        {
            int rand = UnityEngine.Random.Range(0, i + 1);
            int temp = cardIndex[rand];
            cardIndex[rand] = cardIndex[i];
            cardIndex[i] = temp;
        }

        for (int i = 0; i < stageSize[currentStage]; ++i)
        {
            for (int j = 0; j < stageSize[currentStage]; ++j)
            {
                card.transform.localScale = new Vector3(cardScale, cardScale, 1);
                card.transform.Find("front").localScale = new Vector3(cardScale, cardScale, 1);

                GameObject newCard = Instantiate(card);

                newCard.transform.parent = GameObject.Find("Cards").transform;
                newCard.transform.position = new Vector3((i*(cardScale+cardSpace)), (j*(cardScale+cardSpace)), 1);

                string picName = "pic" + cardIndex[i * stageSize[currentStage] + j].ToString();
                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(picName);
            }
        }
        Vector3 leftDown = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));
        Cards.transform.position = leftDown + new Vector3(0.5f + cardScale / 2, 1.5f + cardScale / 2, 0);
        //============ end MakeBoard
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        scoreTxt.text = score.ToString();
        attemptsTxt.text = attempts.ToString();

        if (time <= 0f)
        {
            gameOver();
        }
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        
        if(firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(mach);
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();
            Success();
            score += 10;

            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            if (cardsLeft == 2)
            {

                gameOver();
            }
        }
        else
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent <card>().closeCard();
            fail();                                     
            timepenalty();
            if (score >= 2)
            {
                score -= 2;
            }
            else
            {
                score = 0;
            }
        }
        attempts += 1;
    
        firstCard = null;
        secondCard = null;
    }

    public void gameOver()
    {
        Time.timeScale = 0f;
        endPanel.SetActive(true);
        score += (int)time * 5;

        finalScore.text = score.ToString();
        finalAttempts.text = attempts.ToString();

        if (PlayerPrefs.HasKey("bestScore" + currentStage.ToString()) == false)
        {
            PlayerPrefs.SetInt("bestScore" + currentStage.ToString(), score);
        }
        else
        {
            if (PlayerPrefs.GetInt("bestScore" + currentStage.ToString()) < score)
            {
                PlayerPrefs.SetInt("bestScore" + currentStage.ToString(), score);
            }
        }
    }

    public void SucStart()
    {
        successTxt.SetActive(true);
        if (firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name == "pic1" || firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name == "pic2")
        {
            realsucTxt.text = "����\n�̵���";
        }
        else if (firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name == "pic2" || firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name == "pic3")
        {
            realsucTxt.text = "����\n������";
        }
        else if (firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name == "pic4" || firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name == "pic5")
        {
            realsucTxt.text = "����\n�ڹ���";
        }
        else
        {
            realsucTxt.text = "����\n��α�";
        }
        realsucTxt.text = realsucTxt.text.Replace("\\n", "\n");
    }

    public void failStart()
    {
        failTxt.SetActive(true);
    }
    public void SucEnd()
    {
        successTxt.SetActive(false);
    }

    public void failEnd()
    {
        failTxt.SetActive(false);
    }
    public void Success()
    {
        SucStart();
        Invoke("SucEnd", 1.0f);
    }

    public void fail()
    {
        failStart();
        Invoke("failEnd", 1.0f);
    }
    public void timepenalty()
    {
        Time.timeScale = 0f;
        if (time >= 2f)
        {
            time = time - 2f;
        }
        else
            time = 0f;
        makeminus2();

        Time.timeScale = 1f;
    }
    public void makeminus2()
    {
        float x = 500f;
        float y = 900f;
        Instantiate(Tpenalty, new Vector3(x, y, 0), Quaternion.identity);
        //gameObject.transform.parent = GameObject.Find("timeTxt").transform;
    }



}
