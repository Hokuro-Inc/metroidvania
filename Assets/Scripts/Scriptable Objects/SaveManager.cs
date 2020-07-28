using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class SaveManager : ScriptableObject
{
    [SerializeField] private ExtendedFloatValue playerHealth;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private VectorValue playerPosition;

    public void Reset()
    {
        playerHealth.Reset();
        playerInventory.Reset();
        playerPosition.Reset();
    }
}
