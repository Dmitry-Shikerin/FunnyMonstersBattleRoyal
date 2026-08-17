using System;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUtils.Extensions
{
    public static class MathExt
    {
        public static float Normalize(this float value, float min, float max)
        {
            float result = (value - min) / (max - min);
            return Mathf.Clamp01(result);
        }
        
        /// <summary>
        /// Переводит значение из одного диапазона в другой
        /// </summary>
        /// <param name="value">Входное значение</param>
        /// <param name="inMin">Минимум входного диапазона</param>
        /// <param name="inMax">Максимум входного диапазона</param>
        /// <param name="outMin">Минимум выходного диапазона</param>
        /// <param name="outMax">Максимум выходного диапазона</param>
        /// <returns>Сконвертированное значение</returns>
        public static float Map(this float value, float inMin, float inMax, 
            float outMin, float outMax)
        {
            // Проверка деления на ноль
            if (Math.Abs(inMax - inMin) < float.Epsilon)
                throw new ArgumentException("Входной диапазон не может быть нулевым");
            
            return (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
        }
    
        /// <summary>
        /// Переводит значение с проверкой выхода за границы
        /// </summary>
        public static float MapClamped(this float value, float inMin, float inMax, 
            float outMin, float outMax)
        {
            double result = Map(value, inMin, inMax, outMin, outMax);
        
            // Ограничиваем результат выходными границами
            return Math.Max(outMin, (float)Math.Min(outMax, result));
        }
    }
}