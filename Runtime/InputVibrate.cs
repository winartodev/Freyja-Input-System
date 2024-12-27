using UnityEngine;

namespace Freyja.InputSystem.Vibrate
{
    public abstract class InputVibrate : MonoBehaviour
    {
        #region Methods

        protected void OnDisable()
        {
            SetVibration(0, 0);
        }

        public abstract void SetVibration(float lowFrequency, float highFrequency);

        #endregion
    }
}