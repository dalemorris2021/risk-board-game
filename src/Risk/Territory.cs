namespace Risk {
    public class Territory {
        public string Name { get; }
        public Player OccupyingPlayer { get; set; }
        public uint ArmySize { get; private set; }

        public Territory(string name) {
            Name = name;
            OccupyingPlayer = null;
            ArmySize = 0;
        }

        public void IncreaseArmySize(uint size) {
            ArmySize += size;
        }

        public void DecreaseArmySize(uint size) {
            if (size > ArmySize) {
                throw new OverflowException("ArmySize overflowed");
            }
            ArmySize -= size;
        }
    }
}
