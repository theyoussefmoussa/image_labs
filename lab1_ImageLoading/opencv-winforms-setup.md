# OpenCV WinForms Project (Legacy cvlib / OpenCV 1.x)

This project demonstrates image loading and display using a legacy OpenCV 1.x wrapper (`cvlib`) in a Windows Forms application.

---

## 📋 Table of Contents

- [Project Overview](#-project-overview)
- [Important Notice](#️-important-notice)
- [Prerequisites](#-prerequisites)
- [Required Files](#-required-files-very-important)
- [Project Setup Steps](#️-project-setup-steps)
- [How to Run](#️-how-to-run)
- [Troubleshooting](#-troubleshooting)
- [Contributing](#-contributing)

---

## 📌 Project Overview

| Property | Value |
|---|---|
| Platform | Windows Forms (.NET Framework) |
| Framework | .NET Framework 4.8 |
| Architecture | x86 (required) |
| OpenCV Version | 1.x (legacy) |
| Wrapper | `openCV.dll` (cvlib-based) |

---

## ⚠️ Important Notice

This project uses **very old OpenCV libraries**. It is intended for:
- Digital Image Processing labs
- Educational purposes only

Not recommended for production systems.

---

## 🖥️ Prerequisites

- **OS:** Windows only
- **IDE:** Visual Studio 2019 or later
- **.NET Framework:** 4.8 (must be installed)
- **Architecture:** x86 build only — x64 will **not** work

---

## 📁 Required Files (VERY IMPORTANT)

Copy all required DLLs into your project's **`bin/Debug/`** folder (same directory as the `.exe`):

### Native OpenCV DLLs:
- `cv100.dll`
- `cxcore100.dll`
- `highgui100.dll`
- `libguide40.dll`

### Managed Wrapper:
- `openCV.dll`

---

## ⚙️ Project Setup Steps

### 1. Create Project

Create a new project:
- Type: **Windows Forms App (.NET Framework)**
- Framework: **4.8**
- Name: `lab1` (or any name)

---

### 2. Set Platform Target

Go to:

```
Project → Properties → Build
```

Set:
- Platform target: **x86**

---

### 3. Add OpenCV Reference

- Right click **References**
- Click **Add Reference**
- Browse and select `openCV.dll`

---

### 4. Add UI Components

Add the following controls:

| Control | Name |
|---|---|
| PictureBox | `pictureBox1` |
| MenuStrip → File → Open | `openToolStripMenuItem` |
| OpenFileDialog | `openFileDialog1` |

---

### 5. Connect Events

Ensure the menu item is connected to the click handler:

#### Option A — Designer
- Click the menu item
- Go to ⚡ **Events**
- Assign: `openToolStripMenuItem_Click`

#### Option B — Manual Code

```csharp
this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
```

---

### 6. Click Handler Code

Add this method to your form class:

```csharp
private void openToolStripMenuItem_Click(object sender, EventArgs e)
{
    if (openFileDialog1.ShowDialog() == DialogResult.OK)
    {
        string imagePath = openFileDialog1.FileName;
        IplImage img = Cv.LoadImage(imagePath, LoadMode.Color);
        pictureBox1.Image = BitmapConverter.ToBitmap(img);
    }
}
```

---

## ▶️ How to Run

1. **Clean Solution** — `Build → Clean Solution`
2. **Rebuild Solution** — `Build → Rebuild Solution`
3. Confirm **Platform = x86** in the toolbar
4. Run with `Ctrl + F5`
5. Use **File → Open** to load an image — it will display in the PictureBox

---

## 🔧 Troubleshooting

| Problem | Cause | Fix |
|---|---|---|
| `DllNotFoundException` | DLLs missing or wrong location | Copy all DLLs to `bin/Debug/` |
| `BadImageFormatException` | Project built as x64 | Set platform target to **x86** |
| Black / empty PictureBox | Image path is wrong or unsupported format | Use `.jpg` or `.bmp` files |
| `FileNotFoundException` on `openCV.dll` | Reference not added | Re-add reference via **Add Reference → Browse** |

---

## 🤝 Contributing

This project is intended for educational use in Digital Image Processing labs.

If you'd like to contribute:
1. Fork the repository
2. Create a new branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request
