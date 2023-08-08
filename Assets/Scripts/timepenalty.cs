using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(GameObject.Find("timeTxt").transform.parent);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(0.0f, 0.5f, 0.0f);
        if (transform.position.y > 1000.0f)
        {
            Destroy(gameObject);
        }

    }
}
