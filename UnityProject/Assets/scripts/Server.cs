using System;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Net;
using System.IO;
using System.Globalization;


public class Server : MonoBehaviour {

    //General Init
    private List<ServerClient> clients;
    private List<int> disconnectIndex;

    public int Port;
    public TcpListener server;
    private bool serverStarted;

    Camera MainCamera;

    // Use this for initialization
    void Start () {
        clients = new List<ServerClient>();
        disconnectIndex = new List<int>();

        try {
            server = new TcpListener(IPAddress.Any, Port);
            server.Start();

            Startlistening();
            serverStarted = true;
            Debug.Log("Server has started on port:" + Port.ToString());
        }
        catch (Exception e) {
            Debug.Log("Socket Error " + e.Message);
        }

        InvokeRepeating("UpdateLoop", 0f, 0.003f);
    }

    public void UpdateLoop()
    {
        if(!serverStarted)
            return;
        if(clients.Count == 0)
            return;

        for(int c = 0; c < clients.Count; c++ ) {
            //Check if clients are connected
            if(!isConnected(clients[c].tcp)) {
                clients[c].tcp.Close();
                disconnectIndex.Add(c);
                Debug.Log(clients[c].clientName + " has disconnected from the server");
                continue;
            }
            // Check for data from client
            else {
                NetworkStream s = clients[c].tcp.GetStream();
                if(s.DataAvailable) {
                  //  StreamReader reader = new StreamReader(s, true);
                    byte[] DataLength = new byte[1];
                    s.Read(DataLength, 0, 1);
                    if (DataLength != null)
                    {
                        byte[] RecievedString = new byte[(int)DataLength[0]];
                        s.Read(RecievedString, 0, (int)DataLength[0]);
                        OnIncomingData(clients[c],RecievedString); //Handles incoming data
                    }
                    s.Flush();
                }               
            }
        }
        //Clean up Disconnected Clients
        for(int i = 0; i < disconnectIndex.Count; i++) {
            clients.RemoveAt(disconnectIndex[i]);
        }
        disconnectIndex.Clear();
    }

    //Checks if client is connected
    private bool isConnected(TcpClient c)
    {
        try {
            if(c != null && c.Client != null && c.Client.Connected) //Makes sure the client is connected
            {
                if(c.Client.Poll(0, SelectMode.SelectRead))         //Polls the Client for activity
                {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0); //Checks for response
                }
                
                return true;
            }
            else
                return false;
        }
        catch {
            return false;
        }
    }
    //Begins connection with client
    private void AcceptServerClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;
        ServerClient NewClient = new ServerClient(listener.EndAcceptTcpClient(ar), null);
        Debug.Log("Someone has connected");
        clients.Add(NewClient);
        Startlistening();
    }

    //Starts listening on server socket
    private void Startlistening()
    {
        server.BeginAcceptTcpClient(AcceptServerClient, server);
    }

    //Try to close all the connections gracefully
    public void OnApplicationQuit()
    {
        for (int i = 0; i < clients.Count; i++)
        {
            try
            {
                clients[i].tcp.GetStream().Close();
                clients[i].tcp.Close();
                server.Stop();
            }
            catch { }
        }
        Debug.Log("Connections Closed");
    }

    //Sends out data
    public void OutgoingData(ServerClient c, byte[] data) {
        NetworkStream ClientStream = c.tcp.GetStream();
        try
        {
        ClientStream.Write(data, 0, data.Length);
        }
        catch (Exception e) {
            Debug.LogError("Could not write to client.\n Error:" + e);
        }
    }

    //Handler for incoming data
    private void OnIncomingData(ServerClient c, byte [] ReceivedData)
    {
        string RecivedString = Encoding.UTF8.GetString(ReceivedData,0,ReceivedData.Length);
        char[] delimiterChar = { ':', ','};
        string[] cleanData = RecivedString.Split(delimiterChar);
        string Response;

        //Switch to handle debug messages and extra info between Matlab and Unity
        switch(cleanData[0])
        {
            case "ModRobot":
                ModifyRobot(float.Parse(cleanData[1], CultureInfo.InvariantCulture), float.Parse(cleanData[2], CultureInfo.InvariantCulture), float.Parse(cleanData[3], CultureInfo.InvariantCulture), float.Parse(cleanData[4], CultureInfo.InvariantCulture), float.Parse(cleanData[5], CultureInfo.InvariantCulture), float.Parse(cleanData[6], CultureInfo.InvariantCulture));
                break;
            case "ModGrab":
                ModifyGrab(int.Parse(cleanData[1], CultureInfo.InvariantCulture));
                break;
            case "NewObj":
                ModifyCube(int.Parse(cleanData[1], CultureInfo.InvariantCulture));
                break;
            default:
                break;
            case "Camera":
                Debug.Log(cleanData[2]);
                StartCoroutine(SendCamCapture(c, cleanData[2], cleanData[3], cleanData[1]));
                Response = cleanData[3] + ".png";
                break;
        }
    }

    private void ModifyRobot(float a1, float a2, float a3, float a4, float a5, float a6)
    {
        GameObject robot = GameObject.Find("Robot1");
        RobotTest robotIns = robot.GetComponent<RobotTest>();

        robotIns.J1Angle = a1;
        robotIns.J2Angle = a2;
        robotIns.J3Angle = a3;
        robotIns.J4Angle = a4;
        robotIns.J5Angle = a5;
        robotIns.J6Angle = a6;
    }

    private void ModifyGrab(int grab)
    {
        GameObject end_eff = GameObject.Find("AnimationEE");
        ColliderTest end_eff_grab = end_eff.GetComponent<ColliderTest>();

        // end_eff_grab.grab = grab;
        if (grab == 1)
        {
            end_eff_grab.GrabObj();
        }
        if (grab == 0)
        {
            end_eff_grab.ReleaseObj();
        }
    }
    private void ModifyCube(int init)
    {
        GameObject inst_cube_obj = GameObject.Find("CanvasMain");
        CubeInstantiate inst_cube = inst_cube_obj.GetComponent<CubeInstantiate>();
        if (init == 1)
        {
            inst_cube.InstantiateRed();
        }
        if (init == 2)
        {
            inst_cube.InstantiateGreen();
        }
        if (init == 3)
        {
            inst_cube.InstantiateBlue();
        }
    }
        //Organizes and Sends Picture
        IEnumerator SendCamCapture(ServerClient c, string FilePath, string FileName, string CameraSelect)
    {
        GameObject Cube = GameObject.Find("CameraPrtSc");
        CameraPtrSc CubeComp = Cube.GetComponent<CameraPtrSc>();
        CubeComp.CaptureImage(FilePath, FileName, CameraSelect);
        while (!CubeComp.CaptureDone())
        {
            yield return null;
        }
        StartCoroutine(SerializeCapture(c, CubeComp.GetPrtSc(), CubeComp.Width, CubeComp.Height));
    }

    IEnumerator SerializeCapture(ServerClient c, byte[] PixelData, int Width, int Length)
    {
        OutgoingData(c, PixelData);
        yield return null;
    }
}

public class ServerClient {

    public TcpClient tcp;
    public string clientName;
    public GameObject ClientObj;

    public ServerClient(TcpClient clientSocket, string Name) {
        clientName = Name;
        tcp = clientSocket;
    }
}