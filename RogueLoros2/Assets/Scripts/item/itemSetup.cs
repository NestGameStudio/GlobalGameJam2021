using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum itemType {
    clockwise,
    counter_clockwise
}

public class itemSetup : MonoBehaviour {

    public GameObject itemPanel;
    public Sprite[] itemSprite; // 0 clockwise, 1 counter

    // Gameobject, o slot criado no panel
    public void SetupItem(GameObject slot, itemType tipo) {

        // cria um tipo para aquele item
        item item = slot.AddComponent<item>();
        item.setType(tipo);

        switch (tipo) {
            case itemType.clockwise:
                slot.GetComponentInChildren<Button>().image.sprite = itemSprite[0];
                break;
            case itemType.counter_clockwise:
                slot.GetComponentInChildren<Button>().image.sprite = itemSprite[1];
                break;
            default:
                break;
        }
        
    }
}
