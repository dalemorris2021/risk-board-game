using System.Collections;
using System.Drawing;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;

namespace Risk.Tests {
    public class Territory {
        [Theory]
        [InlineData("Alaska")]
        [InlineData("Northwest Territory")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\\")]
        public void NameShouldBeAsGiven(string territoryName) {
            Risk.Territory t1 = new Risk.Territory(territoryName);

            string name = t1.Name;

            Assert.Equal(territoryName, name);
        }

        [Fact]
        public void OccupyingPlayerShouldBeNull() {
            Risk.Territory t1 = new Risk.Territory("");

            Assert.Null(t1.OccupyingPlayer);
        }

        [Theory]
        [ClassData(typeof(PlayerData))]
        public void OccupyingPlayerShouldBeAsGiven(Player occupyingPlayer) {
            Risk.Territory t1 = new Risk.Territory("");

            t1.OccupyingPlayer = occupyingPlayer;

            Assert.Equivalent(occupyingPlayer, t1.OccupyingPlayer);
        }

        public class PlayerData : IEnumerable<object[]> {
            public IEnumerator<object[]> GetEnumerator() {
                yield return new object[] { new Player("John", Color.Black) };
                yield return new object[] { new Player("", Color.Blue) };
                yield return new object[] { new Player("Mark Brown", Color.Red) };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Fact]
        public void ArmySizeShouldBeZero() {
            Risk.Territory t1 = new Risk.Territory("");

            Assert.Equal(0U, t1.ArmySize);
        }

        [Theory]
        [InlineData(0U, 0U)]
        [InlineData(0U, 1U)]
        [InlineData(0U, 10U)]
        [InlineData(0U, 500U)]
        [InlineData(0U, 100_000U)]
        [InlineData(10U, 0U)]
        [InlineData(10U, 1U)]
        [InlineData(10U, 10U)]
        [InlineData(10U, 500U)]
        [InlineData(10U, 100_000U)]
        public void ArmySizeShouldIncreaseByGiven(uint initSize, uint size) {
            Risk.Territory t1 = new Risk.Territory("");

            t1.IncreaseArmySize(initSize);
            t1.IncreaseArmySize(size);

            Assert.Equal(initSize + size, t1.ArmySize);
        }

        [Theory]
        [InlineData(10U, 0U)]
        [InlineData(10U, 1U)]
        [InlineData(10U, 10U)]
        [InlineData(100_000U, 0U)]
        [InlineData(100_000U, 1U)]
        [InlineData(100_000U, 10U)]
        [InlineData(100_000U, 500U)]
        [InlineData(100_000U, 100_000U)]
        public void ArmySizeShouldDecreaseByGiven(uint initSize, uint size) {
            Risk.Territory t1 = new Risk.Territory("");
            t1.IncreaseArmySize(initSize);

            t1.DecreaseArmySize(size);

            Assert.Equal(initSize - size, t1.ArmySize);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(10, 100)]
        [InlineData(1000, 5000)]
        public void DecreaseArmySizeShouldThrowException(uint initSize, uint size) {
            Risk.Territory t1 = new Risk.Territory("");
            t1.IncreaseArmySize(initSize);

            Assert.Throws<OverflowException>(() => t1.DecreaseArmySize(size));
        }
    }
}
