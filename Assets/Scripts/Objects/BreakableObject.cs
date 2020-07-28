using System.Collections;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IDamage
{
    //test
    public Inventory playerInventory;
    public Item item;
    public GameObject drop;

    private IDrop[] itemDrop;

    [Tooltip("Referencia al animador")]
    private Animator anim;

    // Obtenemos el componente animador
    void Start()
    {
        itemDrop = drop.GetComponents<IDrop>();
        anim = GetComponent<Animator>();
    }

    // En caso de ser golpeado lo destruimos
    public void Damage(float damage = 0f)
    {        
        //anim.SetTrigger("Destroy");
        //test
        //playerInventory.AddItem(item);
        //itemDrop.GiveToPlayer(1);
        foreach (IDrop drop in itemDrop)
        {
            drop.GiveToPlayer(1);
        }
        //
        StartCoroutine(BreakCo());
    }

    // Corrutina para a la animación
    private IEnumerator BreakCo()
    {
        yield return new WaitForSeconds(0.3f);
        this.gameObject.SetActive(false);
    }
}
