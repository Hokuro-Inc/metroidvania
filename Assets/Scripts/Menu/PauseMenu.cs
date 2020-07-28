using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Tooltip("Índice de la escena del menú principal")]
    public int sceneIndex;
    [Header("Paneles del menú")]
    [Tooltip("Panel del menú de pausa")]
    public GameObject pauseMenu;
    [Tooltip("Panel del menú de inventario")]
    public GameObject inventoryMenu;
    [Tooltip("Panel del menú de opciones")]
    public GameObject optionsMenu;
    [Tooltip("Panel del menú de controles")]
    public GameObject controlsMenu;

    private bool isPaused = false;
    private GameObject lastPanel;

    // Inicializa el menu de pausa
    void Start()
    {
        lastPanel = pauseMenu;
        pauseMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    // Compruba que botones se pulsan para activar la pausa o no
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!controlsMenu.activeInHierarchy && !optionsMenu.activeInHierarchy)
            {
                isPaused = !isPaused;
            }
            Pause();
        }

        if (Input.GetKeyDown(InputManager.keys["Inventory"]))
        {
            isPaused = true;
            Inventory();
        }

        CheckTimeScale();
    }

    // Comprueba si está o no pausado para congelar el juego o no
    private void CheckTimeScale()
    {
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // Continuar jugando
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    // Menu de pausa
    public void Pause()
    {
        ChangePanel(pauseMenu);
    }

    // Menú de inventario
    public void Inventory()
    {
        ChangePanel(inventoryMenu);
    }

    // Menú de pciones
    public void Options()
    {
        ChangePanel(optionsMenu);
    }
    
    // Menú de controles
    public void Controls()
    {
        ChangePanel(controlsMenu);
    }

    // Salir al menú principal
    public void QuitToMenu()
    {
        isPaused = false;
        InventorySaveManager.inventorySaveManager.SaveItems();
        GameSaveManager.gameSaveManager.SaveScriptableObjects();
        SceneManager.LoadScene(sceneIndex);
    }

    private void ChangePanel(GameObject panel)
    {
        lastPanel.SetActive(false);
        panel.SetActive(isPaused);
        lastPanel = panel;
    }
}
