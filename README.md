# Ndock 

## 📱 À propos
Ndock est une application mobile développée avec .NET MAUI (Multi-platform App UI), permettant de créer une expérience native sur Android et iOS à partir d'une base de code unique.

## 🛠 Technologies Utilisées
- .NET 8.0
- .NET MAUI
- C# 12.0
- Plateformes supportées :
  - Android
  - iOS

## 📂 Structure du Projet
Le projet est organisé selon l'architecture suivante :

### Mobile/
- **Pages/** - Contient les pages de l'application
- **Views/** - Composants d'interface utilisateur réutilisables
- **Models/** - Classes de modèles de données
- **ViewModels/** - Implémentation du pattern MVVM
- **Services/** - Services et logique métier
- **Handlers/** - Gestionnaires personnalisés
- **Assets/** - Ressources statiques
- **Resources/** - Ressources de l'application
- **Platforms/** - Code spécifique aux plateformes
  - Android
  - iOS

### Fichiers Principaux
- `App.xaml/App.xaml.cs` - Point d'entrée de l'application
- `AppShell.xaml` - Navigation et structure de l'application
- `MauiProgram.cs` - Configuration de l'application

## 🚀 Installation

### Prérequis
- Visual Studio 2022 ou JetBrains Rider 2025.1
- .NET 8.0 SDK
- Workload de développement mobile avec .NET MAUI
- Xcode (pour le développement iOS, Mac uniquement)
- Android Studio et SDK (pour le développement Android)

### Configuration
1. Cloner le dépôt
2. Ouvrir la solution `Ndock.sln` dans votre IDE
3. Restaurer les packages NuGet
4. Compiler le projet

## 📱 Déploiement

### Android
1. Connecter un appareil Android ou démarrer un émulateur
2. Sélectionner la configuration Android
3. Appuyer sur F5 pour déboguer

### iOS
1. Connecter un appareil iOS ou démarrer le simulateur
2. Sélectionner la configuration iOS
3. Appuyer sur F5 pour déboguer

## 🏗 Architecture
L'application suit le pattern MVVM (Model-View-ViewModel) avec une séparation claire des responsabilités :
- **Models** : Représentation des données
- **Views** : Interface utilisateur
- **ViewModels** : Logique de présentation
- **Services** : Logique métier et accès aux données
