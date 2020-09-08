using Hokuro.Functions;
using Hokuro.Utils;
using System;
using UnityEngine;

namespace Hokuro.Debugging
{
    public static class Debugging
    {
        /// <summary>
        /// Creates a Button in the UI
        /// </summary>
        public static SpriteUI ButtonUI(Vector2 anchoredPosition, string text, Action ClickFunc)
        {
            return SpriteUI.CreateDebugButton(anchoredPosition, new Vector2(80, 50), ClickFunc, Color.green, text);
        }

        /// <summary>
        /// Debugs a line to draw a proyectile
        /// </summary>
        public static void DebugProjectile(Vector3 from, Vector3 to, float speed, float projectileSize)
        {
            Vector3 dir = (to - from).normalized;
            Vector3 pos = from;
            UpdateFunction.Create(() => {
                Debug.DrawLine(pos, pos + dir * projectileSize);
                float distanceBefore = Vector3.Distance(pos, to);
                pos += dir * speed * Time.deltaTime;
                float distanceAfter = Vector3.Distance(pos, to);
                if (distanceBefore < distanceAfter)
                {
                    return true;
                }
                return false;
            });
        }
    }
}