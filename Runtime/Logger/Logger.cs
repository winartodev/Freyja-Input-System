using Log = Freyja.Logger.Logger;

namespace Freyja.InputSystem
{
    public static class Logger
    {
        private static Log _show;

        public static Log Show
        {
            get
            {
                if (_show == null)
                {
                    _show = Log.AddLog("Freyja Input System");
                }

                return _show;
            }
        }
    }
}