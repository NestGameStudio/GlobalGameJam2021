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

            audioManager.instance.enemyAttackAudio();

            CameraShake.instance.shakeCam(2, 1, 0.5f);
        }
    }
}
