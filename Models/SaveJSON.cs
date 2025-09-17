using System.Text.Json;
using SistemaEscolarWeb.Models;

public static class SaveJSON
{
    private const string FileName = "escola.json";

    public static Escola Load()
    {
        if (!File.Exists(FileName))
            return new Escola();

        var json = File.ReadAllText(FileName);
        return JsonSerializer.Deserialize<Escola>(json) ?? new Escola();
    }

    public static async Task Save(Escola escola)
    {
        var json = JsonSerializer.Serialize(escola, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(FileName, json);
    }
}