using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public AudioClip main_bgm;
    public AudioClip start_bgm;
    public AudioSource audioSource;

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

}
