namespace Risk.Tests;

public class Card {
    [Theory]
    [InlineData("Alaska")]
    [InlineData("Northwest Territory")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\\")]
    public void NameShouldBeAsGiven(string territoryName) {
        Risk.Card c1 = new Risk.Card(territoryName, Risk.CardType.Infantry);

        string name = c1.TerritoryName;

        Assert.Equal(name, territoryName);
    }

    [Theory]
    [InlineData(Risk.CardType.Infantry)]
    [InlineData(Risk.CardType.Cavalry)]
    [InlineData(Risk.CardType.Artillery)]
    public void CardTypeShouldBeAsGiven(Risk.CardType cardType) {
        Risk.Card c1 = new Risk.Card("", cardType);

        Risk.CardType type = c1.CardType;

        Assert.Equal(type, cardType);
    }
}
