using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startBtn : MonoBehaviour
{
    public void gameStart()
    {
        SceneManager.LoadScene("StageScene");       
    }

    public void StageScene()
    {
        gameManager.I.isMainScene = false;
        SceneManager.LoadScene("StageScene");
    }
    public void stage1()
    {
        gameManager.I.currentStage = 0;
        gameManager.I.isMainScene = false;
        SceneManager.LoadScene("MainScene"); //스테이지 1,2,3,4 현재 기존 스테이지로 연결해놓음
    }                                         
    public void stage2()
    {
        gameManager.I.currentStage = 1;
        gameManager.I.isMainScene = false;
        SceneManager.LoadScene("MainScene");
    }
    public void stage3()
    {
        gameManager.I.currentStage = 2;
        gameManager.I.isMainScene = false;
        SceneManager.LoadScene("MainScene");
    }
    public void stage4()
    {
        gameManager.I.currentStage = 3;
        gameManager.I.isMainScene = false;
        SceneManager.LoadScene("MainScene");
    }
    public void resetBtn()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("StartScene");
    }
}
