using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game
{
    public static class MathTool
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ScaleInRange(Vector2 value, Vector2 xRange, Vector2 yRange)
        {
            Vector2 result;

            result.x = value.x > 0
                ? Map0(value.x, 1, xRange.y)
                : Map0(value.x, 1, xRange.x);

            result.y = value.y > 0
                ? Map0(value.y, 1, yRange.y)
                : Map0(value.y, 1, yRange.x);

            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Map0(float x, float inMax, float outMax)
        {
            return x * outMax / inMax;
        }
    }
}