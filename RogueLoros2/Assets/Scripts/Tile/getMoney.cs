using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getMoney : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("got money");
        moneySystem.instance.addMoney(10);
        gameObject.SetActive(false);
    }
}
