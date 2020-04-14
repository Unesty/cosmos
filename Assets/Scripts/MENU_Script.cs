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

        
        //выход из новой игры
        if (Level_panel && Input.GetKeyDown(KeyCode.Escape))
        {
            New_Game_button(false);
        }

        //отмена выхода из игры
        if (Exit_Panel&&Input.GetKeyDown(KeyCode.Escape))
        {
            Exit_Panel_But(false);
        }
    }

    //НОВАЯ ИГРА
    public void New_Game_button(bool Sostoyanie)
    {
        if (Sostoyanie)
        {
            Level_panel.SetActive(true);
            Fon_Panel.SetActive(false);
            Button_Menu_Panel.SetActive(false);
        }
        else
        {
            Level_panel.SetActive(false);
            Fon_Panel.SetActive(true);
            Button_Menu_Panel.SetActive(true);
        }
    }

    //загрузка нужного уровня
    public void Load_level(int Num_Scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Num_Scene);
    }

    // EXIT Панель
    public void Exit_Panel_But(bool Control)
    {
        if (Control)
        {
            Button_Menu_Panel.SetActive(false);
            Exit_Panel.SetActive(true);
        }
        else
        {
            Button_Menu_Panel.SetActive(true);
            Exit_Panel.SetActive(false);
        }
    }

    // кнопка подтверждения выхода (Yes)
    public void Exit_Button()
    {
        Application.Quit();
    }



}
