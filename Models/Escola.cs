//using System.Text.Json;
namespace SistemaEscolar.Models;

public class Escola
{
    private const string FileName = "escola.json";
 
    public List<Serie> Series { get; set; } = new();
    //public List<Turma> Turmas { get; set; } = new();
    //public List<Estudante> Estudantes { get; set; } = new();
    //public List<Diario> Diarios { get; set; } = new();
    //public List<Aula> Aulas { get; set; } = new();
    //public List<Registro> Registros { get; set; } = new();

/*
    public void Salvar()
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FileName, json);
    }

    public Escola()
    {
        if (File.Exists(FileName)){
            var json = File.ReadAllText(FileName);
            var escolaCarregada = JsonSerializer.Deserialize<Escola>(json);
            if (escolaCarregada != null)
            {
                Series = escolaCarregada.Series;
                Turmas = escolaCarregada.Turmas;
                Estudantes = escolaCarregada.Estudantes;
                Diarios = escolaCarregada.Diarios;
                Aulas = escolaCarregada.Aulas;
                Registros = escolaCarregada.Registros;
            }
        }
    }
*/
    
}