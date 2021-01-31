using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneySystem : MonoBehaviour
{
    public int money = 0;
    public Text moneyText;
    // Start is called before the first frame update
    void Start()
    {
        atualizarText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //mais 2 de dinheiro
            addMoney(2);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //menos 5 de dinheiro
            removeMoney(5);
        }
    }
    void atualizarText()
    {
        moneyText.text = "$ " + money.ToString();
    }
    public void addMoney(int quantity)
    {
        money += quantity;
        atualizarText();
    }
    public void removeMoney(int quantity)
    {
        if (money > 0) 
        {
            
            if(quantity > money)
            {
                money = 0;
            }
            else
            {
                money -= quantity;
            }

            atualizarText();
        }
    }
}
