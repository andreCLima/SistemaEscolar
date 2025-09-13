namespace SistemaEscolar.Models;

public class Estudante
{
	public int ID { get; set; }

	public int TurmaID { get; set; }
	public string Nome { get; set; } = string.Empty;
	//public Turma TurmaID { get; set; } = new Turma();
}


