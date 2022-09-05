using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Returns found components where key is <see cref="GameObject"/> name, value is <see cref="Component"/> itself.
        /// </summary>
        /// <typeparam name="T">Unity <see cref="Component"/> type.</typeparam>
        /// <param name="obj">This game object</param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public static Dictionary<string, T> GetComponentsInChildrenAsNameDictionary<T>(this GameObject obj, bool includeInactive = false) where T : Component
        {
            var components = obj.GetComponentsInChildren<T>(includeInactive);
            if (components.Length == 0)
                return null;
            var dict = new Dictionary<string, T>(components.Length);
            for (var i = 0; i < components.Length; i++)
            {
                if (dict.ContainsKey(components[i].name))
                    dict.Add(components[i].name + components[i].GetInstanceID().ToString(), components[i]);
                else
                    dict.Add(components[i].name, components[i]);
            }

            return dict;
        }

        public static bool TryGetGameObjectInChildrenWithName(this GameObject obj, string childName, out GameObject gameObject, bool includeInactive = false)
        {
            if (obj.TryGetComponentInChildrenWithName<Component>(childName, out var component, includeInactive))
            {
                gameObject = component.gameObject;
                return true;
            }

            gameObject = default;
            return false;
        }
        public static bool TryGetComponentInChildrenWithName<T>(this GameObject obj, string childName, out T component, bool includeInactive = false) where T : Component
        {
            var components = obj.GetComponentsInChildren<T>(includeInactive);
            if (components.Length == 0)
            {
                component = default;
                return false;
            }

            for (var i = 0; i < components.Length; i++)
            {
                // ReSharper disable once InvertIf
                if (components[i].name.Equals(childName, StringComparison.InvariantCulture))
                {
                    component = components[i];
                    return true;
                }
            }

            component = default;
            return false;
        }

        public static bool TryGetComponentInChildren<T>(this Behaviour obj, out T component, bool includeInactive = false) where T : Component
        {
            var components = obj.GetComponentsInChildren<T>(includeInactive);
            if (components.Length == 0)
            {
                component = default;
                return false;
            }

            for (var i = 0; i < components.Length; i++)
            {
                // ReSharper disable once InvertIf
                if (components[i].GetType() == typeof(T))
                {
                    component = components[i];
                    return true;
                }
            }

            component = default;
            return false;
        }
    
        public static bool TryGetComponentInChildren(this Behaviour obj, Type type, out UnityEngine.Component component, bool includeInactive = false)
        {
            var components = obj.GetComponentsInChildren(type, includeInactive);
            if (components.Length == 0)
            {
                component = default;
                return false;
            }

            for (var i = 0; i < components.Length; i++)
            {
                // ReSharper disable once InvertIf
                if (components[i].GetType() == type)
                {
                    component = components[i];
                    return true;
                }
            }

            component = default;
            return false;
        }

        /// <summary>
        /// Similar to TryGetComponent but throws exception if component missed. Use this for initialization.
        /// </summary>
        /// <exception cref="MissingComponentException">when failed to get component.</exception>
        public static T EnsureHasComponent<T>(this Behaviour gameObject) where T : Component
        {
#if UNITY_2019_3_OR_NEWER
            if (gameObject.TryGetComponent(typeof(T), out var component))
                return (T)component;
#else
            var component = gameObject.GetComponent<T>();
            if (component != null)
                return component;
#endif
            throw new MissingComponentException($"Requested component {typeof(T).Name} not exists");
        }

        public static T EnsureHasComponentInParent<T>(this Behaviour gameObject) where T : Component
        {
            var component = gameObject.GetComponentInParent(typeof(T));

            if (component == null)
                throw new MissingComponentException($"Requested component {typeof(T).Name} not exists in any parent of this gameobject");

            return (T)component;
        }

        public static T EnsureHasComponentInChildren<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponentInChildren(typeof(T), true);

            if (component == null)
                throw new MissingComponentException($"Requested component {typeof(T).Name} not exists in any parent of this gameobject");

            return (T)component;
        }

        public static T EnsureHasComponentInChildren<T>(this GameObject gameObject, string name) where T : Component
        {
            if (!gameObject.TryGetComponentInChildrenWithName<T>(name, out var component, true))
                throw new MissingComponentException($"Requested component {typeof(T).Name} not exists in any parent of this gameobject");

            return (T)component;
        }

        public static void SetParent(this GameObject gameObject, GameObject parent, bool worldPositionStays = false)
        {
            gameObject.transform.SetParent(parent.transform, worldPositionStays);
        }

        /// <summary>
        /// Sets GameObject's layer.
        /// </summary>
        /// <param name="gameObject">GameObject to set the layer to.</param>
        /// <param name="layerName">Layer name.</param>
        /// <param name="withChildren">If true, then the same layer will be applied for children.</param>
        public static void SetLayer(this GameObject gameObject, string layerName, bool withChildren)
        {
            var layer = LayerMask.NameToLayer(layerName);

            if (!withChildren)
            {
                gameObject.layer = layer;
                return;
            }

            SetLayerRecursively(gameObject.transform, layer);
        }

        private static void SetLayerRecursively(Transform transform, LayerMask layer)
        {
            transform.gameObject.layer = layer;

            for (var i = 0; i < transform.childCount; i++)
            {
                SetLayerRecursively(transform.GetChild(i), layer);
            }
        }

        public static T SetName<T>(this T obj, string name) where T : UnityEngine.Object
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("GameObject name can not be empty", nameof(name));

            obj.name = name;
            return obj;
        }
    }
}