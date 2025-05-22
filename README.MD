# Project Hybrid - MW2 PS3 Modding Tool

This repository contains a C# Windows Forms application designed to interact with the game Modern Warfare 2 running on a PlayStation 3 console. It utilizes libraries like PS3Lib and ps3tmapi_net to communicate with the console and modify game memory or execute remote procedures.

**Disclaimer:** This project involves modifying game behavior, which may violate terms of service and could lead to account suspension or other penalties. Use at your own risk.

## Repository Structure

The main project files are located within the `ObrisBaseV3` directory:

-   **Project Hybrid 1.14.csproj**: The main project file for the C# application.
-   **Project Hybrid/**: Contains core application code.
  -   **Aimbot.cs**: Likely contains logic for an aimbot feature.
  -   **Form1.cs**: The main form for the Windows Forms application.
  -   **Program.cs**: The entry point of the application.
  -   **Ps3Memory.cs**: Handles memory reading and writing to the PS3.
  -   **RPC/**: Contains classes for Remote Procedure Calls.
    -   **Mw2Library.cs**: Specific functions and offsets for MW2.
    -   **Mw2RPC.cs**: Implementation for MW2 RPC.
    -   **RPC.cs**: General RPC implementation.
  -   **Theme/**: Contains UI styling components (e.g., FlatUI.cs).
-   **Properties/**: Project properties and resource files.
-   **bin/**: Compiled executable and dependencies.
-   **obj/**: Intermediate build files.

## Features

Based on the code files, the application likely includes features such as:

-   Connecting to a PlayStation 3 console (via Target Manager or CCAPI).
-   Reading and writing game memory.
-   Executing Remote Procedure Calls (RPC) within the game.
-   Specific game modifications for Modern Warfare 2, potentially including:
  -   Aimbot functionality.

## Requirements

-   Windows Operating System.
-   .NET Framework (version 4.5.1 or compatible).
-   Visual Studio or a compatible C# IDE to build the project.
-   A PlayStation 3 console running a compatible firmware (CFW/HEN).
-   PS3 connection software (e.g., Target Manager or CCAPI) and the necessary libraries.

## Getting Started

1.  Clone this repository.
2.  Open the `Project Hybrid 1.14.csproj` file in Visual Studio.
3.  Ensure all required dependencies (PS3Lib, ps3tmapi_net, DevComponents.DotNetBar2, etc.) are correctly referenced. You may need to obtain these libraries separately if they are not included or linked correctly.
4.  Build the project.
5.  Run the compiled executable (`ObrisBaseV3.exe` in the `bin\Debug` or `bin\Release` folder).
6.  Connect the application to your PS3 console using the provided interface.

---

*This project is provided for educational and research purposes only.*