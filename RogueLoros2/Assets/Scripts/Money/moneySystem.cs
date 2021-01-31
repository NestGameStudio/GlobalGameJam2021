using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneySystem : MonoBehaviour
{
    public static moneySystem instance {get; private set;}

    public int money = 0;
    public Text moneyText;

    private void Awake()
    {
        //lida com duplicatas de instancia
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

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
        moneyText.text = money.ToString();

        GameManager.instance.checarItensCompraveis();
    }
    public void addMoney(int quantity)
    {
        if (money < 100)
        {
            if (quantity + money >= 99)
            {
                money = 99;
            }
            else
            {
                money += quantity;
                atualizarText();
            }
        }
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
