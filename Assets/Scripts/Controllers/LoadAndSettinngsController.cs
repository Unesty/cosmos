using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAndSettinngsController : MonoBehaviour
{
    // Бэк фона
    [SerializeField] private GameObject bgPanel;
    // Панель в главном менб
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject exitPanel;
    
    void Start()
    {
        levelPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

    void Update()
    {        
        //выход из новой игры
        if (levelPanel && Input.GetKeyDown(KeyCode.Escape))
        {
            New_Game_button(false);
        }

        //отмена выхода из игры
        if (exitPanel&&Input.GetKeyDown(KeyCode.Escape))
        {
            Exit_Panel_But(false);
        }
    }

    //НОВАЯ ИГРА
    public void New_Game_button(bool Sostoyanie)
    {
        if (Sostoyanie)
        {
            levelPanel.SetActive(true);
            bgPanel.SetActive(false);
            panelMainMenu.SetActive(false);
        }
        else
        {
            levelPanel.SetActive(false);
            bgPanel.SetActive(true);
            panelMainMenu.SetActive(true);
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
            panelMainMenu.SetActive(false);
            exitPanel.SetActive(true);
        }
        else
        {
            panelMainMenu.SetActive(true);
            exitPanel.SetActive(false);
        }
    }

    // кнопка подтверждения выхода (Yes)
    public void Exit_Button()
    {
        Application.Quit();
    }

    //кнопка выхода в главное меню из загрузки меню
    public void Back_to_menu_button()
    {
        New_Game_button(false);
    }

}
