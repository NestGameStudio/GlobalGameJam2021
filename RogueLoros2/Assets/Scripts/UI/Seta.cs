using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seta : MonoBehaviour
{
    public static Seta instance { get; private set; }
    public GameObject Target;

    RectTransform rt;

    GameObject player;
    private void Awake()
    {
        //lida com duplicatas de instancia
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        rt = GetComponent<RectTransform>();

        Target = GameObject.FindGameObjectWithTag("Goal");

        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void changeTarget()
    {
        gameObject.SetActive(true);
        Target = GameManager.instance.firstTileObject;
    }
    void Update()
    {
        if (Target != null)
        {
            transform.LookAt(Target.transform.position);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}