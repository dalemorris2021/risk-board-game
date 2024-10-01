using System.Collections;
using System.Drawing;

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

            Assert.Equal(0, t1.ArmySize);
        }
    }
}
