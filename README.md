# ColdFeather-Frenzy

## About the Game

- A simple mini-game in which the main objective is to sort chickens into the right enclosures while under increasing time pressure
- Genre: Casual Puzzle Game
- Platform: Windows, MacOS, WebGL (play on itch.io: https://toastheo.itch.io/coldfeather-frenzy-alpha)

## Gameplay

The game is usually played with a mouse and keyboard. The mechanics are relatively self-explanatory. The chickens must be dragged and dropped into the corresponding colored enclosures. 
The trick here is that more and more chickens appear over time and you only have a limited amount of time to assign them correctly.
(Note: The game can also be played on mobile devices via the website, but is not optimized for this.)

![image](https://github.com/toastheo/ColdFeather-Frenzy/assets/114708595/738fd61b-feb9-4727-a5c3-1ecd6747edf0)
![image](https://github.com/toastheo/ColdFeather-Frenzy/assets/114708595/63f7c4dc-78d6-43fc-ac1e-af0fe842e8c7)

## Build Requirements

### Unity Version

This project was developed using Unity version 2022.3.12f1. To ensure compatibility, please use this version or newer. You can download it from the [unity website](https://unity.com/de/download "Unity Download Page").

### External Dependencies

The game uses 2 different asset packs:
The Chickenpack asset is subject to the royality-free license and can therefore be integrated directly into the project.
The download link can be found [here](https://vmiinv.itch.io/chickenpack-asset "Chicken Pack Asset").

The Cozy Farm Asset Pack, on the other hand, prohibits any resale of the assets. For this reason, the asset pack must be purchased independently. Instructions on how to do this can be found in the [Build Instructions](#build-instructions).
The download link can be found [here](https://shubibubi.itch.io/cozy-farm "Cozy Farm Asset Pack")

## Build Instructions
1. **Clone the Repository:** First clone the repository to your local machine using Git. You can use: `git clone https://github.com/toastheo/ColdFeather-Frenzy`

2. **Install the Asset Pack:** The Cozy Farm Asset Pack is not included in the project due to its license. First buy the full version of the asset pack on the following website: https://shubibubi.itch.io/cozy-farm.
Extract the "full_version.zip". Then drag the entire content of the subfolder "full version" into the following path of the Unity project:
Assets/Farm Assets/Cozy Farm Assets:

![image](https://github.com/toastheo/ColdFeather-Frenzy/assets/114708595/93461cf7-1373-4ef5-ab13-10ea60dbbf38)

The paths to the assets are already entered in the .gitignore. If you plan to change the position of the assets or add new ones, you must adjust the .gitignore accordingly.

3. **Open the Project in Unity:** Launch Unity Hub, click on 'Add' and navigate to the cloned project folder. Select the folder to add the project to your unity hub. Once the project has loaded, you should be ready to go.

4. **Build the Game:** If you only want to build the project, you can follow these steps:
- In Unity, open the project and navigate to **File > Build Settings**.
- Select your target platform from the list (e.g., Windows, Mac, WebGL).
- Click on **Build And Run**. Choose a location to save the built game and wait for Unity to compile the project.
- After the build is complete, the game executable will be available in the chosen directory.

## Contributors
- [Kurt (Bolzenpinguin)](https://github.com/Bolzenpinguin)
- [jorezst](https://github.com/jorezst)

## Acknowledgements
We would like to thank Prof. Marco Block-Berlitz from the HTW Dresden, who supported and inspired us throughout the entire project in the "Beleuchtung und Rendering" (lighting and rendering) module.

We would also like to thank the creators of the asset packs [vmiinv](https://vmiinv.itch.io "itch.io profile of vmiinv") and [Shubibubi](https://shubibubi.itch.io "itch.io profile of Shubibubi").

## Contact
If you have any questions, feedback or bug reports, please contact us directly.
