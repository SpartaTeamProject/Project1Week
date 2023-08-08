using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public Animator anim;
    public AudioClip flip;
    public AudioSource audioSource;

    float count = 0.0f;
    float timeLog;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openCard()
    {
        audioSource.PlayOneShot(flip);
        anim.SetBool("isOpen", true);

        transform.Find("front").gameObject.SetActive(true);

        transform.Find("back").gameObject.SetActive(false);

        if (gameManager.I.firstCard == null)
        {
            gameManager.I.firstCard = gameObject;

        }
        else
        {
            gameManager.I.secondCard = gameObject;
            gameManager.I.isMatched();
        }

        
        // 선택된 적 있는 카드의 색상 변경 (회색)
        transform.Find("back").Find("Canvas").Find("Image").GetComponent<Image>().color = new Color32(212, 212, 212, 255);
    }

    void emptyCard() // 첫 번째 카드 선택 후 5초 지나면 다시 뒤집기
    {
        gameManager.I.firstCard = null;
        gameManager.I.secondCard = null;
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

    }
    
}