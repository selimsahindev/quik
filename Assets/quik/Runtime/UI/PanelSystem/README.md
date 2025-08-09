# Panel System

A lightweight, extensible UI Panel System packaged with **_quik._** template, designed to handle opening, closing, and transitioning between panels in an organized way.
Built with **_quik._** Signals for event handling and **DOTween** for smooth animations.

**Note:** If you plan to use this Panel System in a different project, you’ll need to replace the current **_quik._** signaling system dependency with your own event/messaging implementation.

---

## Features

* 📦 **Decoupled Architecture** – Uses a signal-based event system for communication between components.
* 🎯 **DOTween Integration** – Animate panel transitions with minimal code.
* 🧩 **Easily Extensible** – Add new panels without modifying core logic.
* 🔄 **State-Safe** – Prevents multiple panels from overlapping unexpectedly.
* ⚡ **Lightweight & Performant** – Minimal GC allocations, clean codebase.

## Dependencies

The Panel System requires:

* **_quik._** Signals – For event publishing & subscribing.
* DOTween – For animations and transitions.

Make sure both packages are installed in your Unity project.