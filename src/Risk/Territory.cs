namespace Risk {
    public class Territory {
        public string Name { get; }
        public Player OccupyingPlayer { get; set; }
        public int ArmySize { get; set; }

        public Territory(string name) {
            Name = name;
            OccupyingPlayer = null;
            ArmySize = 0;
        }
    }
}
