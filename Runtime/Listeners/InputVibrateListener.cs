using System;
using System.Collections;
using System.Collections.Generic;

using Freyja.InputSystem.Listeners;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Freyja.InputSystem.Vibrate
{
    [AddComponentMenu("Freyja/Input System/Input Vibrate Listener")]
    [RequireComponent(typeof(InputDeviceTypeListener))]
    public class InputVibrateListener : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private PlayerInput m_PlayerInput;

        [SerializeField]
        private InputDeviceTypeListener m_InputDeviceTypeListener;

        [Space(8)]
        [SerializeField]
        private List<VibrationSchemeData> m_VibrationSchemes;

        #endregion

        #region Privates

        private Coroutine _crOnVibrate;

        #endregion

        #region Methods

        public void SetVibration(float lowFreq, float highFreq, float duration = 0.5f)
        {
            StopCurrentVibration();

            if (m_InputDeviceTypeListener.ActiveDevice != DeviceType.Gamepad)
            {
                return;
            }

            var vibration = GetVibrationScheme();
            if (vibration == null)
            {
                return;
            }

            if (duration > 0.0f)
            {
                _crOnVibrate = StartCoroutine(Coroutine_SetVibration(vibration.InputVibrate, lowFreq, highFreq, duration));
            }
            else
            {
                vibration.InputVibrate?.SetVibration(lowFreq, highFreq);
            }
        }

        private void StopCurrentVibration()
        {
            if (_crOnVibrate != null)
            {
                StopCoroutine(_crOnVibrate);
                _crOnVibrate = null;
            }
        }

        private VibrationSchemeData GetVibrationScheme()
        {
            var controlScheme = m_PlayerInput.currentControlScheme;
            var vibration = m_VibrationSchemes.Find(data => data.Scheme == controlScheme);

            if (vibration == null || vibration.InputVibrate == null)
            {
                Logger.Show.LogError($"Input Vibrate with scheme '{controlScheme}' could not be found.");
                return null;
            }

            return vibration;
        }

        private void Reset()
        {
            if (m_PlayerInput == null)
            {
                m_PlayerInput = GetComponent<PlayerInput>();
            }

            if (m_InputDeviceTypeListener == null)
            {
                m_InputDeviceTypeListener = GetComponent<InputDeviceTypeListener>();
            }

            var gamepadVibration = GetComponentInChildren<InputGamepadVibration>();
            if (gamepadVibration == null)
            {
                var newGameObject = new GameObject("Gamepad Vibration");
                newGameObject.transform.SetParent(transform);

                newGameObject.AddComponent<InputGamepadVibration>();
            }

            if (m_VibrationSchemes == null || m_VibrationSchemes.Count == 0)
            {
                m_VibrationSchemes = new List<VibrationSchemeData>
                {
                    new VibrationSchemeData("Gamepad", gamepadVibration),
                };
            }
        }

        #endregion

        #region Coroutines

        private IEnumerator Coroutine_SetVibration(InputVibrate vibrate, float lowFreq, float highFreq, float duration = 0.5f)
        {
            vibrate.SetVibration(lowFreq, highFreq);

            yield return new WaitForSeconds(duration);

            vibrate.SetVibration(0, 0);

            _crOnVibrate = null;
        }

        #endregion

        #region Inner Class

        [Serializable]
        public class VibrationSchemeData
        {
            #region Fields

            [SerializeField]
            private string m_Scheme;

            [SerializeField]
            private InputVibrate m_InputVibrate;

            #endregion

            #region Properties

            public string Scheme
            {
                get => m_Scheme;
            }

            public InputVibrate InputVibrate
            {
                get => m_InputVibrate;
            }

            #endregion

            #region Constructors

            public VibrationSchemeData(string scheme, InputVibrate inputVibrate)
            {
                m_Scheme = scheme;
                m_InputVibrate = inputVibrate;
            }

            #endregion
        }

        #endregion
    }
}