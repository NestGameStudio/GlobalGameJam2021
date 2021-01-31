using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkSpawnPointConnection : MonoBehaviour
{
    public bool hasConnection = false;

    public tileSetup connectionTile;

    public void detectConnection()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "SpawnPoint")
            {
                //gameObject.SetActive(false);
                if (hitCollider.gameObject != gameObject)
                {
                    hasConnection = true;
                    connectionTile = hitCollider.gameObject.GetComponentInParent<tileSetup>();
                    //gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    //gameObject.SetActive(false);
                }
                else if(hitCollider.gameObject == null)
                {
                    hasConnection = false;
                }
            } else {

                hasConnection = false;
                connectionTile = null;

            }
        }
    }
    private void OnDrawGizmos()
    {
        if (hasConnection)
        {
            Gizmos.color = Color.green;
            
        }
        else
        {
            Gizmos.color = Color.white;
        }

        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }

}
