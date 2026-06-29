<h1 align="center">🚀 Scrappy-2D — The Way Back Home</h1>

<p align="center">
  <em>A 2D console-style game built from scratch with C# and the XNA Framework.</em><br/>
  <strong>BSc Software Engineering · Semester 1 Group Project (Group 19)</strong>
</p>

<p align="center">
  <a href="https://youtu.be/FAmaDrKhM9A">
    <img src="https://img.shields.io/badge/▶_Demo-Watch_on_YouTube-red?style=for-the-badge&logo=youtube" alt="Demo"/>
  </a>
  <img src="https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=csharp" alt="C#"/>
  <img src="https://img.shields.io/badge/Framework-XNA-blue?style=for-the-badge" alt="XNA"/>
  <img src="https://img.shields.io/badge/Theme-UN_SDGs-009EDB?style=for-the-badge" alt="SDGs"/>
</p>

---

## 🌍 About the Project

*The Way Back Home* is a 2D isometric-style console game tied to the **UN Sustainable Development Goals (SDGs)**. An alien has crash-landed on Earth and must complete environmental cleaning tasks to collect spaceship parts and find a way home.

The project was pushed well beyond the expected console-application scope — real 2D graphics rendering was implemented using the **XNA Framework**, making it the most technically ambitious delivery in the group. As the sole developer on the graphics and engine layer, the full rendering pipeline, game loop, and pathfinding system were designed and built from scratch.

---

## 🎮 Gameplay

- **Protagonist** — a stranded alien completing cleaning tasks across a tile-based map
- **Objective** — collect spaceship parts by interacting with environmental objects
- **NPC Robot** — designed to guide and explain gameplay; partially implemented due to deadline constraints and missing isometric tile assets
- **SDG Theme** — environmental cleanup as a metaphor for sustainable stewardship of Earth

---

## 🛠️ Technical Highlights

| Feature | Details |
|---|---|
| **Language** | C# (.NET) |
| **Rendering** | XNA Framework — lightweight, well-documented 2D game library |
| **Pathfinding** | Breadth-First Search (BFS) for NPC navigation across the tile grid |
| **Architecture** | Object-Oriented (OOP) following agile practices throughout the semester |
| **Team** | Group 19 — sole developer for the graphics and engine layer |

---

## 📐 Architecture & Design

The codebase is structured around the XNA `Game` class managing the update/draw loop, with custom classes for game entities, world objects, collision, and interaction. BFS was adapted to a tile-based world graph — the most technically challenging part of the project, and a significant early milestone in understanding algorithms in practice.

---

## 🚀 Getting Started

**Prerequisites:** .NET Framework + Visual Studio

```bash
git clone https://github.com/aIex-personal/scrappy-2d.git
# Open TheWayBackHome.sln in Visual Studio and run
```

---

## 🎥 Demo

[![Watch the demo](https://img.youtube.com/vi/FAmaDrKhM9A/maxresdefault.jpg)](https://youtu.be/FAmaDrKhM9A)

---

## 💡 Reflections

Implementing BFS while adhering to OOP and agile practices was a genuine challenge for a first semester project. The NPC guide robot was scoped down due to time and missing assets, but the core game loop and pathfinding system work correctly. A formative lesson in both graphics programming and the realities of scope management.