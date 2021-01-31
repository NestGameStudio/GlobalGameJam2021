﻿using System.Collections;
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
        /*
        // Get the position of the object in screen space
        Vector3 objScreenPos = Camera.main.WorldToScreenPoint(Target.transform.position);

        // Get the directional vector between your arrow and the object
        Vector3 dir = (objScreenPos - rt.position).normalized;

        // Calculate the angle 
        // We assume the default arrow position at 0° is "up"
        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir, Vector3.up));

        // Use the cross product to determine if the angle is clockwise
        // or anticlockwise
        Vector3 cross = Vector3.Cross(dir, Vector3.up);
        angle = -Mathf.Sign(cross.z) * angle;

        // Update the rotation of your arrow
        rt.localEulerAngles = new Vector3(rt.localEulerAngles.x, rt.localEulerAngles.y, angle);
        */

        Vector3 dir = transform.TransformDirection(Target.transform.position - player.transform.position);
        float angle = Mathf.Atan2(-dir.x, dir.z) * Mathf.Rad2Deg;
        rt.eulerAngles = new Vector3(0, 0, angle);
    }
}