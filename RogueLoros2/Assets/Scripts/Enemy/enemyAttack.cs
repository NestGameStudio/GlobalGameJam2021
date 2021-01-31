using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    public GameObject pickParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("tomou dano");
            if (pickParticle != null) Instantiate(pickParticle, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            hpSystem.instance.menosVida();
            moneySystem.instance.addMoney(3);
        }
    }
}
