using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    // Diccionario que traduce la acción a una tecla
    public static Dictionary<string, KeyCode> keys;

    [Header("Colores de los botones")]
    [Tooltip("Color normal")]
    [SerializeField] private Color32 normal;
    [Tooltip("Color del botón cuando se selecciona")]
    [SerializeField] private Color32 selected;
    [Header("Textos de los botones")]
    [Tooltip("Texto que representa que tecla está asignada a moverse a la izquierda")]
    [SerializeField] private Text left;
    [Tooltip("Texto que representa que tecla está asignada a moverse a la derecha")]
    [SerializeField] private Text right;
    [Tooltip("Texto que representa que tecla está asignada a saltar")]
    [SerializeField] private Text jump;
    [Tooltip("Texto que representa que tecla está asignada a atacar")]
    [SerializeField] private Text attack;
    [Tooltip("Texto que representa que tecla está asignada a interactuar")]
    [SerializeField] private Text interact;
    [Tooltip("Texto que representa que tecla está asignada al inventario")]
    [SerializeField] private Text inventory/*, pause*/;

    // Último botón elegido
    private GameObject currentKey;

    // Inicializamos el diccionario
    void Start()
    {
        keys = new Dictionary<string, KeyCode>
        {
            { "Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")) },
            { "Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")) },
            { "Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")) },
            { "Attack", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack", "LeftControl")) },
            { "Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "F")) },
            { "Inventory", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Inventory", "I")) }
        };
        //keys.Add("Pause", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pause", "Escape")));

        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        jump.text = keys["Jump"].ToString();
        attack.text = keys["Attack"].ToString();
        interact.text = keys["Interact"].ToString();
        inventory.text = keys["Inventory"].ToString();
        //pause.text = keys["Pause"].ToString();
    }

    /*void OnEnable()
    {
        
    }*/

    // Reasignamos una tecla y la representamos
    void OnGUI()
    {
        if (currentKey != null)
        {
            Event currentEvent = Event.current;
            if (currentEvent.keyCode == KeyCode.Escape)
            {
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
                return;
            }
            if (currentEvent.isKey)
            {
                keys[currentKey.name] = currentEvent.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = currentEvent.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }

    // Cambiamos una tecla
    public void ChangeKey(GameObject cliked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }

        currentKey = cliked;
        currentKey.GetComponent<Image>().color = selected;
    }

    // Guardamos la configuración
    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}
