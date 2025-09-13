using SistemaEscolar.Models;

namespace SistemaEscolar;

public static class App
{
	private static readonly Escola escola = SaveJSON.Load();
	//private static readonly Escola escola = new Escola();

	public static void Run()
	{
		Seed();
		while (true)
		{
			Console.Clear();
			Console.WriteLine("1- Escola");
			Console.WriteLine("2- Turma");
			Console.WriteLine("3- Diario");
			Console.Write("Escolha: ");
			var opt = Console.ReadLine();
			switch (opt)
			{
				case "1": MenuEscola(); break;
				case "2": MenuTurma(); break;
				case "3": MenuDiarioGlobal(); break;
				default: return;
			}
		}
	}

	private static void Seed()
	{
		if (escola.Series.Count > 0) return;
		
		// Criar séries
		var s1 = new Serie { ID = 1, Nome = "1 ano" };
		var s2 = new Serie { ID = 2, Nome = "2 ano" };
		var s3 = new Serie { ID = 3, Nome = "3 ano" };
		escola.Series.AddRange(new[] { s1, s2, s3 });

		// Criar turmas e adicionar às séries
		var t1 = new Turma { ID = 1, Nome = "A" };
		var t2 = new Turma { ID = 2, Nome = "B" };
		var t3 = new Turma { ID = 3, Nome = "C" };
		
		s1.Turmas.Add(t1);
		s2.Turmas.AddRange(new[] { t2, t3 });

		// Criar estudantes e adicionar às turmas
		var est1 = new Estudante { ID = 1, Nome = "Ana" };
		var est2 = new Estudante { ID = 2, Nome = "Bruno" };
		var est3 = new Estudante { ID = 3, Nome = "Carlos" };
		var est4 = new Estudante { ID = 4, Nome = "Diana" };
		var est5 = new Estudante { ID = 5, Nome = "Edu" };
		
		t1.Estudantes.Add(est1);
		t2.Estudantes.AddRange(new[] { est2, est3 });
		t3.Estudantes.AddRange(new[] { est4, est5 });

		// Criar diários e adicionar às turmas
		var d1 = new Diario { ID = 1, Disciplina = "Mat.", Educador = "João" };
		var d2 = new Diario { ID = 2, Disciplina = "Port.", Educador = "Maria" };
		var d3 = new Diario { ID = 3, Disciplina = "Geo.", Educador = "Leo" };
		
		t2.Diarios.AddRange(new[] { d1, d2 });
		t3.Diarios.Add(d3);

		// Criar aulas e adicionar aos diários
		var a1 = new Aula { ID = 1, DiaSemana = "Seg", NumeroAula = 1 };
		var a2 = new Aula { ID = 2, DiaSemana = "Qua", NumeroAula = 2 };
		var a3 = new Aula { ID = 3, DiaSemana = "Sex", NumeroAula = 3 };
		
		d1.Aulas.Add(a1);
		d2.Aulas.Add(a2);
		d3.Aulas.Add(a3);
	}

	private static void Pause()
	{
		Console.WriteLine();
		Console.Write("Enter para continuar...");
		Console.ReadLine();
	}

