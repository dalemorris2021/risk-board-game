using System.Collections;

namespace Risk.Tests {
    public class Continent {
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
    }
}
