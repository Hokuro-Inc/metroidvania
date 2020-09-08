using Cinemachine;
using Hokuro.Functions;
using UnityEngine;

namespace Hokuro.CameraUtils
{
    // Class for camera helper methods
    public class CameraUtils
    {
        private const float moveAmount = 100f;
        private const float edgeSize = 20f;
        private const float zoomChangeAmount = 80f;

        /// <summary>
        /// Moves the camera by moving the mouse towards the edge of the screen
        /// </summary>
        public static void HandleEdgeScrolling(ref float xPos, ref float yPos)
        {
            if (Input.mousePosition.x > Screen.width - edgeSize)
            {
                xPos += moveAmount * Time.deltaTime;
            }
            if (Input.mousePosition.x < edgeSize)
            {
                xPos -= moveAmount * Time.deltaTime;
            }
            if (Input.mousePosition.y > Screen.height - edgeSize)
            {
                yPos += moveAmount * Time.deltaTime;
            }
            if (Input.mousePosition.y < edgeSize)
            {
                yPos -= moveAmount * Time.deltaTime;
            }
        }

        /// <summary>
        /// Moves camera with keys
        /// </summary>
        public static void HandleManualMovement(ref float xPos, ref float yPos, KeyCode upKey, KeyCode downKey, KeyCode rightKey, KeyCode leftKey)
        {
            if (Input.GetKeyDown(rightKey))
            {
                xPos += moveAmount * Time.deltaTime;
            }
            if (Input.GetKeyDown(leftKey))
            {
                xPos -= moveAmount * Time.deltaTime;
            }
            if (Input.GetKeyDown(upKey))
            {
                yPos += moveAmount * Time.deltaTime;
            }
            if (Input.GetKeyDown(downKey))
            {
                yPos -= moveAmount * Time.deltaTime;
            }
        }

        /// <summary>
        /// Changes camera zoom with the mouse wheel
        /// </summary>
        public static void HandleZoom(ref float zoom)
        {
            if (Input.mouseScrollDelta.y > 0f)
            {
                zoom -= zoomChangeAmount * Time.deltaTime * 10f;
            }
            if (Input.mouseScrollDelta.y < 0f)
            {
                zoom += zoomChangeAmount * Time.deltaTime * 10f;
            }
        }

        /// <summary>
        /// Shakes screen for a certain time and certain intensity with normal camera
        /// </summary>
        public static void ShakeScreen(float intensity, float timer, Camera camera)
        {
            Vector3 lastCameraMovement = Vector3.zero;
            UpdateFunction.Create(() =>
            {
                timer -= Time.deltaTime;
                Vector3 randomMovement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * intensity;
                camera.transform.position = camera.transform.position - lastCameraMovement + randomMovement;
                lastCameraMovement = randomMovement;
                return timer <= 0f;
            }, "CameraShake");
        }

        /// <summary>
        /// Shakes screen for a certain time and certain intensity with cinemachine camera
        /// </summary>
        public static void ShakeScreen(float intensity, float timer, CinemachineVirtualCamera camera)
        {
            float totalTimer = timer;
            float startingIntensity = intensity;
            CinemachineBasicMultiChannelPerlin cinemachineBMCP = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBMCP.m_AmplitudeGain = intensity;
            UpdateFunction.Create(() =>
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    cinemachineBMCP.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (timer / totalTimer));
                    if (startingIntensity < 0.1f) cinemachineBMCP.m_AmplitudeGain = 0f;
                }
                return cinemachineBMCP.m_AmplitudeGain == 0f;
            }, "CameraShake");
        }
    }
}