# ⬛ Lab 3 — Grayscale Conversion

A Windows Forms application that loads a color image and converts it to **grayscale** using the **average method** via .NET's `System.Drawing.Bitmap` pixel manipulation.

---

## 📋 Table of Contents

- [Lab Overview](#-lab-overview)
- [Prerequisites](#️-prerequisites)
- [Required Files](#-required-files)
- [Project Setup](#️-project-setup)
- [Code Walkthrough](#-code-walkthrough)
- [How to Run](#️-how-to-run)
- [Troubleshooting](#-troubleshooting)

---

## 📌 Lab Overview

| Property | Value |
|---|---|
| Platform | Windows Forms (.NET Framework) |
| Framework | .NET Framework 4.8 |
| Architecture | x86 (required) |
| OpenCV Version | 1.x (legacy) |
| Wrapper | `cvlib` via `openCV.dll` |

**What this lab covers:**
- Loading a color image using OpenCV
- Converting an `IplImage` to a .NET `Bitmap` for pixel-level access
- Applying grayscale conversion using the **average method**
- Displaying the original and grayscale images side by side

---

## 🖥️ Prerequisites

- **OS:** Windows only
- **IDE:** Visual Studio 2022 / 2026
- **.NET Framework:** 4.8
- **Architecture:** x86 — x64 will **not** work

---

## 📁 Required Files

All DLLs are located in the `files/` folder. Copy them to `bin/Debug/` before running:

### Native OpenCV DLLs:
- `cv100.dll`
- `cxcore100.dll`
- `highgui100.dll`
- `libguide40.dll`

### Managed Wrapper:
- `openCV.dll`

---

## ⚙️ Project Setup

### 1. Set Platform Target
```
Project → Properties → Build → Platform target: x86
```

### 2. Add Reference
- Right click **References** → **Add Reference**
- Browse and select `files/openCV.dll`

### 3. Add UI Controls

| Control | Name |
|---|---|
| PictureBox (original) | `pictureBox1` |
| PictureBox (processed) | `pictureBox2` |
| MenuStrip → File → Open | `openToolStripMenuItem` |
| MenuStrip → Convert → Grayscale | `convertToGrayToolStripMenuItem` |
| OpenFileDialog | `openFileDialog1` |

---

## 🧩 Code Walkthrough

### Fields
```csharp
IplImage image1; // Original image loaded using OpenCV
Bitmap bmp;      // Bitmap used for pixel manipulation in .NET
```

> **Why two types?**
> OpenCV is used for loading and resizing (fast, hardware-friendly), while .NET's `Bitmap` is used for per-pixel access via `GetPixel` / `SetPixel` (simpler API, no unsafe code needed).

---

### Open & Load Image
Same as previous labs — loads the image in color mode and displays it resized in `pictureBox1`.

```csharp
image1 = cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);
pictureBox1.Image = (System.Drawing.Image)resized_image;
```

> **Note:** `pictureBox1.Image` is used here (not `BackgroundImage`) so it can be read back as a `Bitmap` in the next step.

---

### IplImage → Bitmap Conversion

```csharp
bmp = new Bitmap(pictureBox1.Image);
```

Direct casting from `IplImage` to `Bitmap` is invalid, so the image is re-read from the `PictureBox` after being assigned as a `System.Drawing.Image`.

---

### Grayscale Algorithm — Average Method

For each pixel, the grayscale intensity is computed as the arithmetic mean of the three color channels:

```
gray = (R + G + B) / 3
```

Then all three channels are set to that value, keeping the alpha unchanged:

```csharp
p   = bmp.GetPixel(x, y);

int a   = p.A;
int r   = p.R;
int g   = p.G;
int b   = p.B;
int avg = (r + g + b) / 3;

bmp.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
```

Setting `R = G = B = avg` produces a neutral gray tone with no color bias.

---

### Full Grayscale Handler

```csharp
private void convertToGrayToolStripMenuItem_Click(object sender, EventArgs e)
{
    bmp = new Bitmap(pictureBox1.Image);

    int width  = bmp.Width;
    int height = bmp.Height;
    Color p;

    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            p = bmp.GetPixel(x, y);

            int a   = p.A;
            int r   = p.R;
            int g   = p.G;
            int b   = p.B;
            int avg = (r + g + b) / 3;

            bmp.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
        }
    }

    pictureBox2.Image = bmp;
}
```

---

### Average Method vs. Luminosity Method

| Method | Formula | Notes |
|---|---|---|
| **Average** *(used here)* | `(R + G + B) / 3` | Simple, equal weight for all channels |
| **Luminosity** | `0.299R + 0.587G + 0.114B` | Matches human eye sensitivity, more accurate |

The average method is straightforward but can appear slightly washed out compared to the luminosity method.

---

## ▶️ How to Run

1. Copy all DLLs from `files/` → `bin/Debug/`
2. `Build → Rebuild Solution`
3. Confirm platform is **x86** in the toolbar
4. Run with `Ctrl + F5`
5. Go to **File → Open** and select an image
6. Go to **Convert → Grayscale**
7. The grayscale image appears in the second PictureBox ✅

---

## 🔧 Troubleshooting

| Problem | Cause | Fix |
|---|---|---|
| `DllNotFoundException` | DLLs missing from `bin/Debug/` | Copy all files from `files/` to `bin/Debug/` |
| `BadImageFormatException` | Project built as x64 | Set platform target to **x86** |
| `NullReferenceException` on convert | `pictureBox1.Image` is null | Open an image first before converting |
| `InvalidCastException` | Casting `IplImage` directly to `Bitmap` | Use `new Bitmap(pictureBox1.Image)` instead |
| Output looks identical to original | `pictureBox2.Image` not assigned | Ensure `pictureBox2.Image = bmp` is called |
| `FileNotFoundException` on `openCV.dll` | Reference missing | Re-add via **Add Reference → Browse** |