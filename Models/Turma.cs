namespace SistemaEscolar.Models;

public class Turma
{
	public int ID { get; set; }

	public int SerieID { get; set; }
	public string Nome { get; set; } = string.Empty; // ex.: "2º ano B"
													 //public Serie SerieID { get; set; } = new Serie();

	public List<Estudante> Estudantes { get; set; } = new();
	public List<Diario> Diarios { get; set; } = new();

    public override string ToString()
    {
        return $"{this.ID,2} | {this.Nome,-12} | {this.Estudantes.Count,9}";
    }
}


