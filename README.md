# La Petite Princesse Project

Presentation link: https://drive.google.com/file/d/197LEFqmOmiGaoSfdsQ3fa_qp4aCBUDbG/view?usp=sharing

Packages from Unity were used in the project, to install them if they aren't at the start of the project, go to Window/Package Management/Package Manager, and search for the packages needed (names given in each planet section).

## Planet 1
### Created assets
- All four blender animations: Tank shooting two shells, triforce forming together, pot playing hopscotch, cogs desynchronizing.

### Scripts
- `CustomTiling.cs` - Ajuste le "tiling" au démarrage du jeu. Il utilise un MaterialPropertyBlock, une méthode optimisée permettant de modifier l'apparence de cet objet spécifique sans affecter les autres éléments de la scène qui partagent le même matériau. N'est qu'une tentative pour des textures de murs, qui ont été scrap.

- `DoorInteraction.cs` - Gère l'interaction avec la souris. Lorsqu'un clic est détecté sur la porte, ce script communique directement avec le gestionnaire de scènes pour lancer le niveau d'après.

- `ZoneAnimation.cs` - Démarre une animation ciblée lorsqu'un personnage entre dans le Trigger. Le script vérifie d'abord que l'entité entrant dans cette zone possède bien l'étiquette "Player" avant de lancer le signal de départ au composant Animator.


### External assets (without modification)
 - Door: https://assetstore.unity.com/packages/3d/props/exterior/wooden-double-door-front-handle-entrance-297797
 - Furniture: https://assetstore.unity.com/packages/3d/props/furniture/furniture-free-pack-192628
 - Wood textures for floor and ceiling: https://assetstore.unity.com/packages/3d/props/furniture/furniture-free-pack-192628

## Planet 2
### Created assets
- Spectator cars
- Spectator stands

### External assets (without modification)
- TD4/5/6 assets for the racetrack and the race logic (turn count, checkpoints, ...)
- Unity assets for the racing lights

### External assets (with modifications)
 - Player car: https://sketchfab.com/3d-models/rookie-lightning-mcqueen-91f7594ccbb44da6ac596abe2af1fc6d
 - AI cars: https://sketchfab.com/3d-models/jackson-storm-3561b33ed9ff4a869668c1bcd13e5ee4
   https://sketchfab.com/3d-models/danny-swervez-82c64ab36d544a76a8586b109bc7787d
new textures and modified imported textures were used for the models to reduce the ressemblance to the original source.

### Tutorials

- importing 3D models to Unity: https://youtu.be/kHJo3bPiIpc?si=hsZmgIuwOL1L1RUJ

- IHM and Virtual Worlds creation's online assignments:
  https://www-sop.inria.fr/members/Hui-Yin.Wu/course/CMV/TD4-Racing-cars/TD4Racingcars.html
  https://www-sop.inria.fr/members/Hui-Yin.Wu/course/CMV/TD5-Racing-cars/TD5Racingcars.html
  https://www-sop.inria.fr/members/Hui-Yin.Wu/course/CMV/TD6-Racing-cars/TD6RacingCarsFinal.html

## Planet 3
### Created assets
3D models:
- Clown fish
- Aquarium
- Door to change scene

Scripts:
- `Boid.cs` - Defines an object as a entity to be controlled by a `BoidManager.cs`.
- `BoidManager.cs` - Controls the number of entity that is alive, the boundingBox where the entity will have to remain and getting them back if they go out of it, how they behave together and stay in groups.
- `FishButton.cs` - Detects when the player gets near the button located in front of the security fences and shows a text saying 'Press E to add a fish' and when the player presses the E key and is near it, it will call the `BoidManager.cs` to add a fish to the aquarium with a splash sound.
- `DoorPassageTrigger.cs` - Detects when the player passes through the opened door and redirects them to the correct planet scene. The destination is determined by a stored button name set earlier by the door system. It acts as the transition trigger between the cavern and the selected planet.
- `FirstPersonController.cs` - Provides the player’s first-person movement and camera look system using a `CharacterController`. It also manages cursor locking and allows movement/look controls to be enabled, disabled, or partially restricted depending on the gameplay state. This script is the core player control system of the planet.

### External assets (without modification)

