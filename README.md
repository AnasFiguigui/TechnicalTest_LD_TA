# README_LD.txt

## Unity Version
**Unity 6 (6000.0.062f1)**  
2D URP (Universal Render Pipeline).

---

## Build

[![Technical test build](https://img.shields.io/badge/Build-0A66C2?style=for-the-badge&logo=Build&logoColor=white)](https://drive.google.com/drive/u/0/folders/1vHmGYJGGnPrzb50VLjY7jUFB9HaAqs3N)


---

## Description of Classes and Managers

### Core Layer (Assets/Scripts/Core)
- **EventBus.cs**  
  Static class that defines UnityEvents used for decoupled communication between systems.  
  Events used:
  - `OnDamageDealt(int)` → Fired when the player deals damage to the enemy.  
  - `OnEnemyDefeated()` → Fired when an enemy reaches 0 HP.  
  - `OnEnemyRespawned()` → Fired when an enemy respawns after a delay.

---

### Game Layer (Assets/Scripts/Game)
- **PlayerController.cs**  
  Handles player input (movement + attack). Uses the *New Input System* through `PlayerInput`.  
  - Movement via keyboard (WASD/Arrow keys) or gamepad stick.  
  - Attack via Left Click or Gamepad West button.  
  - Attacks use a `Physics2D.OverlapCircleAll()` to detect enemies in range.  

- **EnemyController.cs**  
  Manages HP, takes damage, dies, and respawns after 3 seconds.  
  On respawn, re-enables visuals, collider, and Canvas, then sends an `OnEnemyRespawned()` event.  

- **HeroStatsSO.cs / EnemyStatsSO.cs**  
  ScriptableObjects holding entity stats: `HP`, `ATK`, and `DEF`.  
  Used by Player and Enemy for data-driven balancing.

---

### UI Layer (Assets/Scripts/UI)
- **HealthUIController.cs**  
  Manages Player and Enemy HP display using TextMeshPro text fields.  
  Subscribes to events from `EventBus` to update in real time:
  - `OnDamageDealt` → Decrease enemy HP.  
  - `OnEnemyDefeated` → Set HP to 0.  
  - `OnEnemyRespawned` → Reset HP to full.

---

## Role of ScriptableObjects
ScriptableObjects are used to separate gameplay data from logic.  
They define stats like HP, Attack, and Defense for the Player and Enemy.

---

# README_TA.txt

## Effects Used
- Shader Graph: SpriteOutline.shadergraph (OutlineColor, Thickness)
- Post-Process: Bloom + Vignette (subtle)
- Lighting: Global Light 2D

## Pixel Perfect & Canvas
- Pixel Perfect Camera (640x360 base, x2=1280x720, x3=1920x1080)
- Pixel Snapping ON
- Canvas Scaler Reference: 640x360, Match 0.5

## Pipeline
URP 2D Renderer:
- 2D Lights Enabled
- Renderer Data: URP_Renderer2D.asset
- Volume Profile: GlobalVolumeProfile.asset