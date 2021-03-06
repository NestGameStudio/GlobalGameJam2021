﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkSpawnPointConnection : MonoBehaviour
{
    public bool hasConnection = false;

    public tileSetup connectionTile;

    public bool hasTile = false;

    public tileSetup tile;

    public bool active = false;

    public GameObject marker;

    private void Start()
    {
        if (GetComponentInChildren<visualizeFutureTile>() != null)
        {
            marker = GetComponentInChildren<visualizeFutureTile>().gameObject;
        }
    }

    public void detectConnection()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "SpawnPoint" || hitCollider.gameObject.tag == "Tile" || hitCollider.gameObject.tag == "Money" || hitCollider.gameObject.tag == "Goal")
            {
                //gameObject.SetActive(false);
                if (hitCollider.gameObject.GetComponentInParent<tileSetup>() != GetComponentInParent<tileSetup>() && hitCollider.GetComponent<checkSpawnPointConnection>() != null)
                {
                    hasTile = true;
                    if (hitCollider.GetComponent<checkSpawnPointConnection>().active)
                    {
                        hasConnection = true;
                        connectionTile = hitCollider.gameObject.GetComponentInParent<tileSetup>();
                        
                        //gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        //gameObject.SetActive(false);
                    }
                    else
                    {
                        hasConnection = false;
                        connectionTile = null;
                        hasTile = true;
                    }
                }
                else if(hitCollider.gameObject == null)
                {
                    hasConnection = false;
                    hasTile = false;
                }

            } else {

                hasConnection = false;
                connectionTile = null;

            }
        }

        if (active == false || hasTile && hasConnection == false)
        {
            if (marker != null)
            {
                marker.SetActive(false);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (hasConnection && active)
        {
            Gizmos.color = Color.green;
            
        }
        else if(hasConnection == false && active)
        {
            Gizmos.color = Color.white;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }

}
