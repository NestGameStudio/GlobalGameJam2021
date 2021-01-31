using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class item : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    public itemType type;

    public Text priceText;

    public int precoObj = 0;

    Button button;

    private void Start() {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.7f).setEaseOutBounce();

        priceText = GetComponentInChildren<Text>();
    }
    void setUI(int preco)
    {
        priceText = GetComponentInChildren<Text>();

        if (priceText != null)
        {
            priceText.text = "$ " + preco.ToString();
        }
        else
        {
            Debug.Log("nao achou pricetext");
        }
    }

    public void setType(itemType type, int preco) {
        this.type = type;
        precoObj = preco;
        setUI(preco);
    }

    public void DoAction() {

        if (GameManager.instance.grabbedTile) {

            if (type == itemType.clockwise) {
                RotateClockwise();
                //deduzir dinheiro
                moneySystem.instance.removeMoney(precoObj);
            } else if (type == itemType.counter_clockwise) {
                RotateCounterClockwise();
                //deduzir dinheiro
                moneySystem.instance.removeMoney(precoObj);
            }
            /*
            else if(type == itemType.reroll)
            {
                reroll();
            }
            */
            

            GameManager.instance.checarItensCompraveis();

        } else {
            Debug.Log("Não há tile selecionado");
        }
        if(type == itemType.reroll)
        {
            reroll();
            //deduzir dinheiro
            moneySystem.instance.removeMoney(precoObj);
        }
    } 
    private void reroll()
    {
        GameManager.instance.instanciarTilePreview();
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

    public void activateButton()
    {
        button = GetComponentInChildren<Button>();
        button.interactable = true;
    }
    public void deactivateButton()
    {
        button = GetComponentInChildren<Button>();
        button.interactable = false;
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        print("I was clicked");

    }

    public void OnDrag(PointerEventData eventData)
    {
        print("I'm being dragged!");

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponentInChildren<Button>().interactable)
        {
            LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.1f).setEaseOutSine();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (GetComponentInChildren<Button>().interactable)
        //{
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.1f).setEaseInSine();
        //}
    }

}
