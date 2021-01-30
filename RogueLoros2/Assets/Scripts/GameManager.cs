using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance  {get; private set;}

    //referencias dentro do gamemanager
    public GameObject itemPanel;
    public GameObject tilePanel;
    public GameObject itemSlot;
    public GameObject tilePreview;
    public GameObject Tile;
    public Transform tileWorld;

    //public GameObject hoverPrefab;

    public bool isPlacingTile = false;

    public GameObject activeTile;

    //numero de slots para itens
    public int numberItems = 4;

    //numero de prevviews de tile no painel de tiles
    public int numberTilePreview = 4;
    
    // Start is called before the first frame update
    void Awake()
    {
        //lida com duplicatas de instancia
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        //instanciar itemslots dentro do itempanel
        instanciarItemSlots();

        //instanciar tilePreviews dentro do tilepanel
        instanciarTilePreview();

        //instanciar primeiro tile
        firstTile();
    }

    // Update is called once per frame
    void Update()
    {
        interacaoDeColocacaoTile();

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

    void firstTile()
    {
        GameObject tile = Instantiate(Tile,transform.position,Quaternion.identity);
        tile.transform.parent = tileWorld;
        tile.transform.position = new Vector3(0, 0, 0);
    }

    public void createHoverInstance(tileType tipo)
    {
        isPlacingTile = true;

        //Instantiate(hoverPrefab,transform.position,Quaternion.identity);
    }

    void interacaoDeColocacaoTile()
    {
        //booleana que desliga os botoes de criar tiles
        if (isPlacingTile)
        {
            for (int x = 0; x < tilePanel.transform.childCount; x++)
            {
                Debug.Log("tile buttons disabled");

                tilePanel.transform.GetChild(x).GetComponentInChildren<Button>().interactable = false;
            }

            //cancelar colocacao de tile
            if (Input.GetMouseButtonDown(1))
            {
                isPlacingTile = false;

                for (int x = 0; x < tilePanel.transform.childCount; x++)
                {
                    Debug.Log("tile buttons disabled");

                    tilePanel.transform.GetChild(x).GetComponentInChildren<Button>().interactable = true;
                    tilePanel.transform.GetChild(x).gameObject.SetActive(true);
                }
            }
        }
    }
}
