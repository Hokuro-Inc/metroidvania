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

    bool isPaused;

    // Inicializa el menu de pausa
    void Start()
    {
        isPaused = false;
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
            if (!controlsMenu.activeInHierarchy)
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
        pauseMenu.SetActive(isPaused);
        inventoryMenu.SetActive(false);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    // Menú de inventario
    public void Inventory()
    {
        pauseMenu.SetActive(false);
        inventoryMenu.SetActive(true);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    // Menú de pciones
    public void Options()
    {
        pauseMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        optionsMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }
    
    // Menú de controles
    public void Controls()
    {
        pauseMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    // Salir al menú principal
    public void QuitToMenu()
    {
        isPaused = false;
        InventorySaveManager.inventorySaveManager.SaveItems();
        GameSaveManager.gameSaveManager.SaveScriptableObjects();
        SceneManager.LoadScene(sceneIndex);
    }
}
