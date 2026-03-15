# CleanBaseline for AutoCAD

A C# plugin built with the AutoCAD .NET API to optimize engineering and drafting workflows. This tool helps clean up drawing files by allowing users to export selected elements to a new drawing while keeping the exact original coordinates, or by quickly managing the state of all layers.

## 🚀 Features

* **Main Command (`CleanBaseEric`):** The unified entry point that guides the user interactively through command-line options (`[Yes/Close/ActiveLay]`).
* **Smart Element Export:** Allows the user to select specific objects in the current drawing and instantly copy them to a new, blank file (based on the `acad.dwt` template). It uses the `WblockCloneObjects` method to guarantee that the original spatial coordinates are 100% preserved.
* **Batch Layer Processing (`ActiveLay`):** A cleanup module that scans the layer table of the current document. It turns on, thaws, and unlocks all layers in the project. The code includes built-in protection to skip the current active layer, preventing the AutoCAD `eInvalidLayer` fatal error.

## 🛠️ Technologies Used

* C#
* AutoCAD .NET API (`accoremgd.dll`, `acmgd.dll`, `acdbmgd.dll`)

## ⚙️ How to Install and Use (Local Setup)

1. **Build the Project:**
   * Open the solution in Visual Studio.
   * Make sure the build platform is set to `x64`.
   * Build the solution (`Ctrl + Shift + B`) to generate the `.dll` file.

2. **Load into AutoCAD:**
   * Open AutoCAD and any drawing file (`.dwg`).
   * In the command line, type `NETLOAD` and press Enter.
   * Navigate to the `bin\Debug\net8.0\` folder (or your corresponding framework version) and select the `CleanBaseline_eric.dll` file.

3. **Run the Plugin:**
   * Type the command `CleanBaseEric` in the command line.
   * Select the desired elements when prompted.
   * Choose the next action:
     * `Yes`: Creates the new file and copies the selected elements.
     * `ActiveLay`: Processes and unlocks all layers in the current file.
     * `Close`: Safely cancels the operation.

## 👨‍💻 Author

Developed by **Eric Castro**, a Civil Engineer transitioning into Software Development. This project automates and improves engineering workflows.
