using UnityEngine;

public enum Type
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    public Type doorType;
    public bool isOpen = false;

    private Collider2D col;

    // Start is called before the first frame update
    private void Awake()
    {
        col = GetComponent<Collider2D>();
        col.enabled = true;
    }

    // Update is called once per frame
    /*private void Update()
    {
        if (Input.GetButtonDown("Interact") && inRange)
        {
            if (!isOpen)
            {
                OpenDoor();
            }
        }
    }*/

    public void OpenDoor()
    {
        isOpen = true;
        col.enabled = false;
    }

    public override void Interact()
    {
        if (!isOpen)
        {
            OpenDoor();
        }
    }
}
