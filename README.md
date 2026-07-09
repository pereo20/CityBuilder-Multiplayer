# CityBuilder Multiplayer - Android Game

A multiplayer city building game with heroes, combat system, and resource management built with Unity and Firebase.

## 🎮 Features

- **City Building System**: Build and manage your city on a grid-based map
- **30+ Heroes**: Recruit and upgrade unique heroes with different abilities
- **Combat System**: Train troops, increase combat power, and raid other cities
- **Multiplayer**: Real-time multiplayer with PvP raids and cooperative gameplay
- **Resource Management**: Manage gold, elixir, and other resources
- **Building Types**: 30+ different building types with unique functions
- **Android Ready**: Optimized for Android devices

## 📋 Project Structure

```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs
│   │   ├── GridSystem.cs
│   │   └── TimeManager.cs
│   ├── Buildings/
│   │   ├── BuildingSystem.cs
│   │   ├── BuildingData.cs
│   │   └── Building.cs
│   ├── Heroes/
│   │   ├── HeroManager.cs
│   │   ├── HeroData.cs
│   │   └── Hero.cs
│   ├── Combat/
│   │   ├── CombatSystem.cs
│   │   ├── CombatCalculator.cs
│   │   └── BattleUnit.cs
│   ├── Multiplayer/
│   │   ├── FirebaseManager.cs
│   │   ├── PlayerManager.cs
│   │   └── RaidSystem.cs
│   ├── Resources/
│   │   ├── ResourceManager.cs
│   │   └── ResourceData.cs
│   ├── UI/
│   │   ├── UIManager.cs
│   │   ├── BuildingUI.cs
│   │   ├── HeroUI.cs
│   │   └── CombatUI.cs
│   └── Save/
│       └── SaveSystem.cs
├── Prefabs/
├── Scenes/
└── Resources/
```

## 🛠️ Setup Instructions

### Prerequisites
- Unity 2022 LTS or newer
- Android SDK
- Firebase account

### Installation

1. Clone the repository
```bash
git clone https://github.com/pereo20/CityBuilder-Multiplayer.git
```

2. Open in Unity

3. Configure Firebase
   - Download your Firebase config file
   - Place it in `Assets/Resources/`
   - Follow Firebase setup guide below

4. Build for Android
   - File > Build Settings
   - Select Android platform
   - Configure your signing key

## 🔥 Firebase Setup

1. Create a Firebase project at [firebase.google.com](https://firebase.google.com)
2. Enable these services:
   - Realtime Database
   - Authentication (Anonymous)
   - Cloud Storage
3. Download `google-services.json`
4. Place in `Assets/Plugins/Android/`

## 🎮 Game Systems

### Grid System
- 50x50 grid-based city map
- Free placement on empty tiles
- Collision detection

### Building System
- 30+ building types:
  - Resource generators (Gold Mine, Elixir Collector, etc.)
  - Defense buildings (Cannons, Archer Towers, etc.)
  - Training facilities (Barracks, Academy, etc.)
  - Support buildings (Storage, Hospital, etc.)
  - Special buildings (Town Hall, Portal, etc.)

### Hero System
- 30 unique heroes with different classes:
  - Warriors
  - Mages
  - Archers
  - Support Heroes
  - Legendary Heroes
- Level up system (1-50)
- Ability system
- Equipment slots

### Combat System
- Train troops from buildings
- Increase combat power through upgrades
- Real-time battle calculations
- Raid system for PvP

### Multiplayer
- Firebase Realtime Database for player data sync
- Anonymous authentication
- Real-time player data updates
- Raid notifications

## 📱 Supported Platforms

- Android 8.0+ (API 26+)
- Tested on various Android devices

## 🚀 Development

To contribute or develop:
1. Create a feature branch
2. Make your changes
3. Test on Android device
4. Submit pull request

## 📄 License

MIT License - feel free to use this for learning and development

## 👨‍💻 Author

Created by pereo20

---

**Happy building!** 🏰
