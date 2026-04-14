# Lab 4 — Histogram Analysis

A Windows Forms application built in C# that demonstrates basic image processing using OpenCV (via `cvlib`). The app lets you load a color image, convert it to grayscale manually using unsafe pointer operations, and visualize its pixel intensity histogram.

---

## Objectives

- Load and display a color image using OpenCV's `IplImage`
- Implement grayscale conversion manually via direct memory access (unsafe code)
- Compute and render a pixel intensity histogram from the grayscale image

---

## Features

| Feature | Description |
|---|---|
| **Open Image** | Load `.jpg`, `.jpeg`, `.bmp`, or `.png` files via a file dialog |
| **Convert to Grayscale** | Manually converts BGR → grayscale using average formula `(R+G+B)/3` via unsafe pointer arithmetic |
| **Show Histogram** | Computes a 256-bin intensity histogram and renders it as a bar chart |

---

## Project Structure

```
lab4_histogram_analysis/
├── Form1.cs          # Main form — all image processing logic lives here
├── Form1.Designer.cs # Auto-generated UI layout (3 PictureBoxes + menu strip)
└── Program.cs        # Entry point
```

### Key Logic in `Form1.cs`

- **`openToolStripMenuItem_Click`** — loads image with `CvLoadImage`, resizes it to fit `pictureBox1`
- **`convertToGrayToolStripMenuItem_Click`** — iterates over every pixel using raw memory pointers, writes the average gray value to all 3 channels, stores result in `grayImage`
- **`histogramToolStripMenuItem_Click`** — counts pixel frequencies into a `int[256]` array, normalizes, and draws vertical bars onto a `Bitmap` displayed in `pictureBox3`

---

## Setup & Requirements

- **IDE:** Visual Studio (Windows)
- **Framework:** .NET Framework (Windows Forms)
- **Dependencies:** OpenCV via `cvlib` wrapper (must be referenced in the project)
- **Build setting:** Project must allow **unsafe code** (`Project Properties → Build → Allow unsafe code`)

### Run Steps

1. Clone or download the project
2. Open `lab4_histogram_analysis.sln` in Visual Studio
3. Ensure `cvlib` is referenced and OpenCV DLLs are present
4. Enable unsafe code in project settings
5. Build and run (`F5`)

### Usage Order

> The menu items must be used **in order** — skipping a step will show an error prompt.

```
File → Open Image  →  Image → Convert to Grayscale  →  Image → Show Histogram
```