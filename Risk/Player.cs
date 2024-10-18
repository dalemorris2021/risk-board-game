namespace Risk;

using System.Drawing;

public class Player {
    public string Name { get; set; }
    public Color Color { get; set; }
    public int NumArmies { get; set; }

    public Player(string name, Color color) {
        Name = name;
        Color = color;
        NumArmies = 0;
    }
}
