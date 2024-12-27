using Freyja.InputSystem.Vibrate;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Freyja.InputSystem.Listeners
{
    [AddComponentMenu("Freyja/Input System/Input Gamepad Vibration")]
    public class InputGamepadVibration : InputVibrate
    {
        #region Methods

        public override void SetVibration(float lowFrequency, float highFrequency)
        {
            if (Gamepad.current != null)
            {
                Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
            }
        }

        #endregion
    }
}