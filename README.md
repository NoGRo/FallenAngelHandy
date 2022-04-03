# Fallen Angel Handy
This project is a buttplug integration for the game **Fallen Angel Marielle** to be played with a stroker device.

is a platform game. where you go through a dungeon and fight with enemies, when an enemy catches you, a sex scene is played in the device for 14 seconds. 
The game has more than 150 sex scenes, 37 min of funscript unique content, 7 levels, 9 bosses, a fun and challenging progression.

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

## Core and Tools
you probably don't need to modify this part

#### ButtplugService 
provides methods to control the device and play the funscripts

#### ScriptBuilder
build a dynamic funscript using game values such as hit damage, hp, busters, etc.

#### GameListener
Minimalist http server, listens for request on a port specified in the configuration class, captures the Route and QueryString and sends them to the player class 

## Mod the game
To make your own integration you will need the game to send http request to the Listener, reporting the events of the game.
How to do it depends on what technology the game is made on. many H-Games are either Game Maker Studio 2 (GMS2) projects, or based on HTML and Javascript

### GMS2, UndertaleModTool
If the game has a **data.win** file, you can open it and modify its code with UndertaleModTool, you need to find where the events you want to capture happen. and there call the following function makes a call http.
example:

``http_post_string("http://127.0.0.1:5050/game/hit_pain?strength=" + string(attackHp), "")``

### HTML and JavaScript.
you can find inside the game a **www** folder containing all the game files including a **js** folder, if you are lucky this folder is not minified. You must find where the events you want to capture occur, and make an http call using the fetch function.
example:

`fetch("http://127.0.0.1:5050/game/hit_pain?strength=" + string(attackHp))`
