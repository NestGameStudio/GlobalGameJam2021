using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour {

    public itemType type;

    // considera quantas vezes ele rotacionou para  a direita, se rotaciona para a esquerda, esse numero diminui
    private int rotationCounter = 0;

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

        GameManager.instance.grabbedTile.transform.Rotate(Vector3.up, 90);

        tileSetup tile = GameManager.instance.grabbedTile.GetComponent<tileSetup>();

        Transform aux = tile.spawnPoints[tile.spawnPoints.Length - 1];
        for (int i = tile.spawnPoints.Length; i < 0; i--) {

            if (i == 0) {
                tile.spawnPoints[i] = aux;
            } else {
                tile.spawnPoints[i] = tile.spawnPoints[i - 1];
                //tile.spawnPoints[i + 1] = null;
            }

        }


        print("rotaciona para direita");
    }

    private void RotateCounterClockwise() {

        GameManager.instance.grabbedTile.transform.Rotate(Vector3.up, -90);

        tileSetup tile = GameManager.instance.grabbedTile.GetComponent<tileSetup>();

        Transform aux = tile.spawnPoints[0];
        for (int i = 0; i < tile.spawnPoints.Length; i++) {

            if (i == tile.spawnPoints.Length - 1) {
                tile.spawnPoints[i] = aux;
            } else {
                tile.spawnPoints[i] = tile.spawnPoints[i + 1];
                tile.spawnPoints[i + 1] = null;
            }

        }

        print("rotaciona para esquerda");
    }

}
