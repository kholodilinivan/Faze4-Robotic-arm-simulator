using System.Collections.Generic;
using UnityEngine;

public class CameraController
{
    private static CameraController instance;

    private static CameraController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CameraController();
            }
            return instance;
        }
    }

    public List<CameraPtrSc> cameras = new List<CameraPtrSc>();

    public static void AddCamera(CameraPtrSc cam)
    {
        Instance.cameras.Add(cam);
    }

    public static void RemoveCamera(CameraPtrSc cam)
    {
        Instance.cameras.Remove(cam);
    }

    public static CameraPtrSc CreatePrtSc(int index)
    {
        Instance.cameras[index].CaptureImage();
        return Instance.cameras[index];
    }
}
