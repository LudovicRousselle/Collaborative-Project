using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    //References
    private PlayerInput m_playerInput;
    private EventSystem m_eventSystem;

    //UI
    //Menu
    public GameObject m_menu;
    [SerializeField] private GameObject m_mainMenu;

    //Button EventSystem
    [SerializeField] private Button m_firstButtonMainMenu;

    //Menu
    private bool m_isMenuActive;

    private void OnEnable()
    {
        m_playerInput = new PlayerInput();
        m_playerInput.Enable();
    }

    private void OnDisable()
    {
        m_playerInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_playerInput = new PlayerInput();
        m_playerInput.Enable();

        m_menu.SetActive(false);
        m_eventSystem = EventSystem.current;
        SetupAllInputs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupAllInputs()
    {
        m_playerInput.Default.PauseGame.performed += ctx => PauseMenu();
        m_playerInput.Default.BackMenu.performed += ctx => BackMenu();
    }


    private void PauseMenu()
    {
        m_isMenuActive = !m_isMenuActive;

        if (m_isMenuActive)
        {
            m_menu.SetActive(true);
            mainMenu();
        }
        else
        {
            m_menu.SetActive(false);
        }
    }

    public void BackMenu()
    {
        if (m_mainMenu.activeInHierarchy)
        {
            //Quit the menu
            m_menu.SetActive(false);
            m_isMenuActive = false;
        }
    }

    private void mainMenu()
    {
        m_mainMenu.SetActive(true);
        m_firstButtonMainMenu.Select();
    }
}
