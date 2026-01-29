# 🕷️ HDD Robot Spiders — Technical Test

A clean, modular, and fully testable C# solution for simulating robotic spider navigation on a grid‑based wall.  
This project demonstrates separation of concerns, async service design, validation layering, and NUnit test coverage.

---

## 📌 Problem Summary

A robotic spider is placed on a rectangular wall represented as a grid.  
It receives:

- Wall boundaries (max X, max Y)
- Starting position (x, y, direction)
- A sequence of commands:
  - `L` = turn left
  - `R` = turn right
  - `F` = move forward

The spider must execute the commands and output its final position and orientation.

---

## 🧱 Solution Architecture

This solution follows clean architecture principles and is split into multiple class libraries:

HDD.RobotSpiders/
│
├── HDD.RobotSpiders.ConsoleApp/       # Console UI (input/output only)
├── HDD.RobotSpiders.Domain/           # Core domain models and enums
├── HDD.RobotSpiders.Parser/           # Direction parsing utilities
├── HDD.RobotSpiders.Services/         # Navigation service (async)
├── HDD.RobotSpiders.Validation/       # Input validation layer
├── HDD.RobotSpiders.Tests/            # NUnit test suite
└── Solution Items/
├── .gitignore
└── README.md


---

## ▶️ Running the Application

1. Open the solution in Visual Studio.
2. Set **HDD.RobotSpiders.ConsoleApp** as the startup project.
3. Run the application.

### Example Input

7 15
2 4 Left
FLFLFRFFLF

### Example Output

3 6 Left

---

## 🧪 Running Tests

The project uses **NUnit** for unit testing.

To run tests:

- Open Test Explorer in Visual Studio  
- Or use CLI:

```bash
dotnet test

🧠 Design Principles Applied
   Clean Architecture — clear separation of domain, application, validation, and UI.
   Async/Await — future-proof service design.
   Validation Layer — prevents invalid input from reaching business logic.
   Exception Handling — meaningful error messages.
   Unit Testing — full NUnit coverage for logic and validation.
   Parser Layer — isolates string-to-enum conversion.

🚀 Extensibility
   Multiple spiders
   Logging or telemetry
   REST API or UI front-end
   Additional movement rules

