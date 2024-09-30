namespace Risk {
    public class Territory {
        public string Name { get; }
        public Player OccupyingPlayer { get; }
        public int ArmySize { get; }

        public Territory(string name, int armySize) {
            Name = name;
            OccupyingPlayer = null;
            ArmySize = armySize;
        }
    }
}