namespace SistemaEscolarWeb.Models;

public class Turma
{
	public int ID { get; set; }
	public string Nome { get; set; } = string.Empty; // ex.: "2º ano B"
	public List<Estudante> Estudantes { get; set; } = new();
	public List<Diario> Diarios { get; set; } = new();
}


