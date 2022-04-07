using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    //References
    private PlayerInput m_playerInput;
    private EventSystem m_eventSystem;

    //UI
    //Menu
    [SerializeField] private GameObject m_menu;
    [SerializeField] private GameObject m_mainMenu;
    [SerializeField] private GameObject m_option;
    [SerializeField] private GameObject m_controls;

    //Button EventSystem
    [SerializeField] private GameObject m_firstButtonMainMenu;
    [SerializeField] private GameObject m_firstButtonOptions;

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
        if (m_controls.activeInHierarchy)
        {
            //Goes back to Option Menu
            OptionMenu();
        }
        else if (m_option.activeInHierarchy)
        {
            //Goes back to main Menu
            mainMenu();
        }
        else if (m_mainMenu.activeInHierarchy)
        {
            //Quit the menu
            m_menu.SetActive(false);
            m_isMenuActive = false;
        }
    }

    private void mainMenu()
    {
        m_mainMenu.SetActive(true);
        m_option.SetActive(false);
        m_controls.SetActive(false);

        m_eventSystem.SetSelectedGameObject(m_firstButtonMainMenu);
    }

    public void OptionMenu()
    {
        m_option.SetActive(true);
        m_mainMenu.SetActive(false);
        m_controls.SetActive(false);

        m_eventSystem.SetSelectedGameObject(m_firstButtonOptions);
    }

    public void ControlMenu()
    {
        m_option.SetActive(false);
        m_controls.SetActive(true);
    }
}
