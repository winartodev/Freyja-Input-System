using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

using NewInputSystem = UnityEngine.InputSystem.InputSystem;

namespace Freyja.InputSystem
{
    [AddComponentMenu("Freyja/Input System/Input Scheme Listener")]
    [RequireComponent(typeof(PlayerInput))]
    public class InputSchemeListener : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private PlayerInput m_PlayerInput;

        [SerializeField]
        private string m_CurrentScheme;

        #endregion

        #region Methods

        private void OnEnable()
        {
            NewInputSystem.onEvent += OnEventChanged;
        }

        private void OnDisable()
        {
            NewInputSystem.onEvent -= OnEventChanged;
        }

        private void HandleChangeInputScheme()
        {
            var currentScheme = m_PlayerInput.currentControlScheme;
            if (m_CurrentScheme == currentScheme)
            {
                return;
            }

            m_CurrentScheme = currentScheme;

            InputSchemeEvents.InvokeInputSchemeEvent(m_CurrentScheme);
        }

        private void OnEventChanged(InputEventPtr eventPtr, InputDevice inputDevice)
        {
            if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
            {
                return;
            }

            var controls = inputDevice.allControls;
            var buttonPressed = NewInputSystem.settings.defaultButtonPressPoint;

            for (var i = 0; i < controls.Count; i++)
            {
                var control = controls[i] as ButtonControl;
                if (control == null || control.synthetic || control.noisy)
                {
                    continue;
                }

                if (control.ReadValueFromEvent(eventPtr, out var value) && value >= buttonPressed)
                {
                    break;
                }
            }


            HandleChangeInputScheme();
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