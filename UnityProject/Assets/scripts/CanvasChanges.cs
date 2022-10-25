using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class CanvasChanges : MonoBehaviour
{
    // Cubes Config
    public GameObject RedCube, GreenCube, BlueCube;
    public InputField redcube_pos_x, redcube_pos_y, redcube_pos_z, redcube_rot_x, redcube_rot_y, redcube_rot_z,
    greencube_pos_x, greencube_pos_y, greencube_pos_z, greencube_rot_x, greencube_rot_y, greencube_rot_z,
    bluecube_pos_x, bluecube_pos_y, bluecube_pos_z, bluecube_rot_x, bluecube_rot_y, bluecube_rot_z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Red Cube
        redcube_pos_x.text = (-RedCube.transform.localPosition.x).ToString();
        redcube_pos_y.text = (RedCube.transform.localPosition.z).ToString();
        redcube_pos_z.text = (RedCube.transform.localPosition.y).ToString();

        redcube_rot_x.text = (RedCube.transform.localRotation.x).ToString();
        redcube_rot_y.text = (RedCube.transform.localRotation.z).ToString();
        redcube_rot_z.text = (RedCube.transform.localRotation.y).ToString();

        // Green Cube
        greencube_pos_x.text = (-GreenCube.transform.localPosition.x).ToString();
        greencube_pos_y.text = (GreenCube.transform.localPosition.z).ToString();
        greencube_pos_z.text = (GreenCube.transform.localPosition.y).ToString();

        greencube_rot_x.text = (GreenCube.transform.localRotation.x).ToString();
        greencube_rot_y.text = (GreenCube.transform.localRotation.z).ToString();
        greencube_rot_z.text = (GreenCube.transform.localRotation.y).ToString();

        // Blue Cube
        bluecube_pos_x.text = (-BlueCube.transform.localPosition.x).ToString();
        bluecube_pos_y.text = (BlueCube.transform.localPosition.z).ToString();
        bluecube_pos_z.text = (BlueCube.transform.localPosition.y).ToString();

        bluecube_rot_x.text = (BlueCube.transform.localRotation.x).ToString();
        bluecube_rot_y.text = (BlueCube.transform.localRotation.z).ToString();
        bluecube_rot_z.text = (BlueCube.transform.localRotation.y).ToString();
    }
    
    // Obstacles Config
    public void RedCubeToogle(bool newValue)
    {
        RedCube.SetActive(newValue);
    }
    public void GreenCubeToogle(bool newValue)
    {
        GreenCube.SetActive(newValue);
    }
    public void BlueCubeToogle(bool newValue)
    {
        BlueCube.SetActive(newValue);
    }

    // Red cube position
    public void Reset_RedCubeXYZ()
    {
        redcube_pos_x.text = "0.414";
        redcube_pos_y.text = "-0.196";
        redcube_pos_z.text = "0.1193";
        RedCube.transform.localPosition = new Vector3(-float.Parse(redcube_pos_x.text, CultureInfo.InvariantCulture), float.Parse(redcube_pos_z.text, CultureInfo.InvariantCulture), -float.Parse(redcube_pos_y.text, CultureInfo.InvariantCulture));
    }
    // Red cube Rotation
    public void Reset_RedCubeXYZ_rot()
    {
        redcube_rot_x.text = "0";
        redcube_rot_y.text = "0";
        redcube_rot_z.text = "0";
        RedCube.transform.localRotation = Quaternion.Euler(float.Parse(redcube_rot_x.text, CultureInfo.InvariantCulture), float.Parse(redcube_rot_z.text, CultureInfo.InvariantCulture), float.Parse(redcube_rot_y.text, CultureInfo.InvariantCulture));
    }

    // Green cube position
    public void Reset_GreenCubeXYZ()
    {
        greencube_pos_x.text = "0.414";
        greencube_pos_y.text = "-0.196";
        greencube_pos_z.text = "0.1993";
        GreenCube.transform.localPosition = new Vector3(-float.Parse(greencube_pos_x.text, CultureInfo.InvariantCulture), float.Parse(greencube_pos_z.text, CultureInfo.InvariantCulture), -float.Parse(greencube_pos_y.text, CultureInfo.InvariantCulture));
    }
    // Green cube Rotation
    public void Reset_GreenCubeXYZ_rot()
    {
        greencube_rot_x.text = "0";
        greencube_rot_y.text = "0";
        greencube_rot_z.text = "0";
        GreenCube.transform.localRotation = Quaternion.Euler(float.Parse(greencube_rot_x.text, CultureInfo.InvariantCulture), float.Parse(greencube_rot_z.text, CultureInfo.InvariantCulture), float.Parse(greencube_rot_y.text, CultureInfo.InvariantCulture));
    }

    // BLue cube position
    public void Reset_BlueCubeXYZ()
    {
        bluecube_pos_x.text = "0.414";
        bluecube_pos_y.text = "-0.196";
        bluecube_pos_z.text = "0.039";
        BlueCube.transform.localPosition = new Vector3(-float.Parse(bluecube_pos_x.text, CultureInfo.InvariantCulture), float.Parse(bluecube_pos_z.text, CultureInfo.InvariantCulture), -float.Parse(bluecube_pos_y.text, CultureInfo.InvariantCulture));
    }
    // Blue cube Rotation
    public void Reset_BlueCubeXYZ_rot()
    {
        bluecube_rot_x.text = "0";
        bluecube_rot_y.text = "0";
        bluecube_rot_z.text = "0";
        BlueCube.transform.localRotation = Quaternion.Euler(float.Parse(bluecube_rot_x.text, CultureInfo.InvariantCulture), float.Parse(bluecube_rot_z.text, CultureInfo.InvariantCulture), float.Parse(bluecube_rot_y.text, CultureInfo.InvariantCulture));
    }
}
