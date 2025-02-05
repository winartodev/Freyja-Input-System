using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Freyja.InputSystem
{
    [AddComponentMenu("Freyja/Input System/Events/Input Scheme Events")]
    public class InputSchemeEvents : MonoBehaviour
    {
        #region Static

        private static List<InputSchemeEvents> _inputSchemeEvents = new List<InputSchemeEvents>();

        #endregion

        #region Fields

        public UnityEvent<string> OnInputSchemeChanged;

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

        #region Static Methods

        private static void AddEvent(InputSchemeEvents inputSchemeEvent)
        {
            _inputSchemeEvents.Add(inputSchemeEvent);
        }

        private static void RemoveEvent(InputSchemeEvents inputSchemeEvent)
        {
            _inputSchemeEvents.Remove(inputSchemeEvent);
        }

        public static void InvokeInputSchemeEvent(string inputScheme)
        {
            for (var i = 0; i < _inputSchemeEvents.Count; i++)
            {
                _inputSchemeEvents[i].OnInputSchemeChanged?.Invoke(inputScheme);
            }
        }

        #endregion

        #endregion
    }
}