using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class count321 : MonoBehaviour
{
    public float time = 3;
    public Text count3;
    public AudioSource audioSource;
    public AudioClip start;

    // Start is called before the first frame update
    void Start()
    {
        startSound();
        time = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        count3.text = time.ToString("N0");
        if (time <= 0.01f) 
        {
            Destroy(gameObject);

        }
    }
    void oneshot() 
    {
        audioSource.PlayOneShot(start, 0.5f);
    }
    void startSound() 
    {
        Invoke("oneshot", 0f);
        Invoke("oneshot", 1f);
        Invoke("oneshot", 2f);
    }
    
}
