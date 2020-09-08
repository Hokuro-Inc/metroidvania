using UnityEngine;
using UnityEngine.UI;

namespace Hokuro.Utils
{
    public static class Utils
    {
        //private const int sortingOrderBase = 5000;
        private static Transform cachedCanvasTransform;

        /// <summary>
        /// Sorts sprites for 2D Top-Down games
        /// </summary>
        public static int SortRenderPosition(/*ref int sortOrder,*/ float yPos, int offset)
        {
            //sortOrder = (int)(sortingOrderBase - yPos - offset);
            return (int)(-yPos + offset);
        }

        /// <summary>
        /// Gets the main canvas transform
        /// </summary>
        public static Transform GetCanvasTransform()
        {
            if (cachedCanvasTransform == null)
            {
                Canvas canvas = MonoBehaviour.FindObjectOfType<Canvas>();
                if (canvas != null)
                {
                    cachedCanvasTransform = canvas.transform;
                }
            }
            return cachedCanvasTransform;
        }

        /// <summary>
        /// Gets the default proyect font
        /// </summary>
        public static Font GetDefaultFont()
        {
            return Resources.GetBuiltinResource<Font>("Arial.ttf");
        }

        /// <summary>
        /// Gets the mouse position on the scpace for 2D games
        /// </summary>
        public static Vector3 GetMouseWorldPosition2D()
        {
            Vector3 mousePosition = GetMouseWorldPosition3D(Camera.main);
            mousePosition.z = 0f;
            return mousePosition;
        }

        /// <summary>
        /// Gets the mouse position on the scpace for 3D games
        /// </summary>
        public static Vector3 GetMouseWorldPosition3D()
        {
            return GetMouseWorldPosition3D(Camera.main);
        }

        /// <summary>
        /// Gets the mouse position on the scpace for 3D games using a custom camera
        /// </summary>
        public static Vector3 GetMouseWorldPosition3D(Camera camera)
        {
            return camera.ScreenToWorldPoint(Input.mousePosition);
        }
    
        /// <summary>
        /// Generates a random normalized direction
        /// </summary>
        public static Vector3 RandomDirection()
        {
            return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        }

        /// <summary>
        /// Generates a vector given an angle
        /// </summary>
        public static Vector3 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }

        /// <summary>
        /// Generates an angle given a vector
        /// </summary>
        public static float GetAngleFromVector(Vector3 vector)
        {
            vector = vector.normalized;
            float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

            if (angle < 0) angle += 360;

            return angle;
        }

        /// <summary>
        /// Rotates a 2D vector given a rotation vector
        /// </summary>
        public static Vector3 ApplyRotationToVector(Vector3 vector, Vector3 rotationVector)
        {
            return ApplyRotationToVector(vector, GetAngleFromVector(rotationVector));
        }

        /// <summary>
        /// Rotates a 2D vector given an angle
        /// </summary>
        public static Vector3 ApplyRotationToVector(Vector3 vector, float angle)
        {
            return Quaternion.Euler(0f, 0f, angle) * vector;
        }

        /// <summary>
        /// Converts decimal value to hexadecimal string
        /// </summary>
        public static string DecToHex(int value)
        {
            return value.ToString("X2");
        }

        /// <summary>
        /// Converts hexadecimal string to decimal value
        /// </summary>
        public static int HexToDec(string hex)
        {
            return System.Convert.ToInt32(hex, 16);
        }

        /// <summary>
        /// Generates a color given an hexadecimal string
        /// </summary>
        public static Color GetColorFromHex(string hex)
        {
            float red = HexToDec(hex.Substring(0, 2));
            float green = HexToDec(hex.Substring(2, 2));
            float blue = HexToDec(hex.Substring(4, 2));

            float alpha = 255f;
            if (hex.Length >= 8)
            {
                alpha = HexToDec(hex.Substring(6, 2));
            }

            return new Color(red, green, blue, alpha);
        }

        /// <summary>
        /// Generates a hexadecimal string given a color
        /// </summary>
        public static string GetStringFromColor(Color color)
        {
            string red = DecToHex(Mathf.RoundToInt(color.r));
            string green = DecToHex(Mathf.RoundToInt(color.g));
            string blue = DecToHex(Mathf.RoundToInt(color.b));

            return red + green + blue;
        }

        /// <summary>
        /// Generates a hexadecimal string given a color with alpha
        /// </summary>
        public static string GetStringFromColorWithAlpha(Color color)
        {
            string alpha = DecToHex(Mathf.RoundToInt(color.a));

            return GetStringFromColor(color) + alpha;
        }

        /// <summary>
        /// Draw a UI Sprite
        /// </summary>
        public static RectTransform DrawSprite(Transform parent, Vector2 pos, Vector2 size, Sprite sprite = null, Color color = default, string name = null)
        {
            // Setup icon
            if (name == null || name == "") name = "Sprite";

            GameObject gameObject = new GameObject(name, typeof(RectTransform), typeof(Image));
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.SetParent(parent, false);
            rectTransform.sizeDelta = size;
            rectTransform.anchoredPosition = pos;

            Image image = gameObject.GetComponent<Image>();
            image.sprite = sprite;
            image.color = color;

            return rectTransform;
        }

        public static Text DrawTextUI(string textString, Vector2 anchoredPosition, int fontSize, Font font = null)
        {
            return DrawTextUI(textString, GetCanvasTransform(), anchoredPosition, fontSize, font);
        }
        public static Text DrawTextUI(string textString, Transform parent, Vector2 anchoredPosition, int fontSize, Font font = null)
        {
            GameObject gameObject = new GameObject("Text", typeof(RectTransform), typeof(Text));
            gameObject.transform.SetParent(parent, false);
            Transform textTransform = gameObject.transform;
            textTransform.SetParent(parent, false);
            textTransform.localPosition = Vector3.zero;
            textTransform.localScale = Vector3.one;

            RectTransform textRectTransform = gameObject.GetComponent<RectTransform>();
            textRectTransform.sizeDelta = new Vector2(0, 0);
            textRectTransform.anchoredPosition = anchoredPosition;

            Text text = gameObject.GetComponent<Text>();
            text.text = textString;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.alignment = TextAnchor.MiddleLeft;
            if (font == null) font = GetDefaultFont();
            text.font = font;
            text.fontSize = fontSize;

            return text;
        }
    }
}