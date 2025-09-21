using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;

namespace ShareX.HelpersLib.Platform
{
    public class WindowsPlatformServices : IPlatformServices
    {
        // Reuse existing Windows APIs from NativeMethods
        public Bitmap CaptureScreen()
        {
            try
            {
                var screenBounds = Screen.PrimaryScreen.Bounds;
                var bitmap = new Bitmap(screenBounds.Width, screenBounds.Height);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(screenBounds.X, screenBounds.Y, 0, 0, screenBounds.Size);
                }
                return bitmap;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to capture screen: {ex.Message}");
                return new Bitmap(800, 600);
            }
        }

        public Bitmap CaptureWindow(IntPtr windowHandle)
        {
            try
            {
                var rect = GetWindowRectangle(windowHandle);
                var bitmap = new Bitmap(rect.Width, rect.Height);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    var hdc = graphics.GetHdc();
                    NativeMethods.PrintWindow(windowHandle, hdc, 0);
                    graphics.ReleaseHdc(hdc);
                }
                return bitmap;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to capture window: {ex.Message}");
                return new Bitmap(800, 600);
            }
        }

        public Bitmap CaptureRegion(Rectangle region)
        {
            try
            {
                var bitmap = new Bitmap(region.Width, region.Height);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(region.X, region.Y, 0, 0, region.Size);
                }
                return bitmap;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to capture region: {ex.Message}");
                return new Bitmap(region.Width, region.Height);
            }
        }

        public IntPtr GetForegroundWindow()
        {
            return NativeMethods.GetForegroundWindow();
        }

        public string GetWindowTitle(IntPtr windowHandle)
        {
            try
            {
                int length = NativeMethods.GetWindowTextLength(windowHandle);
                if (length == 0) return string.Empty;
                
                var builder = new System.Text.StringBuilder(length + 1);
                NativeMethods.GetWindowText(windowHandle, builder, builder.Capacity);
                return builder.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get window title: {ex.Message}");
                return "Unknown Window";
            }
        }

        public Rectangle GetWindowRectangle(IntPtr windowHandle)
        {
            try
            {
                NativeMethods.RECT rect;
                if (NativeMethods.GetWindowRect(windowHandle, out rect))
                {
                    return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get window rectangle: {ex.Message}");
            }
            
            return new Rectangle(0, 0, 800, 600);
        }

        public bool IsWindowVisible(IntPtr windowHandle)
        {
            return NativeMethods.IsWindowVisible(windowHandle);
        }

        public bool IsAdministrator()
        {
            try
            {
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to check administrator status: {ex.Message}");
                return false;
            }
        }

        public string GetOperatingSystemProductName(bool fullName)
        {
            try
            {
                return Environment.OSVersion.VersionString;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get OS name: {ex.Message}");
                return "Windows";
            }
        }

        public void SetClipboardText(string text)
        {
            try
            {
                Clipboard.SetText(text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to set clipboard text: {ex.Message}");
            }
        }

        public string GetClipboardText()
        {
            try
            {
                return Clipboard.GetText();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get clipboard text: {ex.Message}");
                return string.Empty;
            }
        }

        public void SetClipboardImage(Image image)
        {
            try
            {
                Clipboard.SetImage(image);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to set clipboard image: {ex.Message}");
            }
        }

        public Image GetClipboardImage()
        {
            try
            {
                return Clipboard.GetImage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get clipboard image: {ex.Message}");
                return null;
            }
        }

        public bool CreateProcess(string applicationName, string commandLine, out int processId)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = applicationName,
                    Arguments = commandLine,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                
                var process = Process.Start(startInfo);
                if (process != null)
                {
                    processId = process.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to create process: {ex.Message}");
            }
            
            processId = 0;
            return false;
        }

        public void TerminateProcess(int processId)
        {
            try
            {
                var process = Process.GetProcessById(processId);
                process.Kill();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to terminate process: {ex.Message}");
            }
        }

        public bool RegisterHotkey(IntPtr windowHandle, int hotkeyId, int modifiers, int key)
        {
            try
            {
                return NativeMethods.RegisterHotKey(windowHandle, hotkeyId, (uint)modifiers, (uint)key);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to register hotkey: {ex.Message}");
                return false;
            }
        }

        public bool UnregisterHotkey(IntPtr windowHandle, int hotkeyId)
        {
            try
            {
                return NativeMethods.UnregisterHotKey(windowHandle, hotkeyId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to unregister hotkey: {ex.Message}");
                return false;
            }
        }

        public void ShowNotification(string title, string text, string iconPath = null)
        {
            // Use Windows Forms notification for now
            try
            {
                var notification = new NotifyIcon
                {
                    Visible = true,
                    Icon = SystemIcons.Information
                };
                notification.ShowBalloonTip(3000, title, text, ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to show notification: {ex.Message}");
            }
        }

        public void RegisterFileAssociation(string extension, string description, string executablePath)
        {
            // Windows file association registration would go here
            Debug.WriteLine($"File association registration: {extension} -> {executablePath}");
        }

        public void UnregisterFileAssociation(string extension)
        {
            // Windows file association removal would go here
            Debug.WriteLine($"File association removal: {extension}");
        }
    }
}
