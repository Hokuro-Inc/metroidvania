using UnityEngine;

[System.Serializable]
public class Loot
{
    [Tooltip("Objeto del loot")]
    public Item item;
    [Tooltip("Probabilidad del loot")]
    public float chance;
}

// Scriptable Object de loots de los enemigos
[CreateAssetMenu(menuName = "Inventory/Loot")]
public class LootTable : ScriptableObject
{
    [Tooltip("Posibles drops del enemigo")]
    public Loot[] loots;

    // Devuelve o no un objeto como loot en función de la probabilidad
    public Item LootItem()
    {
        float cumulatedProb = 0f;
        float currentProb = Random.Range(0f, 100f);
        for(int i = 0; i < loots.Length; i++)
        {
            cumulatedProb += loots[i].chance;
            if (currentProb <= cumulatedProb)
            {
                return loots[i].item;
            }
        }
        return null;
    }
}
