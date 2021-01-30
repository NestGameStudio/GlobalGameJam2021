using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //referencias dentro do gamemanager
    public GameObject itemPanel;
    public GameObject tilePanel;
    public GameObject itemSlot;
    public GameObject tilePreview;

    //numero de slots para itens
    public int numberItems = 4;

    //numero de prevviews de tile no painel de tiles
    public int numberTilePreview = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        //instanciar itemslots dentro do itempanel
        instanciarItemSlots();

        //instanciar tilePreviews dentro do tilepanel
        instanciarTilePreview();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void instanciarItemSlots()
    {
        for(int x = 0; x < numberItems; x++)
        {
            GameObject slot = Instantiate(itemSlot,transform.position,Quaternion.identity);
            slot.transform.parent = itemPanel.transform;
        }
    }
    void instanciarTilePreview()
    {
        for (int x = 0; x < numberTilePreview; x++)
        {
            GameObject slot = Instantiate(tilePreview, transform.position, Quaternion.identity);
            slot.transform.parent = tilePanel.transform;
        }
    }
}
