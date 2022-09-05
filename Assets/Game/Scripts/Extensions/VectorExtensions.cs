using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game
{
    public static class VectorExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float HorizontalMagnitude(this Vector3 v)
        {
            return new Vector2(v.x, v.z).magnitude;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Horizontal(this Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }
    }
}