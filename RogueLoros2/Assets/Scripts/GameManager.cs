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
    public RectTransform objectiveUI;

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
    public Vector2 goalX = new Vector2(-5,5);

    //min/max - distancia y ate chave
    public Vector2 goalY = new Vector2(5, 10);

    private float tileChavePos;

    public int[] itemPrecos;

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

    [Header("Configs dos tiles de moeda")]
    public int numeroTilesMoeda = 10;
    public int raio = 50;

    public GameObject coinTile;

    Vector2 tileGoalPos;

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

        //instanciar tiles de moeda
        instanciarTileCoin();

        //mover todo o palco para frente
        //transformAll();
    }
    private void Start()
    {
        playerInGame = GameObject.FindGameObjectWithTag("Player");

        itemPanel.transform.parent.gameObject.SetActive(false);

        
    }
    // Update is called once per frame
    void Update()
    {
        interacaoDeColocacaoTile();

    }
    IEnumerator objectiveTextShow()
    {
        //fade in
        objectiveUI.gameObject.GetComponent<Text>().color = new Color(1,1,1,0);
        Image sombrinha = objectiveUI.transform.parent.GetComponentInChildren<Image>();
        sombrinha.color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(0.5f);

        LeanTween.alphaText(objectiveUI, 1, 2).setEaseOutExpo();
        LeanTween.alpha(sombrinha.GetComponent<RectTransform>(), 1f, 2).setEaseOutExpo();

        yield return new WaitForSeconds(3);

        //fade out
        LeanTween.alphaText(objectiveUI, 0, 1);
        LeanTween.alpha(sombrinha.GetComponent<RectTransform>(), 0, 1);

        yield return new WaitForSeconds(1);
        Destroy(objectiveUI.transform.parent.gameObject);

    }
    void transformAll()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 50);
    }
    //ativar ui depois de animacao de inicio
    public void activateUI()
    {
        itemPanel.transform.parent.gameObject.SetActive(true);

        StartCoroutine(objectiveTextShow());
    }
    void instanciarTileCoin()
    {
        //instanciar os tiles de moeda num raio predeterminado
        for (int i = 0; i < numeroTilesMoeda; i++)
        {
            int randomX = Random.Range(-raio/2, raio/2 + 1);
            int randomY = Random.Range(1, raio + 1);

            //nao pode ocupar espaco do tile inicial ou do tile do objetivo
            if (randomX == 0 && randomY == 0 || randomX == tileGoalPos.x && randomY == tileGoalPos.y)
            {
                randomX = Random.Range(-raio / 2, raio / 2 + 1);
                randomY = Random.Range(1, raio + 1);
            }
            else
            {
                Vector3 location = new Vector3(randomX * 2.5f, 0, randomY * 2.5f);

                GameObject moneyTile = Instantiate(coinTile, location, Quaternion.identity);
                moneyTile.transform.parent = tileWorld.transform;
            }
        
        }
    }

    void instanciarTilePresets(GameObject tile, Vector2 x, Vector2 y)
    {
        float xTrue = (int)Random.Range(x.x,x.y);
        float yTrue = (int)Random.Range(y.x, y.y);


        tileGoalPos.x = xTrue;
        tileGoalPos.y = yTrue;

        tileChavePos = yTrue; //usado para forçar posição da Death Fog

        Vector3 local = new Vector3(xTrue*2.5f,0, yTrue*2.5f);
        GameObject tileGoal = Instantiate(tile, local,Quaternion.identity);
        tileGoal.transform.parent = tileWorld;

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
            GetComponent<itemSetup>().SetupItem(slot, (itemType) x, itemPrecos[x]);
            slot.GetComponentInChildren<Button>().onClick.AddListener(slot.GetComponent<item>().DoAction);

        }

        checarItensCompraveis();
    }

    public void checarItensCompraveis()
    {
        int playerMoney = GetComponent<moneySystem>().money;

        for (int i = 0; i < itemPanel.transform.childCount; i++)
        {
            //enable buttons of items if their cost is less than the money the player has
            if (itemPanel.transform.GetChild(i).GetComponent<item>().precoObj <= playerMoney)
            {
                itemPanel.transform.GetChild(i).GetComponent<item>().activateButton();
            }
            else
            {
                itemPanel.transform.GetChild(i).GetComponent<item>().deactivateButton();
            }
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
        tile.GetComponent<tileSetup>().firstTile = true;
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

        activeTile.GetComponent<tileSetup>().checkConnections();
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

            matrizTiles[index].Add(newTile);
        } 

        

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
        //newTile.GetComponent<tileSetup>().checkConnections();

        hasTilePreviewsLeft();

        //habilitar movimento do player
        playerInGame.GetComponent<PlayerController>().canMove = true;

        //float randomPos = Random.Range(-1f,1.1f);
        //newTile.transform.transform.rotation = Quaternion.Euler(grabbedTile.transform.transform.rotation.x, grabbedTile.transform.transform.rotation.y + randomPos, grabbedTile.transform.transform.rotation.z );
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
        //resetar y de ultimo tile
        activeTile.transform.GetChild(1).transform.position = new Vector3(activeTile.transform.GetChild(1).transform.position.x,0, activeTile.transform.GetChild(1).transform.position.z);
        activeTile.GetComponentInChildren<MeshRenderer>().material.color = Color.white;

        activeTile = tileObject.gameObject;

        //afundou
        activeTile.transform.GetChild(1).transform.position = new Vector3(activeTile.transform.GetChild(1).transform.position.x, -0.25f, activeTile.transform.GetChild(1).transform.position.z);
        activeTile.GetComponentInChildren<MeshRenderer>().material.color = new Color(0.7f, 0.7f, 0.7f);

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
            DeathFogInGame = Instantiate(DeathFog, new Vector3 (0, 0, (2.5f * (tileChavePos + 1))), Quaternion.identity);
            //DeathFogInGame.transform.parent = tileWorld;
            print("começou");

            activeTile.gameObject.tag = null;

            //tirar item em cima
            //activeTile.GetComponent<hoverItems>().destroyItem();
        }
        /*
        else if (activeTile.gameObject.CompareTag("Money"))
        {
            //pegou dinheiro
            print("got money");
            moneySystem.instance.addMoney(10);

            activeTile.gameObject.tag = null;

            //tirar item em cima
            activeTile.GetComponent<hoverItems>().destroyItem();
        }
        */
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

        //move a tile forward
        DeathFogInGame.transform.position = new Vector3 (0,0, DeathFogInGame.transform.position.z - 2.5f);

        foreach(GameObject tile in matrizTiles[matrizTiles.Count - 1]) {

            if (activeTile == tile) {
                print("você perdeu");

                SceneController.instance.morte();
            }
            Destroy(tile);
        }

        activeTile.GetComponent<tileSetup>().checkConnections();

        matrizTiles.RemoveAt(matrizTiles.Count - 1);

        print("destrui ultima linha");
    }

}
