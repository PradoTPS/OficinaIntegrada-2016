using UnityEngine;
using System.Collections;

public class SpawnPosition : MonoBehaviour
{


    public Transform[] spawnPos;

    // Use this for initialization
    void Start()
    {
        spawnPos = GetComponentsInChildren<Transform>();
    }

}