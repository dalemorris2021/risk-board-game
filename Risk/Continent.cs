namespace Risk;

public record Continent(string Name, uint ArmyBonus, ICollection<Territory> Territories);
