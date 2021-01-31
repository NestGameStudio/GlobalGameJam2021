using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour {

    public itemType type;


    private void Start() {
        
    }

    public void setType(itemType type) {
        this.type = type;
    }

    public void DoAction() {

        if (GameManager.instance.grabbedTile) {

            if (type == itemType.clockwise) {
                RotateClockwise();
            } else if (type == itemType.counter_clockwise) {
                RotateCounterClockwise();
            }

        } else {
            Debug.Log("Não há tile selecionado");
        }
    } 

    private void RotateClockwise() {

        for (int i = 0; i < 3; i++) {
            RotateCounterClockwise();
        }
    }

    private void RotateCounterClockwise() {

        GameManager.instance.grabbedTile.transform.Rotate(Vector3.up, -90);

        tileSetup tile = GameManager.instance.grabbedTile.GetComponent<tileSetup>();

        Transform auxSP = tile.spawnPoints[0];
        visualizeFutureTile auxVFT = tile.tileMarkers[0];
        for (int i = 0; i < tile.spawnPoints.Length; i++) {

            if (i == tile.spawnPoints.Length - 1) {
                tile.spawnPoints[i] = auxSP;
                tile.tileMarkers[i] = auxVFT;
            } else {
                tile.spawnPoints[i] = tile.spawnPoints[i + 1];
                tile.spawnPoints[i + 1] = null;
                tile.tileMarkers[i] = tile.tileMarkers[i + 1];
                tile.tileMarkers[i + 1] = null;
            }

        }

    }

}
