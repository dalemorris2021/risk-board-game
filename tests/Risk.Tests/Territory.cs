namespace Risk.Tests;

using System.Drawing;

public class Territory {
    public class PlayerData : IEnumerable<object[]> {
        public IEnumerator<object[]> GetEnumerator() {
            yield return new object[] { new Risk.Player("John", Color.Black) };
            yield return new object[] { new Risk.Player("", Color.Blue) };
            yield return new object[] { new Risk.Player("Mark Brown", Color.Red) };
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

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
