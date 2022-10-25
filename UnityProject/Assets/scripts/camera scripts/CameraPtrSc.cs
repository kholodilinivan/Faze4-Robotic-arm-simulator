using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class CameraPtrSc : MonoBehaviour
{
    public string cameraName;
    public Camera cam;
    public RawImage source;
    //If you need to pull the picture from the camera
    public RenderTexture cameraRender { get; private set; }

    [Header("Save to file")]
    public bool saveToFile;
    //If the build is il2cpp, it will not work to save to the disk where windows is installed
    public string folder;
    public string nameFile;

    public int Height = 980;
    public int Width = 980;
    byte[] encodedPng;
    bool screenshotDone;
    public int x, y;

    string CompleteFilePath;

    // Use this for initialization
    void Start()
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
            if (cam == null)
                cam = Camera.main;
        }

        cameraRender = new RenderTexture(Width + x, Height + y, 16);
        cam.targetTexture = cameraRender;
        source.texture = cameraRender;
        cam.aspect = 1;
    }

    private void OnEnable()
    {
        CameraController.AddCamera(this);
    }

    private void OnDisable()
    {
        CameraController.RemoveCamera(this);
    }

    private void OnDestroy()
    {
        CameraController.RemoveCamera(this);
    }

    public void CaptureImage()
    {
        screenshotDone = false;
        StartCoroutine(GetRender());
    }

    public bool CaptureDone()
    {
        return screenshotDone;
    }

    public byte[] GetPrtSc()
    {
        screenshotDone = false;
        return encodedPng;
    }

    public void CaptureImage(string FilePath, string Filename, string CameraSelect)
    {
        CompleteFilePath = FilePath + "/" + Filename;
        Debug.Log("File was Saved at:" + CompleteFilePath + ".png");
        StartCoroutine(GetRender());
    }

    IEnumerator GetRender()
    {
        yield return new WaitForEndOfFrame();
        Texture2D tempTex = new Texture2D(Width, Height, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = cameraRender;//Sets the Render
        tempTex.ReadPixels(new Rect(x, y, Width, Height), 0, 0);
        tempTex.Apply();
        //RenderTexture.active = null;
        encodedPng = tempTex.EncodeToPNG();
        Destroy(tempTex);
        if (saveToFile)
        {
            try
            {
                File.WriteAllBytes(CompleteFilePath + ".png", encodedPng);
                Debug.Log($"[File SAVE]{folder}.png");
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        //
        //
        yield return null;
        cameraRender.Release();
        cameraRender = new RenderTexture(Width + x, Height + y, 16);
        cam.targetTexture = cameraRender;
        source.texture = cameraRender;
        screenshotDone = true;
    }
}
