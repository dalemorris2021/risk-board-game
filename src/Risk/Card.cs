namespace Risk;

public record Card(string Name, CardType Type);

public enum CardType {
    Infantry,
    Cavalry,
    Artillery,
}
