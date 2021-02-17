using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTiles : MonoBehaviour
{
    void OnTriggerStay(Collider other){
        
        if(other.tag != "Player")
       {
           if (other.tag == "Tile" || other.tag == "Money" || other.tag == "Goal") {
                //Debug.Log("Death Fog entrou em um Trigger de tag " + other.tag,other.gameObject);
                Debug.Log("Death Fog pegou o seguinte tile: " + other.gameObject.name, other.gameObject);
                Destroy(other.gameObject);
           }
        }
       

     }
}
