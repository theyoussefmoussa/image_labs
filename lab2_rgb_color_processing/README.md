# 🔵🟢🔴 Lab 2 — RGB Color Channel Processing

A Windows Forms application that loads a digital image and isolates individual **RGB color channels** using direct memory manipulation via unsafe pointers and the legacy **OpenCV 1.x** (`cvlib`) wrapper in C#.

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
- Loading a digital image from disk using OpenCV
- Understanding the **BGR memory layout** of image pixels
- Isolating the Red, Green, or Blue channel using unsafe pointer arithmetic
- Displaying the original and processed images side by side

---

## 🖥️ Prerequisites

- **OS:** Windows only
- **IDE:** Visual Studio 2022 / 2026
- **.NET Framework:** 4.8
- **Architecture:** x86 — x64 will **not** work
- **Unsafe Code:** must be enabled in project settings

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

### 2. Enable Unsafe Code
```
Project → Properties → Build → ✅ Allow unsafe code
```

### 3. Add Reference
- Right click **References** → **Add Reference**
- Browse and select `files/openCV.dll`

### 4. Add UI Controls

| Control | Name |
|---|---|
| PictureBox (original) | `pictureBox1` |
| PictureBox (processed) | `pictureBox2` |
| MenuStrip → File → Open | `openToolStripMenuItem` |
| MenuStrip → Channels → Red | `redToolStripMenuItem` |
| MenuStrip → Channels → Green | `greenToolStripMenuItem` |
| MenuStrip → Channels → Blue | `blueToolStripMenuItem` |
| OpenFileDialog | `openFileDialog1` |

---

## 🧩 Code Walkthrough

### Fields
```csharp
IplImage image1; // Original loaded image
IplImage img;    // Processed image (after channel filtering)
```

---

### Open & Load Image
Same as Lab 1 — loads the image in color mode and displays it resized in `pictureBox1`.

```csharp
image1 = cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);
```

---

### BGR Memory Layout

OpenCV stores pixels in **BGR order** (not RGB). Each pixel occupies 3 consecutive bytes in memory:

```
index = (width × row × channels) + (col × channels)

index + 0 → Blue
index + 1 → Green
index + 2 → Red
```

To isolate a channel, the other two are zeroed out while the target channel is copied from the source.

---

### Channel Filtering (Core Logic)

```csharp
unsafe
{
    int srcIndex, dstIndex;

    for (int r = 0; r < img.height; r++)
        for (int c = 0; c < img.width; c++)
        {
            srcIndex = dstIndex = (img.width * r * img.nChannels) + (c * img.nChannels);

            *(byte*)(dstAdd + dstIndex + 0) = 0;                               // Blue  = 0
            *(byte*)(dstAdd + dstIndex + 1) = 0;                               // Green = 0
            *(byte*)(dstAdd + dstIndex + 2) = *(byte*)(srcAdd + srcIndex + 2); // Red   = original
        }
}
```

The same pattern is reused for Green and Blue — only the kept index changes.

---

### Full Red Channel Handler

```csharp
private void redToolStripMenuItem_Click(object sender, EventArgs e)
{
    img = cvlib.CvCreateImage(
        new CvSize(image1.width, image1.height),
        image1.depth,
        image1.nChannels
    );

    int srcAdd = image1.imageData.ToInt32();
    int dstAdd = img.imageData.ToInt32();

    unsafe
    {
        int srcIndex, dstIndex;

        for (int r = 0; r < img.height; r++)
            for (int c = 0; c < img.width; c++)
            {
                srcIndex = dstIndex = (img.width * r * img.nChannels) + (c * img.nChannels);

                *(byte*)(dstAdd + dstIndex + 0) = 0;                               // Blue
                *(byte*)(dstAdd + dstIndex + 1) = 0;                               // Green
                *(byte*)(dstAdd + dstIndex + 2) = *(byte*)(srcAdd + srcIndex + 2); // Red
            }
    }

    CvSize size = new CvSize(pictureBox2.Width, pictureBox2.Height);
    IplImage resized_image = cvlib.CvCreateImage(size, img.depth, img.nChannels);
    cvlib.CvResize(ref img, ref resized_image, cvlib.CV_INTER_LINEAR);

    pictureBox2.BackgroundImage = (System.Drawing.Image)resized_image;
}
```

Green and Blue handlers follow the exact same structure — only the zeroed/kept byte index differs.

---

### Channel Index Summary

| Channel | Kept Index | Zeroed Indices |
|---|---|---|
| 🔴 Red | `+ 2` | `+ 0`, `+ 1` |
| 🟢 Green | `+ 1` | `+ 0`, `+ 2` |
| 🔵 Blue | `+ 0` | `+ 1`, `+ 2` |

---

## ▶️ How to Run

1. Copy all DLLs from `files/` → `bin/Debug/`
2. Enable **Allow unsafe code** in project properties
3. `Build → Rebuild Solution`
4. Confirm platform is **x86** in the toolbar
5. Run with `Ctrl + F5`
6. Go to **File → Open** and select an image
7. Select a channel from the **Channels** menu
8. The filtered image appears in the second PictureBox ✅

---

## 🔧 Troubleshooting

| Problem | Cause | Fix |
|---|---|---|
| `DllNotFoundException` | DLLs missing from `bin/Debug/` | Copy all files from `files/` to `bin/Debug/` |
| `BadImageFormatException` | Project built as x64 | Set platform target to **x86** |
| `CS0227` compile error | Unsafe code not enabled | Enable **Allow unsafe code** in Build settings |
| Empty / black PictureBox | Image failed to load silently | Check the file path and format |
| Channel menu does nothing | `image1` is null | Open an image first before selecting a channel |
| `FileNotFoundException` on `openCV.dll` | Reference missing | Re-add via **Add Reference → Browse** |