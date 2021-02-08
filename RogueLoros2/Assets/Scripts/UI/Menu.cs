using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool isMenu = false;
    
    public void MenuToggle()
    {
        isMenu = !isMenu;
        PlayerController.instance.isOnMenu = isMenu;
    }
}
