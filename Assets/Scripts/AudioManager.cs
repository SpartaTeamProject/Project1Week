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
        slider.value = PlayerPrefs.GetFloat("MusicVoulume", 0.75f); //초기화
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
        mixer.SetFloat("MusicVoulume", Mathf.Log10(sliderValue) * 20); //Mathf.Log10 = (sliderValue)값이 100,000이라면 5를 반환 상용로그 계산코드임
                                                                       //오디오 믹서의 볼륨값은 0~100이 아닌 -80~0 이고 & 슬라이더의 값은 0.0001 ~ 1 이다.
                                                                       //이러한 코드를 사용하는 이유는 각각의 값을 서로에게 맞추기 위해서임 
                                                                       //sliderValue에 0.0001을 대입하면 -80, 최대 값인 1을 대입하면 0이 나온다.
    }
}
