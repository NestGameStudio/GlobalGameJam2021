using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance  {get; private set;}

    public GameObject Player;
    private GameObject playerInGame;
    public GameObject DeathFog;
    private GameObject DeathFogInGame;

    //referencias dentro do gamemanager
    public GameObject itemPanel;
    public GameObject tilePanel;
    public GameObject itemSlot;
    public GameObject tilePreview;
    public GameObject Tile;
    public Transform tileWorld;

    public Button nextTurnButton;

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

    //min/max - distancia X ate chave
    public Vector2 goalX = new Vector2(5,10);

    //min/max - distancia y ate chave
    public Vector2 goalY = new Vector2(5, 10);

    /*
    //distancia X ate chave 
    public int goalX = 5;

    //distancia y ate chave 
    public int goalY = 8;
    */

    public GameObject tileChave;

    public bool startFinalAttack = false;

    private int walkCounter = 0;

    private List<List<GameObject>> matrizTiles = new List<List<GameObject>>();

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

        //criar tiles pre setados
        instanciarTilePresets(tileChave, goalX, goalY);
    }
    private void Start()
    {
        playerInGame = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        interacaoDeColocacaoTile();

    }

    void instanciarTilePresets(GameObject tile, Vector2 x, Vector2 y)
    {
        float xTrue = (int)Random.Range(x.x,x.y);
        float yTrue = (int)Random.Range(y.x, y.y);
        Vector3 local = new Vector3(xTrue*2.5f,0, yTrue*2.5f);
        GameObject tileGoal = Instantiate(tile, local,Quaternion.identity);

        while(yTrue > matrizTiles.Count) {
            matrizTiles.Add(new List<GameObject>());
        }
        matrizTiles.Add(new List<GameObject>());
        matrizTiles[(int)yTrue].Add(tileGoal);
    }

    public void instanciarItemSlots()
    {
        for(int x = 0; x < System.Enum.GetValues(typeof(itemType)).Length; x++)
        {
            GameObject slot = Instantiate(itemSlot,transform.position,Quaternion.identity);
            slot.transform.parent = itemPanel.transform;

            // seta o item conforme o tiletype
            GetComponent<itemSetup>().SetupItem(slot, (itemType) x);
            slot.GetComponentInChildren<Button>().onClick.AddListener(slot.GetComponent<item>().DoAction);

        }
    }
    public void instanciarTilePreview()
    {
        int numberTilePreviewsCompensated = numberTilePreview - GetComponent<TileRandomizer>().UITilePanel.transform.childCount;
        for (int x = 0; x < numberTilePreviewsCompensated; x++)
        {
            GameObject slot = Instantiate(tilePreview, transform.position, Quaternion.identity);
            slot.transform.parent = tilePanel.transform;
            GetComponent<TileRandomizer>().RandomizeAllTiles();
        }
    }

    void firstTile()
    {
        GameObject tile = Instantiate(Tile,transform.position,Quaternion.identity);
        tile.transform.parent = tileWorld;
        tile.transform.position = new Vector3(0, 0, 0);
        tile.gameObject.tag = "SpawnPoint";

        tileType type = (tileType)Random.Range(0, System.Enum.GetValues(typeof(tileType)).Length);
        tile.GetComponent<tileSetup>().updateTile(type);
        activeTile = tile;

        matrizTiles.Add(new List<GameObject>());
        matrizTiles[0].Add(tile);
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
        GameObject newTile = Instantiate(grabbedTile,position, grabbedTile.transform.rotation);
        newTile.GetComponent<tileSetup>().updateTile(tipoTileGrabbed);

        // adiciona os tiles na matriz para serem destruidos quando chegar o ataque final
        int index = (int) (newTile.transform.position.z / 2.5f);
        print(index);
        if (index > matrizTiles.Count - 1) {
            matrizTiles.Add(new List<GameObject>());
        } 

        matrizTiles[index].Add(newTile);

        for(int x = 0; x < newTile.GetComponent<tileSetup>().spawnPoints.Length; x++)
        {
            newTile.GetComponent<tileSetup>().spawnPoints[x].gameObject.SetActive(grabbedTile.GetComponent<tileSetup>().spawnPoints[x].gameObject.active);
        }
        //grabbedTile.GetComponent<tileSetup>().updateTile(tipoTileGrabbed);
        //newTile.transform.parent = parent;
        //newTile.transform.rotation = grabbedTile.transform.rotation;
        newTile.transform.parent = tileWorld;

        //destroy tile preview
        Destroy(grabbedTilePreview);

        //destruir o tile criado para servir como visualizacao
        Destroy(grabbedTile);

        //parar visualizacao de tileplacement
        isPlacingTile = false;

        //retornar ao estado 0 -> todos os botoes interativos


        reenableTileButtons();

        activeTile.GetComponent<tileSetup>().checkConnections();

        hasTilePreviewsLeft();

        //habilitar movimento do player
        playerInGame.GetComponent<PlayerController>().canMove = true;

    }

    void interacaoDeColocacaoTile()
    {
        //booleana que desliga os botoes de criar tiles
        if (isPlacingTile)
        {
            nextTurnButton.gameObject.SetActive(false);

            //habilitar movimento do player
            playerInGame.GetComponent<PlayerController>().canMove = false;

            for (int x = 0; x < tilePanel.transform.childCount; x++)
            {
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
        else
        {
            //habilitar movimento do player
            playerInGame.GetComponent<PlayerController>().canMove = true;

            nextTurnButton.gameObject.SetActive(true);
        }
    }
    void reenableTileButtons()
    {
        for (int x = 0; x < tilePanel.transform.childCount; x++)
        {
            tilePanel.transform.GetChild(x).GetComponentInChildren<Button>().interactable = true;
            tilePanel.transform.GetChild(x).gameObject.SetActive(true);
        }

        //desativar marcadores
        activeTile.GetComponent<tileSetup>().deactivateMarkers();

        //hasTilePreviewsLeft();

        //hasTilePreviewsLeft();
    }
    void positionPlayer()
    {
        GameObject newPlayer = Instantiate(Player,activeTile.transform.position,Quaternion.identity);
        newPlayer.transform.parent = gameObject.transform;
    }

    public void movePlayer(tileSetup tileObject)
    {
        activeTile = tileObject.gameObject;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = activeTile.transform.position;

        activeTile.GetComponent<tileSetup>().checkConnections();

        if (startFinalAttack) {
            walkCounter += 1;

            if (walkCounter == 2) {
                DestroyLastLineOfTiles();
                walkCounter = 0;
            }
        }

        // precisa estar embaixo desse if par aque ele não comece a contar erroneamente os tiles
        if (activeTile.gameObject.CompareTag("Goal")) {
            startFinalAttack = true;
            // Alterar offset da câmera para deixar player mais pro topo da tela
            DeathFogInGame = Instantiate(DeathFog, new Vector3 (tileChave.transform.position.x, tileChave.transform.position.y, tileChave.transform.position.z + 2.5f), Quaternion.identity);
            print("começou");
        }

        if(activeTile.gameObject.CompareTag("SpawnPoint") && startFinalAttack) {

            SceneController.instance.vitoria();
            print("você ganhou");
        }
    }

    void hasTilePreviewsLeft()
    {
        //Debug.Log("tilepanel childcount: " + tilePanel.transform.childCount);
        if(tilePanel.transform.childCount == 3)
        {
            //Debug.Log("acabaram cartas");
            //acabaram os tiles -> religar o botao
            nextTurnButton.interactable = true;
        }
    }

    private void DestroyLastLineOfTiles() {

        foreach(GameObject tile in matrizTiles[matrizTiles.Count - 1]) {

            if (activeTile == tile) {
                print("você perdeu");

                SceneController.instance.morte();
            }
            Destroy(tile);
        }

        matrizTiles.RemoveAt(matrizTiles.Count - 1);

        print("destrui ultima linha");
    }

}
