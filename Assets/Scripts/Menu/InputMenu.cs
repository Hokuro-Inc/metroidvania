using UnityEngine;
using UnityEngine.UI;

public class InputMenu : MonoBehaviour
{
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
    [Tooltip("Texto que representa que tecla está asignada al dash")]
    [SerializeField] private Text dash;
    [Tooltip("Texto que representa que tecla está asignada a interactuar")]
    [SerializeField] private Text interact;
    [Tooltip("Texto que representa que tecla está asignada al inventario")]
    [SerializeField] private Text inventory;
    /*, pause*/

    // Último botón elegido
    private GameObject currentKey;

    private void Start()
    {
        left.text = InputManager.keys["Left"].ToString();
        right.text = InputManager.keys["Right"].ToString();
        jump.text = InputManager.keys["Jump"].ToString();
        attack.text = InputManager.keys["Attack"].ToString();
        dash.text = InputManager.keys["Dash"].ToString();
        interact.text = InputManager.keys["Interact"].ToString();
        inventory.text = InputManager.keys["Inventory"].ToString();
        //pause.text = InputManager.keys["Pause"].ToString();
    }

    // Reasignamos una tecla y la representamos
    private void OnGUI()
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
                InputManager.keys[currentKey.name] = currentEvent.keyCode;
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

    public void SaveKeys()
    {
        InputManager.SaveKeys();
    }
}
