using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelLock : MonoBehaviour
{
    int levelnum; //현재 스테이지 번호, 오픈한 스테이지 번호
    public GameObject stageNumObject;
    public GameObject[] locks;

    void Start()
    {
        //PlayerPrefs.DeleteAll(); //테스트 할 때 활성화 한번 하고 비활성화 후 재시작 해주세요
        Button[] stages = stageNumObject.GetComponentsInChildren<Button>();

        levelnum = PlayerPrefs.GetInt("levelReached");
        //레벨에 따라 버튼 활성화/자물쇠 아이콘 활성화
        for (int i = levelnum + 1; i < stages.Length; i++)
        {
            stages[i].interactable = false;
            locks[i].gameObject.SetActive(true);
        }
    }
}
