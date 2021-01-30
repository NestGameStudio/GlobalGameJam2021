﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isMovementLegal(1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isMovementLegal(3);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isMovementLegal(4);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isMovementLegal(2);
        }
    }
    //1 - cima
    //2 - baixo
    //3 - direita 
    //4 - esquerda
    void isMovementLegal(int direction)
    {
        switch (direction)
        {
            case 1:

                //checar se no tile ativo, dentro do spawnpoint acessado tem um objeto tile
                tileSetup tileAtivo = GameManager.instance.activeTile.GetComponent<tileSetup>();

                //NAO FAZER MAIS CHECANDO FILHO, FAZER CHECANDO SPAWNPOINTS ATIVOS
                
                if (tileAtivo.spawnPoints[0].GetComponent<checkSpawnPointConnection>().connectionTile != tileAtivo && tileAtivo.spawnPoints[0].GetComponent<checkSpawnPointConnection>().hasConnection)
                {
                    //executa o movimento
                    GameManager.instance.movePlayer(tileAtivo.spawnPoints[0].GetComponent<checkSpawnPointConnection>().connectionTile);

                }
                else
                {
                    debugCantWalk();
                }

                break;
            case 2:
                
                //checar se no tile ativo, dentro do spawnpoint acessado tem um objeto tile
                tileAtivo = GameManager.instance.activeTile.GetComponent<tileSetup>();
                

                if (tileAtivo.spawnPoints[1].GetComponent<checkSpawnPointConnection>().connectionTile != tileAtivo && tileAtivo.spawnPoints[1].GetComponent<checkSpawnPointConnection>().hasConnection)
                {
                    //executa o movimento
                    GameManager.instance.movePlayer(tileAtivo.spawnPoints[1].GetComponent<checkSpawnPointConnection>().connectionTile);

                }
                else
                {
                    debugCantWalk();
                }

                break;
            case 3:

                //checar se no tile ativo, dentro do spawnpoint acessado tem um objeto tile
                tileAtivo = GameManager.instance.activeTile.GetComponent<tileSetup>();


                if (tileAtivo.spawnPoints[2].GetComponent<checkSpawnPointConnection>().connectionTile != tileAtivo && tileAtivo.spawnPoints[2].GetComponent<checkSpawnPointConnection>().hasConnection)
                {
                    //executa o movimento
                    GameManager.instance.movePlayer(tileAtivo.spawnPoints[2].GetComponent<checkSpawnPointConnection>().connectionTile);

                }
                else
                {
                    debugCantWalk();
                }

                break;
            case 4:

                //checar se no tile ativo, dentro do spawnpoint acessado tem um objeto tile
                tileAtivo = GameManager.instance.activeTile.GetComponent<tileSetup>();


                if (tileAtivo.spawnPoints[3].GetComponent<checkSpawnPointConnection>().connectionTile != tileAtivo && tileAtivo.spawnPoints[3].GetComponent<checkSpawnPointConnection>().hasConnection)
                {
                    //executa o movimento
                    GameManager.instance.movePlayer(tileAtivo.spawnPoints[3].GetComponent<checkSpawnPointConnection>().connectionTile);

                }
                else
                {
                    debugCantWalk();
                }

                break;
        }
    }

    void debugCantWalk()
    {
        Debug.Log("Nao e possivel andar para essa direcao");
    }
}
