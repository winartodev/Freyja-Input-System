using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;

using NewInputSystem = UnityEngine.InputSystem.InputSystem;

namespace Freyja.InputSystem
{
    [AddComponentMenu("Freyja/Input System/Input Device Type Listener")]
    public class InputDeviceTypeListener : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private PlayerInput m_PlayerInput;

        #endregion

        #region Properties

        public DeviceType _activeDevice;

        #endregion

        #region Methods

        private void OnEnable()
        {
            NewInputSystem.onActionChange += OnDeviceChange;
        }

        private void OnDisable()
        {
            NewInputSystem.onActionChange -= OnDeviceChange;
        }

        private void OnDeviceChange(object obj, InputActionChange actionChange)
        {
            if (actionChange == InputActionChange.ActionPerformed)
            {
                var inputAction = (InputAction)obj;
                var activeControl = inputAction.activeControl;

                DeviceType activeDeviceTmp;
                if (activeControl.device is Keyboard || activeControl.device is Mouse)
                {
                    activeDeviceTmp = DeviceType.Keyboard;
                }
                else if (activeControl.device is Gamepad)
                {
                    activeDeviceTmp = DeviceType.Gamepad;
                    if (activeControl.device is XInputController)
                    {
                        // TODO: Handle input specific to XInputController (e.g., Xbox controllers). 
                    }
                    else if (activeControl.device is DualShockGamepad)
                    {
                        // TODO: Handle input specific to DualShockGamepad (e.g., PlayStation controllers).
                    }
                }
                else
                {
                    activeDeviceTmp = DeviceType.Others;
                }

                if (_activeDevice != activeDeviceTmp)
                {
                    _activeDevice = activeDeviceTmp;
                    InputDeviceTypeEvent.HandleChangeInputDevice(_activeDevice);
                }
            }
        }

        public InputBinding GetBinding(string actionName, DeviceType deviceType)
        {
            return m_PlayerInput.actions[actionName].bindings[(int)deviceType];
        }

        private void Reset()
        {
            if (m_PlayerInput == null)
            {
                m_PlayerInput = GetComponent<PlayerInput>();
            }
        }

        #endregion
    }
}