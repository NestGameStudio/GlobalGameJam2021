using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getVida : MonoBehaviour
{
    //public int money = 10;
    public GameObject pickParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("got vida");
            hpSystem.instance.maisVida();
            //if (pickParticle != null) Instantiate(pickParticle, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
