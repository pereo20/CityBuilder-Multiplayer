# CityBuilder Multiplayer - Development Setup Guide

## 🚀 Quick Start

### Prerequisites
- Unity 2022 LTS or newer
- Android SDK (for building)
- Firebase Account
- Git

### Installation Steps

1. Clone Repository
```bash
git clone https://github.com/pereo20/CityBuilder-Multiplayer.git
cd CityBuilder-Multiplayer
```

2. Open in Unity
- Unity Hub - Add Project - Select the cloned folder
- Open the project

3. Firebase Setup
   - Create Firebase Project at firebase.google.com
   - Add Android app
   - Download google-services.json
   - Copy to Assets/Plugins/Android/
   - Enable Anonymous Authentication
   - Create Realtime Database

4. Install Firebase SDK
   - Download Firebase Unity SDK
   - Import in Unity
   - Window - Firebase - Generate C# Project

5. Android Build Settings
   - File - Build Settings
   - Select Android platform
   - Player Settings:
     - Target API Level: 30+
     - Minimum API Level: 26
   - Create Keystore

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── Core/
│   ├── Buildings/
│   ├── Heroes/
│   ├── Combat/
│   ├── Multiplayer/
│   ├── Resources/
│   ├── UI/
│   └── Save/
├── Prefabs/
├── Scenes/
└── Resources/
```

## 🎮 Key Features

- 50x50 grid-based city
- 30+ building types
- 30 unique heroes
- Combat system with leveling
- Multiplayer raids
- Firebase integration
- Android optimized

## 🛠️ Development Tips

### Creating a Building
```csharp
GameManager.Instance.BuildingSystem.TryPlaceBuilding("gold_mine", new Vector2Int(5, 5));
```

### Recruiting a Hero
```csharp
Hero hero = GameManager.Instance.HeroManager.RecruitHero("barbarian_king");
```

### Initiating Battle
```csharp
var heroes = GameManager.Instance.HeroManager.GetActiveBattleHeroes();
GameManager.Instance.GetComponent<CombatSystem>().InitiateBattle(heroes, enemyHeroes);
```

## 📱 Building for Android

1. File - Build Settings
2. Select Android
3. Click Build
4. Choose output folder
5. Install APK on device

## 🔐 Security

- Never commit google-services.json
- Use Firebase Security Rules
- Validate server-side
- Protect API keys

---

**Happy building!** 🏰