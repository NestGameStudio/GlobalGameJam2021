using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileRandomizer : MonoBehaviour {

    public GameObject Tile;
    public GameObject UITilePanel;

    //public int numeroMaxTiles = 4;
    private void Awake()
    {
        //instanciar tilePreviews dentro do tilepanel
        //GameManager.instance.instanciarTilePreview();
    }

    private void Start()
    {
        //RandomizeAllTiles();
    }

    public void RandomizeAllTiles() {


        for (int i = 0; i < UITilePanel.transform.childCount; i++) {

            int randRotation = Random.Range(0, 4);
            RandomizeSingleTiles(i, randRotation);
        }

        audioManager.instance.refreshTilesAudio();
       
    }

    // os 4 espaços da UI são númerados de 0 a 3 da esquerda para a direita
    // o valor recebdo pela função é relacionado ao número do slot de tile na UI
    public void RandomizeSingleTiles(int tileNum, int randomRotation) {

        //tileType type = (tileType) Random.Range(0, System.Enum.GetValues(typeof(tileType)).Length - 1);

        //essa funcao atualiza o tile do mapa tambem, entao ela nao pode ser chamada para atualizar os tiles do preview
        //Tile.GetComponent<tileSetup>().updateTile(type);

        Button UIButton = UITilePanel.transform.GetChild(tileNum).GetComponentInChildren<Button>();

        GameManager.instance.nextTurnButton.interactable = false;

        if (UIButton.GetComponentInParent<tilePreview_Properties>() != null)
        {
            UIButton.GetComponentInParent<tilePreview_Properties>().randomizePreview(randomRotation);
        }
        else
        {
            Debug.LogError("Tile randomizado não foi encontrado",UIButton);
        }
        
    }
}
