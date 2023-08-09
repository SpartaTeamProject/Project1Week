using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public Animator anim;
    public AudioClip flip;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        open();        
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
            Invoke("emptyCard", 5.0f);

        }
        else
        {
            gameManager.I.secondCard = gameObject;
            gameManager.I.isMatched();
        }
        Invoke("emptyCard", 5.0f);

        // 선택된 적 있는 카드의 색상 변경 (회색)
        transform.Find("back").Find("Canvas").Find("Image").GetComponent<Image>().color = new Color32(212, 212, 212, 255);
    }

    void emptyCard() // 첫 번째 카드 선택 후 5초 지나면 다시 뒤집기
    {
        gameManager.I.firstCard = null;
        closedCardInvoke();

    }

    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 0.1f);
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void closeCard()
    {
        Invoke("closedCardInvoke", 1.0f);
    }

    void closedCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);

    }

    public void open()
    {
        //anim.SetBool("isOpen", true);
        gameManager.I.timeTxtObject.SetActive(false);
        transform.Find("front").gameObject.SetActive(true);

        transform.Find("back").gameObject.SetActive(false);
        Invoke("closed", 3f);
    }
    public void closed()
    {
        //anim.SetBool("isOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
        gameManager.I.time = 30f;
        gameManager.I.timeTxtObject.SetActive(true);

    }
}