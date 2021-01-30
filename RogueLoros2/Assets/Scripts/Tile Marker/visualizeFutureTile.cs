using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualizeFutureTile : MonoBehaviour
{
    //public GameObject tile;
    //private GameObject newTile;

    bool detectClick = false;

    private void OnMouseEnter()
    {
        //GameObject tileCreated = Instantiate(tile, transform.position,Quaternion.identity);
        //tileCreated.GetComponent<tileSetup>().tipoTileAtual = GameManager.instance.tipoTileGrabbed;

        GameManager.instance.grabbedTile.SetActive(true);
        GameManager.instance.grabbedTile.transform.position = gameObject.transform.position;

        detectClick = true;

    }
    private void OnMouseExit()
    {
        GameManager.instance.grabbedTile.SetActive(false);
        //GameManager.instance.grabbedTile.transform.position = gameObject.transform.position;

        detectClick = false;
    }

    private void Update()
    {
        //quando clica em cima do quadrado da visualizacao
        if(detectClick && Input.GetMouseButtonDown(0))
        {
            GameManager.instance.colocarTile(gameObject.transform.position);
            detectClick = false;

            //destruir spawnpoint para nao poder ser usado depois
            Destroy(transform.parent.gameObject);
        }
    }
}
