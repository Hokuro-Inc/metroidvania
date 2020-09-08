using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hokuro.Utils
{
    public class TextUI
    {
        public GameObject gameObject;
        public RectTransform rectTransform;
        public Transform transform;

        public TextUI(Transform parent, Vector2 anchoredPosition, string text, Color color, int fontSize, Font font)
        {
            SetupParent(parent, anchoredPosition);
            SetColor(color);
            Utils.DrawTextUI(text, transform, Vector2.zero, fontSize, font);
        }

        private void SetupParent(Transform parent, Vector2 anchoredPosition)
        {
            gameObject = new GameObject("TextUI", typeof(RectTransform));
            transform = gameObject.transform;
            rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.SetParent(parent, false);
            rectTransform.sizeDelta = new Vector2(0f, 0f);
            rectTransform.anchorMin = new Vector2(0f, 0.5f);
            rectTransform.anchorMax = new Vector2(0f, 0.5f);
            rectTransform.pivot = new Vector2(0f, 0.5f);
            rectTransform.anchoredPosition = anchoredPosition;
        }

        public void SetColor(Color color)
        {
            foreach (Transform transform in transform)
            {
                Text text = transform.GetComponent<Text>();
                if (text != null)
                {
                    text.color = color;
                }
            }
        }

        public float GetWidth()
        {
            foreach (Transform transform in transform)
            {
                Text text = transform.GetComponent<Text>();
                if (text != null)
                {
                    return text.preferredWidth;
                }
            }
            return 0f;
        }

        public float GetHeight()
        {
            foreach (Transform transform in transform)
            {
                Text text = transform.GetComponent<Text>();
                if (text != null)
                {
                    return text.preferredHeight;
                }
            }
            return 0f;
        }

        public void AddOutline(Color color, float size)
        {
            foreach (Transform transform in transform)
            {
                if (transform.GetComponent<Text>() != null)
                {
                    Outline outline = transform.gameObject.AddComponent<Outline>();
                    outline.effectColor = color;
                    outline.effectDistance = new Vector2(size, size);
                }
            }
        }

        public void SetAnchorMiddle()
        {
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        }

        public void CenterOnPosition(Vector2 position)
        {
            rectTransform.anchoredPosition = position + new Vector2(-GetWidth() / 2f, 0f);
        }
    }
}