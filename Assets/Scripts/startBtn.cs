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
        AudioManager._instance.settingPanel.SetActive(false);
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
        if (gameManager.I.failTxt.activeSelf == true || gameManager.I.successTxt.activeSelf == true || gameManager.I.failTxt == null || gameManager.I.successTxt == null)
            return;

        PlayerPrefs.DeleteAll();
        AudioManager._instance.settingPanel.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }
    public async void reStartBtn()
    {
        gameManager.I.isMainScene = false;
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene");
    }

    public void CallSetting()
    {
        AudioManager._instance.settingPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExitSetting()
    {
        AudioManager._instance.settingPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
