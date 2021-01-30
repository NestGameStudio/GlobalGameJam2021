using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualizeFutureTile : MonoBehaviour
{
    //public GameObject tile;
    //private GameObject newTile;

    private void OnMouseEnter()
    {
        //GameObject tileCreated = Instantiate(tile, transform.position,Quaternion.identity);
        //tileCreated.GetComponent<tileSetup>().tipoTileAtual = GameManager.instance.tipoTileGrabbed;

        GameManager.instance.grabbedTile.SetActive(true);
        GameManager.instance.grabbedTile.transform.position = gameObject.transform.position;
    }
    private void OnMouseExit()
    {
        GameManager.instance.grabbedTile.SetActive(false);
        //GameManager.instance.grabbedTile.transform.position = gameObject.transform.position;
    }
}
