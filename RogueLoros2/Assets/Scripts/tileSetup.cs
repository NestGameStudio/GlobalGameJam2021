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

//[ExecuteInEditMode]
public class tileSetup : MonoBehaviour
{
    public GameObject tileGraphics;

    public Texture2D[] tileTextures;

    public tileType tipoTileAtual;

    public Transform[] spawnPoints;

    public visualizeFutureTile[] tileMarkers;

    public bool assignOnPlay = false;

    public bool firstTile = false;
    // Start is called before the first frame update
    void Start()
    {
        //dar o numero de cada spawnpoint pros marcadores para detectar onde nao existe conexao
        assignMarkerNumbers();

        if (assignOnPlay)
        {
            //tileType type = (tileType)Random.Range(0, System.Enum.GetValues(typeof(tileType)).Length);
            tileType type = tileType.FourSides;
            updateTile(type);
        }

        if (firstTile)
        {
            tileType type = tileType.ThreeSides;
            updateTile(type);
            //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z);
            //RotateCounterClockwise(this);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.isPlaying == false){
            updateTile(tipoTileAtual);
        }


    }
    
    public void activateMarkers()
    {
        for(int x = 0; x < spawnPoints.Length; x++)
        {
            if (spawnPoints[x].GetComponent<checkSpawnPointConnection>().hasConnection == false)
            {
                spawnPoints[x].transform.GetChild(0).gameObject.SetActive(true);
            }
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

    public void updateTile(tileType tipoTile) {

        if (tipoTile != tipoTileAtual) {
            tipoTileAtual = tipoTile;
        }

        switch (tipoTile)
        {
            case tileType.OneSide:
                tileGraphics.GetComponent<MeshRenderer>().material.mainTexture = tileTextures[0];

                //deixar ativado apenas o primeiro spawnpoint
                for (int x = 0; x < spawnPoints.Length; x++)
                {
                    if(x == 0 || x == 2)
                    {
                        spawnPoints[x].GetComponent<checkSpawnPointConnection>().active = true;
                    }
                    else
                    {
                        spawnPoints[x].GetComponent<checkSpawnPointConnection>().active = false;
                    }
                }

                break;
            case tileType.OneSideB:
                tileGraphics.GetComponent<MeshRenderer>().material.mainTexture = tileTextures[1];

                //ativar spawnpoints correspondentes
                for (int x = 0; x < spawnPoints.Length; x++)
                {
                    if (x == 0 || x == 1)
                    {
                        spawnPoints[x].GetComponent<checkSpawnPointConnection>().active = true;
                    }
                    else
                    {
                        spawnPoints[x].GetComponent<checkSpawnPointConnection>().active = false;
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
                        spawnPoints[x].GetComponent<checkSpawnPointConnection>().active = true;
                    }
                    else
                    {
                        spawnPoints[x].GetComponent<checkSpawnPointConnection>().active = false;
                    }
                }

                break;
            case tileType.FourSides:
                tileGraphics.GetComponent<MeshRenderer>().material.mainTexture = tileTextures[3];

                //ativar spawnpoints correspondentes
                for (int x = 0; x < spawnPoints.Length; x++)
                {

                    spawnPoints[x].GetComponent<checkSpawnPointConnection>().active = true;

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

    private void RotateCounterClockwise(tileSetup thisTile)
    {

        thisTile.transform.Rotate(Vector3.up, -90);

        tileSetup tile = thisTile.GetComponent<tileSetup>();

        Transform auxSP = tile.spawnPoints[0];
        visualizeFutureTile auxVFT = tile.tileMarkers[0];
        for (int i = 0; i < tile.spawnPoints.Length; i++)
        {

            if (i == tile.spawnPoints.Length - 1)
            {
                tile.spawnPoints[i] = auxSP;
                tile.tileMarkers[i] = auxVFT;
            }
            else
            {
                tile.spawnPoints[i] = tile.spawnPoints[i + 1];
                tile.spawnPoints[i + 1] = null;
                tile.tileMarkers[i] = tile.tileMarkers[i + 1];
                tile.tileMarkers[i + 1] = null;
            }

        }

    }
}
