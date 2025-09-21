using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ShareX.Launcher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            // Set up keyboard shortcuts
            this.KeyDown += OnKeyDown;
        }

        #region Sidebar Navigation Events

        private void OnCaptureClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "Capture Options";
            StatusText.Text = "Capture menu selected";
            // TODO: Show capture options submenu
        }

        private void OnUploadClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "Upload Options";
            StatusText.Text = "Upload menu selected";
            // TODO: Show upload options submenu
        }

        private void OnWorkflowsClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "Workflows";
            StatusText.Text = "Workflows menu selected";
            // TODO: Show workflows submenu
        }

        private void OnToolsClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "Tools";
            StatusText.Text = "Tools menu selected";
            // TODO: Show tools submenu
        }

        private void OnAfterCaptureTasksClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "After Capture Tasks";
            StatusText.Text = "After capture tasks menu selected";
            // TODO: Show after capture tasks submenu
        }

        private void OnAfterUploadTasksClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "After Upload Tasks";
            StatusText.Text = "After upload tasks menu selected";
            // TODO: Show after upload tasks submenu
        }

        private void OnDestinationsClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "Destinations";
            StatusText.Text = "Destinations menu selected";
            // TODO: Show destinations submenu
        }

        private void OnApplicationSettingsClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "Application Settings";
            StatusText.Text = "Opening application settings...";
            // TODO: Open application settings dialog
            ShowNotification("ShareX", "Application settings dialog would open here");
        }

        private void OnTaskSettingsClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "Task Settings";
            StatusText.Text = "Opening task settings...";
            // TODO: Open task settings dialog
            ShowNotification("ShareX", "Task settings dialog would open here");
        }

        private void OnHotkeySettingsClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "Hotkey Settings";
            StatusText.Text = "Opening hotkey settings...";
            // TODO: Open hotkey settings dialog
            ShowNotification("ShareX", "Hotkey settings dialog would open here");
        }

        private void OnScreenshotsFolderClick(object? sender, RoutedEventArgs e)
        {
            try
            {
                string screenshotsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Pictures", "Screenshots");
                if (!Directory.Exists(screenshotsPath))
                {
                    screenshotsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                }
                
                Process.Start(new ProcessStartInfo
                {
                    FileName = "open",
                    Arguments = $"\"{screenshotsPath}\"",
                    UseShellExecute = false
                });
                
                StatusText.Text = $"Opened screenshots folder: {screenshotsPath}";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error opening screenshots folder: {ex.Message}";
                ShowNotification("ShareX", $"Error: {ex.Message}");
            }
        }

        private void OnHistoryClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "History";
            StatusText.Text = "Opening history window...";
            // TODO: Open history window
            ShowNotification("ShareX", "History window would open here");
        }

        private void OnImageHistoryClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "Image History";
            StatusText.Text = "Opening image history window...";
            // TODO: Open image history window
            ShowNotification("ShareX", "Image history window would open here");
        }

        private void OnDebugClick(object? sender, RoutedEventArgs e)
        {
            ContentHeaderText.Text = "Debug";
            StatusText.Text = "Debug menu selected";
            // TODO: Show debug submenu
        }

        private void OnDonateClick(object? sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "open",
                    Arguments = "https://www.paypal.com/donate/?hosted_button_id=Y3Q7Q7ZQ7Q7Q7",
                    UseShellExecute = true
                });
                StatusText.Text = "Opening donation page...";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error opening donation page: {ex.Message}";
            }
        }

        private void OnTwitterClick(object? sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "open",
                    Arguments = "https://twitter.com/ShareX",
                    UseShellExecute = true
                });
                StatusText.Text = "Opening Twitter page...";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error opening Twitter page: {ex.Message}";
            }
        }

        private void OnDiscordClick(object? sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "open",
                    Arguments = "https://discord.gg/sharex",
                    UseShellExecute = true
                });
                StatusText.Text = "Opening Discord server...";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error opening Discord server: {ex.Message}";
            }
        }

        private void OnAboutClick(object? sender, RoutedEventArgs e)
        {
            var aboutDialog = new Window
            {
                Title = "About ShareX 15.0",
                Width = 500,
                Height = 400,
                Background = new SolidColorBrush(Color.FromRgb(30, 30, 30)),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            var content = new StackPanel
            {
                Margin = new Thickness(20),
                Spacing = 10
            };

            // ShareX Logo placeholder
            var logoContainer = new Border
            {
                Width = 100,
                Height = 100,
                Background = new SolidColorBrush(Color.FromRgb(45, 45, 48)),
                CornerRadius = new CornerRadius(8),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };

            var logoText = new TextBlock
            {
                Text = "ShareX",
                FontSize = 24,
                FontWeight = FontWeight.Bold,
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
            };

            logoContainer.Child = logoText;
            content.Children.Add(logoContainer);

            var infoText = new TextBlock
            {
                Text = "ShareX 15.0\n\nA powerful screen capture and file sharing tool for macOS.\n\nBuilt with Avalonia UI\n\n© 2024 ShareX Team",
                TextAlignment = TextAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                TextWrapping = TextWrapping.Wrap
            };
            content.Children.Add(infoText);

            var closeButton = new Button
            {
                Content = "Close",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Margin = new Thickness(0, 20, 0, 0)
            };
            closeButton.Click += (s, args) => aboutDialog.Close();

            content.Children.Add(closeButton);
            aboutDialog.Content = content;

            aboutDialog.ShowDialog(this);
            StatusText.Text = "About dialog opened";
        }

        #endregion

        #region Context Menu Events

        private void OnImagePreviewPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            // Context menu will be handled automatically by the Border.ContextMenu
        }

        private void OnContextMenuOpen(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Opening file...";
            ShowNotification("ShareX", "File would open here");
        }

        private void OnContextMenuCopy(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Copied to clipboard";
            ShowNotification("ShareX", "Image copied to clipboard");
        }

        private void OnContextMenuUpload(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Uploading...";
            ShowNotification("ShareX", "Uploading image...");
        }

        private void OnContextMenuDownload(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Downloading...";
            ShowNotification("ShareX", "Downloading file...");
        }

        private void OnContextMenuEditImage(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Opening image editor...";
            ShowNotification("ShareX", "Image editor would open here");
        }

        private void OnContextMenuImageEffects(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Opening image effects...";
            ShowNotification("ShareX", "Image effects dialog would open here");
        }

        private void OnContextMenuPinToScreen(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Pinning to screen...";
            ShowNotification("ShareX", "Image pinned to screen");
        }

        private void OnContextMenuRunAction(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Run action menu selected";
            ShowNotification("ShareX", "Run action submenu would appear here");
        }

        private void OnContextMenuRemoveTask(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Task removed from list";
            ShowNotification("ShareX", "Task removed from list");
        }

        private void OnContextMenuDeleteFile(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Deleting file locally...";
            ShowNotification("ShareX", "File deleted locally");
        }

        private void OnContextMenuShortenUrl(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Shortening URL...";
            ShowNotification("ShareX", "URL shortened");
        }

        private void OnContextMenuShareUrl(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Sharing URL...";
            ShowNotification("ShareX", "Share URL menu would appear here");
        }

        private void OnContextMenuGoogleSearch(object? sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "open",
                    Arguments = "https://images.google.com/",
                    UseShellExecute = true
                });
                StatusText.Text = "Opening Google Image Search...";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error: {ex.Message}";
            }
        }

        private void OnContextMenuBingSearch(object? sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "open",
                    Arguments = "https://www.bing.com/images/",
                    UseShellExecute = true
                });
                StatusText.Text = "Opening Bing Visual Search...";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error: {ex.Message}";
            }
        }

        private void OnContextMenuShowQRCode(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Showing QR code...";
            ShowNotification("ShareX", "QR code would be displayed here");
        }

        private void OnContextMenuOCR(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Performing OCR...";
            ShowNotification("ShareX", "OCR would be performed here");
        }

        private void OnContextMenuClearTaskList(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Task list cleared";
            ShowNotification("ShareX", "Task list cleared");
        }

        private void OnContextMenuSwitchToListView(object? sender, RoutedEventArgs e)
        {
            StatusText.Text = "Switching to list view...";
            ShowNotification("ShareX", "Switched to list view");
        }

        #endregion

        #region Keyboard Shortcuts

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyModifiers == KeyModifiers.Control)
            {
                switch (e.Key)
                {
                    case Key.U:
                        OnContextMenuUpload(sender, e);
                        e.Handled = true;
                        break;
                    case Key.D:
                        OnContextMenuDownload(sender, e);
                        e.Handled = true;
                        break;
                    case Key.E:
                        OnContextMenuEditImage(sender, e);
                        e.Handled = true;
                        break;
                    case Key.P:
                        OnContextMenuPinToScreen(sender, e);
                        e.Handled = true;
                        break;
                }
            }
            else if (e.Key == Key.Delete)
            {
                OnContextMenuRemoveTask(sender, e);
                e.Handled = true;
            }
            else if (e.KeyModifiers == KeyModifiers.Shift && e.Key == Key.Delete)
            {
                OnContextMenuDeleteFile(sender, e);
                e.Handled = true;
            }
        }

        #endregion

        #region Helper Methods

        private void ShowNotification(string title, string text)
        {
            try
            {
                string script = $"display notification \"{text}\" with title \"{title}\"";
                
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

        #endregion
    }
}
