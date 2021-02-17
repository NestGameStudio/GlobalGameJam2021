using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}

public class tileAnimation : MonoBehaviour
{
    public bool canAnimate = true;
    public GameObject movableGO;
    private float randomNum;
    private float t = 0;
    private bool tUp = true;
    // Start is called before the first frame update
    void Awake()
    {
        movableGO = gameObject;
        randomNum = RandomFromDistribution.RandomRangeLinear(0.8f, 1.2f, 0.5f);
        //randomNum = 0.1f;
        animate();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAnimate)
        {
            movableGO.transform.position = new Vector3(movableGO.transform.position.x, Damp(-0.15f, 0.15f, 1, t), movableGO.transform.position.z);
            if (tUp)
            {
                t += Time.deltaTime / 1.7f * randomNum;
            }
            else
            {
                t -= Time.deltaTime / 1.7f * randomNum;
            }
            if (t > 1)
            {
                tUp = false;
            }
            else if (t < 0)
            {
                tUp = true;
            }
        }
    }
    public void animate()
    {
        if (canAnimate)
        {
            //LeanTween.moveY(movableGO, -0.1f, Random.Range(1,2)).setLoopPingPong().setEaseInOutSine().setSpeed(Mathf.Abs(transform.position.x/transform.position.z).Remap(0.1f,10,0,1));
            //LeanTween.moveY(movableGO, -0.1f,1).setLoopPingPong().setEaseInOutSine().setSpeed(randomNum);
        }
    }
    public static float Damp(float a, float b, float lambda, float dt)
    {
        return Mathf.Lerp(a, b, 1 - Mathf.Exp(-lambda * dt));
}
}
