using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ShareX.HelpersLib.Platform
{
    public class MacPlatformServices : IPlatformServices
    {
        // P/Invoke declarations for macOS APIs
        [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
        private static extern IntPtr CGDisplayCreateImage(uint displayId);
        
        [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
        private static extern IntPtr CGImageCreateWithImageInRect(IntPtr image, RectangleF rect);
        
        [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
        private static extern IntPtr CGDataProviderCreateWithData(IntPtr data, byte[] bytes, int size, IntPtr releaseCallback);
        
        [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
        private static extern IntPtr CGColorSpaceCreateDeviceRGB();
        
        [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
        private static extern IntPtr CGImageCreate(int width, int height, int bitsPerComponent, int bitsPerPixel, int bytesPerRow, IntPtr colorSpace, uint bitmapInfo, IntPtr provider, float[] decode, bool shouldInterpolate, uint intent);
        
        [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
        private static extern void CGImageRelease(IntPtr image);
        
        [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
        private static extern void CGDataProviderRelease(IntPtr provider);
        
        [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
        private static extern void CGColorSpaceRelease(IntPtr colorSpace);
        
        [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
        private static extern IntPtr NSWorkspace_sharedWorkspace();
        
        [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
        private static extern void NSWorkspace_notificationCenter(IntPtr workspace);
        
        [DllImport("/usr/lib/libc.dylib")]
        private static extern int getuid();
        
        [DllImport("/usr/lib/libc.dylib")]
        private static extern int geteuid();

        public Bitmap CaptureScreen()
        {
            try
            {
                // Use screencapture command-line tool as fallback
                string tempFile = Path.GetTempFileName() + ".png";
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "screencapture",
                    Arguments = $"-x \"{tempFile}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                process?.WaitForExit();
                
                if (File.Exists(tempFile))
                {
                    var bitmap = new Bitmap(tempFile);
                    File.Delete(tempFile);
                    return bitmap;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to capture screen: {ex.Message}");
            }
            
            return new Bitmap(800, 600); // Fallback empty bitmap
        }

        public Bitmap CaptureWindow(IntPtr windowHandle)
        {
            // For MVP, capture entire screen and let user crop
            return CaptureScreen();
        }

        public Bitmap CaptureRegion(Rectangle region)
        {
            try
            {
                string tempFile = Path.GetTempFileName() + ".png";
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "screencapture",
                    Arguments = $"-x -R {region.X},{region.Y},{region.Width},{region.Height} \"{tempFile}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                process?.WaitForExit();
                
                if (File.Exists(tempFile))
                {
                    var bitmap = new Bitmap(tempFile);
                    File.Delete(tempFile);
                    return bitmap;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to capture region: {ex.Message}");
            }
            
            return new Bitmap(region.Width, region.Height); // Fallback empty bitmap
        }

        public IntPtr GetForegroundWindow()
        {
            // macOS doesn't have the same concept of "foreground window" as Windows
            // Return a placeholder for now
            return IntPtr.Zero;
        }

        public string GetWindowTitle(IntPtr windowHandle)
        {
            // Placeholder implementation
            return "Unknown Window";
        }

        public Rectangle GetWindowRectangle(IntPtr windowHandle)
        {
            // Placeholder implementation
            return new Rectangle(0, 0, 800, 600);
        }

        public bool IsWindowVisible(IntPtr windowHandle)
        {
            // Placeholder implementation
            return true;
        }

        public bool IsAdministrator()
        {
            // On macOS, check if running as root
            return getuid() == 0 || geteuid() == 0;
        }

        public string GetOperatingSystemProductName(bool fullName)
        {
            try
            {
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "sw_vers",
                    Arguments = "-productName",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                });
                
                if (process != null)
                {
                    process.WaitForExit();
                    string name = process.StandardOutput.ReadToEnd().Trim();
                    
                    if (fullName)
                    {
                        var versionProcess = Process.Start(new ProcessStartInfo
                        {
                            FileName = "sw_vers",
                            Arguments = "-productVersion",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        });
                        
                        if (versionProcess != null)
                        {
                            versionProcess.WaitForExit();
                            string version = versionProcess.StandardOutput.ReadToEnd().Trim();
                            return $"{name} {version}";
                        }
                    }
                    
                    return name;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get OS name: {ex.Message}");
            }
            
            return "macOS";
        }

        public void SetClipboardText(string text)
        {
            try
            {
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "pbcopy",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    CreateNoWindow = true
                });
                
                if (process != null)
                {
                    process.StandardInput.Write(text);
                    process.StandardInput.Close();
                    process.WaitForExit();
                }
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
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "pbpaste",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                });
                
                if (process != null)
                {
                    process.WaitForExit();
                    return process.StandardOutput.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get clipboard text: {ex.Message}");
            }
            
            return string.Empty;
        }

        public void SetClipboardImage(Image image)
        {
            try
            {
                string tempFile = Path.GetTempFileName() + ".png";
                image.Save(tempFile, ImageFormat.Png);
                
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "osascript",
                    Arguments = $"-e 'set the clipboard to (read (POSIX file \"{tempFile}\") as JPEG picture)'",
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                process?.WaitForExit();
                
                File.Delete(tempFile);
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
                string tempFile = Path.GetTempFileName() + ".png";
                
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "osascript",
                    Arguments = $"-e 'set theFile to open for access POSIX file \"{tempFile}\" with write permission' -e 'write (the clipboard as JPEG picture) to theFile' -e 'close access theFile'",
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                process?.WaitForExit();
                
                if (File.Exists(tempFile) && new FileInfo(tempFile).Length > 0)
                {
                    var image = Image.FromFile(tempFile);
                    File.Delete(tempFile);
                    return image;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get clipboard image: {ex.Message}");
            }
            
            return null;
        }

        public bool CreateProcess(string applicationName, string commandLine, out int processId)
        {
            try
            {
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = applicationName,
                    Arguments = commandLine,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                
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
            // macOS hotkey registration would require Carbon or Cocoa APIs
            // For MVP, return false to indicate not supported
            return false;
        }

        public bool UnregisterHotkey(IntPtr windowHandle, int hotkeyId)
        {
            // macOS hotkey unregistration would require Carbon or Cocoa APIs
            // For MVP, return false to indicate not supported
            return false;
        }

        public void ShowNotification(string title, string text, string iconPath = null)
        {
            try
            {
                string script = $"display notification \"{text}\" with title \"{title}\"";
                if (!string.IsNullOrEmpty(iconPath))
                {
                    script += $" sound name \"Glass\"";
                }
                
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "osascript",
                    Arguments = $"-e '{script}'",
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                process?.WaitForExit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to show notification: {ex.Message}");
            }
        }

        public void RegisterFileAssociation(string extension, string description, string executablePath)
        {
            // macOS file associations are handled through LaunchServices
            // This would require more complex implementation
            Debug.WriteLine($"File association registration not implemented for macOS: {extension}");
        }

        public void UnregisterFileAssociation(string extension)
        {
            // macOS file association removal would require LaunchServices
            Debug.WriteLine($"File association removal not implemented for macOS: {extension}");
        }
    }
}
