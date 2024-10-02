namespace Risk.Tests {
    public class Continent {
        public class TerritoriesData : IEnumerable<object[]> {
            public IEnumerator<object[]> GetEnumerator() {
                yield return new object[] { new HashSet<Risk.Territory>() };
                yield return new object[] {
                    new HashSet<Risk.Territory>([ new Risk.Territory("Alaska") ])
                };
                yield return new object[] {
                    new HashSet<Risk.Territory>([
                        new Risk.Territory("Alaska"),
                        new Risk.Territory("Northwest Territory"),
                        new Risk.Territory("New Guinea"),
                    ])
                };
            }
            
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [InlineData("Alaska")]
        [InlineData("Northwest Territory")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\\")]
        public void NameShouldBeAsGiven(string name) {
            Risk.Continent c1 = new Risk.Continent(name, new HashSet<Risk.Territory>(), 0);

            string s = c1.Name;

            Assert.Equal(s, name);
        }

        [Theory]
        [ClassData(typeof(TerritoriesData))]
        public void TerritoriesShouldBeAsGiven(ISet<Risk.Territory> territories) {
            Risk.Continent c1 = new Risk.Continent("", territories, 0);

            HashSet<Risk.Territory> terrs = (HashSet<Risk.Territory>) c1.Territories;

            Assert.Equivalent(terrs, territories);
        }

        [Theory]
        [InlineData(0U)]
        [InlineData(1U)]
        [InlineData(5U)]
        [InlineData(10U)]
        [InlineData(1000U)]
        public void ArmyBonusShouldBeAsGiven(uint armyBonus) {
            Risk.Continent c1 = new Risk.Continent("", new HashSet<Risk.Territory>(), armyBonus);

            uint bonus = c1.ArmyBonus;

            Assert.Equivalent(bonus, armyBonus);
        }
    }
}
