# ShareX Launcher

A modern ShareX launcher application built with Avalonia UI for macOS, featuring a dark theme interface that closely matches the original ShareX design.

## Features

### 🎨 Modern Dark Theme Interface
- Dark theme styling that matches ShareX's visual design
- Clean, professional sidebar navigation
- Responsive layout with proper spacing and typography

### 📱 Sidebar Navigation
The launcher includes a comprehensive sidebar with organized sections:

#### Core Functions
- 📷 Capture
- ⬆ Upload  
- ⊞ Workflows
- 💼 Tools

#### Task Management
- 📄 After capture tasks
- ☁ After upload tasks
- 🌐 Destinations

#### Settings
- 🔧 Application settings
- ⚙ Task settings
- ⌨ Hotkey settings

#### Files
- 📁 Screenshots folder
- 🕒 History
- 🖼 Image history

#### Debug & Social
- 🚧 Debug
- ❤ Donate
- 🐦 Twitter
- 💬 Discord
- 👑 About

### 🖼 Main Content Area
- Image preview with placeholder ShareX logo
- Context menu with comprehensive actions
- Status bar showing current operations

### 🔧 Context Menu Actions
Right-click on the image preview to access:
- 📁 Open
- 📋 Copy
- ⬆ Upload (Ctrl+U)
- ⬇ Download (Ctrl+D)
- ✏ Edit image... (Ctrl+E)
- 🎨 Add image effects...
- 📌 Pin to screen (Ctrl+P)
- ▶ Run action
- ❌ Remove task from list (Del)
- 🗑 Delete file locally... (Shift+Del)
- 🔗 Shorten URL
- 📤 Share URL
- 🔍 Google image search...
- 🔍 Bing visual search (OCR)...
- 📱 Show QR code...
- 📄 OCR image...
- 🗑 Clear task list
- 📋 Switch to list view

### ⌨️ Keyboard Shortcuts
- `Ctrl+U` - Upload
- `Ctrl+D` - Download
- `Ctrl+E` - Edit image
- `Ctrl+P` - Pin to screen
- `Del` - Remove task from list
- `Shift+Del` - Delete file locally

## Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- macOS (tested on macOS 14.6.0)

### Building and Running

1. **Build the project:**
   ```bash
   cd ShareX.Launcher
   dotnet build
   ```

2. **Run the launcher:**
   ```bash
   dotnet run
   ```

3. **Or use the provided run script:**
   ```bash
   ./run-launcher.sh
   ```

### Project Structure
```
ShareX.Launcher/
├── App.axaml              # Application styling and theme
├── App.axaml.cs           # Application entry point
├── MainWindow.axaml       # Main UI layout
├── MainWindow.axaml.cs    # Main window logic
├── Program.cs             # Application startup
├── ShareX.Launcher.csproj # Project configuration
├── app.manifest           # Application manifest
└── Assets/                # Asset files directory
```

## Technical Details

### Built With
- **Avalonia UI** - Cross-platform .NET UI framework
- **.NET 9.0** - Modern .NET runtime
- **C#** - Primary programming language

### Architecture
- MVVM-ready architecture with code-behind pattern
- Event-driven UI interactions
- Modular navigation system
- Extensible context menu system

### Styling
- Custom dark theme with ShareX color palette
- Responsive design principles
- Consistent spacing and typography
- Professional icon integration

## Future Enhancements

This launcher provides the foundation for a full ShareX implementation. Future enhancements could include:

- Integration with actual ShareX capture functionality
- Real image preview and manipulation
- File upload and sharing capabilities
- Hotkey system integration
- Settings persistence
- Plugin system for extensibility

## Contributing

This is a demonstration launcher that closely replicates the ShareX interface. For contributions to the main ShareX project, please visit the official ShareX repository.

## License

This project is part of the ShareX ecosystem. Please refer to the main ShareX project for licensing information.
