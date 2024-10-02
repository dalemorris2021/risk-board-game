namespace Risk.Tests {
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
        [InlineData(CardType.Infantry)]
        [InlineData(CardType.Cavalry)]
        [InlineData(CardType.Artillery)]
        public void CardTypeShouldBeAsGiven(CardType cardType) {
            Risk.Card c1 = new Risk.Card("", cardType);

            CardType type = c1.CardType;

            Assert.Equal(type, cardType);
        }
    }
}
