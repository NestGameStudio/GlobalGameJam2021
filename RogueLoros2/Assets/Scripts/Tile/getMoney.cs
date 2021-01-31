using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getMoney : MonoBehaviour
{
    public int money = 10;
    public GameObject pickParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("got money");
            moneySystem.instance.addMoney(money);
            if(pickParticle!=null) Instantiate(pickParticle,transform.position,Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
