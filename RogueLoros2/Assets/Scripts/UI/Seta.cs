using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seta : MonoBehaviour
{
    public GameObject Target;

    RectTransform rt;

    GameObject player;

    void Start()
    {
        rt = GetComponent<RectTransform>();

        Target = GameObject.FindGameObjectWithTag("Goal");

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Target != null)
        {
            transform.LookAt(Target.transform.position);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}