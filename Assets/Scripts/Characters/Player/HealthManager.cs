using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Tooltip("Slider que controla la barra de vida")]
    public Slider slider;
    [Tooltip("Controla la vida del jugador")]
    public ExtendedFloatValue playerCurrentHP;
    [Tooltip("Cuanto se incrementa la barra de vida por PowerUp")]
    public float amountToIncrease;
    [Tooltip("Como de rápido se mueve la barra")]
    public float changeSpeed;

    private RectTransform rt;

    // Inicializa la barra de vida
    void Start()
    {
        rt = GetComponent<RectTransform>();
        SetMaxHealth();
    }

    // Establece el máximo de vida
    public void SetMaxHealth()
    {
        slider.maxValue = playerCurrentHP.maxValue;
        slider.value = playerCurrentHP.runtimeValue;
        rt.sizeDelta = new Vector2(playerCurrentHP.maxValue, rt.sizeDelta.y);
    }

    // Actualiza la vida
    public void SetHealth()
    {
        StartCoroutine(SetHealthCo());
    }

    // Incrementa la vida máxima
    public void IncreaseHealth()
    {
        slider.maxValue += amountToIncrease;
        slider.value = slider.maxValue;
        playerCurrentHP.maxValue = slider.maxValue;
        playerCurrentHP.runtimeValue = playerCurrentHP.maxValue;
        rt.sizeDelta = new Vector2(playerCurrentHP.maxValue, rt.sizeDelta.y);
    }

    // Corrutina para suavizar el movimiento de la barra
    IEnumerator SetHealthCo()
    {
        while (Mathf.Abs(slider.value - playerCurrentHP.runtimeValue) >= 0.1f)
        {
            slider.value = Mathf.Lerp(slider.value, playerCurrentHP.runtimeValue, changeSpeed * Time.deltaTime);
            //slider.value = Mathf.MoveTowards(slider.value, PlayerCurrentHP.runtimeValue, changeSpeed * Time.deltaTime);
            yield return null;
        }
        slider.value = playerCurrentHP.runtimeValue;
    }
}
