using UnityEngine;

// Scriptable Object para almacenar valores flotantes
[CreateAssetMenu(menuName = "Value/Float Value")]
[System.Serializable]
public class FloatValue : GenericScriptableObject
{
    [Tooltip("Valor al inicio del juego")]
    public float initialValue;
}
