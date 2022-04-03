# Fallen Angel Handy
This project is a buttplug integration for the game **Fallen Angel Marielle** to be played with a stroker device.

is a platform game. where you go through a dungeon and fight with enemies, when an enemy catches you a sex scene is played in the device for 14 seconds. The game has more than 150 sex scenes, 37 min of funscript unique content, 7 levels, 9 bosses, a fun and challenging progression.

## How to play
2. Download the modified version of the game [DOWNLOAD GAME](https://mega.nz/file/mcUylbAK#ijqQP7RmhxbAwuZSQ3TObOa-t6uZw3Ap1f0GRewWV3g)
2. Unpack the zip
3. Open [Intiface](https://github.com/intiface/intiface-desktop/releases/download/v27.0.0/intiface-desktop-27.0.0-win.exe)
4. Open **Fallen Angel Handy**.exe
5. Put your device in Bluetooth mode
6. Once the device is connected and recognized, the **Launch button** will be enabled
7. Whenever an enemy catches you, or a scene is played, let it run (don't skip) till loop (14 secs). 
8. Try to climb a floor until you beat the final boss (without cum).


# How to integrate other games.
Once the project is copied, you will need to modify the following files:

#### Game
 1. **Status**, it has the state of the game like the character's HP, power-ups and that kind of information
 2. **Config**, bundle configuration values ​​so they're not hardcoded everywhere, such as filler speed and length or gallery path.
#### Player
1. **Player** captures all the events coming from the game, coordinates the game modes by delegating the logics to the different players
2. **Attack, Gallery, Filler** control the device according to the events and parameters received from the game. you have to write your own logics according to the dynamics of the game you want to integrate
#### Gallery
1. Contains all the Funscript files to be used.

## Core
you probably don't need to modify this part

### ButtplugService 
provides methods to control the device and play the funscripts

### ScriptBuilder
build a dynamic funscript using game values such as hit damage, hp, busters, etc.

### GameListener
Minimalist http server, listens for request on a port specified in the configuration class, captures the Route and QueryString and sends them to the player class 
