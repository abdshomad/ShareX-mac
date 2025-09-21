using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ShareX.Simple
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Avalonia.Markup.Xaml.AvaloniaXamlLoader.Load(this);
        }

        private void OnNewCapture(object? sender, RoutedEventArgs e)
        {
            OnScreenCapture(sender, e);
        }

        private async void OnScreenCapture(object? sender, RoutedEventArgs e)
        {
            try
            {
                if (StatusText != null)
                    StatusText.Text = "Capturing screen...";
                
                await Task.Run(() =>
                {
                    // Use screencapture command-line tool
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
                        // Save to desktop
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string fileName = $"ShareX_ScreenCapture_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
                        string filePath = Path.Combine(desktopPath, fileName);
                        
                        File.Move(tempFile, filePath);
                        
                        Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                        {
                            if (StatusText != null)
                                StatusText.Text = $"Screen captured: {fileName}";
                            if (HistoryListBox != null)
                                HistoryListBox.Items.Add($"Screen captured: {fileName}");
                            ShowNotification("ShareX", $"Screen captured: {fileName}");
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                if (StatusText != null)
                    StatusText.Text = $"Error: {ex.Message}";
                if (HistoryListBox != null)
                    HistoryListBox.Items.Add($"Error capturing screen: {ex.Message}");
            }
        }

        private void OnRegionCapture(object? sender, RoutedEventArgs e)
        {
            try
            {
                if (StatusText != null)
                    StatusText.Text = "Region capture - Select area with mouse...";
                
                // For MVP, we'll capture the entire screen and let user crop later
                // In a full implementation, we'd show a region selection overlay
                OnScreenCapture(sender, e);
                if (StatusText != null)
                    StatusText.Text = "Region capture completed (full screen for MVP)";
            }
            catch (Exception ex)
            {
                if (StatusText != null)
                    StatusText.Text = $"Error: {ex.Message}";
                if (HistoryListBox != null)
                    HistoryListBox.Items.Add($"Error capturing region: {ex.Message}");
            }
        }

        private void OnUploadFile(object? sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Filters.Add(new FileDialogFilter { Name = "All Files", Extensions = { "*" } });
                dialog.Filters.Add(new FileDialogFilter { Name = "Images", Extensions = { "png", "jpg", "jpeg", "gif", "bmp" } });
                
                var result = dialog.ShowAsync(this).Result;
                if (result?.Length > 0)
                {
                    if (StatusText != null)
                        StatusText.Text = $"File selected: {Path.GetFileName(result[0])}";
                    if (HistoryListBox != null)
                        HistoryListBox.Items.Add($"File selected: {result[0]}");
                    // TODO: Implement file upload
                    ShowNotification("ShareX", $"File selected: {Path.GetFileName(result[0])}");
                }
            }
            catch (Exception ex)
            {
                if (StatusText != null)
                    StatusText.Text = $"Error: {ex.Message}";
                if (HistoryListBox != null)
                    HistoryListBox.Items.Add($"Error selecting file: {ex.Message}");
            }
        }

        private void OnAbout(object? sender, RoutedEventArgs e)
        {
            var dialog = new Window
            {
                Title = "About ShareX for macOS",
                Width = 400,
                Height = 300,
                Content = new TextBlock
                {
                    Text = "ShareX for macOS\n\nA cross-platform screen capture and file sharing tool.\n\nVersion: 1.0.0\nBuilt with Avalonia UI",
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                    TextWrapping = Avalonia.Media.TextWrapping.Wrap
                }
            };
            dialog.ShowDialog(this);
        }

        private void OnExit(object? sender, RoutedEventArgs e)
        {
            Close();
        }

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
    }
}
