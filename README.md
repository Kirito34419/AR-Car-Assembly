🏎️ Car Assembly Simulation (Unity)

A 3D interactive car assembly experience built in Unity.
Players can assemble a realistic Aston Martin DB11 model part by part — snapping components like the bumper, hood, seats, and doors together with accurate positioning logic.

🎯 Overview

This project simulates the step-by-step assembly of a car using separate 3D parts.
Each part can be grabbed, moved, and snapped into place using custom logic that detects proximity and automatically aligns it with the correct position and rotation.

Once assembled, the completed vehicle represents a visually accurate model, ready for future upgrades like driving physics and UI-based part selection.

🧩 Features

🪛 Interactive Assembly: Pick and snap car parts into place with collision and distance checks.

🧠 Smart Snapping System: Automatically aligns parts with target transforms when close enough.

🔄 Rotation Support: Rotate parts smoothly using the mouse scroll wheel for precise fitting.

🏗️ Hierarchical Build Order: Assemble the car step by step — bumper → radiator → engine → hood → etc.

🎮 Camera Control: Smooth orbit, zoom, and pan movement with a custom SimpleCameraController.

🧱 Core Scripts
AssemblyManager.cs

Handles the entire assembly flow — managing which part is next, verifying correct placement, and snapping pieces automatically.

if (Vector3.Distance(part.position, target.position) < snapDistance)
{
    part.position = target.position;
    part.rotation = target.rotation;
    part.SetParent(targetParent);
}

SimpleCameraController.cs

Provides smooth free-look camera movement to explore the assembly from any angle.

🚗 Current Build Order

amdb11_bumper_F.001

amdb11_bumper_R.001

amdb11_radiator.001

amdb11_engine_v8.001

amdb11_hood.001

amdb11_trunk.001

amdb11_seat_FL.001

amdb11_seat_FR.001

amdb11_steer.001

amdb11_door_L.001

amdb11_door_R.001

amdb11_wheel_03.004

amdb11_wheel_03.005

amdb11_wheel_03.006

amdb11_wheel_03.007

🌱 Future Plans

🖥️ UI Panel: Display next part to assemble and highlight progress visually.

⚙️ Physics Simulation: Realistic rigidbody interaction once all parts are assembled.

🚘 Drive Mode: Allow the player to drive away after successful assembly.

🛠️ Tech Stack

Engine: Unity 2022+

Language: C#

Model: Aston Martin DB11 (separated parts)

Platform: PC

📸 Demo Preview

(Add screenshots or GIFs of assembly steps here)

🧩 How to Use

Open the Unity project.

Load the main scene.

Move and rotate parts to snap them into place in the correct order.

Watch as the car gradually takes shape!
