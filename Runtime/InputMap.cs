using System.Collections.Generic;

using UnityEngine;

namespace Freyja.InputSystem
{
    [AddComponentMenu("Freyja InputSystem/Listener/Input Map Listener")]
    public class InputMap : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private string m_ActionMap;

        [SerializeField]
        private int m_Priority;

        #endregion

        #region Privates

        private List<InputMap> _inputMaps = new List<InputMap>();

        #endregion

        #region Methods

        private void OnEnable()
        {
            if (string.IsNullOrEmpty(m_ActionMap))
            {
                return;
            }
        }

        private void OnDisable()
        {
        }

        private void RemoveInputMaps()
        {
        }

        private void AddInputMaps()
        {
        }

        #endregion
    }
}