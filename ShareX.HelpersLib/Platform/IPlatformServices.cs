using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ShareX.HelpersLib.Platform
{
    public interface IPlatformServices
    {
        // Screen capture
        Bitmap CaptureScreen();
        Bitmap CaptureWindow(IntPtr windowHandle);
        Bitmap CaptureRegion(Rectangle region);
        
        // Window management
        IntPtr GetForegroundWindow();
        string GetWindowTitle(IntPtr windowHandle);
        Rectangle GetWindowRectangle(IntPtr windowHandle);
        bool IsWindowVisible(IntPtr windowHandle);
        
        // System information
        bool IsAdministrator();
        string GetOperatingSystemProductName(bool fullName);
        
        // File operations
        void SetClipboardText(string text);
        string GetClipboardText();
        void SetClipboardImage(Image image);
        Image GetClipboardImage();
        
        // Process management
        bool CreateProcess(string applicationName, string commandLine, out int processId);
        void TerminateProcess(int processId);
        
        // Hotkeys
        bool RegisterHotkey(IntPtr windowHandle, int hotkeyId, int modifiers, int key);
        bool UnregisterHotkey(IntPtr windowHandle, int hotkeyId);
        
        // Notifications
        void ShowNotification(string title, string text, string iconPath = null);
        
        // File associations
        void RegisterFileAssociation(string extension, string description, string executablePath);
        void UnregisterFileAssociation(string extension);
    }
}
