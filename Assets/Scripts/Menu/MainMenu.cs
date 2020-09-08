using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Tooltip("Índice de la escena a cargar")]
    [SerializeField] private int sceneIndex;
    [Header("Paneles")]
    [Tooltip("Panel del menú principal")]
    [SerializeField] private GameObject mainMenu;
    [Tooltip("Panel de los ajustes")]
    [SerializeField] private GameObject optionsMenu;
    [Tooltip("Panel de la pantalla de carga")]
    [SerializeField] private GameObject loadingScreen;
    [Header("Barra de carga")]
    [Tooltip("Slider de la barra de carga")]
    [SerializeField] private Slider loadingBar;
    [Tooltip("Texto de progreso de la barra de carga")]
    [SerializeField] private Text progressText;

    // Inicializa la pantalla principal
    void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        loadingScreen.SetActive(false);
    }

    // Inicia una nueva partida
    public void NewGame()
    {
        //InventorySaveManager.inventorySaveManager.RestartGame();
        GameSaveManager.instance.RestartGame();
        StartCoroutine(LoadAsync());
    }

    // Carga la última partida guardada
    public void LoadGame()
    {
        //InventorySaveManager.inventorySaveManager.LoadItems();
        //GameSaveManager.gameSaveManager.LoadScriptableObjects();
        GameSaveManager.instance.LoadData();
        StartCoroutine(LoadAsync());
    }

    // Activa el menú de opciones
    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    // Vuelta al menú principal
    public void Back()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    // Salir al escritorio
    public void QuitGame()
    {
        Application.Quit();
    }

    // Corrutina para la carga de la escena
    private IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
}
