using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.UIElements;

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
    float time = 30;

    public GameObject firstCard;
    public GameObject secondCard;

    public AudioClip mach;
    public AudioSource audioSource;

    public Text scoreTxt;
    public Text attemptsTxt;
    public Text finalScore;
    public Text finalAttempts;
    public Text highestScore;
    
    public int currentStage = 0;
    public int maxSize = 5;


    int score = 0;
    int attempts = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        highestScore.text = PlayerPrefs.GetInt("bestScore" + currentStage.ToString()).ToString();

        int[] stageSize = { 2, 3, 4, 4 };
        float cardSpace = 0.15f;
        float cardScale = (maxSize - (cardSpace * (stageSize[currentStage] -1)))/ stageSize[currentStage];

        Debug.Log(cardScale);

        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < stageSize[currentStage]; ++i)
        {
            for (int j = 0; j < stageSize[currentStage]; ++j)
            {
                card.transform.localScale = new Vector3(cardScale, cardScale, 1);
                card.transform.Find("front").localScale = new Vector3(cardScale, cardScale, 1);

                GameObject newCard = Instantiate(card);
                newCard.transform.parent = GameObject.Find("Cards").transform;
                newCard.transform.position = new Vector3((i*(cardScale+cardSpace)), (j*(cardScale+cardSpace)), 0);
                newCard.transform.position += new Vector3(-2.0f, -3.0f);

                string picName = "pic" + rtans[i * stageSize[currentStage] + j].ToString();
                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(picName);
            }
        }
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
}
