namespace SistemaEscolar.Models;

public class Registro
{
	public int ID { get; set; }
	public int DiarioID { get; set; }
	public DateOnly Data { get; set; }
	public string Texto { get; set; } = string.Empty;
	//public Diario DiarioID { get; set; } = new Diario();
}


