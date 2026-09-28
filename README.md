# Meteor Shooter: Lab Submission 4 (Week 5)

**Course:** DIG4778C, Fall 2026
**Unity version:** Unity 6 (6000.0.71f1). The starter project was made in 2022.3.37f1.

## Team

- Connor Hewitt



## About the game

A simple shooter prototype. You fly a spaceship and shoot meteors. After you destroy 5 meteors, a big meteor spawns that takes 5 shots to destroy. You only have one life.

### Controls

| Action | Key |
|---|---|
| Move | WASD or arrow keys |
| Shoot | Space (1 second cooldown) |
| Restart after dying | R |

## What was changed

### 1. SOLID refactor

| Principle | How it's applied |
|---|---|
| **Single Responsibility** | The old `Player` script was split into `PlayerMovement` (movement and screen wrap) and `PlayerShooter` (firing and cooldown). Meteor spawning moved out of `GameManager` into `MeteorSpawner`. `GameManager` now only handles game state, player spawn, and restart. |
| **Open/Closed** | `EnemyBase` holds the shared falling, hit-counting and collision logic. `Meteor` and `BigMeteor` extend it and override small hooks. A new enemy type can be added by subclassing without editing existing code. |
| **Liskov Substitution** | `Meteor` and `BigMeteor` can be used anywhere an `EnemyBase` or `IDamageable` is expected. |
| **Interface Segregation** | `IDamageable` exposes a single method, `TakeHit()`. |
| **Dependency Inversion** | Enemies no longer call `GameObject.Find("GameManager")`. Scripts communicate through the static `GameEvents` class (player died, meteor destroyed, big meteor spawned, and so on), so nothing depends on a concrete `GameManager`. |

### 2. New Input System

All input was moved from the old Input Manager (`Input.GetAxis`, `Input.GetKeyDown`) to the new Input System package, reading `Keyboard.current` in `PlayerMovement`, `PlayerShooter` and `GameManager`.

### 3. Cinemachine

- **Camera tracking:** a `CinemachineCamera` with Position Composer follows the ship. `GameManager` assigns the follow target when the player spawns.
- **Screen shake:** `CameraShakeTrigger` uses a `CinemachineImpulseSource` (with an Impulse Listener on the camera) to shake the screen whenever an asteroid is destroyed.
- **Zoom:** `CameraZoomController` zooms the camera out when the big meteor spawns and back in when it is destroyed or the player dies.

## Scripts

| Script | Purpose |
|---|---|
| `GameManager` | Spawns the player, tracks game over, handles restart, sets the camera follow target |
| `MeteorSpawner` | Spawns meteors on a timer and spawns the big meteor after 5 kills |
| `PlayerMovement` | Ship movement and screen wrap |
| `PlayerShooter` | Laser firing and cooldown |
| `Laser` | Laser projectile movement |
| `EnemyBase` | Shared enemy behavior (abstract base class) |
| `Meteor` | Regular meteor (1 hit) |
| `BigMeteor` | Big meteor (5 hits) |
| `IDamageable` | Interface for anything that can be hit |
| `GameEvents` | Static event hub used to decouple scripts |
| `CameraShakeTrigger` | Fires camera shake when an asteroid is destroyed |
| `CameraZoomController` | Zooms the camera for the big meteor |

## How to run

1. Clone this repository.
2. Open the project folder in Unity Hub using Unity 6 (6000.0.71f1) or a compatible version.
3. Open the scene `Assets/Scenes/Week5Lab.unity`.
4. Press Play.
