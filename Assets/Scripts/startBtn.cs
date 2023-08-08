using System.Collections;
using System.Collections.Generic;
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
    public async void stage1()
    {
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene"); //�������� 1,2,3,4 ���� ���� ���������� �����س���
    }                                         //�ʿ� ���°�� stage �Լ� �Ѱ��� ���ϰ�
    public async void stage2()
    {
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene");
    }
    public async void stage3()
    {
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene");
    }
    public async void stage4()
    {
        startSource.PlayOneShot(click, 0.5f);
        await Task.Delay(400);
        SceneManager.LoadScene("MainScene");
    }
}
