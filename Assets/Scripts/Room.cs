using Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Tooltip("Cámara de la habitación")]
    [SerializeField] private CinemachineVirtualCamera vCam;
    [Tooltip("Enemigos asignados a la habitación")]
    [SerializeField] private Enemy[] enemies;

    // Aseguramos que las cámaras empiezan desactivadas
    void Start()
    {
        vCam.Follow = FindObjectOfType<PlayerMovementTest>().transform;
        vCam.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            vCam.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            vCam.gameObject.SetActive(false);
        }
    }

    public void RestartRoom()
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
}
