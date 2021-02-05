using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tilePreview_Properties : MonoBehaviour, IPointerClickHandler,IDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public enum previewType
    {
        FourSides,
        ThreeSides,
        OneSideB,
        OneSide
    }

    public previewType tipoPreview;

    public Texture2D[] tileTextures;

    [HideInInspector] public int randomCount = 0; 

    // Start is called before the first frame update
    void Awake()
    {
        //randomizePreview();
    }
    private void Start()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.7f).setEaseOutBounce();
    }

    public void randomizePreview(int randomRotation)
    {

        randomCount = randomRotation;

        //Create a seed
        //int seed = (int)Time.deltaTime;
        //Set a seed in the Random Generator
        int firstPass = Random.Range(0, System.DateTime.Now.Millisecond);
        //int secondPass = firstPass * Random.Range(0, firstPass);
        int secondPass = (int)RandomFromDistribution.RandomRangeLinear(0, 100, 1);

        //Random.InitState(secondPass);


        previewType type = (previewType)(int)RandomFromDistribution.RandomRangeLinear(0, System.Enum.GetValues(typeof(previewType)).Length ,0.5f);
        tipoPreview = type;

        switch (tipoPreview)
        {
            case previewType.OneSide:
                GetComponentInChildren<Image>().sprite = Sprite.Create((Texture2D)tileTextures[0], new Rect(0, 0, 64, 64), new Vector2()); 
                break;
            case previewType.OneSideB:
                GetComponentInChildren<Image>().sprite = Sprite.Create((Texture2D)tileTextures[1], new Rect(0, 0, 64, 64), new Vector2());



                break;
            case previewType.ThreeSides:
                GetComponentInChildren<Image>().sprite = Sprite.Create((Texture2D)tileTextures[2], new Rect(0, 0, 64, 64), new Vector2());



                break;
            case previewType.FourSides:
                GetComponentInChildren<Image>().sprite = Sprite.Create((Texture2D)tileTextures[3], new Rect(0, 0, 64, 64), new Vector2());

                break;
        }

        //print("tem que rotacionar x vezes " + randomCount);
        for (int i = 0; i < randomCount; i++) {
            //print("rotacionei");
            GetComponentInChildren<Image>().transform.Rotate(Vector3.forward, -90);
        }
    }

    public void GrabTile()
    {

        audioManager.instance.chooseTilePreview();

        if(GameManager.instance != null)
        {
            switch (tipoPreview)
            {
                case previewType.OneSide:
                    GameManager.instance.createHoverInstance(tileType.OneSide,gameObject);

                    break;
                case previewType.OneSideB:
                    GameManager.instance.createHoverInstance(tileType.OneSideB, gameObject);

                    break;
                case previewType.ThreeSides:
                    GameManager.instance.createHoverInstance(tileType.ThreeSides, gameObject);

                    break;
                case previewType.FourSides:
                    GameManager.instance.createHoverInstance(tileType.FourSides, gameObject);

                    break;
            }

            gameObject.SetActive(false);

        }
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
