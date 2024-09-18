namespace Risk {
    public class Continent {
        public string Name { get; }
        public List<Territory> Territories { get; }
        public int ArmyBonus { get; }

        public Continent(string name, List<Territory> territories, int armyBonus) {
            Name = name;
            Territories = territories;
            ArmyBonus = armyBonus;
        }
    }
}
