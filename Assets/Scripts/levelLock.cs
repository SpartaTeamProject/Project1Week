using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelLock : MonoBehaviour
{
    int levelnum; //���� �������� ��ȣ, ������ �������� ��ȣ
    public GameObject stageNumObject;
    public GameObject[] locks;

    void Start()
    {
        //PlayerPrefs.DeleteAll(); //�׽�Ʈ �� �� Ȱ��ȭ �ѹ� �ϰ� ��Ȱ��ȭ �� ����� ���ּ���
        Button[] stages = stageNumObject.GetComponentsInChildren<Button>();

        levelnum = PlayerPrefs.GetInt("levelReached");
        //������ ���� ��ư Ȱ��ȭ/�ڹ��� ������ Ȱ��ȭ
        for (int i = levelnum + 1; i < stages.Length; i++)
        {
            stages[i].interactable = false;
            locks[i].gameObject.SetActive(true);
        }
    }
}
