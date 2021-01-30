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
        previewType type = (previewType)Random.Range(0, System.Enum.GetValues(typeof(previewType)).Length - 1);
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
}
