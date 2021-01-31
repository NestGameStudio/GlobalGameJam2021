using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverItems : MonoBehaviour
{
    public GameObject hoveringItem;

    public void destroyItem()
    {
        Debug.Log("destroy item");
        Destroy(hoveringItem);

        gameObject.tag = null;
    }
}
