using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance  {get; private set;}

    public GameObject Player;

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

    [HideInInspector]
    public tileType tipoTileGrabbed;

    [HideInInspector]
    public GameObject grabbedTile;

    [HideInInspector]
    public GameObject grabbedTilePreview;

    [HideInInspector]
    public Quaternion grabbedTileRotation;

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

        //criar player
        positionPlayer();
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

        tileType type = (tileType)Random.Range(0, System.Enum.GetValues(typeof(tileType)).Length);
        tile.GetComponent<tileSetup>().updateTile(type);
        activeTile = tile;
    }

    public void createHoverInstance(tileType tipo,GameObject previewGrabbed)
    {
        isPlacingTile = true;
        tipoTileGrabbed = tipo;

        //pegando o botao que foi apertado para ser destruido depois
        grabbedTilePreview = previewGrabbed;

        //instanciou tile que sera ativado no local de preview antes de de fato o colocar na posicao
        grabbedTile = Instantiate(Tile,transform.position,Quaternion.identity);
        grabbedTile.GetComponent<tileSetup>().updateTile(tipo);
        grabbedTile.SetActive(false);
    }

    public void colocarTile(Vector3 position, Transform parent)
    {
        //instanciar tile no local
        GameObject newTile = Instantiate(grabbedTile,position,grabbedTileRotation);
        newTile.GetComponent<tileSetup>().updateTile(tipoTileGrabbed);
        newTile.transform.parent = parent;

        //destroy tile preview
        Destroy(grabbedTilePreview);

        //destruir o tile criado para servir como visualizacao
        Destroy(grabbedTile);

        //parar visualizacao de tileplacement
        isPlacingTile = false;

        //retornar ao estado 0 -> todos os botoes interativos


        reenableTileButtons();

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

            //ativar marcadores
            activeTile.GetComponent<tileSetup>().activateMarkers();

            //cancelar colocacao de tile
            if (Input.GetMouseButtonDown(1))
            {
                isPlacingTile = false;

                //destruir o tile criado para servir como visualizacao
                Destroy(grabbedTile);

                reenableTileButtons();
            }
        }
    }
    void reenableTileButtons()
    {
        for (int x = 0; x < tilePanel.transform.childCount; x++)
        {
            Debug.Log("tile buttons disabled");

            tilePanel.transform.GetChild(x).GetComponentInChildren<Button>().interactable = true;
            tilePanel.transform.GetChild(x).gameObject.SetActive(true);
        }

        //desativar marcadores
        activeTile.GetComponent<tileSetup>().deactivateMarkers();
    }
    void positionPlayer()
    {
        GameObject newPlayer = Instantiate(Player,activeTile.transform.position,Quaternion.identity);
        newPlayer.transform.parent = gameObject.transform;
    }
}
