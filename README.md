# 🖼️ Digital Image Processing Labs (DIP)

A structured collection of Digital Image Processing laboratory exercises built with **C# Windows Forms** in **Visual Studio 2022**. Each lab demonstrates a core DIP concept — from basic image handling to histogram-based enhancement — combining theoretical understanding with practical desktop application implementation.

---

## 📋 Table of Contents

- [Repository Overview](#-repository-overview)
- [Labs Overview](#-labs-overview)
- [Project Structure](#-project-structure)
- [Prerequisites](#️-prerequisites)
- [Getting Started](#-getting-started)
- [Technologies Used](#️-technologies-used)
- [Project Goals](#-project-goals)
- [Notes](#-notes)
- [Contributing](#-contributing)

---

## 📌 Repository Overview

| Property | Value |
|---|---|
| Language | C# |
| Platform | Windows Forms (.NET Framework) |
| IDE | Visual Studio 2022 |
| Architecture | x86 |
| Domain | Digital Image Processing |

---

## 🧪 Labs Overview

### 🟢 Lab 1 — Image Loading

> Open and display digital images through an interactive desktop UI.

- Load images from disk using a file dialog
- Support for common formats: `.jpg`, `.png`, `.bmp`
- Preview images inside a `PictureBox` control
- Basic UI interaction for file selection

---

### 🎨 Lab 2 — RGB Color Processing

> Explore how digital images are represented in the RGB color model.

- Split an image into its **Red**, **Green**, and **Blue** channels
- Display each channel as a separate grayscale visualization
- Understand how color components combine to form a full-color image

---

### ⚫ Lab 3 — Grayscale Conversion

> Convert color images to grayscale and analyze intensity distribution.

- Apply standard RGB-to-grayscale conversion formula
- Visualize brightness levels across the image
- Analyze pixel intensity as a foundation for further processing

---

### 📊 Lab 4 — Histogram Analysis

> Compute and visualize the histogram of a grayscale image.

- Calculate pixel intensity frequency distribution (0–255)
- Render the histogram as a bar chart in the UI
- Understand contrast, brightness spread, and tonal range

---

### 📈 Lab 5 — Histogram Equalization

> Automatically enhance image contrast using histogram equalization.

- Apply the histogram equalization algorithm
- Redistribute pixel intensities for better visual contrast
- Side-by-side comparison of original vs. enhanced image

---

## 📁 Project Structure

```
DIP-Labs/
│
├── Lab1_ImageLoading/
│   └── ...
│
├── Lab2_RGBProcessing/
│   └── ...
│
├── Lab3_GrayscaleConversion/
│   └── ...
│
├── Lab4_HistogramAnalysis/
│   └── ...
│
├── Lab5_HistogramEqualization/
│   └── ...
│
└── README.md
```

---

## 🖥️ Prerequisites

- **OS:** Windows only
- **IDE:** Visual Studio 2022 (or later)
- **.NET Framework:** 4.8
- **Architecture:** x86 — x64 will **not** work with legacy OpenCV wrapper

> For Lab 1, additional native DLLs are required. See [Lab 1 setup guide](./Lab1_ImageLoading/README.md) for details.

---

## 🚀 Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/DIP-Labs.git
   ```

2. **Open a lab** — navigate to the desired lab folder and open the `.sln` file in Visual Studio 2022

3. **Set platform target**
   ```
   Project → Properties → Build → Platform target: x86
   ```

4. **Build and run**
   - `Build → Rebuild Solution`
   - `Ctrl + F5`

---

## ⚙️ Technologies Used

- **C#** — primary programming language
- **.NET Framework 4.8** — application framework
- **Windows Forms** — desktop UI framework
- **Visual Studio 2022** — IDE
- **OpenCV 1.x (cvlib wrapper)** — legacy image processing library *(Lab 1)*
- **GDI+ / System.Drawing** — image rendering and manipulation

---

## 🎯 Project Goals

- Implement core DIP concepts in a practical, hands-on environment
- Strengthen understanding of image representation and transformation
- Build a structured academic portfolio as a foundation for computer vision
- Prepare for advanced topics in image processing and data science

---

## 📌 Notes

- Each lab is implemented as a **standalone module** — no cross-lab dependencies
- Code is structured for **educational clarity** over production optimization
- More labs and enhancements may be added over time

---

## 🤝 Contributing

Contributions, improvements, and suggestions are welcome.

1. Fork the repository
2. Create a new branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request
