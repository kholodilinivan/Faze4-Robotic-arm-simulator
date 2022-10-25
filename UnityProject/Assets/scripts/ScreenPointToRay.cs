using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenPointToRay : MonoBehaviour
{
    public GameObject baseOrig;
    public InputField click_pos_x, click_pos_y, click_pos_z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                click_pos_x.text = (-(hit.point.x - baseOrig.transform.position.x)).ToString();
                click_pos_y.text = (hit.point.z - baseOrig.transform.position.z).ToString();
                click_pos_z.text = (hit.point.y - baseOrig.transform.position.y).ToString();
            }
        }
    }
}
