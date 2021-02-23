using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileHighlighter : MonoBehaviour
{

    float t = 0;
    bool tUp = true;

    void OnTriggerStay(Collider other)
    {

        if (other.tag != "Player")
        {
            if (other.tag == "Tile" || other.tag == "Money" || other.tag == "Goal")
            {
                //Debug.Log("Death Fog entrou em um Trigger de tag " + other.tag,other.gameObject);
                //Debug.Log("Death Fog pegou o seguinte tile: " + other.gameObject.name, other.gameObject);
                if (other.GetComponentInChildren<MeshRenderer>() != null)
                {

                    //other.GetComponentInChildren<MeshRenderer>().sharedMaterial.color = Color.red;

                    other.GetComponentInChildren<MeshRenderer>().sharedMaterial.color = new Color(1, Mathf.Lerp(1, 0.3f, t), Mathf.Lerp(1, 0.3f, t));

                    

                    //LeanTween.color(other.GetComponentInChildren<MeshRenderer>().gameObject, Color.red, 1).setLoopPingPong();
                }
            }
        }


    }
    private void Update()
    {
        if (t >= 1)
        {
            tUp = false;
        }
        else if (t <= 0)
        {
            tUp = true;
        }

        if (tUp)
        {
            t += Time.deltaTime;
        }
        else
        {
            t -= Time.deltaTime;
        }
    }
}
