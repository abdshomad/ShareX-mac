using System;
using System.Runtime.InteropServices;

namespace ShareX.HelpersLib.Platform
{
    public static class PlatformFactory
    {
        private static IPlatformServices _instance;
        private static readonly object _lock = new object();

        public static IPlatformServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = CreatePlatformServices();
                        }
                    }
                }
                return _instance;
            }
        }

        private static IPlatformServices CreatePlatformServices()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return new MacPlatformServices();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsPlatformServices();
            }
            else
            {
                // For other platforms (Linux, etc.), return a basic implementation
                return new MacPlatformServices(); // Use Mac implementation as fallback
            }
        }

        public static void Reset()
        {
            lock (_lock)
            {
                _instance = null;
            }
        }
    }
}
