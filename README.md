# Charles' Inferno

A 3D platformer made in Unity. Climb a tower as fast as you can while the lava keeps rising beneath you. Find hidden chests to unlock new abilities, such as a double jump and a grappling hook, and use them to reach the top.

Developed as a university assignment for the *3D Modelling and Game Development* course.

## Gameplay

- Climb to the top of the level before the rising lava catches you.
- Open chests scattered around the level to unlock abilities.
- Reach checkpoints to save your progress. After dying, you respawn at the last checkpoint with the lava reset to the height it had when you reached it.
- Your completion time is shown on the victory screen.

## Controls

| Input | Action |
|---|---|
| `W` `A` `S` `D` / `Arrow up` `Arrow down` `Arrow left` `Arrow right` | Move |
| Mouse | Look around |
| `Left Shift` | Run |
| `Space` | Jump (press again in the air with the double jump) |
| `E` | Open a chest |
| `1` / `2` / `3` | Select an unlocked ability |
| Hold `Left Mouse Button` | Use the grappling hook (while it is selected) |
| `Esc` / `P` | Pause menu |

## Features

- **Third-person character controller** built on `CharacterController`, with a Mixamo character and idle, walk, run and jump animations driven by an Animator state machine.
- **Orbit camera** that follows the player and supports mouse look with a clamped vertical angle.
- **Ability system**
  - Chests unlock abilities. Each chest has an `AbilityType` set in the Inspector.
  - Opening a chest raises a static C# event that the player inventory and the HUD listen to, so the chest does not need a reference to either.
  - Abilities derive from an abstract `Ability` base class with `OnEquip` / `OnUnequip` hooks and can be selected with the number keys.
  - **Double jump**: allows one extra jump in the air.
  - **Grappling hook**: a Raycast from the crosshair finds a grappable surface (set through a layer mask), a `LineRenderer` draws the rope, and the player is pulled towards the hook point.
- **Rising lava** with an animated Shader Graph material. Touching it kills the player.
- **Checkpoints** that store the player position, camera direction and lava height, with respawn handling (including resetting the player's vertical velocity and cancelling an active grapple).
- **Sound effects** for chest opening and checkpoints.
- **UI**: main menu, pause menu, game over screen, victory screen with the run time, highscores screen, ability HUD, and on-screen hints for checkpoints and chest interaction.

## Technical highlights

- **Raycast** for grappling hook targeting.
- **LineRenderer** for the rope.
- **Shader Graph** for the lava surface (world-space noise with scrolling and colour blending).
- Event-driven communication between chests, inventory and HUD (`static event Action<AbilityType>`).
- Scene management with a separate victory scene and panels for pause and game over.

## Getting started

### Requirements

- Unity (version used for development: see `ProjectSettings/ProjectVersion.txt`)
- Universal Render Pipeline (URP) and the Input System package (both included in the project)

### Running the project

1. Clone the repository.
2. Open the project folder in Unity Hub using the Unity version listed in `ProjectSettings/ProjectVersion.txt`.
3. Open the `MainMenu` scene and press **Play**.

### Building

Open **File → Build Profiles**, make sure the scenes are listed in this order: `MainMenu`, `Game`, `Victory`, `Highscores`, then build for your platform.

## Project structure

```
Assets/
  Scenes/       MainMenu, Game, Victory
  Scripts/      Gameplay, UI and ability scripts
  Prefabs/      Chest, checkpoint, platforms and other reusable objects
  Audio/        Sound effects
  Settings/     Render pipeline and volume settings
ProjectSettings/
```

### Main scripts

| Script | Purpose |
|---|---|
| `PlayerMovement` | Character movement, jumping and gravity |
| `CameraMovement` | Third-person camera and mouse look |
| `PlayerInventory` | Stores unlocked abilities and handles ability selection |
| `Ability`, `DoubleJumpAbility`, `GrapplingHookAbility` | Ability base class and its implementations |
| `Chest` | Opens, plays its animation and sound, and grants an ability |
| `CheckpointController` | Saves the respawn state when the player reaches a checkpoint |
| `GameData` | Static holder of the checkpoint data |
| `LavaRise` | Moves the lava upwards and detects the player touching it |

## Credits

- Character model and animations: [Mixamo](https://www.mixamo.com/)
- Sound effects: [Freesound](https://freesound.org/)
- Built with [Unity](https://unity.com/)

## Author

Balázs Laczkó-Albert

3D Modelling and Game Development

Eszterházy Károly Katolikus Egyetem
