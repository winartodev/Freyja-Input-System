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

        public InputDeviceType ActiveInputDevice { get; private set; }

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

                InputDeviceType activeInputDeviceTmp;
                if (activeControl.device is Keyboard || activeControl.device is Mouse)
                {
                    activeInputDeviceTmp = InputDeviceType.Keyboard;
                }
                else if (activeControl.device is Gamepad)
                {
                    activeInputDeviceTmp = InputDeviceType.Gamepad;
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
                    activeInputDeviceTmp = InputDeviceType.Others;
                }

                if (ActiveInputDevice != activeInputDeviceTmp)
                {
                    ActiveInputDevice = activeInputDeviceTmp;
                    InputDeviceTypeEvent.HandleChangeInputDevice(ActiveInputDevice);
                }
            }
        }

        public InputBinding GetBinding(string actionName, InputDeviceType inputDeviceType)
        {
            return m_PlayerInput.actions[actionName].bindings[(int)inputDeviceType];
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