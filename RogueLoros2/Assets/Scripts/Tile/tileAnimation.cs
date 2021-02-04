using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileAnimation : MonoBehaviour
{
    public bool canAnimate = false;
    public GameObject movableGO;
    // Start is called before the first frame update
    void Start()
    {
        movableGO = gameObject;
        animate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void animate()
    {
        if (canAnimate)
        {
            LeanTween.moveY(movableGO, -0.1f, Random.Range(1,2)).setLoopPingPong().setEaseInOutSine().setDelay(Random.Range(0,5));
        }
    }
}
