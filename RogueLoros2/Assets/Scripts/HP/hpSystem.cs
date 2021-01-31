using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpSystem : MonoBehaviour
{
    public static hpSystem instance { get; private set; }

    //max life = 3
    public int health = 3;
    public GameObject[] HPicons;
    public Sprite hpIconOn;
    public Sprite hpIconOff;

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
        updateLifeCounter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //mais vida
            maisVida();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            //menos vida
            menosVida();
        }
    }
    void updateLifeCounter()
    {
        for (int i = 0; i < HPicons.Length; i++)
        {
            //caso a vida esteja preenchida
            if(i < health)
            {
                HPicons[i].GetComponent<Image>().sprite = hpIconOn;
            }
            else
            {
                HPicons[i].GetComponent<Image>().sprite = hpIconOff;
            }
        }
    }
    public void maisVida()
    {
        if (health <= 2)
        {
            health += 1;
            updateLifeCounter();
        }
    }
    public void menosVida()
    {
        if (health > 0)
        {
            health -= 1;
            updateLifeCounter();
        }
        if (health <= 0)
        {
            //morreu
            health -= 1;
            updateLifeCounter();
            Debug.Log("morreu");

            SceneController.instance.morte();
        }
    }
}
