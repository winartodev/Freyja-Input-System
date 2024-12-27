using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Freyja.InputSystem
{
    [AddComponentMenu("Freyja/Input System/Input Action Map")]
    public class InputActionMap : MonoBehaviour
    {
        #region Static

        private static List<InputActionMap> _inputMaps = new List<InputActionMap>();

        #endregion

        #region Fields

        [SerializeField]
        private string m_ActionMap;

        [SerializeField]
        private int m_Priority;

        #endregion

        #region Properties

        public int Priority
        {
            get => m_Priority;
            set
            {
                m_Priority = value;

                SortInputMapByPriority();
            }
        }

        public string ActionMap
        {
            get => m_ActionMap;
            set
            {
                m_ActionMap = value;
                SortInputMapByPriority();
            }
        }

        #endregion

        #region Methods

        private void OnEnable()
        {
            AddInputMap(this);
        }

        private void OnDisable()
        {
            RemoveInputMap(this);
        }

        private static void AddInputMap(InputActionMap inputMap)
        {
            if (_inputMaps == null)
            {
                _inputMaps = new List<InputActionMap>();
            }

            if (_inputMaps.Contains(inputMap) || inputMap == null)
            {
                return;
            }

            _inputMaps.Add(inputMap);

            SortInputMapByPriority();
        }

        private static void RemoveInputMap(InputActionMap inputMap)
        {
            if (_inputMaps == null)
            {
                return;
            }

            _inputMaps.Remove(inputMap);

            SortInputMapByPriority();
        }

        private static void SortInputMapByPriority()
        {
            if (_inputMaps.Count <= 0)
            {
                return;
            }

            var playerInput = FindObjectOfType<PlayerInput>();
            if (playerInput == null)
            {
                return;
            }

            _inputMaps.Sort((x, y) => y.Priority.CompareTo(x.Priority));
            if (playerInput.currentActionMap.name == _inputMaps[0].ActionMap)
            {
                return;
            }

            playerInput.SwitchCurrentActionMap(_inputMaps[0].ActionMap);
        }

        #endregion
    }
}