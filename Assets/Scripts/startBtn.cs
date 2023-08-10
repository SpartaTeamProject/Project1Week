using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class startBtn : MonoBehaviour
{

    public AudioClip click;
    public AudioSource startSource;


    public async void GameStart()
    {
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("StageScene");
        AudioManager._instance.settingPanel.SetActive(false);
    }
    public async void StageBtn0()
    {
        gameManager.I.currentStage = 0;
        gameManager.I.isMainScene = false;
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene"); //�������� 1,2,3,4 ���� ���� ���������� �����س���
    }                                         
    public async void StageBtn1()

    {
        gameManager.I.currentStage = 1;
        gameManager.I.isMainScene = false;
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene"); //�������� 1,2,3,4 ���� ���� ���������� �����س���
    }                                         //�ʿ� ���°�� stage �Լ� �Ѱ��� ���ϰ�
    public async void StageBtn2()
    {
        gameManager.I.currentStage = 2;
        gameManager.I.isMainScene = false;
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene");
    }
    public async void StageBtn3()
    {
        gameManager.I.currentStage = 3;
        gameManager.I.isMainScene = false;
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene");
    }
    public async void StageScene()
    {
        gameManager.I.isMainScene = false;
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400); // 매칭 성공 실패 텍스트가 사라지기 전에 누르니 missing에러가 발생합니다, 충분한 시간 이후 씬 이동을 진행합니다. 
        SceneManager.LoadScene("StageScene");
    }
    public void ResetBtn()
    {
        PlayerPrefs.DeleteAll();
        AudioManager._instance.settingPanel.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }

    public async void RePlayBtn()
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