Unity packages:
- Security Fence: https://assetstore.unity.com/packages/3d/props/industrial/modular-fences-pack-34359#description
- 4 other fish models: https://assetstore.unity.com/packages/3d/characters/animals/fish/fish-polypack-202232
- Wall and ceiling (texture only): https://assetstore.unity.com/packages/2d/textures-materials/yughues-free-architectural-materials-13234#description
- Floor (texture only): https://assetstore.unity.com/packages/2d/textures-materials/wood/hand-painted-seamless-wood-texture-vol-6-162145#content

Sound:
- Splash Sound: https://pixabay.com/sound-effects/search/splash/
- Ambient music (Dive): https://archive.org/details/pkmn-rse-soundtrack
 
## Planet 4
### Created assets
3D models:
- Piles of coins

Scripts:
- `ButtonInteractionZone.cs` - Detects when the player enters or leaves the interaction area around a button. When the player enters, it shows the button object and unlocks the cursor so the UI can be used. When the player exits, it restores player control through the `FirstPersonController.cs`.
- `DoorManager.cs` - Handles the full door opening sequence, including the reveal effect, smoke particles, smoke sound effect, hiding scene objects, and rotating the door around its hinge. It also stores which destination was selected so the next trigger knows which scene to load. This script is the main controller for the portal/door animation flow.
- `DoorPassageTrigger.cs` - Detects when the player passes through the opened door and redirects them to the correct planet scene. The destination is determined by a stored button name set earlier by the door system. It acts as the transition trigger between the cavern and the selected planet.
- `FirstPersonController.cs` - Provides the player’s first-person movement and camera look system using a `CharacterController`. It also manages cursor locking and allows movement/look controls to be enabled, disabled, or partially restricted depending on the gameplay state. This script is the core player control system of the planet.
- `RevealEffect.cs` - Creates the reveal animation between the genie and the lamp by animating material cutoff values over time. It also controls smoke effects, smoke sound effect, enhances the spotlight during the reveal, and triggers an event when the sequence is finished. This script is responsible for the main magical visual effect of the scene.
- `RevealTrigger.cs` - Starts the reveal sequence when the player enters a specific trigger zone. It temporarily disables player control, smoothly moves the player to a target position, rotates them toward the reveal, and then launches the reveal effect. This helps create a more cinematic presentation.
- `RevealUIManager.cs` - Manages the visibility of the UI buttons linked to the reveal sequence. It first hides all buttons, then shows the initial action button, and later displays the different destination choices and menu button. It also adjusts player control and cursor visibility to support UI interaction.

### External assets (without modification)

3D models:
- Rock: https://www.blenderkit.com/get-blenderkit/5eabc804-de1f-4992-9a23-aabe70d40da8/
- Cavern: https://sketchfab.com/3d-models/el-matador-beach-rock-wall-iphone-lidar-scan-67ad68b00ec2405f90aeeb4df7d9a9e9

Unity packages:
- Buttons: https://assetstore.unity.com/packages/2d/gui/icons/2d-rpg-button-7-278861

Sound:
- Yoga music: https://pixabay.com/fr/music/ambiant-yoga-510555/
- Smoke sound: https://pixabay.com/sound-effects/smoke-454927/

### External assets (with modifications)

3D models:
- Genie: https://sketchfab.com/3d-models/genie-3d-f1c089c880b4492c9220bd91f94f4dce - Changed a bit the map texture and added an alpha channel to enable transparency effect
- Lamp: https://sketchfab.com/3d-models/genie-de-la-lampe-61e126e34f8142dfa263362717ae9c13 - Kept only the lamp, and added an alpha channel to enable transparency effect

### Tutorials

Particle system: https://johnstejskal.com/wp/how-to-create-realistic-smoke-in-unity/

Font import and creation: https://blog.terresquall.com/2023/10/how-to-import-and-use-fonts-in-your-text-ui-elements-in-unity/

Lights: https://docs.unity.com/en-us/unity-studio/develop/gameobjects/lights/spotlight ; https://www.youtube.com/watch?v=upEt2kQ10fM

Cinemachine (only tried for the genie animation but not implemented): https://docs.unity3d.com/Packages/com.unity.cinemachine@3.1/manual/samples-tutorials.html

