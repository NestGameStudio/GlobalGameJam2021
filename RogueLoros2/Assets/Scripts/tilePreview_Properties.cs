using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tilePreview_Properties : MonoBehaviour
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

    // Start is called before the first frame update
    void Awake()
    {
        //randomizePreview();
    }

    public void randomizePreview()
    {
        //Create a seed
        //int seed = (int)Time.deltaTime;
        //Set a seed in the Random Generator
        int firstPass = Random.Range(0, System.DateTime.Now.Millisecond);
        int secondPass = firstPass * Random.Range(0, firstPass);

        Random.InitState(secondPass);


        previewType type = (previewType)Random.Range(0, System.Enum.GetValues(typeof(previewType)).Length );
        tipoPreview = type;

        switch (tipoPreview)
        {
            case previewType.OneSide:
                GetComponentInChildren<Image>().sprite = Sprite.Create((Texture2D)tileTextures[0], new Rect(0, 0, 32, 32), new Vector2());

                

                break;
            case previewType.OneSideB:
                GetComponentInChildren<Image>().sprite = Sprite.Create((Texture2D)tileTextures[1], new Rect(0, 0, 32, 32), new Vector2());



                break;
            case previewType.ThreeSides:
                GetComponentInChildren<Image>().sprite = Sprite.Create((Texture2D)tileTextures[2], new Rect(0, 0, 32, 32), new Vector2());



                break;
            case previewType.FourSides:
                GetComponentInChildren<Image>().sprite = Sprite.Create((Texture2D)tileTextures[3], new Rect(0, 0, 32, 32), new Vector2());



                break;
        }

    }

    public void GrabTile()
    {
        if(GameManager.instance != null)
        {
            switch (tipoPreview)
            {
                case previewType.OneSide:
                    GameManager.instance.createHoverInstance(tileType.OneSide);

                    break;
                case previewType.OneSideB:
                    GameManager.instance.createHoverInstance(tileType.OneSideB);

                    break;
                case previewType.ThreeSides:
                    GameManager.instance.createHoverInstance(tileType.ThreeSides);

                    break;
                case previewType.FourSides:
                    GameManager.instance.createHoverInstance(tileType.FourSides);

                    break;
            }

            gameObject.SetActive(false);

        }
    }
}
