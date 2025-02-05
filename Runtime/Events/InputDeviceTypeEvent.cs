using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Freyja.InputSystem
{
    [AddComponentMenu("Freyja/Input System/Events/Input Device Type Event")]
    public class InputDeviceTypeEvent : MonoBehaviour
    {
        #region Static

        private static List<InputDeviceTypeEvent> _events = new List<InputDeviceTypeEvent>();

        #endregion

        #region Fields

        public UnityEvent<InputDeviceType> OnDeviceEvent;

        #endregion

        #region Properties

        public InputDeviceType InputDeviceType { get; set; }

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

        private static void AddEvent(InputDeviceTypeEvent deviceEvent)
        {
            _events.Add(deviceEvent);
        }

        private static void RemoveEvent(InputDeviceTypeEvent deviceEvent)
        {
            _events.Remove(deviceEvent);
        }

        public static void HandleChangeInputDevice(InputDeviceType inputDeviceType)
        {
            for (var i = 0; i < _events.Count; i++)
            {
                _events[i].InputDeviceType = inputDeviceType;
                _events[i].OnDeviceEvent?.Invoke(inputDeviceType);
            }
        }

        #endregion

        #endregion
    }
}