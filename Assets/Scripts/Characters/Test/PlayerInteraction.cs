using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerBase))]
public class PlayerInteraction : MonoBehaviour
{
    private PlayerBase playerBase;
    private PlayerInput playerInput;
    private GameObject otherGameObject = null;
    private int interactableLayerIndex;
    private bool canInteract = false;
    private bool interacting = false;

    private void Awake()
    {
        playerBase = GetComponent<PlayerBase>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        interactableLayerIndex = LayerMask.NameToLayer("Interactable");
    }

    private void Update()
    {
        if (canInteract && playerInput.HandleInteraction())
        {
            interacting = !interacting;
            otherGameObject.GetComponent<IInteract>().Interact();
            playerBase.SetState(interacting ? State.interacting : State.moving);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == interactableLayerIndex)
        {
            canInteract = true;
            otherGameObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == interactableLayerIndex)
        {
            canInteract = false;
            otherGameObject = null;
        }
    }
}
