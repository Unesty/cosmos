using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MENU_Script : MonoBehaviour
{
    public GameObject Fon_Panel;
    public GameObject Button_Menu_Panel;
    public GameObject Level_panel;
    public GameObject Exit_Panel;


    // Start is called before the first frame update
    void Start()
    {
        Fon_Panel = GameObject.Find("Canvas/Fon_Panel");
        Button_Menu_Panel = GameObject.Find("Canvas/Button_Menu_Panel");
        Level_panel = GameObject.Find("Canvas/LEVEL_Panel");
        Exit_Panel = GameObject.Find("Canvas/Exit_Panel");

        Level_panel.SetActive(false);
        Exit_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
