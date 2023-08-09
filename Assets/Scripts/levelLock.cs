using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class levelLock : MonoBehaviour
{
    int levelnum; //현재 스테이지 번호, 오픈한 스테이지 번호
    public GameObject stageNumObject;
    public GameObject[] locks;
    public Text[] highScoreTxt;
    void Start()
    {
        Button[] stages = stageNumObject.GetComponentsInChildren<Button>();

        levelnum = PlayerPrefs.GetInt("levelReached");
        //레벨에 따라 버튼 활성화/자물쇠 아이콘 활성화

        Debug.Log(levelnum);
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].interactable = false;
            locks[i].gameObject.SetActive(true);
            highScoreTxt[i].text = PlayerPrefs.GetInt("bestScore" + i.ToString()).ToString();
        }


        if(levelnum == 0)
        {
            stages[0].interactable = true;
            locks[0].gameObject.SetActive(false);
        }
        else if(levelnum == 1)
        {
            for (int i = 0; i < (levelnum + 1); i++)
            {
                stages[i].interactable = true;
                locks[i].gameObject.SetActive(false);
                Debug.Log("반복했다.");
            }
        }
        else if (levelnum == 2)
        {
            for (int i = 0; i < (levelnum + 1); i++)
            {
                stages[i].interactable = true;
                locks[i].gameObject.SetActive(false);
                Debug.Log("반복했다.");
            }
        }
        else if (levelnum == 3)
        {
            for (int i = 0; i < (levelnum + 1); i++)
            {
                stages[i].interactable = true;
                locks[i].gameObject.SetActive(false);
                Debug.Log("반복했다.");
            }
        }

        /*
    switch (levelnum)
    {
        case 0:

            break;
        case 1:
            for (int i = 0; i < levelnum; i++)
            {
                stages[i].interactable = true;
                locks[i].gameObject.SetActive(false);
                Debug.Log("반복했다.");
            }
            break;
        case 2:
            for (int i = 0; i < levelnum; i++)
            {
                stages[i].interactable = true;
                locks[i].gameObject.SetActive(false);
            }
            break;
        case 3:
            for (int i = 0; i < levelnum; i++)
            {
                stages[i].interactable = true;
                locks[i].gameObject.SetActive(false);
            }
            break;
    }
        */


    }
}
