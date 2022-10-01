using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTest : MonoBehaviour
{
    public GameObject J1;
    public float J1Angle;
    public GameObject J2;
    public float J2Angle;
    public GameObject J3;
    public float J3Angle;
    public GameObject J4;
    public float J4Angle;
    public GameObject J5;
    public float J5Angle;
    public GameObject J6;
    public float J6Angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        J1.transform.localRotation = Quaternion.AngleAxis(-J1Angle, new Vector3(0, 1, 0));
        J2.transform.localRotation = Quaternion.AngleAxis(-J2Angle, new Vector3(1, 0, 0));
        J3.transform.localRotation = Quaternion.AngleAxis(J3Angle, new Vector3(1, 0, 0));
        J4.transform.localRotation = Quaternion.AngleAxis(J4Angle, new Vector3(0, 0, 1));
        J5.transform.localRotation = Quaternion.AngleAxis(J5Angle, new Vector3(1, 0, 0));
        J6.transform.localRotation = Quaternion.AngleAxis(-J6Angle, new Vector3(0, 1, 0));
    }
}
