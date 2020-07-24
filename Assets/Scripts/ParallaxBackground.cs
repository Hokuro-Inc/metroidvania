using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Tooltip("Referencia a la posición de la cámara")]
    [SerializeField] private Transform cameraTransform;
    [Tooltip("Multiplicador del efecto de parallax")]
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    [Tooltip("Para hacer que la imagen sea infinita en el eje X")]
    [SerializeField] private bool infiniteHorizontal;
    [Tooltip("Para hacer que la imagen sea infinita en el eje Y")]
    [SerializeField] private bool infiniteVertical;

    // Última posición de la cámara
    private Vector3 lastCameraPos;
    // Anchura de la imagen en unidades
    private float textureUnitSizeX;
    // Altura de la imagen en unidades
    private float textureUnitSizeY;

    // Inicializamos variables y obtenemos componentes
    void Start()
    {
        lastCameraPos = cameraTransform.transform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    // Actualizamos la posición del fondo
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPos;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPos = cameraTransform.position;

        if (infiniteHorizontal)
        {
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offsetPosX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3(cameraTransform.position.x + offsetPosX, transform.position.y);
            }
        }

        if (infiniteVertical)
        {
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeX)
            {
                float offsetPosY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(cameraTransform.position.x, transform.position.y + offsetPosY);
            }
        }
    }
}
