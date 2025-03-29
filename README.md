# risk-board-game

## Overview

This project contains a C# implementation of the classic board game Risk. The game involves strategic conquest and control of territories on a world map. Players aim to dominate the world by controlling all territories through battles and strategic planning.
Furthermore, the project also will contain a webservice and a database for the project which will allow players to submit their own Risk AI's to battle each other on the website.

## Project Structure

- `Risk/`: Contains the main game logic and implementation.
    - `Game.cs`: Core game mechanics and rules.
    - `Player.cs`: Player-related logic and attributes.
    - `Territory.cs`: Territory-related logic and attributes.
- `Risk.Tests/`: Contains unit tests for the game logic.
    - `GameTests.cs`: Unit tests for game mechanics.
    - `PlayerTests.cs`: Unit tests for player logic.
    - `TerritoryTests.cs`: Unit tests for territory logic.
- `data/`: Contains JSON data files used by the game.
    - `territories.json`: JSON file containing the territories and their adjacent territories.

## Prerequisites

- .NET SDK
- A compatible IDE (e.g., JetBrains Rider)

## Building the Project

To build the project, run the following command:
```sh
./build.sh
```

## Running the Project

To run the project, run the following command:
```sh
./run.sh
```

## Testing the Project

To run the project tests, run the following command:
```sh
./test.sh
```

### Testing Platform

Project testing has been automated using the .NET xUnit framework.

### Test Cases

The following is a list of test cases that the project currently runs. For a more detailed overview, see src/Risk.Tests.

* Game can run with 2 bots
* Game can run with 6 bots
* InputHandler returns expected string
* InputHandler throws exception at end of file
