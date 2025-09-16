namespace SistemaEscolarWeb.Models;

public class Aula
{
	public int ID { get; set; }
	public string DiaSemana { get; set; } = string.Empty; // Dom, Seg, Ter, Qua, Qui, Sex, Sab
	public int NumeroAula { get; set; } // 1..7
	//public Diario DiarioID { get; set; } = new Diario();
}


