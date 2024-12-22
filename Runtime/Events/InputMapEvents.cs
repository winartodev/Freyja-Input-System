using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Freyja.InputSystem
{
    [AddComponentMenu("Freyja/Input System/Input Map Events")]
    public class InputMapEvents : MonoBehaviour
    {
        #region Static

        private static List<InputMapEvents> _events = new List<InputMapEvents>();

        #endregion

        #region Fields

        public UnityEvent<string> OnInputMapChanged;

        #endregion

        #region Methods

        private void OnEnable()
        {
            AddEvent(this);
        }

        private void OnDisable()
        {
            RemoveEvent(this);
        }

        private void InvokeInputMapEvent(string map)
        {
            OnInputMapChanged?.Invoke(map);
        }

        #region Static Methods

        private static void AddEvent(InputMapEvents events)
        {
            _events.Add(events);
        }

        private static void RemoveEvent(InputMapEvents events)
        {
            _events.Remove(events);
        }

        public static void HandleChangeInputMap(string map)
        {
            for (var i = 0; i < _events.Count; i++)
            {
                var inputMapEvents = _events[i];
                inputMapEvents.InvokeInputMapEvent(map);
            }
        }

        #endregion

        #endregion
    }
}