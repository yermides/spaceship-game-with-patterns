using UnityEngine;
using System;

namespace Helpers
{
    public static class GameObjectExtension
    {
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component 
        {
            return gameObject.TryGetComponent<T>(out T _);
        }
        
        public static bool HasComponent(this GameObject gameObject, Type type) 
        {
            return gameObject.TryGetComponent(type, out var _);
        }
        
        public static bool HasAllComponents(this GameObject gameObject, params Type[] types)
        {
            foreach (var type in types)
            {
                if (!gameObject.HasComponent(type))
                {
                    return false;
                }
            }

            return true;
        }
        
        public static bool HasAnyComponents(this GameObject component, params Type[] types)
        {
            foreach (var type in types)
            {
                if (component.HasComponent(type))
                {
                    return true;
                }
            }

            return false;
        }

        public static T GetOrAddComponent<T>(this GameObject component) where T : Component 
        {
            if (!component.HasComponent<T>())
            {
                return component.AddComponent<T>();
            }
            else
            {
                return component.GetComponent<T>();
            }
        }
    }
}