using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //setar para acompanhar o player no inicio
        GetComponent<CinemachineVirtualCamera>().m_Follow = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
