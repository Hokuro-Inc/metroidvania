using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Tooltip("Referencia al mezclador de sonido")]
    [SerializeField] private AudioMixer audioMixer;
    [Tooltip("Lista de resoluciones")]
    [SerializeField] private Dropdown resolutionsDropdown;
    [Tooltip("Barra de volumen")]
    [SerializeField] private Slider volumeSlider;
    [Tooltip("Botón de pantalla completa")]
    [SerializeField] private Toggle fullscreenToogle;
    [Tooltip("Lista de calidad gráfica")]
    [SerializeField] private Dropdown qualityDropdown;

    // Vector de resoluciones que almacena todas las resoluciones disponibles
    private Resolution[] resolutions;

    // Recoge las resoluciones del dispositivo
    void Start()
    {
        GetResolutions();
    }

    // Actualiza el panel de ajustes
    void OnEnable()
    {
        GetSavedSettings();
    }

    /*void OnDisable()
    {
        PlayerPrefs.Save();
    }*/

    // Recupera los datos de los ajustes
    private void GetSavedSettings()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            SetVolume(PlayerPrefs.GetFloat("volume"));
        }

        if (PlayerPrefs.HasKey("fullscreen"))
        {
            bool isFullscreen = PlayerPrefs.GetInt("fullscreen") == 1 ? true : false;
            SetFullscreen(isFullscreen);
        }

        if (PlayerPrefs.HasKey("quality"))
        {
            SetQuality(PlayerPrefs.GetInt("quality"));
        }

        if (PlayerPrefs.HasKey("resolution"))
        {
            SetResolution(PlayerPrefs.GetInt("resolution"));
        }

    }

    // Ajusta el volumen
    public void SetVolume(float volume)
    {
        if (volume == 0f)
        {
            volume = float.Epsilon;
        }
        audioMixer.SetFloat("volume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("volume", volume);
        volumeSlider.value = volume;
    }

    // Ajusta la pantalla completa
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        int fullscreen = isFullscreen ? 1 : 0;
        PlayerPrefs.SetInt("fullscreen", fullscreen);
        fullscreenToogle.isOn = isFullscreen;
    }

    // Ajusta la calida gráfica
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
        qualityDropdown.value = qualityIndex;
    }

    // Ajusta la resolución
    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", resolutionIndex);
        resolutionsDropdown.value = resolutionIndex;
    }

    // Recoge todas las resoluciones disponibles del dispositivo
    private void GetResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = PlayerPrefs.HasKey("resolution") ? PlayerPrefs.GetInt("resolution") : 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            //if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            if (resolutions[i].Equals(Screen.currentResolution) && !PlayerPrefs.HasKey("resolution"))
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }
}
