# Unity RPG Game Project

A 3D RPG game featuring skill-based combat, character progression, and enemy AI systems. Built with Unity.

![Gameplay Screenshot](/path/to/screenshot.png) <!-- Add your screenshot later -->
![alt text](image.png)
## Features

### Core Systems
- **Character Stats**
  - Health System (`SistemaVida`) with damage/healing mechanics
  - Energy System (`SistemaEnergia`) for skill usage
  - XP System (`SistemaXP`) with level progression
  - Skill System (`SistemaDeHabilidades`) managing abilities and cooldowns

### Playable Characters
- **Warrior Class**
  - Physical combat skills
  - Damage reduction passive
  - Special attack abilities
  
- **Hades Class** (Magic User)
  - Life drain ability
  - Dark magic attacks
  - Energy-to-health conversion

### Skill System
- **Skill Types**
  - **HealRestorer**: Gradual health regeneration
  - **Kamehameha**: Charged projectile attack
  - **DamageZone**: Area-of-effect persistent damage

- **Skill Features**
  - Energy cost management
  - Cooldown system
  - Visual/audio effects
  - ScriptableObject-based configuration

### Enemy System
- AI-powered enemies with chase behavior
- Enemy health system (`EnemyHealth`)
- Damage reaction system
- Basic combat interactions

### UI System
- Health bar with dynamic color changes
- Energy circle with regeneration visualization
- Skill UI with:
  - Icon displays
  - Cooldown indicators
  - Key binding labels
- Damage feedback effects

## Installation

1. **Requirements**
   - Unity 2022.3 LTS or newer
   - Universal Render Pipeline (URP)
   - Input System package

2. **Setup**
   ```bash
   git clone https://github.com/yourusername/rpg-game.git
   Open in Unity Hub