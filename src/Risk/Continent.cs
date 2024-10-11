namespace Risk;

public class Continent {
    public string Name { get; }
    public ISet<Territory> Territories { get; }
    public uint ArmyBonus { get; }

    public Continent(string name, ISet<Territory> territories, uint armyBonus) {
        Name = name;
        Territories = territories;
        ArmyBonus = armyBonus;
    }
}
