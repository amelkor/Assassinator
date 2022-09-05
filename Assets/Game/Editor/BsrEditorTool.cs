using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using ColorUtility = UnityEngine.ColorUtility;

namespace Game.Editor
{
    [Flags]
    public enum ColorOptions
    {
        Content = 1 << 0,
        Background = 1 << 1
    }

    public static class Colors
    {
        public static Color FromHTML(string hex) => ColorUtility.TryParseHtmlString(hex, out var color) ? color : Color.black;
        
        public static Color red { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; } = new(0.8980392f, 0.2235294f, 0.2078431f, 1f);
        public static Color lightRed { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; } = new(0.8980392f, 0.4352941f, 0.3764706f, 1f);
        public static Color purple { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; } = new(0.5568628f, 0.1411765f, 0.6666667f, 1f);
        public static Color blue { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; } = new(0.1176471f, 0.5333334f, 0.8980392f, 1f);
        public static Color green { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; } = FromHTML("#4b830d");
        public static Color lightGreen { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; } = new(0.4862745f, 0.7019608f, 0.2588235f, 1f);
    }

    internal static class BsrEditorTool
    {
        private static UnityEngine.Color _cachedContentColor = GUI.contentColor;
        private static UnityEngine.Color _cachedBackgroundColor = GUI.backgroundColor;

        public static void BeginColor(UnityEngine.Color color, ColorOptions options)
        {
            if (options.HasFlagFast(ColorOptions.Content))
            {
                _cachedContentColor = GUI.contentColor;
                GUI.contentColor = color;
            }

            if (options.HasFlagFast(ColorOptions.Background))
            {
                _cachedBackgroundColor = GUI.backgroundColor;
                GUI.backgroundColor = color;
            }
        }

        public static void BeginColor(UnityEngine.Color color, Func<bool> predicate, ColorOptions options)
        {
            if (predicate())
                BeginColor(color, options);
        }

        public static void BeginColor(Func<Color> predicate, ColorOptions options)
        {
            var color = predicate();
            BeginColor(color, options);
        }

        public static void EndColor(ColorOptions options)
        {
            if (options.HasFlagFast(ColorOptions.Content)) GUI.contentColor = _cachedContentColor;
            if (options.HasFlagFast(ColorOptions.Background)) GUI.backgroundColor = _cachedBackgroundColor;
        }
    }

    internal static class ColorOptionsExtensions
    {
        public static bool HasFlagFast(this ColorOptions value, ColorOptions flag) => (value & flag) != 0;
    }
}