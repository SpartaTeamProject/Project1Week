using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.UIElements;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.SocialPlatforms.Impl;

public class gameManager : MonoBehaviour
{
    public static gameManager I;

    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
        DontDestroyOnLoad(gameObject);


    }
    public GameObject timeTxtObject;
    public Text timeTxt;
    public GameObject endPanel;
    public GameObject card;
    public float time = 30;

    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject Cards;

    public AudioClip mach;
    public AudioClip unmatched;
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

    public int currentStage;
    public int maxSize = 5;
    public bool perpect = false;
    public GameObject perpectPanel;

    int score = 0;
    int attempts = 0;


    public bool isMainScene = false;
    public bool isCleared = false;
    int cardsLeft;

    // Start is called before the first frame update
    public void Start()
    {
        perpect = false;
        if (SceneManager.GetActiveScene().name != "MainScene" || !isMainScene)
            return;

        Time.timeScale = 1f;
        highestScore.text = PlayerPrefs.GetInt("bestScore" + currentStage.ToString()).ToString();

        score = 0;
        attempts = 0;

        isCleared = false;
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

        // 디버그 시 무한루프 방지용도
        if ((stageSize[currentStage]<=0) && (stageSize[currentStage] >= 4))
            stageSize[currentStage] = 4;

        float cardSpace = 0.15f;
        float cardScale = (maxSize - (cardSpace * (stageSize[currentStage] -1)))/ stageSize[currentStage];

        int cardIndexSize = Convert.ToInt32(Math.Pow(stageSize[currentStage], 2));
        int[] cardIndex = new int[cardIndexSize];
        int[] playerIndex = new int[cardIndexSize/2];

        // 어떤 사진을 쓸 것인지 결정
        bool[] checkOverlap = new bool[8];
        for (int i = 0; i < playerIndex.Length; ++i)
        {
            int rand;
            do
            {
                rand = UnityEngine.Random.Range(0, 8);
            } while (checkOverlap[rand] == true);
            checkOverlap[rand] = true;
            playerIndex[i] = rand;
        }

        // 결정된 사진 2장씩 채워넣기
        for (int i = 0; i < playerIndex.Length; ++i)
            for (int j = 0; j < 2; ++j)
                cardIndex[(2 * i) + j] = playerIndex[i];

        //for (int i = 0; i < cardIndexSize-1; ++i)
        //    cardIndex[i] = 0;


        // 홀수 갯수의 카드일 경우 X 표시 추가
        if (stageSize[currentStage]%2==1)
            cardIndex[^1] = 99;

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


        //Placement
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

                //imageRotate on stage 4
                if (currentStage == 3)
                {
                    float[] angles = { 0, 90, 180, 270 };
                    angles = angles.OrderBy(item => UnityEngine.Random.Range(-1.0f, 1.0f)).ToArray();
                    newCard.transform.Find("front").transform.eulerAngles = new Vector3(0, 0, angles[0]);
                }
                //====================


            }
        }
        Vector3 leftDown = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));
        Cards.transform.position = leftDown + new Vector3(0.5f + cardScale / 2, 1.5f + cardScale / 2, 0);
        //============ end MakeBoard


        //carsLeft

        if (currentStage == 0)
        {
            cardsLeft = 4;
        }
        else if (currentStage == 1)
        {
            cardsLeft = 9;
        }
        else
        {
            cardsLeft = 16;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainScene" || !isMainScene)
            return;

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

            cardsLeft -= 2;
            Debug.Log(cardsLeft);
            if (cardsLeft < 2)
            {
                perpect = true;
                gameOver();
            }
        }
        else
        {
            audioSource.PlayOneShot(unmatched, 0.2f);
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
        
        score += (int)time * 5;
        if (perpect == true)
        {
            PerpectGame();
        }
        finalScore.text = score.ToString();

        if (cardsLeft < 2)
        {
            finalAttempts.text = (attempts + 1).ToString();
        }
        else
        {
            finalAttempts.text = attempts.ToString();
        }

        endPanel.SetActive(true);

        if (PlayerPrefs.HasKey("bestScore" + currentStage.ToString()) == false)
        {
            PlayerPrefs.SetInt("bestScore" + currentStage.ToString(), score);
        }
        else
        {
            if ((PlayerPrefs.GetInt("bestScore" + currentStage.ToString()) >= (currentStage + 10))) //해당 스테이지 키값에 이미 목표점수를 넘긴 기록이 있는지 체크
                isCleared = true;

            if (PlayerPrefs.GetInt("bestScore" + currentStage.ToString()) < score)
            {
                PlayerPrefs.SetInt("bestScore" + currentStage.ToString(), score);
            }
        }



        //목표점수 도달 시 스테이지 해금
        if ((PlayerPrefs.GetInt("bestScore" + currentStage.ToString()) >= (currentStage + 10)) && !isCleared) //임시로 목표점수 설정
        {
            PlayerPrefs.SetInt("levelReached", (currentStage + 1)); //현재 스테이지를 깨면 스테이지락 해제

            if (PlayerPrefs.GetInt("levelReached") >= 3) //마지막 스테이지인 경우 (일단 4스테이지)
            {
                PlayerPrefs.SetInt("levelReached", 3);
                //보상을 주거나 어떤 상호작용이 있으면 좋을듯?
            }
        }

    }

    public void SucStart() // 매칭 성공시 성공 텍스트 on 및 이름 변경
    {
        successTxt.SetActive(true);
        if (firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name == "pic0" || firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name == "pic1")
        {
            realsucTxt.text = "성공\n이도현";
        }
        else if (firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name == "pic2" || firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name == "pic3")
        {
            realsucTxt.text = "성공\n이현지";
        }
        else if (firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name == "pic4" || firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name == "pic5")
        {
            realsucTxt.text = "성공\n박민혁";
        }
        else
        {
            realsucTxt.text = "성공\n우민규";
        }
        realsucTxt.text = realsucTxt.text.Replace("\\n", "\n");
    }

    public void failStart() // 실패 텍스트 on
    {
        failTxt.SetActive(true);
    }
    public void SucEnd() // 성공 텍스트 off
    {
        successTxt.SetActive(false);
    }

    public void failEnd() // 실패 텍스트 off
    {
        failTxt.SetActive(false);
    }
    public void Success() // 성공 텍스트 on 및 1초후 off
    {
        SucStart();
        Invoke("SucEnd", 1.0f);
    }

    public void fail() // 실패 텍스트 on 및 1초후 off
    {
        failStart();
        Invoke("failEnd", 1.0f);
    }
    public void timepenalty() // 실패시 시간 2초 감소
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
    public void makeminus2() // -2초 텍스트 생성 
    {
        float x = 500f;
        float y = 900f;
        Instantiate(Tpenalty, new Vector3(x, y, 0), Quaternion.identity);
        //gameObject.transform.parent = GameObject.Find("timeTxt").transform;
    }

    public void PerpectGame() 
    {
        if (currentStage == 0 && attempts==1)
        {
            score *= 2;
            perpectPanel.SetActive(true);
        }
        else if (currentStage == 1 && attempts == 3)
        {
            score *= 2;
            perpectPanel.SetActive(true);
        }
        else if (currentStage >= 2 && attempts == 7)
        {
            score *= 2;
            perpectPanel.SetActive(true);

        }
        perpect = false;
    }


}
