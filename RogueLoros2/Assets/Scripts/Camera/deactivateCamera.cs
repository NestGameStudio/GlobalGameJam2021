using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class deactivateCamera : MonoBehaviour
{
    public float seconds = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(animInicio(seconds));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator animInicio(float sec)
    {
        yield return new WaitForSeconds(sec);
        GetComponent<CinemachineVirtualCamera>().enabled = false;
    }
}
