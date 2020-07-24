using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    [Tooltip("Slider que controla la barra de mana")]
    public Slider slider;
    [Tooltip("Controla el mana del jugador")]
    public ExtendedFloatValue PlayerCurrentMana;
    [Tooltip("Cuanto se incrementa la barra de mana por PowerUp")]
    public float amountToIncrease;
    [Tooltip("Como de rápido se mueve la barra")]
    public float changeSpeed;

    private RectTransform rt;

    // Inicializa la barra de vida
    void Start()
    {
        rt = GetComponent<RectTransform>();
        SetMaxMana();
    }

    // Establece el máximo de mana
    public void SetMaxMana()
    {
        slider.maxValue = PlayerCurrentMana.maxValue;
        slider.value = PlayerCurrentMana.runtimeValue;
        rt.sizeDelta = new Vector2(PlayerCurrentMana.maxValue, rt.sizeDelta.y);
    }

    // Actualiza el mana
    public void SetMana()
    {
        StartCoroutine(SetManaCo());
    }

    // Incrementa el mana máxima
    public void IncreaseMana()
    {
        slider.maxValue += amountToIncrease;
        slider.value = slider.maxValue;
        PlayerCurrentMana.maxValue = slider.maxValue;
        PlayerCurrentMana.runtimeValue = PlayerCurrentMana.maxValue;
        rt.sizeDelta = new Vector2(PlayerCurrentMana.maxValue, rt.sizeDelta.y);
    }

    // Corrutina para suavizar el movimiento de la barra
    IEnumerator SetManaCo()
    {
        while (Mathf.Abs(slider.value - PlayerCurrentMana.runtimeValue) >= 0.1f)
        {
            slider.value = Mathf.Lerp(slider.value, PlayerCurrentMana.runtimeValue, changeSpeed * Time.deltaTime);
            //slider.value = Mathf.MoveTowards(slider.value, PlayerCurrentHP.runtimeValue, changeSpeed * Time.deltaTime);
            yield return null;
        }
        slider.value = PlayerCurrentMana.runtimeValue;
    }
}
