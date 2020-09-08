using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hokuro.Utils
{
    public class SpriteUI
    {
        private static Transform GetCanvasTransform()
        {
            return Utils.GetCanvasTransform();
        }

        public static SpriteUI CreateDebugButton(Vector2 anchoredPosition, Vector2 size, Action action, Color color, string text)
        {
            if (color.r >= 1f) color.r = .9f;
            if (color.g >= 1f) color.g = .9f;
            if (color.b >= 1f) color.b = .9f;

            Color colorOver = color * 1.1f; // button over color lighter

            SpriteUI spriteUI = new SpriteUI(GetCanvasTransform(), Resources.Load<Sprite>("White_1x1"), anchoredPosition, size, color);
            spriteUI.AddButton(action);

            TextUI textUI = new TextUI(spriteUI.gameObject.transform, anchoredPosition, text, Color.black, 12, null);
            textUI.SetAnchorMiddle();
            textUI.CenterOnPosition(Vector2.zero);

            return spriteUI;
        }

        public GameObject gameObject;
        public RectTransform rectTransform;
        public Image image;

        private SpriteUI(Transform parent, Sprite sprite, Vector2 anchoredPosition, Vector2 size, Color color)
        {
            rectTransform = Utils.DrawSprite(parent, anchoredPosition, size, sprite, color, "SpriteUI");
            gameObject = rectTransform.gameObject;
            image = gameObject.GetComponent<Image>();
            image.color = color;
        }

        private ButtonUI AddButton(Action clickFunc)
        {
            ButtonUI buttonUI = gameObject.AddComponent<ButtonUI>();
            if (clickFunc != null)
                buttonUI.clickFunc = clickFunc;
            return buttonUI;
        }
    }
}