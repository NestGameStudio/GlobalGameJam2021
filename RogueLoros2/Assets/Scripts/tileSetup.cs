using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tileType
{
    FourSides,
    ThreeSides,
    OneSideB,
    OneSide
}

[ExecuteInEditMode]
public class tileSetup : MonoBehaviour
{
    public GameObject tileGraphics;

    public Texture2D[] tileTextures;

    public tileType tipoTileAtual;

    public Transform[] spawnPoints;

    public visualizeFutureTile[] tileMarkers;

    // Start is called before the first frame update
    void Start()
    {
        //dar o numero de cada spawnpoint pros marcadores para detectar onde nao existe conexao
        assignMarkerNumbers();

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.isPlaying == false)
        {
            updateTile(tipoTileAtual);
        }
    }
    
    public void activateMarkers()
    {
        for(int x = 0; x < spawnPoints.Length; x++)
        {
            spawnPoints[x].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void deactivateMarkers()
    {
        for (int x = 0; x < spawnPoints.Length; x++)
        {
            spawnPoints[x].transform.GetChild(0).gameObject.SetActive(false);
        }
        //checkConnections();
    }

    public void updateTile(tileType tipoTile)
    {

        tipoTileAtual = tipoTile;

        switch (tipoTile)
        {
            case tileType.OneSide:
                tileGraphics.GetComponent<MeshRenderer>().material.mainTexture = tileTextures[0];

                //deixar ativado apenas o primeiro spawnpoint
                for (int x = 0; x < spawnPoints.Length; x++)
                {
                    if(x == 0 || x == 1)
                    {
                        spawnPoints[x].gameObject.SetActive(true);
                    }
                    else
                    {
                        spawnPoints[x].gameObject.SetActive(false);
                    }
                }

                break;
            case tileType.OneSideB:
                tileGraphics.GetComponent<MeshRenderer>().material.mainTexture = tileTextures[1];

                //ativar spawnpoints correspondentes
                for (int x = 0; x < spawnPoints.Length; x++)
                {
                    if (x == 0 || x == 2)
                    {
                        spawnPoints[x].gameObject.SetActive(true);
                    }
                    else
                    {
                        spawnPoints[x].gameObject.SetActive(false);
                    }
                }

                break;
            case tileType.ThreeSides:
                tileGraphics.GetComponent<MeshRenderer>().material.mainTexture = tileTextures[2];

                //ativar spawnpoints correspondentes
                for (int x = 0; x < spawnPoints.Length; x++)
                {
                    if (x == 0 || x == 1 || x == 2)
                    {
                        spawnPoints[x].gameObject.SetActive(true);
                    }
                    else
                    {
                        spawnPoints[x].gameObject.SetActive(false);
                    }
                }

                break;
            case tileType.FourSides:
                tileGraphics.GetComponent<MeshRenderer>().material.mainTexture = tileTextures[3];

                //ativar spawnpoints correspondentes
                for (int x = 0; x < spawnPoints.Length; x++)
                {
                    
                    spawnPoints[x].gameObject.SetActive(true);
                    
                }

                break;
        }
    }
    void assignMarkerNumbers()
    {
        for (int x = 0; x < tileMarkers.Length; x++)
        {
            tileMarkers[x].spawnPointNumber = x;
        }
    }

    public void checkConnections()
    {
        for(int x = 0; x < spawnPoints.Length; x++)
        {
            spawnPoints[x].GetComponent<checkSpawnPointConnection>().detectConnection();
        }
    }
}
