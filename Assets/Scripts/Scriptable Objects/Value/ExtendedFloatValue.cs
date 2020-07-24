using UnityEngine;

// Scriptable Object que extiende a Float Value permitiendo almacenar más valores dentro del mismo objeto
[CreateAssetMenu(menuName = "Value/Extended Float Value")]
[System.Serializable]
public class ExtendedFloatValue : FloatValue
{
    [Tooltip("Valor máximo actual durante el juego")]
    public float maxValue;
    [Tooltip("Valor durante la partida")]
    public float runtimeValue;

    // Reseta al valor por defecto para empezar nueva partida
    public override void Reset()
    {
        maxValue = runtimeValue = initialValue;
    }
}
