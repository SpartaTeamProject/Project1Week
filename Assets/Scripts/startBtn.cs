using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startBtn : MonoBehaviour
{

    public AudioClip click;
    public AudioSource startSource;


    public async void gameStart()
    {
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("StageScene");       
    }

    public async void StageScene()
    {
        gameManager.I.isMainScene = false;
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("StageScene");
    }
    public async void stage1()
    {
        gameManager.I.currentStage = 0;
        gameManager.I.isMainScene = false;
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene"); //�������� 1,2,3,4 ���� ���� ���������� �����س���
    }                                         
    public async void stage2()
    {
        gameManager.I.currentStage = 1;
        gameManager.I.isMainScene = false;
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene"); //�������� 1,2,3,4 ���� ���� ���������� �����س���
    }                                         //�ʿ� ���°�� stage �Լ� �Ѱ��� ���ϰ�
    public async void stage3()
    {
        gameManager.I.currentStage = 2;
        gameManager.I.isMainScene = false;
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene");
    }
    public async void stage4()
    {
        gameManager.I.currentStage = 3;
        gameManager.I.isMainScene = false;
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene");
    }
    public void resetBtn()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("StartScene");
    }
}
