using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraController : MonoBehaviour
{
    public static cameraController instance { get; private set; }

    private void Awake()
    {
        //lida com duplicatas de instancia
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

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
    public void changeOrientation()
    {
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.y = -15.3f;
    }
    
}
