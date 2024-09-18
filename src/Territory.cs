namespace Risk {
    public class Territory {
        public string Name { get; set; }
        public Player OccupyingPlayer { get; set; }
        public int ArmySize { get; set; }

        public Territory(string name, Player player, int armySize) {
            Name = name;
            OccupyingPlayer = player;
            ArmySize = armySize;
        }
    }
}