# Little Rocket
My first mobile game. In this game the player steers a rocket, collects points, fuel and spare parts and avoids asteroids. Between runs the player can upgrade his rocket using collected spare parts. Idea of 2 different currencies (one for highscore and one for upgrades) is based on other mobile endless runners such as Jetpack joyride or subway surfers. Health-Fuel mechanics is my version of Hill Climb Racing ideas. I added boost to the rocket (under right finger) so the player can feel more control over maneuvers around asteroids.
![334497534_1157357281615237_6415695023456261988_n](https://user-images.githubusercontent.com/43621858/222897400-ce676676-4619-4b9b-94df-c1a109cf57d8.jpg)
### Programming
I tried to keep my scripts very clean, easy to read and work with. I got rid of a bad habit of setting all variables to public, used properties when needed, and overall improved encapsulation. Every method is short and has a single responsibility. In the whole project I stick to one naming convention. I also used UnityEvents for setting everything up during nice animation at game start, and PleayerPrefs as my save method.
https://github.com/cichy30002/MobileGame/blob/84bb13699ec6ebbf845ee9cc0257d51d1f9820bf/Assets/Scripts/GameManager.cs#L1-L107
### Unity skills
This game for sure looks better than my previous projects (here also I have made all the sprites myself). I improved on making a UI, I'm quite proud of this one. I used the very nice package LeanTween to animate it in simple ways, but the effect is really good. There are a lot of particles which also improve visuals. To make one of them I prepared my first shader using tutorials.\
As stated in the beginning, that was my first mobile game but I managed to understand touch screen controls. Joystick that is used in this game is coded by me and works perfectly. I could've used some joystick asset but this way I understood how Unity handles mobile input better and had more control over how it works in my game.
### Try it
You can download game on android [here.](https://cichy30002.itch.io/little-rocket)
