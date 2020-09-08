using UnityEngine;
using UnityEngine.UI;

public class ContextClue : MonoBehaviour
{
    [Tooltip("Aviso al jugador por objeto interactuable")]
    [SerializeField] private GameObject contextClue;

    private bool contextActive = false;
    private Image image;

    // Obtenemos el componente de la imagen y nos aseguramos de que está desactivada
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    // Activa y desactiva el aviso al jugador
    public void ChangeContext()
    {
        contextActive = !contextActive;
        image.enabled = contextActive;
    }
}
