using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject SettingPanel;
    public GameObject GameMenu;
    private List<GameObject> MenuList = new List<GameObject>();
    private void Start()
    {
        MenuList.Add(GameMenu);
        MenuList.Add(SettingPanel);
    }
    public void OpenMenu(GameObject Menu)
    {
        Menu.SetActive(true);
        GameMenu.SetActive(false);
        
    }
    public void ReturnMain(GameObject Menu)
    {
        Menu.SetActive(false);
        GameMenu.SetActive(true);
    }
}
