using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        print("is on final attack");
        GameManager.instance.startFinalAttack = true;
    }
}
