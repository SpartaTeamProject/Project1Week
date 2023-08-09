using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeTxt : MonoBehaviour
{
    public Text limitTimeTxt;
    public GameObject runRtan;
    public Animator anim;
    public float rtanSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isLimit", false);
        runRtan.SetActive(false);
    }


    void Update()
    {

        //�ð����� ��� (�ӽ÷� 10�� ����)
        if (gameManager.I.time < 10f)
        {
            anim.SetBool("isLimit", true);
            runRtan.SetActive(true);
            if (runRtan.transform.position.x < -3.5f)
                runRtan.SetActive(false);
            else
                runRtan.transform.position += new Vector3(rtanSpeed, 0, 0);
            //���� �����ϰ� ��ź�̰� �� �������� Ÿ�� ����&&��ź�� �̵�����, ��Ȱ��ȭ

            //����� �Ŵ����� ���� ���̷� ���� �߰��� ��������?
            //==>��ź�̿� ����� �ҽ��� �� ��, ����� �ҽ��� Play on Awake ����� ���� �ذ���
        }

        if (gameManager.I.endPanel)
            runRtan.SetActive(false);
    }
}
