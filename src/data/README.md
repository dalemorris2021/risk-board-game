# Data Directory

This directory contains JSON files that define the game data for the Risk project.

## Files

### `cards.json`
This file contains the card data used in the game. Each card has a `Name` and a `Type`.

### `continents.json`
This file defines the continents in the game. Each continent has a `Name`, an `ArmyBonus`, and a list of `Territories`.

### `territories.json`
This file lists the territories and their adjacent territories. Each key is a territory name, and its value is a list of adjacent territories.

## File Descriptions

- **cards.json**: Contains an array of card objects. Each object has:
    - `Name`: The name of the territory.
    - `Type`: The type of the card (0, 1, or 2).

- **continents.json**: Contains an array of continent objects. Each object has:
    - `Name`: The name of the continent.
    - `ArmyBonus`: The bonus armies awarded for controlling the continent.
    - `Territories`: A list of territory objects within the continent.

- **territories.json**: Contains an object where each key is a territory name and its value is an array of adjacent territories.

## Example

### `cards.json`
```json
[
  {
    "Name": "Alaska",
    "Type": 0
  },
  ...
]
```
### `continents.json`
```json
[
  {
    "Name": "North America",
    "ArmyBonus": 5,
    "Territories": [
      {
        "Name": "Alaska"
      },
      ...
    ]
  },
  ...
]
```
### territories.json
```json
{
  "Alaska": [
    "Northwest Territory",
    "Alberta",
    "Kamchatka"
  ],
  ...
}...
```