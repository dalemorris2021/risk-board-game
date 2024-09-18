namespace Risk {
    public class Territory {
        public string Name { get; }
        public Player OccupyingPlayer { get; }
        public int ArmySize { get; }

        public Territory(string name, Player player, int armySize) {
            Name = name;
            OccupyingPlayer = player;
            ArmySize = armySize;
        }
    }
}