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
        SceneManager.LoadScene("MainScene"); //스테이지 1,2,3,4 현재 기존 스테이지로 연결해놓음
    }                                         //필요 없는경우 stage 함수 한개로 줄일것
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
