using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Tooltip("Índice de la escena a cargar")]
    [SerializeField] private int sceneIndex;
    [Header("Vectores de posición")]
    [Tooltip("Referencia a la posición del jugador")]
    [SerializeField] private Vector2 playerPosition;
    [Tooltip("Almacena la posición en la que aparecerá el jugador al entrar en la escena")]
    [SerializeField] private VectorValue startingPosition;
    [Header("Elementos de la transición")]
    [Tooltip("Panel de entrada a la escena")]
    [SerializeField] private GameObject fadeInPanel;
    [Tooltip("Panel de salida de la escena")]
    [SerializeField] private GameObject fadeOutPanel;
    [Tooltip("Duración de la transición")]
    [SerializeField] private float fadeWait;

    void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            startingPosition.initialValue = playerPosition;
            //SceneManager.LoadScene(sceneToLoad);
            StartCoroutine(FadeCo());
        }
    }

    IEnumerator FadeCo()
    {
        if (fadeInPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
            yield return new WaitForSeconds(fadeWait);
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
}
