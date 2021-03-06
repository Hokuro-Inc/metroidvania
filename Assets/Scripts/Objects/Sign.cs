﻿using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    [Tooltip("Referencia al objeto de tipo Text que mostrará el texto")]
    [SerializeField] private Text dialogText;
    [Tooltip("Referencia al objeto que almacena el texto")]
    [SerializeField] private GameObject dialogBox;
    [Tooltip("Texto a mostrar")]
    [TextArea]
    [SerializeField] private string dialog;

    // Aseguramos que se inicializa desactivado
    private void Start()
    {       
        dialogBox.SetActive(false);
    }

    // Comprueba si se interactua para activar o desactivar de nuevo el objeto
    /*private void Update()
    {
        if (Input.GetKeyDown(InputManager.keys["Interact"]) && inRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }*/

    // Extiende la funcionalidad base desactivando el objeto en caso de alejarnos
    /*protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (other.gameObject.layer == playerLayer && !other.isTrigger)
        {
            dialogBox.SetActive(false);
        }
    }*/

    public override void Interact()
    {
        if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }
        else
        {
            dialogBox.SetActive(true);
            dialogText.text = dialog;
        }
    }
}
