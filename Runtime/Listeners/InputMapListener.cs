using UnityEngine;
using UnityEngine.InputSystem;

namespace Freyja.InputSystem
{
    [AddComponentMenu("Freyja/Input System/Listeners/Input Map Listener")]
    [RequireComponent(typeof(PlayerInput))]
    public class InputMapListener : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private PlayerInput m_PlayerInput;

        [SerializeField]
        private string m_CurrentMap;

        #endregion

        #region Methods

        private void Update()
        {
            var currentMap = m_PlayerInput.currentActionMap?.name;
            if (string.IsNullOrEmpty(currentMap))
            {
                return;
            }

            if (currentMap == m_CurrentMap)
            {
                return;
            }

            m_CurrentMap = currentMap;

            InputMapEvents.HandleChangeInputMap(m_CurrentMap);
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