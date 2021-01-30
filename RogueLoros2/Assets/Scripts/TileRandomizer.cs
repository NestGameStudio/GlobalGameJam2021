using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileRandomizer : MonoBehaviour {

    public GameObject Tile;
    public GameObject UITilePanel;

    public void RandomizeAllTiles() {

        for (int i = 0; i < UITilePanel.transform.childCount - 1; i++) {
            RandomizeSingleTiles(i);
        }

    }

    // os 4 espaços da UI são númerados de 0 a 3 da esquerda para a direita
    // o valor recebdo pela função é relacionado ao número do slot de tile na UI
    public void RandomizeSingleTiles(int tileNum) {

        tileType type = (tileType) Random.Range(0, System.Enum.GetValues(typeof(tileType)).Length - 1);
        Tile.GetComponent<tileSetup>().updateTile(type);

        try {


            Texture texture = Tile.GetComponent<tileSetup>().tileTextures[0];

            Button UIButton = UITilePanel.transform.GetChild(tileNum).GetComponentInChildren<Button>();
            UIButton.image.sprite = Sprite.Create((Texture2D) texture, new Rect(0, 0, 32, 32), new Vector2()); ;

        } catch (System.Exception) {

            Debug.LogError("Tile randomizado não foi encontrado");
            throw;
        }

    }
}
