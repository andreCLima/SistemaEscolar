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

    public static void Save(Escola escola)
    {
        var json = JsonSerializer.Serialize(escola, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FileName, json);
    }
}