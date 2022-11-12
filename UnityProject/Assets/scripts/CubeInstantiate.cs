using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInstantiate : MonoBehaviour
{
    public GameObject myPrefabRed, myPrefabGreen, myPrefabBlue;

    void Start()
    {
        GameObject obj = Instantiate(myPrefabGreen, new Vector3(-3.669f, 0.514f, 3.445f), Quaternion.identity);
        obj.transform.parent = GameObject.Find("BaseOrigin").transform;
    }

    public void InstantiateRed()
    {
        GameObject obj = Instantiate(myPrefabRed, new Vector3(-3.669f, 0.514f, 3.445f), Quaternion.identity);
        obj.transform.parent = GameObject.Find("BaseOrigin").transform;
    }

    public void InstantiateGreen()
    {
        GameObject obj = Instantiate(myPrefabGreen, new Vector3(-3.669f, 0.514f, 3.445f), Quaternion.identity);
        obj.transform.parent = GameObject.Find("BaseOrigin").transform;
    }
    public void InstantiateBlue()
    {
        GameObject obj = Instantiate(myPrefabBlue, new Vector3(-3.669f, 0.514f, 3.445f), Quaternion.identity);
        obj.transform.parent = GameObject.Find("BaseOrigin").transform;
    }
}