	private static void MenuEscola()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("1- Resumo");
			Console.WriteLine("2- Cadastrar Série");
			Console.WriteLine("3- Detalhes");
			Console.WriteLine("4- Voltar");
			Console.Write("Escolha: ");
			var opt = Console.ReadLine();
			switch (opt)
			{
				case "1": EscolaResumo(); break;
				case "2": EscolaCadastrarSerie(); break;
				case "3": EscolaDetalhes(); break;
				case "4": return;
			}
		}
	}

	private static void EscolaResumo()
	{
		Console.Clear();
		Console.WriteLine("Quant. de Turmas por Série:");
		foreach (var s in escola.Series)
		{
			Console.WriteLine($"- {s.Nome}: {s.Turmas.Count} turmas");
		}
		Console.WriteLine();
		Console.WriteLine("Quant. de Estudantes por Série:");
		foreach (var s in escola.Series)
		{
			var qtEst = s.Turmas.Sum(t => t.Estudantes.Count);
			Console.WriteLine($"- {s.Nome}: {qtEst} estudantes");
		}
		Pause();
	}

	private static void EscolaCadastrarSerie()
	{
		Console.Clear();
		Console.WriteLine("Cadastrar nova Série");
		Console.Write("Nome da Série: ");
		//?? se expressao a esquerda for Null ele retorna a da direita
		var nome = Console.ReadLine() ?? string.Empty;
		if (string.IsNullOrWhiteSpace(nome))
		{
			Console.WriteLine("Nome inválido.");
			Pause();
			return; //finaliza
		}
		var nova = new Serie
		{
			ID = escola.Series.Count == 0 ? 1 : escola.Series.Max(s => s.ID) + 1,
			Nome = nome
		};
		escola.Series.Add(nova);
		SaveJSON.Save(escola);
	

		Console.WriteLine("Série cadastrada!");
		Pause();
	}

	private static void EscolaDetalhes()
	{
		Console.Clear();
		Console.WriteLine("Detalhes da Escola");
		Console.WriteLine($"Séries: {escola.Series.Count}");
		
		var totalTurmas = escola.Series.Sum(s => s.Turmas.Count);
		var totalEstudantes = escola.Series.Sum(s => s.Turmas.Sum(t => t.Estudantes.Count));
		var totalDiarios = escola.Series.Sum(s => s.Turmas.Sum(t => t.Diarios.Count));
		var totalAulas = escola.Series.Sum(s => s.Turmas.Sum(t => t.Diarios.Sum(d => d.Aulas.Count)));
		
		Console.WriteLine($"Turmas: {totalTurmas}");
		Console.WriteLine($"Estudantes: {totalEstudantes}");
		Console.WriteLine($"Diários: {totalDiarios}");
		Console.WriteLine($"Aulas: {totalAulas}");
		Console.WriteLine();
		Console.WriteLine("Turmas por Série:");
		foreach (var s in escola.Series)
		{
			Console.WriteLine($"- {s.Nome}: {s.Turmas.Count}");
		}
		Console.WriteLine();
		Console.WriteLine("Estudantes por Série:");
		foreach (var s in escola.Series)
		{
			var qtEst = s.Turmas.Sum(t => t.Estudantes.Count);
			Console.WriteLine($"- {s.Nome}: {qtEst}");
		}
		Pause();
	}

	private static void MenuTurma()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("TURMA");
			Console.WriteLine("1- Listar");
			Console.WriteLine("2- Detalhes");
			Console.WriteLine("3- Cadastrar");
			Console.WriteLine("4- Alterar");
			Console.WriteLine("5- Excluir");
			Console.WriteLine("6- Voltar");
			Console.Write("Escolha: ");
			var opt = Console.ReadLine();
			switch (opt)
			{
				case "1": TurmaListar(); break;
				case "2": TurmaDetalhes(); break;
				case "3": TurmaCadastrar(); break;
				case "4": TurmaAlterar(); break;
				//case "5": TurmaExcluir(); break;
				case "6": return;
			}
		}
	}

	private static void TurmaListar()
	{
		Console.Clear();
		Console.WriteLine("Id | Turma        | Serie   | Estudante");
		foreach (var s in escola.Series)
		{
			foreach (var t in s.Turmas)
			{
				Console.WriteLine($"{t.ID,2} | {t.Nome,-12} | {s.Nome,-7} | {t.Estudantes.Count,9}");
			}
		}
		Pause();
	}

	private static Turma? PerguntarTurmaPorId()
	{
		Console.Write("Qual o ID Turma: ");
		if (!int.TryParse(Console.ReadLine(), out var id)) return null;
		return escola.Series.SelectMany(s => s.Turmas).FirstOrDefault(t => t.ID == id);
	}

	private static void TurmaDetalhes()
	{
		Console.Clear();
		var turma = PerguntarTurmaPorId();
		if (turma is null)
		{
			Console.WriteLine("Turma não encontrada.");
			Pause();
			return;
		}
		// Encontrar a série da turma
		var serie = escola.Series.FirstOrDefault(s => s.Turmas.Contains(turma));
		
		while (true)
		{
			Console.Clear();
			Console.WriteLine($"Turma: {turma.Nome} ({serie?.Nome ?? "Série não encontrada"})");
			Console.WriteLine("1- Aulas");
			Console.WriteLine("2- Diario");
			Console.WriteLine("3- Estudantes");
			Console.WriteLine("4- Voltar");
			Console.Write("Escolha: ");
			var opt = Console.ReadLine();
			switch (opt)
			{
				case "1": SubmenuAulas(turma); break;
				case "2": SubmenuDiario(turma); break;
				case "3": SubmenuEstudantes(turma); break;
				case "4": return;
			}
		}
	}

	private static void SubmenuAulas(Turma turma)
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("1- Listar");
			Console.WriteLine("2- Cadastrar");
			Console.WriteLine("3- Voltar");
			Console.Write("Escolha: ");
			var opt = Console.ReadLine();
			switch (opt)
			{
				case "1":
					Console.Clear();
					var aulasTurma = turma.Diarios.SelectMany(d => d.Aulas)
						.OrderBy(a => a.DiaSemana).ThenBy(a => a.NumeroAula);
					Console.WriteLine("Dia | Nº | Disciplina | Professor");
					foreach (var a in aulasTurma)
					{
						var diario = turma.Diarios.FirstOrDefault(d => d.Aulas.Contains(a));
						Console.WriteLine($"{a.DiaSemana,-3} | {a.NumeroAula,1} | {diario?.Disciplina,-10} | {diario?.Educador}");
					}
					Pause();
					break;
				case "2":
					Console.Clear();
					if (turma.Diarios.Count == 0)
					{
						Console.WriteLine("Não há diários para esta turma. Cadastre primeiro em 'Diario'.");
						Pause();
						break;
					}
					Console.WriteLine("Selecione o Diário (Disciplina - Professor):");
					for (var i = 0; i < turma.Diarios.Count; i++)
						Console.WriteLine($"{i + 1}- {turma.Diarios[i].Disciplina} - {turma.Diarios[i].Educador}");
					Console.Write("Escolha: ");
					if (!int.TryParse(Console.ReadLine(), out var idx) || idx < 1 || idx > turma.Diarios.Count) break;
					var diarioSel = turma.Diarios[idx - 1];
					Console.Write("Dia da semana (Dom,Seg,Ter,Qua,Qui,Sex,Sab): ");
					var dia = Console.ReadLine() ?? string.Empty;
					Console.Write("Número da aula (1..7): ");
					if (!int.TryParse(Console.ReadLine(), out var num) || num < 1 || num > 7) break;
					
					// Encontrar o próximo ID disponível
					var maxId = escola.Series.SelectMany(s => s.Turmas)
						.SelectMany(t => t.Diarios)
						.SelectMany(d => d.Aulas)
						.DefaultIfEmpty(new Aula { ID = 0 })
						.Max(a => a.ID);
					
					var novaAula = new Aula
					{
						ID = maxId + 1,
						DiaSemana = dia,
						NumeroAula = num
					};
					diarioSel.Aulas.Add(novaAula);
					SaveJSON.Save(escola);
					Console.WriteLine("Aula cadastrada!");
					Pause();
					break;
				case "3": return;
			}
		}
	}

	private static void SubmenuDiario(Turma turma)
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("1- Listar");
			Console.WriteLine("2- Cadastrar");
			Console.WriteLine("3- Voltar");
			Console.Write("Escolha: ");
			var opt = Console.ReadLine();
			switch (opt)
			{
				case "1":
					Console.Clear();
					Console.WriteLine("Disciplina | Professor");
					foreach (var d in turma.Diarios)
						Console.WriteLine($"{d.Disciplina,-10} | {d.Educador}");
					Pause();
					break;
				case "2":
					Console.Clear();
					Console.Write("Nome da Disciplina: ");
					var disc = Console.ReadLine() ?? string.Empty;
					Console.Write("Nome do Professor: ");
					var prof = Console.ReadLine() ?? string.Empty;
					
					// Encontrar o próximo ID disponível
					var maxId = escola.Series.SelectMany(s => s.Turmas)
						.SelectMany(t => t.Diarios)
						.DefaultIfEmpty(new Diario { ID = 0 })
						.Max(d => d.ID);
					
					var novo = new Diario
					{
						ID = maxId + 1,
						Disciplina = disc,
						Educador = prof
					};
					turma.Diarios.Add(novo);
					SaveJSON.Save(escola);
					Console.WriteLine("Diário cadastrado!");
					Pause();
					break;
				case "3": return;
			}
		}
	}

	private static void SubmenuEstudantes(Turma turma)
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("1- Listar");
			Console.WriteLine("2- Cadastrar");
			Console.WriteLine("3- Voltar");
			Console.Write("Escolha: ");
			var opt = Console.ReadLine();
			switch (opt)
			{
				case "1":
					Console.Clear();
					var alunos = turma.Estudantes.OrderBy(e => e.Nome);
					Console.WriteLine("ID | Nome");
					foreach (var e in alunos)
						Console.WriteLine($"{e.ID,2} | {e.Nome}");
					Pause();
					break;
				case "2":
					Console.Clear();
					Console.Write("Nome do estudante: ");
					var nome = Console.ReadLine() ?? string.Empty;
					
					// Encontrar o próximo ID disponível
					var maxId = escola.Series.SelectMany(s => s.Turmas)
						.SelectMany(t => t.Estudantes)
						.DefaultIfEmpty(new Estudante { ID = 0 })
						.Max(e => e.ID);
					
					var novo = new Estudante
					{
						ID = maxId + 1,
						Nome = nome
					};
					turma.Estudantes.Add(novo);
					SaveJSON.Save(escola);
					Console.WriteLine("Estudante cadastrado!");
					Pause();
					break;
				case "3": return;
			}
		}
	}

	private static void TurmaCadastrar()
	{
		Console.Clear();
		Console.Write("Nome da turma: ");
		var nome = Console.ReadLine() ?? string.Empty;
		Console.WriteLine("Escolha a Série:");
		for (var i = 0; i < escola.Series.Count; i++)
			Console.WriteLine($"{escola.Series[i].ID}- {escola.Series[i].Nome}");
		Console.Write("Escolha: ");
		if (!int.TryParse(Console.ReadLine(), out var idx) || idx < 1 || idx > escola.Series.Count) return;
		var serie = escola.Series[idx - 1];
		
		// Encontrar o próximo ID disponível
		var maxId = escola.Series.SelectMany(s => s.Turmas)
			.DefaultIfEmpty(new Turma { ID = 0 })
			.Max(t => t.ID);
		
		var nova = new Turma
		{
			ID = maxId + 1,
			Nome = nome,
			SerieID = serie.ID
		};
		serie.Turmas.Add(nova);
		SaveJSON.Save(escola);
		Console.WriteLine("Turma cadastrada!");
		Pause();
	}

	private static void TurmaAlterar()
	{
		Console.Clear();
		var turma = PerguntarTurmaPorId();
		if (turma is null)
		{
			Console.WriteLine("Turma não encontrada!!!");
			Pause();
			return;	
		}
        var serie = escola.Series.FirstOrDefault(s => s.Turmas.Contains(turma));

		Console.WriteLine("Id | Turma        | Serie   | Estudante");
		Console.WriteLine($"{turma.ToString()}");

		Console.WriteLine("Qual o novo Nome da Turma: ");
		var nome = Console.ReadLine() ?? string.Empty;
		turma.Nome = nome;
		SaveJSON.Save(escola);

			
		Pause();

	}

	private static void MenuDiarioGlobal()
	{
		Console.Clear();
		Console.WriteLine("Diários (professores e suas disciplinas):");
		foreach (var s in escola.Series)
		{
			foreach (var t in s.Turmas)
			{
				foreach (var d in t.Diarios)
				{
					Console.WriteLine($"Turma {t.Nome} | {d.Disciplina} - {d.Educador}");
				}
			}
		}
		Pause();
	}
}


