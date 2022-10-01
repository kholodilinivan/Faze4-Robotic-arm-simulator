using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    public int grab = 2; // 0 - release the object, 1 - grab, 2 - do nothing
    public GameObject anim;
    Collider CollidedWith;

    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>().speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (grab == 1)
        {
            StartCoroutine(TestCoroutine());
        }

        if (grab == 0)
        {
            ReleaseObj();   
        }
    }

    private void ReleaseObj()
    {
        anim.GetComponent<Animator>().StartPlayback();
        anim.GetComponent<Animator>().speed = -1f;
        CollidedWith.transform.parent = null;
        CollidedWith.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CollidedWith = other;
        print(other);
    }

    IEnumerator TestCoroutine()
    {
        grab = 2;
        anim.GetComponent<Animator>().speed = 1f;
        yield return new WaitForSeconds(3.7f);
        anim.GetComponent<Animator>().speed = 0f;
        CollidedWith.GetComponent<Rigidbody>().isKinematic = true;
        CollidedWith.transform.parent = this.transform;
    }
}
