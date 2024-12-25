using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Freyja.InputSystem
{
    [AddComponentMenu("Freyja/Input System/Input Action Vector3 Events")]
    public class InputActionVector2Events : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private InputActionProperty m_InputActionProperty;

        public UnityEvent<Vector2> OnInputAction;

        #endregion

        #region Methods

        private void OnEnable()
        {
            m_InputActionProperty.action.Enable();
            m_InputActionProperty.action.performed += OnPerformedAction;
        }

        private void OnDisable()
        {
            m_InputActionProperty.action.performed -= OnPerformedAction;
        }

        private void InvokeOnInputAction(Vector2 value)
        {
            OnInputAction?.Invoke(value);
        }

        private void OnPerformedAction(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                InvokeOnInputAction(ctx.ReadValue<Vector2>());
            }
        }

        #endregion
    }
}