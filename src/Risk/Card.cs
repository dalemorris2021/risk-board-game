namespace Risk;

public enum CardType {
        Infantry,
        Cavalry,
        Artillery,
}

public class Card {
    public string TerritoryName { get; }
    public CardType CardType { get; }

    public Card(string name, CardType type) {
        TerritoryName = name;
        CardType = type;
    }
}
