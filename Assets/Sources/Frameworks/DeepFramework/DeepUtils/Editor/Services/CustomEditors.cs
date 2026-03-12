using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUtils.Editor.Services
{
    public static class CustomEditors
    {
#if UNITY_EDITOR
        public static bool DrawUIToolkitStyleToggle(bool value, string label = null)
        {
            EditorGUILayout.BeginHorizontal();

            if (!string.IsNullOrEmpty(label))
            {
                EditorGUILayout.LabelField(label, GUILayout.Width(120));
            }

            // Размеры как в UI Toolkit
            Rect toggleRect = GUILayoutUtility.GetRect(36, 20, GUILayout.ExpandWidth(false));

            // Цвета как в стандартном тугле UI Toolkit
            Color backgroundColor = value
                ? new Color(0.0f, 0.5f, 0.9f)
                : // Синий когда включен
                new Color(0.4f, 0.4f, 0.4f); // Серый когда выключен

            Color borderColor = value ? new Color(0.0f, 0.4f, 0.8f) : new Color(0.3f, 0.3f, 0.3f);

            Color thumbColor = Color.white;

            // Рисуем фон с закругленными углами (трек)
            float cornerRadius = 10f;
            DrawRoundedRectangle(toggleRect, backgroundColor, cornerRadius);

            // Рамка с закругленными углами
            DrawRoundedBorder(toggleRect, borderColor, cornerRadius, 1.5f);

            // Круглый бегунок (как в UI Toolkit)
            float thumbSize = 16f;
            float trackWidth = toggleRect.width - 4f;
            float thumbRange = trackWidth - thumbSize;

            // Позиция бегунка с небольшим отступом
            float thumbPos = value ? toggleRect.x + 2f + thumbRange : toggleRect.x + 2f;

            Rect thumbRect = new Rect(thumbPos, toggleRect.y + 2f, thumbSize, thumbSize);

            // Рисуем бегунок с тенью (как в UI Toolkit)
            DrawThumbWithShadow(thumbRect, thumbColor);

            // Обработка клика
            EditorGUIUtility.AddCursorRect(toggleRect, MouseCursor.Link);
            if (GUI.Button(toggleRect, GUIContent.none, GUIStyle.none))
            {
                value = !value;
            }

            EditorGUILayout.EndHorizontal();
            return value;
        }

// Улучшенный метод для рисования закругленного прямоугольника
        private static void DrawRoundedRectangle(Rect rect, Color color, float cornerRadius)
        {
            Texture2D roundedTex = GetCachedRoundedTexture((int)rect.width, (int)rect.height, color, cornerRadius);
            if (roundedTex != null)
            {
                GUI.DrawTexture(rect, roundedTex);
            }
        }

// Улучшенный метод для создания закругленной текстуры
        private static Texture2D CreateRoundedRectTexture(int width, int height, Color color, float cornerRadius)
        {
            Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
            Color[] pixels = new Color[width * height];

            float cornerRadiusSqr = cornerRadius * cornerRadius;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Проверяем все 4 угла
                    bool inTopLeft = (x < cornerRadius && y < cornerRadius) &&
                                     (Vector2.Distance(new Vector2(x, y), new Vector2(cornerRadius, cornerRadius)) >
                                      cornerRadius);

                    bool inTopRight = (x >= width - cornerRadius && y < cornerRadius) &&
                                      (Vector2.Distance(new Vector2(x, y),
                                          new Vector2(width - cornerRadius, cornerRadius)) > cornerRadius);

                    bool inBottomLeft = (x < cornerRadius && y >= height - cornerRadius) &&
                                        (Vector2.Distance(new Vector2(x, y),
                                            new Vector2(cornerRadius, height - cornerRadius)) > cornerRadius);

                    bool inBottomRight = (x >= width - cornerRadius && y >= height - cornerRadius) &&
                                         (Vector2.Distance(new Vector2(x, y),
                                             new Vector2(width - cornerRadius, height - cornerRadius)) > cornerRadius);

                    // Если пиксель в угловой области за пределами радиуса - делаем прозрачным
                    if (inTopLeft || inTopRight || inBottomLeft || inBottomRight)
                    {
                        pixels[y * width + x] = new Color(0, 0, 0, 0);
                    }
                    else
                    {
                        pixels[y * width + x] = color;
                    }
                }
            }

            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }

