using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour
{

    [SyncVar]
    private Vector3 SyncPos;

    [SyncVar]
    private Quaternion SyncRot;

    public Transform myTransform;
    public float lerpRate = 15;
    public Transform fps;
    

    Vector3 lastPos;
    Quaternion lastRot;


    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {

            SpawnPosition sp = GameObject.Find("SpawnPoints").GetComponent<SpawnPosition>();

            int ran = Random.Range(0, sp.spawnPos.Length - 1);

            myTransform.position = sp.spawnPos[ran].position;
            GetComponent<Player>().enabled = true;
            GetComponent<Controller2D>().enabled = true;
           	
        
        }
    }


    void Update()
    {
        UpdateTransform();
    }

    void FixedUpdate()
    {

        TransmitTransform();
    }

    void UpdateTransform()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, SyncPos, Time.deltaTime * lerpRate);
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, SyncRot, Time.deltaTime * lerpRate);

        }
    }

    [Command]
    void Cmd_PassPosition(Vector3 pos)
    {

        SyncPos = pos;
    }

    [Command]
    void Cmd_PassRotation(Quaternion rot)
    {
        SyncRot = rot;
    }

    [ClientCallback]
    void TransmitTransform()
    {
        if (isLocalPlayer)
        {

            float distance = Vector3.Distance(myTransform.position, lastPos);

            if (distance > .5f)
            {
                Cmd_PassPosition(myTransform.position);
                lastPos = myTransform.position;
            }

            float angle = Quaternion.Angle(myTransform.rotation, lastRot);
            if (angle > .5f)
            {
                Cmd_PassRotation(myTransform.rotation);
                lastRot = myTransform.rotation;
            }

        }
    }




}