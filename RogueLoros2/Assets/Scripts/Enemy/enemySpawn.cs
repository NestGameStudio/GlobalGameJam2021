using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public GameObject inimigo;
    public int chance = 20;

    public void spawnInimigo()
    {
        int chanceCheck = Random.Range(0,101);
        if(chance >= chanceCheck)
        {
            //instanciar inimigo
            Instantiate(inimigo,transform.position,Quaternion.identity);
        }
    }
}
