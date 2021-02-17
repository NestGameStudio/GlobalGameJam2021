using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualizeFutureTile: MonoBehaviour {
    //public GameObject tile;
    //private GameObject newTile;

    public int spawnPointNumber = 0;

    bool detectClick = false;

    bool isConnection = false;

    private void OnMouseEnter() {
        //GameObject tileCreated = Instantiate(tile, transform.position,Quaternion.identity);
        //tileCreated.GetComponent<tileSetup>().tipoTileAtual = GameManager.instance.tipoTileGrabbed;
        Debug.Log("mouseEnter marker");

        GameManager.instance.grabbedTile.SetActive(true);
        GameManager.instance.grabbedTile.transform.position = gameObject.transform.position;

        detectClick = true;

        //verifica se ha conexoes disponiveis
        detectConnection();

    }
    private void OnMouseExit() {
        GameManager.instance.grabbedTile.SetActive(false);
        //GameManager.instance.grabbedTile.transform.position = gameObject.transform.position;

        detectClick = false;
    }

    private void Update() {


        //quando clica em cima do quadrado da visualizacao
        if (detectClick && Input.GetMouseButtonDown(0) && isConnection) {
            //o tile novo vai aparecer dentro do spawnpoint
            GameManager.instance.colocarTile(gameObject.transform.position, transform.parent.parent.parent);
            detectClick = false;

            //destruir spawnpoint para nao poder ser usado depois
            Destroy(transform.parent.gameObject);
        }
        else if(Input.GetMouseButtonDown(0) && detectClick && isConnection ==false)
        {
            audioManager.instance.audioBuildFail();
        }



    }
    void normalColor() {
        //fica branco caso haja conexao
        GameManager.instance.grabbedTile.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
    }
    void denialColor() {
        //fica vermelho caso nao haja conexao
        GameManager.instance.grabbedTile.GetComponentInChildren<MeshRenderer>().material.color = Color.red;


    }

    void detectConnection() {
        switch (spawnPointNumber) {
            case 0:

                //Debug.Log("primeiro spawnpoint", GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[2].gameObject);

                //caso tenha conexao a partir do spawnpoint de baixo
                if (GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[2].GetComponent<checkSpawnPointConnection>().active && GetComponentInParent<checkSpawnPointConnection>().hasConnection == false && transform.parent.parent.parent.GetComponent<checkSpawnPointConnection>().hasTile == false && transform.parent.parent.parent.GetComponent<checkSpawnPointConnection>().active) {
                    isConnection = true;
                    normalColor();
                } else {
                    isConnection = false;
                    denialColor();
                }

                break;

            case 1:

                //Debug.Log("segundo spawnpoint", GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[3].gameObject);

                //caso tenha conexao a partir do spawnpoint de baixo
                if (GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[3].GetComponent<checkSpawnPointConnection>().active && GetComponentInParent<checkSpawnPointConnection>().hasConnection == false && transform.parent.parent.parent.GetComponent<checkSpawnPointConnection>().hasTile == false && transform.parent.parent.parent.GetComponent<checkSpawnPointConnection>().active) {
                    isConnection = true;
                    normalColor();
                } else {
                    isConnection = false;
                    denialColor();
                }

                break;

            case 2:

                //Debug.Log("terceiro spawnpoint", GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[0].gameObject);

                //caso tenha conexao a partir do spawnpoint de baixo
                if (GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[0].GetComponent<checkSpawnPointConnection>().active && GetComponentInParent<checkSpawnPointConnection>().hasConnection == false && transform.parent.parent.parent.GetComponent<checkSpawnPointConnection>().hasTile == false && transform.parent.parent.parent.GetComponent<checkSpawnPointConnection>().active) {
                    isConnection = true;
                    normalColor();
                } else {
                    isConnection = false;
                    denialColor();
                }

                break;

            case 3:

                //Debug.Log("quarto spawnpoint", GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[1].gameObject);

                //caso tenha conexao a partir do spawnpoint de baixo
                if (GameManager.instance.grabbedTile.GetComponent<tileSetup>().spawnPoints[1].GetComponent<checkSpawnPointConnection>().active && GetComponentInParent<checkSpawnPointConnection>().hasConnection == false && transform.parent.parent.parent.GetComponent<checkSpawnPointConnection>().hasTile == false && transform.parent.parent.parent.GetComponent<checkSpawnPointConnection>().active) {
                    isConnection = true;
                    normalColor();
                } else {
                    isConnection = false;
                    denialColor();
                }

                break;
        }
    }
}
