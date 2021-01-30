using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum itemType {
    clockwise,
    counter_clockwise
}

public class itemRandomization : MonoBehaviour {

    public GameObject itemPanel;
    public Sprite[] itemSprite; // 0 clockwise, 1 counter

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    /*public void RandomizeAllItens() {

        for (int i = 0; i < itemPanel.transform.childCount; i++) {
            RandomizeSingleItem(i);
        }

    }*/

    // Gameobject, o slot criado no panel
    public void RandomizeSingleItem(GameObject slot) {

        // cria um tipo para aquele item
        int num = Random.Range(0, System.Enum.GetValues(typeof(itemType)).Length);
        print(num);
        itemType randomtype = (itemType) num;
        item itemRandomized = slot.AddComponent<item>();
        itemRandomized.setType(randomtype);

        switch (randomtype) {
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
