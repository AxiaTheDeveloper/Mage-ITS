# Mage-ITS
 
Unity 2021.3.10f1
Itch.io: https://dhyox.itch.io/highvoltage

(programmer notes ('25)) -> newest one NotArtistBranch-But4.2

We developed this for the Mage ITS Game dev competition 2023.

For this project I implemented SRP of SOLID principle.
For optimization: used Comparetag for comparing collider. 
For game programming pattern: I used singleton for managers and state pattern for the game state control. 
For design pattern: I still only used model view pattern.

It's my first time creating this non-character focused game.
Not like the development from before, I started to finally use more inheritence principle in making the tile puzzle class.

It's a simple puzzle with a logic gate mechanic, where players have successfuly connect all the circuit so the electricity from the main source can charge the lamp.
I had a lot of problems in solving the logic for the tile puzzle checker to make sure each tile puzzle didn't overlap and suddenly became its own source of electricity. I also have to make sure that I keep it optimize (because every tile have to keep updating/checking it's own checker)

It's also my first time creating a more interactive tutorial for the game and not just UI showing image of the tutorial.

For the tools and frameworks outside normal framework from unity, we used: LeanTween
