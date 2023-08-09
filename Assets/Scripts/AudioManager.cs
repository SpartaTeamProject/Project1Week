using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;

    public AudioClip main_bgm;
    public AudioClip start_bgm;
    public AudioSource audioSource;

    public AudioMixer mixer;
    public Slider slider;
    public GameObject settingPanel;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVoulume", 0.75f); //�ʱ�ȭ
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "MainScene")
        {
            BgmPlay(main_bgm);
        }
        else if (scene.name == "StartScene")
        {
            BgmPlay(start_bgm);
        }
        else
        {
            if(audioSource.clip == main_bgm)
            {
                BgmPlay(start_bgm);
            }

        }
    }

    private void BgmPlay(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = 0.3f;
        audioSource.Play();
    }



    public void SetVoulume(float sliderValue)
    {
        mixer.SetFloat("MusicVoulume", Mathf.Log10(sliderValue) * 20); //Mathf.Log10 = (sliderValue)���� 100,000�̶�� 5�� ��ȯ ���α� ����ڵ���
                                                                       //����� �ͼ��� �������� 0~100�� �ƴ� -80~0 �̰� & �����̴��� ���� 0.0001 ~ 1 �̴�.
                                                                       //�̷��� �ڵ带 ����ϴ� ������ ������ ���� ���ο��� ���߱� ���ؼ��� 
                                                                       //sliderValue�� 0.0001�� �����ϸ� -80, �ִ� ���� 1�� �����ϸ� 0�� ���´�.
    }
}
