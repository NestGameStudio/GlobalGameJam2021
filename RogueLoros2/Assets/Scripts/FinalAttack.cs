using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAttack : MonoBehaviour
{
    public GameObject pickParticle;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            print("pick key");
            if(pickParticle!=null) Instantiate(pickParticle,transform.position,Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
