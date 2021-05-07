using UnityEngine;

namespace Utils {
    public static class GameObjectExtensions {
        public static T GetComponentInAncestry<T> (this GameObject gameObject) where T : MonoBehaviour {
            var component = gameObject.GetComponent<T>();
            if (component == null) {
                component = GetComponentInAncestry<T>(gameObject.transform.parent.gameObject);
            }
            return component;
        }
    }
}
