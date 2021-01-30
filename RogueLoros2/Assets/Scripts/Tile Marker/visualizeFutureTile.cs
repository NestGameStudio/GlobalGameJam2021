using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualizeFutureTile : MonoBehaviour
{
    //public GameObject tile;
    //private GameObject newTile;

    public int spawnPointNumber = 0;

    bool detectClick = false;

    bool isConnection = false;

    private void OnMouseEnter()
    {
        //GameObject tileCreated = Instantiate(tile, transform.position,Quaternion.identity);
        //tileCreated.GetComponent<tileSetup>().tipoTileAtual = GameManager.instance.tipoTileGrabbed;

        GameManager.instance.grabbedTile.SetActive(true);
        GameManager.instance.grabbedTile.transform.position = gameObject.transform.position;

        detectClick = true;

        //verifica se ha conexoes disponiveis
        detectConnection();

        if (isConnection)
        {
            //fica vermelho caso nao haja conexao
            GameManager.instance.grabbedTile.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        }
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
        if (detectClick && Input.GetMouseButtonDown(0) && isConnection)
        {
            GameManager.instance.colocarTile(gameObject.transform.position);
            detectClick = false;

            //destruir spawnpoint para nao poder ser usado depois
            Destroy(transform.parent.gameObject);
        }
        else
        {
            //fica vermelho caso nao haja conexao
            GameManager.instance.grabbedTile.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }

        
    }

    void detectConnection()
    {
        switch (spawnPointNumber)
        {
            case 0:

                Debug.Log("primeiro spawnpoint", GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[1].gameObject);

                //caso tenha conexao a partir do spawnpoint de baixo
                if (GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[1].gameObject.active)
                {
                    isConnection = true;
                }

                break;

            case 1:

                Debug.Log("segundo spawnpoint", GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[0].gameObject);

                //caso tenha conexao a partir do spawnpoint de baixo
                if (GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[0].gameObject.active)
                {
                    isConnection = true;
                }

                break;

            case 2:

                Debug.Log("terceiro spawnpoint", GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[3].gameObject);

                //caso tenha conexao a partir do spawnpoint de baixo
                if (GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[3].gameObject.active)
                {
                    isConnection = true;
                }

                break;

            case 3:

                Debug.Log("quarto spawnpoint", GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[2].gameObject);

                //caso tenha conexao a partir do spawnpoint de baixo
                if (GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[2].gameObject.active)
                {
                    isConnection = true;
                }

                break;
        }
    }
}
