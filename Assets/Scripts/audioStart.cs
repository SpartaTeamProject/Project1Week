using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioStart : MonoBehaviour
{

    public AudioClip startBgm;
    public AudioSource startBgmSource;
    bool starters = true;

    void Start()
    {
        startBgmSource.clip = startBgm;
        startBgmSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainScene")
        {
            starters = false;
        }
    }

    void musicOn()
    {
        if (starters == false)
        {
            startBgmSource.Stop();
        }
        else
        {
            startBgmSource.Play();
            starters = true;
        }
    }
}
