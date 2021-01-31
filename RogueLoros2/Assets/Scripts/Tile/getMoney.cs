using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getMoney : MonoBehaviour
{
    public int money = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("got money");
            moneySystem.instance.addMoney(money);
            gameObject.SetActive(false);
        }
    }
}
