using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System;

public class card : MonoBehaviour
{
    public Animator anim;
    public AudioClip flip;
    public AudioSource audioSource;
    bool firstOpen = false;
    bool secondOpen = false;
    float timeCount = 0.0f;

    void Start()
    {
        
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
        Invoke("closedCardInvoke", 0.2f);
    }

    void closedCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
        firstOpen = false;
        secondOpen = false;

    }
    
    
}