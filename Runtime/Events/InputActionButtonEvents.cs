using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Freyja.InputSystem
{
    [AddComponentMenu("Freyja/Input System/Input Action Button Events")]
    public class InputActionButtonEvents : MonoBehaviour
    {
        [SerializeField]
        private InputActionProperty m_InputActionProperty;

        [Space(10)]
        public UnityEvent OnInputAction;

        private void OnEnable()
        {
            m_InputActionProperty.action.Enable();
            m_InputActionProperty.action.performed += OnPerformed;
        }

        private void OnDisable()
        {
            m_InputActionProperty.action.Disable();
            m_InputActionProperty.action.performed -= OnPerformed;
        }

        private void OnPerformed(InputAction.CallbackContext ctx)
        {
            OnInputAction?.Invoke();
        }
    }
}