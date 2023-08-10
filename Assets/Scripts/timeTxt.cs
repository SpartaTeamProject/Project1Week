using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class timeTxt : MonoBehaviour
{
    public Text limitTimeTxt;
    public GameObject runRtan;
    public Animator anim;
    public float rtanSpeed;
    public float originSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originSpeed = -0.0125f;
        anim.SetBool("isLimit", false);
    }


    void Update()
    {

        //시간제한 경고 (임시로 10초 지정)
        if (gameManager.I.time <= 10f)
        {
            anim.SetBool("isLimit", true); // timeTxt 색상 변경
            runRtan.SetActive(true);
            if (runRtan.transform.position.x < -3.5f)
                runRtan.SetActive(false);
            else
                runRtan.transform.position += new Vector3(rtanSpeed, 0, 0);
            //대충 적절하게 르탄이가 다 지나가면 타임 오버&&르탄이 이동정지, 비활성화

            //==>르탄이에 오디오 소스를 단 후, 오디오 소스의 Play on Awake 기능을 통해 해결함

            if (Time.timeScale == 0)
                rtanSpeed = 0f;
            else
                rtanSpeed = originSpeed;
        }
    }
}
