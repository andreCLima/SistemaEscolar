namespace SistemaEscolarWeb.Models;

public class Serie
{
	public int ID { get; set; }
	public string Nome { get; set; } = string.Empty; // ex.: "1º ano"
	public List<Turma> Turmas { get; set; } = new(); //new List<Turma>();	
}


