using System.Drawing;

namespace Risk;

public class Player {
    public string Name { get; set; }
    public IEnumerable<Card> Cards { get; set; }
    public int NumArmies { get; set; }
    public int NumTerritoriesOwned { get; set; }
    public Color Color { get; set; }

    public Player(string name, Color color) {
        Name = name;
        Cards = [];
        NumArmies = 0;
        NumTerritoriesOwned = 0;
        Color = color;
    }

    public IEnumerable<Territory> TerritoriesConquered(IDictionary<string, Territory> terrsDict) {
        IList<Territory> territories = [];
        foreach (Territory terr in terrsDict.Values) {
            if (terr.Player == this) {
                territories.Add(terr);
            }
        }

        return territories;
    }

    public void PlaceArmy(Territory terr, int numArmies = 1) {
        if (terr.Player == null) {
            terr.Player = this;
        }

        if (NumArmies >= numArmies) {
            terr.NumArmies += numArmies;
            NumArmies -= numArmies;
        }
    }

    public void PlaceArmyFortify(Territory from, Territory to) {
        const string END_OF_INPUT_MESSAGE = "Reached end of input";
        const string ENTER_NUM_ARMIES_MESSAGE = "How many armies would you like to place?";
        
        if (from.NumArmies == 1) {
            Console.WriteLine($"You only have one army at {from.Name}");
            return;
        }
        
        int numArmies;
        while (true) {
            Console.WriteLine($"There are {from.NumArmies - 1} armies available to move.");
            Console.WriteLine(ENTER_NUM_ARMIES_MESSAGE);
            string? input = Console.ReadLine();
            if (input == null) {
                Console.WriteLine(END_OF_INPUT_MESSAGE);
                throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
            } else if (Int32.TryParse(input, out numArmies)) {
                break;
            } else {
                Console.WriteLine(ENTER_NUM_ARMIES_MESSAGE);
            }
        }

        to.NumArmies += numArmies;
        from.NumArmies -= numArmies;
    }
}
