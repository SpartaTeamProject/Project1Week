using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageName : MonoBehaviour
{
    public Text stage;
    public int currentStage = gameManager.I.currentStage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentStage = gameManager.I.currentStage;
        if (currentStage == 0) 
        {
            stage.text = "stage 01";
        }
        else if (currentStage == 1)
        {
            stage.text = "stage 02";
        }
        else if (currentStage == 2)
        {
            stage.text = "stage 03";
        }
        else if (currentStage == 3)
        {
            stage.text = "stage 04";
        }

    }
}
