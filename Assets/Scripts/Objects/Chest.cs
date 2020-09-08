using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    [Tooltip("Item dentro del cofre")]
    public Item item;
    [Tooltip("Referencia al inventario del jugador para añadir el objeto al mismo")]
    public Inventory playerInventory;
    [Tooltip("Controla si está o no abierto")]
    public bool isOpen;
    [Tooltip("Lo mismo que el anterior pero se guarda entre partidas y cargas")]
    public BoolValue storedOpen;
    [Tooltip("Señal que activa la función para recibir el objeto")]
    public Signal receiveItem;
    [Tooltip("Caja del diálogo")]
    public GameObject dialogBox;
    [Tooltip("Texto del cofre")]
    public Text dialogText;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    /*
    void Update()
    {
        if (Input.GetButtonDown("Interact") && inRange)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                EmptyChest();
            }
        }
    }*/

    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = item.itemDesciption;
        playerInventory.AddItem(item);
        //playerInventory.currentItem = item;
        receiveItem.Raise();
        isOpen = true;
        context.Raise();
        anim.SetTrigger("Open");
    }

    public void EmptyChest()
    {
        dialogBox.SetActive(false);
        receiveItem.Raise();
    }

    public override void Interact()
    {
        if (!isOpen)
        {
            OpenChest();
        }
        else
        {
            EmptyChest();
        }
    }
}
