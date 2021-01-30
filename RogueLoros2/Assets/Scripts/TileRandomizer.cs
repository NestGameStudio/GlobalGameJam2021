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

                RandomizeSingleTiles(i);
        }
        /*
        Debug.Log("UITilePanel childCount: " + UITilePanel.transform.childCount);

        for (int i = 0; i < UITilePanel.transform.childCount; i++)
        {

            if (UITilePanel.transform.childCount > numeroMaxTiles)
            {
                Debug.Log("destruir");
                Destroy(UITilePanel.transform.GetChild(0).gameObject);
            }

        }
        */

        /*
        if (UITilePanel.transform.childCount == 0)
        {
            Debug.Log("instanciou mais cartas");
            //instanciar tilePreviews dentro do tilepanel
            GameManager.instance.instanciarTilePreview();
        }
        */
    }

    // os 4 espaços da UI são númerados de 0 a 3 da esquerda para a direita
    // o valor recebdo pela função é relacionado ao número do slot de tile na UI
    public void RandomizeSingleTiles(int tileNum) {

        //tileType type = (tileType) Random.Range(0, System.Enum.GetValues(typeof(tileType)).Length - 1);

        //essa funcao atualiza o tile do mapa tambem, entao ela nao pode ser chamada para atualizar os tiles do preview
        //Tile.GetComponent<tileSetup>().updateTile(type);

        Button UIButton = UITilePanel.transform.GetChild(tileNum).GetComponentInChildren<Button>();

        GameManager.instance.nextTurnButton.interactable = false;

        if (UIButton.GetComponentInParent<tilePreview_Properties>() != null)
        {
            UIButton.GetComponentInParent<tilePreview_Properties>().randomizePreview();
        }
        else
        {
            Debug.LogError("Tile randomizado não foi encontrado",UIButton);
        }
        /*
        try {


            //Texture texture = Tile.GetComponent<tileSetup>().tileTextures[0];

            

            //UIButton.image.sprite = Sprite.Create((Texture2D) texture, new Rect(0, 0, 32, 32), new Vector2()); ;

        } catch (System.Exception) {

            Debug.LogError("Tile randomizado não foi encontrado");
            throw;
        }
        */
    }
}
