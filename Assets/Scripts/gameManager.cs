using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class gameManager : MonoBehaviour
{
    public static gameManager I;
    

    private void Awake()
    {
        I = this;
    }

    public Text timeTxt;
    public GameObject endTxt;
    public GameObject card;
    float time = 30;

    public GameObject firstCard;
    public GameObject secondCard;

    public AudioClip mach;
    public AudioSource audioSource;

    public int currentStage;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(currentStage);

        // 내가 건들여야 하는 부분
        Time.timeScale = 1f;

        int[] stageSize = { 2, 3, 4, 4 };
        //float[] stageScale = { 1.7f, 1.5f, 1.3f, 1.3f };

        float[] stageScale = { 1f, 1f, 1f, 1f };

        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for(int i=0; i< stageSize[currentStage]; ++i)
        {
            for (int j=0; j < stageSize[currentStage]; ++j)
            {
                card.transform.localScale = new Vector3(stageScale[currentStage], stageScale[currentStage], 1f);

                GameObject newCard = Instantiate(card);
                newCard.transform.parent = GameObject.Find("Cards").transform;
                newCard.transform.position = new Vector3(i * stageScale[currentStage], j * stageScale[currentStage], 0);

                string picName = "pic" + rtans[i*stageSize[currentStage]+j].ToString();
                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(picName);
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time <= 0f)
        {
            Time.timeScale = 0f;
            endTxt.SetActive(true);
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

            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            if (cardsLeft == 2)
            {
                Time.timeScale = 0f;
                endTxt.SetActive(true);
            }
        }
        else
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent <card>().closeCard();
        }
    
        firstCard = null;
        secondCard = null;
    }

}
