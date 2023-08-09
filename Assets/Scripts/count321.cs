using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class count321 : MonoBehaviour
{
    public float time = 3;
    public Text count3;
    
    // Start is called before the first frame update
    void Start()
    {
        time = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        count3.text = time.ToString("N1");
        if (time == 0) 
        {
            Destroy(gameObject);
        }
    }
}
