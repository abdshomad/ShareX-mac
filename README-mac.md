# ShareX for macOS

A cross-platform screen capture and file sharing tool, ported from Windows to macOS using .NET 9 and Avalonia UI.

## 🚀 Features

### ✅ Currently Working (MVP)
- **Screen Capture**: Take full-screen screenshots using native macOS `screencapture` tool
- **Native macOS UI**: Modern, clean interface built with Avalonia UI
- **File Selection**: Choose files for upload with native file dialogs
- **macOS Notifications**: Native notifications using AppleScript
- **Cross-platform Architecture**: Ready for future enhancements

### 🔄 Planned Features
- **Region Capture**: Interactive area selection for screenshots
- **Image Editing**: Basic annotation and editing tools
- **Cloud Upload**: Integration with popular cloud services
- **Hotkeys**: Global keyboard shortcuts for quick capture
- **History**: Capture history and management
- **Multiple Formats**: Support for various image and video formats

## 📋 Requirements

- **macOS**: 10.15 (Catalina) or later
- **.NET 9.0**: Runtime must be installed
- **Homebrew**: For easy .NET installation (recommended)

## 🛠️ Installation

### Option 1: Using Homebrew (Recommended)

```bash
# Install .NET 9.0
brew install dotnet

# Clone the repository
git clone https://github.com/yourusername/ShareX-mac.git
cd ShareX-mac

# Build and run
dotnet run --project ShareX.Simple/ShareX.Simple.csproj
```

### Option 2: Manual Installation

1. Download and install [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
2. Clone this repository
3. Navigate to the project directory
4. Run: `dotnet run --project ShareX.Simple/ShareX.Simple.csproj`

## 🏗️ Building from Source

```bash
# Clone the repository
git clone https://github.com/yourusername/ShareX-mac.git
cd ShareX-mac

# Restore dependencies
dotnet restore

# Build the project
dotnet build ShareX.Simple/ShareX.Simple.csproj

# Run the application
dotnet run --project ShareX.Simple/ShareX.Simple.csproj
```

## 📁 Project Structure

```
ShareX-mac/
├── ShareX.Simple/              # Main macOS application (MVP)
│   ├── Program.cs             # Application entry point
│   ├── App.axaml             # Avalonia app configuration
│   ├── App.axaml.cs          # App initialization
│   ├── MainWindow.axaml      # Main UI layout
│   ├── MainWindow.axaml.cs   # UI logic and screen capture
│   └── ShareX.Simple.csproj  # Project file
├── ShareX/                    # Original Windows application (partial conversion)
├── ShareX.HelpersLib/         # Core libraries with platform abstraction
├── ShareX.ScreenCaptureLib/   # Screen capture functionality
├── ShareX.UploadersLib/       # File upload services
└── README-mac.md             # This file
```

## 🎯 How to Use

1. **Launch the Application**: Run the app using `dotnet run --project ShareX.Simple/ShareX.Simple.csproj`
2. **Take Screenshots**: Click "Screen Capture" button or use the File menu
3. **Select Files**: Use "Upload File" to choose files for sharing
4. **View History**: Check the main window for capture history

### Keyboard Shortcuts (Planned)
- `Cmd+Shift+3`: Full screen capture
- `Cmd+Shift+4`: Region capture
- `Cmd+Shift+5`: Window capture

## 🔧 Technical Details

### Architecture
- **UI Framework**: Avalonia UI for cross-platform native look
- **Runtime**: .NET 9.0 for modern performance and features
- **Platform Abstraction**: Custom `IPlatformServices` interface
- **macOS Integration**: Native `screencapture` tool and AppleScript notifications

### Key Components
- **PlatformFactory**: Automatically detects platform and provides appropriate services
- **MacPlatformServices**: macOS-specific implementations for screen capture, notifications, etc.
- **WindowsPlatformServices**: Windows compatibility layer
- **Cross-platform Graphics**: System.Drawing.Common for image processing

### Dependencies
```xml
<PackageReference Include="Avalonia" Version="11.0.12" />
<PackageReference Include="Avalonia.Desktop" Version="11.0.12" />
<PackageReference Include="Avalonia.Themes.Fluent" Version="11.0.12" />
<PackageReference Include="System.Drawing.Common" Version="9.0.0" />
```

## 🐛 Known Issues

- **Region Capture**: Currently captures full screen (MVP limitation)
- **File Upload**: Selection works, but upload services not yet implemented
- **Hotkeys**: Global shortcuts not yet implemented
- **Image Editing**: Basic editing tools planned for future releases

## 🤝 Contributing

We welcome contributions! Here's how you can help:

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/amazing-feature`
3. **Commit your changes**: `git commit -m 'Add amazing feature'`
4. **Push to the branch**: `git push origin feature/amazing-feature`
5. **Open a Pull Request**

### Development Setup
```bash
# Install development tools
brew install dotnet
brew install git

# Clone and setup
git clone https://github.com/yourusername/ShareX-mac.git
cd ShareX-mac

# Build and test
dotnet build
dotnet test  # When tests are added
```

## 📄 License

This project is licensed under the GNU General Public License v3.0 - see the [LICENSE](LICENSE.txt) file for details.

## 🙏 Acknowledgments

- **Original ShareX**: Built by the ShareX team for Windows
- **Avalonia UI**: Cross-platform UI framework for .NET
- **macOS Community**: For native tooling and best practices

## 📞 Support

- **Issues**: [GitHub Issues](https://github.com/yourusername/ShareX-mac/issues)
- **Discussions**: [GitHub Discussions](https://github.com/yourusername/ShareX-mac/discussions)
- **Documentation**: This README and inline code comments

## 🗺️ Roadmap

### Version 1.1 (Next Release)
- [ ] Region capture with interactive selection
- [ ] Basic image editing tools
- [ ] Global hotkeys
- [ ] Capture history management

### Version 1.2 (Future)
- [ ] Cloud upload services (Imgur, Google Drive, etc.)
- [ ] Video screen recording
- [ ] Advanced image effects
- [ ] Plugin system

### Version 2.0 (Long-term)
- [ ] Full feature parity with Windows ShareX
- [ ] Advanced automation features
- [ ] Custom upload destinations
- [ ] Batch processing

---

**Note**: This is an MVP (Minimum Viable Product) version. Many features from the original Windows ShareX are planned but not yet implemented. The current version focuses on core screen capture functionality with a native macOS experience.

Built with ❤️ for the macOS community.
