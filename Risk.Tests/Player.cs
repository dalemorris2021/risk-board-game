namespace Risk.Tests;

using System.Drawing;

public class Player {
    public class ColorData : IEnumerable<object[]> {
        public IEnumerator<object[]> GetEnumerator() {
            yield return new object[] { Color.Black };
            yield return new object[] { Color.Blue };
            yield return new object[] { Color.Red };
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Theory]
    [InlineData("John")]
    [InlineData("Jane Smith")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\\")]
    public void NameShouldBeAsGiven(string name) {
        Risk.Player p1 = new Risk.Player(name, Color.Black);

        string s = p1.Name;

        Assert.Equal(s, name);
    }

    [Theory]
    [InlineData("John")]
    [InlineData("Jane Smith")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\\")]
    public void NameShouldChangeToGiven(string name) {
        Risk.Player p1 = new Risk.Player("", Color.Black);

        p1.Name = name;

        Assert.Equal(name, p1.Name);
    }

    [Theory]
    [ClassData(typeof(ColorData))]
    public void ColorShouldBeGiven(Color color) {
        Risk.Player p1 = new Risk.Player("", color);

        Color c = p1.Color;

        Assert.Equal(c, color);
    }

    [Theory]
    [ClassData(typeof(ColorData))]
    public void ColorShouldChangeToGiven(Color color) {
        Risk.Player p1 = new Risk.Player("", Color.Black);

        p1.Color = color;

        Assert.Equal(color, p1.Color);
    }
}
