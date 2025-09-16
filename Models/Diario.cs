namespace SistemaEscolarWeb.Models;

public class Diario
{
	public int ID { get; set; }
	//public Turma TurmaID { get; set; } = new Turma();
	public string Disciplina { get; set; } = string.Empty; // ex.: Mat., Port., Geo.
	public string Educador { get; set; } = string.Empty;

	public List<Aula> Aulas { get; set; } = new();
	public List<Registro> Registros { get; set; } = new();
}


