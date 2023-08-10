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

        //�ð����� ��� (�ӽ÷� 10�� ����)
        if (gameManager.I.time <= 10f)
        {
            anim.SetBool("isLimit", true); // timeTxt ���� ����
            runRtan.SetActive(true);
            if (runRtan.transform.position.x < -3.5f)
                runRtan.SetActive(false);
            else
                runRtan.transform.position += new Vector3(rtanSpeed, 0, 0);
            //���� �����ϰ� ��ź�̰� �� �������� Ÿ�� ����&&��ź�� �̵�����, ��Ȱ��ȭ

            //==>��ź�̿� ����� �ҽ��� �� ��, ����� �ҽ��� Play on Awake ����� ���� �ذ���

            if (Time.timeScale == 0)
                rtanSpeed = 0f;
            else
                rtanSpeed = originSpeed;
        }
    }
}
