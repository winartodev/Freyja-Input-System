using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Freyja.InputSystem
{
    [AddComponentMenu("Freyja/Input System/Input Device Type Event")]
    public class InputDeviceTypeEvent : MonoBehaviour
    {
        #region Static

        private static List<InputDeviceTypeEvent> _events = new List<InputDeviceTypeEvent>();

        #endregion

        #region Fields

        public UnityEvent<DeviceType> OnDeviceEvent;

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

        public static void HandleChangeInputDevice(DeviceType deviceType)
        {
            for (var i = 0; i < _events.Count; i++)
            {
                _events[i].OnDeviceEvent?.Invoke(deviceType);
            }
        }

        #endregion

        #endregion
    }
}