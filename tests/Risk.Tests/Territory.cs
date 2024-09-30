using Risk;

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
    }
}
