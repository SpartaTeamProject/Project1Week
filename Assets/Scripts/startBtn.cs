using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameStart()
    {
        SceneManager.LoadScene("StageScene");
    }
    public void stage1()
    {
        SceneManager.LoadScene("MainScene"); //�������� 1,2,3,4 ���� ���� ���������� �����س���
    }                                         //�ʿ� ���°�� stage �Լ� �Ѱ��� ���ϰ�
    public void stage2()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void stage3()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void stage4()
    {
        SceneManager.LoadScene("MainScene");
    }
}
