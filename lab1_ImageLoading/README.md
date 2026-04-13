# 🟢 Lab 1 — Image Loading & Display

A Windows Forms application that loads and displays digital images using the legacy **OpenCV 1.x** (`cvlib`) wrapper in C#.

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
- Resizing the image to fit the display container
- Rendering the image inside a `PictureBox` control
- Handling common image loading errors

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
| PictureBox | `pictureBox1` |
| MenuStrip → File → Open | `openToolStripMenuItem` |
| OpenFileDialog | `openFileDialog1` |

---

## 🧩 Code Walkthrough

### Fields
```csharp
IplImage image1;
```
Holds the loaded image in OpenCV's `IplImage` format — the core image container in OpenCV 1.x.

---

### Open & Load Image
```csharp
openFileDialog1.Filter = "JPEG|*JPG|Bitmap|*.bmp|All|*.*";

if (openFileDialog1.ShowDialog() == DialogResult.OK)
{
    image1 = cvlib.CvLoadImage(
        openFileDialog1.FileName,
        cvlib.CV_LOAD_IMAGE_COLOR
    );
}
```
- Opens a file dialog filtered to `.jpg`, `.bmp`, and all files
- Loads the selected image in **color mode** using `CvLoadImage`

---

### Resize to Fit PictureBox
```csharp
CvSize size = new CvSize(pictureBox1.Width, pictureBox1.Height);

IplImage resized_image = cvlib.CvCreateImage(
    size,
    image1.depth,
    image1.nChannels
);

cvlib.CvResize(
    ref image1,
    ref resized_image,
    cvlib.CV_INTER_LINEAR
);
```
- Creates a new `IplImage` with the same dimensions as `pictureBox1`
- Resizes the original image using **bilinear interpolation** (`CV_INTER_LINEAR`) for smooth scaling

---

### Display in PictureBox
```csharp
pictureBox1.BackgroundImage = (Image)resized_image;
```
Casts the `IplImage` to a `System.Drawing.Image` and assigns it to the `PictureBox`.

---

### Full Handler
```csharp
private void openToolStripMenuItem_Click(object sender, EventArgs e)
{
    openFileDialog1.FileName = " ";
    openFileDialog1.Filter = "JPEG|*JPG|Bitmap|*.bmp|All|*.*";

    if (openFileDialog1.ShowDialog() == DialogResult.OK)
    {
        try
        {
            image1 = cvlib.CvLoadImage(
                openFileDialog1.FileName,
                cvlib.CV_LOAD_IMAGE_COLOR
            );

            CvSize size = new CvSize(
                pictureBox1.Width,
                pictureBox1.Height
            );

            IplImage resized_image = cvlib.CvCreateImage(
                size,
                image1.depth,
                image1.nChannels
            );

            cvlib.CvResize(
                ref image1,
                ref resized_image,
                cvlib.CV_INTER_LINEAR
            );

            pictureBox1.BackgroundImage = (Image)resized_image;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
```

---

## ▶️ How to Run

1. Copy all DLLs from `files/` → `bin/Debug/`
2. `Build → Rebuild Solution`
3. Confirm platform is **x86** in the toolbar
4. Run with `Ctrl + F5`
5. Go to **File → Open** and select an image
6. The image will be displayed in the PictureBox ✅

---

## 🔧 Troubleshooting

| Problem | Cause | Fix |
|---|---|---|
| `DllNotFoundException` | DLLs missing from `bin/Debug/` | Copy all files from `files/` to `bin/Debug/` |
| `BadImageFormatException` | Project built as x64 | Set platform target to **x86** |
| Empty / black PictureBox | Image failed to load silently | Check the file path and format |
| `FileNotFoundException` on `openCV.dll` | Reference missing | Re-add via **Add Reference → Browse** |
| Dialog opens but nothing displays | DLL cast failed | Ensure `image1.depth` and `nChannels` are valid |