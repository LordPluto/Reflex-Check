# Reflex-Check
Practical game created for NeuroScouting Dev Application

### Rules
A colored circle will appear on screen for two seconds. After that, the next time that same circle appears, hit the Spacebar as soon as it appears.

The match can be attempted multiple times; however, points will only be earned if the final guess is correct. Points will be lost for incorrect guesses as well.

### Scoring
Scoring is based on a 100-point system; points are lost after .5 seconds have elapsed since the circle appeared. Each incorrect guess adds more points lost.

Each score will be displayed at the end, as well as the total score.

### Project Organization and Code
The project is separated into three main segments: the Menu, the Game, and the Results. Each object has a reference to the next, in the cycle Menu > Game > Results > Menu. Each segment handles its own logic and passes on any needed information to the next part.

The GameController object handles most of the game logic; at its start, it selects a random group of prefabs (the possible memory objects) with no repeats and instantiates them. Then, it turns them on one at a time, waiting three seconds between turning them on.

The Results and the Menu segments are both just GUI drawings. The Results screen displays all the different scores and adds them up, as well as allowing the option of retrying, goingg back to the menu, or just quitting. The menu allows the player to select how many trials they will attempt, as well as quit if they feel like it.

### Other Interesting or Useful Information
I am kinda creatively bankrupt, so simple colored circles it is. The GUI Skin graphics were made by an artist friend of mine, so there's that too. Don't want to take credit for something I didn't do.

I planned on adding some voice clips to the game - the raw audio is in the Assets folder - but due to unforeseen obstacles such as my friend getting amnesia and forgetting everything, I wasn't able to get to that in time. It's a shame, because I would have liked to use the audio.
