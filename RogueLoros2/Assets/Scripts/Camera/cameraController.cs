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
        GetComponent<CinemachineVirtualCamera>().m_LookAt = player.transform;

        //StartCoroutine(animInicio());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    IEnumerator animInicio()
    {
        
        GameObject goal = GameObject.FindGameObjectWithTag("Goal");

        //setar para acompanhar o player no inicio
        GetComponent<CinemachineVirtualCamera>().m_Follow = goal.transform;
        GetComponent<CinemachineVirtualCamera>().m_LookAt = goal.transform;

        yield return new WaitForSeconds(3);

        
    }
    */
}
