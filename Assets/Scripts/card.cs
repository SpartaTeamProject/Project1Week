using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System;
using UnityEditor.SceneManagement;
using UnityEditor;

public class card : MonoBehaviour
{
    public Animator anim;
    public AudioClip flip;
    public AudioClip empty;
    public AudioSource audioSource;
    public int currentStage;
    bool firstOpen = false;
    bool secondOpen = false;
    float timeCount = 0.0f;

    void Start()
    {
        open();//3초 보여주기 
    }

    // Update is called once per frame
    void Update()
    {
        // 첫 번째 카드 선택 후 5초 지나면 다시 뒤집기
        if (firstOpen == true)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= 3.0f)
            {
                firstOpen = false;
                emptyCard();
                timeCount = 0;
            }
            else
            {
                if (secondOpen == true)
                {
                    timeCount = 0;
                }
            }
        }
    }
    public void openCard()
    {

        audioSource.PlayOneShot(flip);
        anim.SetBool("isOpen", true);

        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);

        if (gameManager.I.firstCard == null)
        {
            firstOpen = true;
            gameManager.I.firstCard = gameObject;
        }
        else
        {
            secondOpen = true;
            gameManager.I.secondCard = gameObject;
            gameManager.I.isMatched();
        }

        
        // 선택된 적 있는 카드의 색상 변경 (회색)
        transform.Find("back").Find("Canvas").Find("Image").GetComponent<Image>().color = new Color32(212, 212, 212, 255);
    }

    void emptyCard() 
    {
        audioSource.PlayOneShot(empty, 0.2f);
        gameManager.I.firstCard = null;
        closedCardInvoke();
    }

    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 1.0f);
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void closeCard()
    {
        Invoke("closedCardInvoke", 0.5f);
    }

    void closedCardInvoke()
    {
        anim.SetBool("isOpen", false);
        
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
        firstOpen = false;
        secondOpen = false;

    }

    public void open()
    {


        currentStage = gameManager.I.currentStage;
        anim.SetBool("isOpen", true);
        

        gameManager.I.timeTxtObject.SetActive(false);
        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);
        Invoke("closed", 3f);
    }
    
    public void closed()
    {
        anim.SetBool("isOpen", false);

        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
        gameManager.I.time = 30f;
        gameManager.I.timeTxtObject.SetActive(true);

    }

}