namespace EncheresPredict.Domain.Entities;

public class UserProfile : BaseEntity
{
    public string Profile { get; private set; } = string.Empty;
    public string RegionsJson { get; private set; } = "[]";
    public decimal BudgetMin { get; private set; }
    public decimal BudgetMax { get; private set; }
    public string TypesJson { get; private set; } = "[]";

    private UserProfile() { }

    public static UserProfile Create(string profile, List<string> regions, decimal budgetMin, decimal budgetMax, List<string> types)
    {
        if (budgetMin >= budgetMax) throw new ArgumentException("Le budget minimum doit être inférieur au budget maximum.");
        return new UserProfile
        {
            Profile = profile,
            RegionsJson = System.Text.Json.JsonSerializer.Serialize(regions),
            BudgetMin = budgetMin,
            BudgetMax = budgetMax,
            TypesJson = System.Text.Json.JsonSerializer.Serialize(types)
        };
    }

    public List<string> GetRegions() =>
        System.Text.Json.JsonSerializer.Deserialize<List<string>>(RegionsJson) ?? [];

    public List<string> GetTypes() =>
        System.Text.Json.JsonSerializer.Deserialize<List<string>>(TypesJson) ?? [];
}
