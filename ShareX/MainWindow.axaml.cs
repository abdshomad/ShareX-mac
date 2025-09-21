using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using ShareX.HelpersLib.Platform;

namespace ShareX
{
    public partial class MainWindow : Window
    {
        private IPlatformServices _platformServices;

        public MainWindow()
        {
            InitializeComponent();
            _platformServices = PlatformFactory.Instance;
        }

        private void OnNewCapture(object? sender, RoutedEventArgs e)
        {
            OnScreenCapture(sender, e);
        }

        private async void OnScreenCapture(object? sender, RoutedEventArgs e)
        {
            try
            {
                StatusText.Text = "Capturing screen...";
                
                await Task.Run(() =>
                {
                    var bitmap = _platformServices.CaptureScreen();
                    
                    // Save to desktop for now
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string fileName = $"ShareX_ScreenCapture_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
                    string filePath = Path.Combine(desktopPath, fileName);
                    
                    bitmap.Save(filePath);
                    
                    Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                    {
                        StatusText.Text = $"Screen captured: {fileName}";
                        HistoryListBox.Items.Add($"Screen captured: {fileName}");
                        _platformServices.ShowNotification("ShareX", $"Screen captured: {fileName}");
                    });
                });
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error: {ex.Message}";
                HistoryListBox.Items.Add($"Error capturing screen: {ex.Message}");
            }
        }

        private async void OnRegionCapture(object? sender, RoutedEventArgs e)
        {
            try
            {
                StatusText.Text = "Region capture - Select area with mouse...";
                
                // For MVP, we'll capture the entire screen and let user crop later
                // In a full implementation, we'd show a region selection overlay
                await OnScreenCapture(sender, e);
                StatusText.Text = "Region capture completed (full screen for MVP)";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error: {ex.Message}";
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
                    StatusText.Text = $"File selected: {Path.GetFileName(result[0])}";
                    HistoryListBox.Items.Add($"File selected: {result[0]}");
                    // TODO: Implement file upload
                    _platformServices.ShowNotification("ShareX", $"File selected: {Path.GetFileName(result[0])}");
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error: {ex.Message}";
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
    }
}
