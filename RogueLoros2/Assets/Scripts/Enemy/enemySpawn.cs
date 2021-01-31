using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public GameObject inimigo;

    public void spawnInimigo(GameObject tileObject, int chance)
    {
        int chanceCheck = Random.Range(0,101);
        if(chance >= chanceCheck)
        {
            //instanciar inimigo
            GameObject inimigoNovo = Instantiate(inimigo,transform.position,Quaternion.identity);
            inimigoNovo.transform.parent = tileObject.transform;
        }
    }
}