// Упрощенная версия для быстрого использования
        private static bool DrawSimpleRoundedToggle(bool value, string label = null)
        {
            EditorGUILayout.BeginHorizontal();

            if (!string.IsNullOrEmpty(label))
            {
                EditorGUILayout.LabelField(label, GUILayout.Width(120));
            }

            Rect toggleRect = GUILayoutUtility.GetRect(40, 20, GUILayout.ExpandWidth(false));

            // Цвета
            Color trackColor = value ? new Color(0.0f, 0.5f, 0.9f) : new Color(0.4f, 0.4f, 0.4f);
            Color thumbColor = Color.white;

            // Рисуем закругленный трек
            DrawRoundedRectangle(toggleRect, trackColor, 10f);

            // Круглый бегунок
            float thumbSize = 16f;
            float thumbPos = value ? toggleRect.x + toggleRect.width - thumbSize - 2f : toggleRect.x + 2f;

            Rect thumbRect = new Rect(thumbPos, toggleRect.y + 2f, thumbSize, thumbSize);
            DrawCircle(thumbRect, thumbColor);

            // Легкая обводка бегунка
            DrawCircleBorder(thumbRect, new Color(0.9f, 0.9f, 0.9f), 1f);

            // Обработка клика
            if (GUI.Button(toggleRect, GUIContent.none, GUIStyle.none))
            {
                value = !value;
            }

            EditorGUILayout.EndHorizontal();
            return value;
        }

// Вспомогательные методы (остаются без изменений)
        private static Dictionary<string, Texture2D> _cachedTextures = new Dictionary<string, Texture2D>();

        private static Texture2D GetCachedRoundedTexture(int width, int height, Color color, float cornerRadius)
        {
            string key = $"rounded_{width}_{height}_{color}_{cornerRadius}";
            if (!_cachedTextures.ContainsKey(key))
            {
                _cachedTextures[key] = CreateRoundedRectTexture(width, height, color, cornerRadius);
            }

            return _cachedTextures[key];
        }

        private static void DrawRoundedBorder(Rect rect, Color color, float cornerRadius, float borderWidth)
        {
            // Упрощенная рамка - в реальном проекте лучше сделать закругленную рамку
            EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width, borderWidth), color);
            EditorGUI.DrawRect(new Rect(rect.x, rect.yMax - borderWidth, rect.width, borderWidth), color);
            EditorGUI.DrawRect(new Rect(rect.x, rect.y, borderWidth, rect.height), color);
            EditorGUI.DrawRect(new Rect(rect.xMax - borderWidth, rect.y, borderWidth, rect.height), color);
        }

        private static void DrawThumbWithShadow(Rect rect, Color color)
        {
            // Тень
            Rect shadowRect = new Rect(rect.x + 1, rect.y + 1, rect.width, rect.height);
            DrawCircle(shadowRect, new Color(0, 0, 0, 0.1f));

            // Основной круг
            DrawCircle(rect, color);
        }

        private static void DrawCircleBorder(Rect rect, Color borderColor, float borderWidth)
        {
            float outerSize = rect.width + borderWidth * 2;
            Rect outerRect = new Rect(rect.x - borderWidth, rect.y - borderWidth, outerSize, outerSize);
            DrawCircle(outerRect, borderColor);

            float innerSize = rect.width;
            Rect innerRect = new Rect(rect.x, rect.y, innerSize, innerSize);
            DrawCircle(innerRect, Color.clear);
        }

        // Метод для рисования круга (остается из предыдущего примера)
        private static void DrawCircle(Rect rect, Color color)
        {
            Texture2D circleTex = CreateCircleTexture((int)rect.width, (int)rect.height, color);
            if (circleTex != null)
            {
                GUI.DrawTexture(rect, circleTex);
                UnityEngine.Object.DestroyImmediate(circleTex);
            }
        }

        private static Texture2D CreateCircleTexture(int width, int height, Color color)
        {
            if (width <= 0 || height <= 0) return null;

            Texture2D texture = new Texture2D(width, height);
            Color[] pixels = new Color[width * height];

            Vector2 center = new Vector2(width / 2f, height / 2f);
            float radius = Mathf.Min(width, height) / 2f;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), center);
                    pixels[y * width + x] = distance <= radius ? color : new Color(0, 0, 0, 0);
                }
            }

            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }
#endif
    }
}