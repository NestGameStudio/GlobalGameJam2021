using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour {

    itemType type;

    public void setType(itemType type) {
        this.type = type;
    }

    public void DoAction() {

        if (GameManager.instance.grabbedTilePreview) {

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

    }

    private void RotateCounterClockwise() {

    }

}
