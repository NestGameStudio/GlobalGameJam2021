using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class tileSetup : MonoBehaviour
{
    public GameObject tileGraphics;

    public Texture[] tileTextures;

    public enum tileType 
    {
        FourSides,
        ThreeSides,
        OneSideB,
        OneSide
    }
    public tileType tipoTile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.isPlaying == false)
        {
            atualizarEditor();
        }
    }
    void atualizarEditor()
    {
        switch(tipoTile)
        {
            case tileType.OneSide:
                tileGraphics.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = tileTextures[0];
                break;
            case tileType.OneSideB:
                tileGraphics.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = tileTextures[1];
                break;
            case tileType.ThreeSides:
                tileGraphics.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = tileTextures[2];
                break;
            case tileType.FourSides:
                tileGraphics.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = tileTextures[3];
                break;
        }
    }
}
