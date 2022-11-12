using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public GameObject sensor1, sensor2;
    public float speed_1 = 0.6f;
    public float speed_2 = 0.3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().velocity = transform.right * speed_1;
    }


    // Write data

    public void OnTriggerEnter(Collider colider)
    {
        if (colider.gameObject.tag == "sensor1")
        {
            StartCoroutine(ExampleCoroutine1());
        }
        if (colider.gameObject.tag == "sensor2")
        {
            StartCoroutine(ExampleCoroutine2());
        }

    }

    IEnumerator ExampleCoroutine1()
    {
        speed_1 = speed_2;
        yield return null;
    }
    IEnumerator ExampleCoroutine2()
    {
        speed_1 = 0f;
        yield return null;
    }
}
