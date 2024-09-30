namespace Risk {
    public class Territory {
        public string Name { get; }
        public Player OccupyingPlayer { get; }
        public int ArmySize { get; }

        public Territory(string name) {
            Name = name;
            OccupyingPlayer = null;
            ArmySize = 0;
        }
    }
}