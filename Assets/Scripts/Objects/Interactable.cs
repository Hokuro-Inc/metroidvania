using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteract
{
    [Tooltip("Señal que activa el aviso")]
    [SerializeField] protected Signal context;

    // Capa del jugador
    /*protected int playerLayer;

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    // Si se acerca al objeto se activa la señal
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer && !other.isTrigger)
        {
            context.Raise();
            inRange = true;
        }
    }

    // Si no se está en rango o se aleja demasiado se desactiva la señal
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer && !other.isTrigger)
        {
            context.Raise();
            inRange = false;
        }
    }*/

    public abstract void Interact();
}
