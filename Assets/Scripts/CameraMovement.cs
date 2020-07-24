using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    // Para cuando el mapa este terminado, limitar la camara para que no se salga
    public Vector2 minPos;
    public Vector2 maxPos;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            //targetPosition.x = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
            //targetPosition.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
