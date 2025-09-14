using SistemaEscolar.Models;

namespace SistemaEscolar;

public static class App
{
	private static readonly Escola escola = SaveJSON.Load();
	//private static readonly Escola escola = new Escola();

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
				case "3": MenuDiario(); break;
				default: return;
			}
		}
	}

	private static void MenuEscola()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("----ESCOLA----");
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
		Console.WriteLine("----ESCOLA -> RESUMO----");
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
		Console.WriteLine("----ESCOLA -> CADASTRO DE SÉRIE----");
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
		Console.WriteLine("----ESCOLA -> DETALHES----");
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
			Console.WriteLine("----TURMA----");
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
				case "5": TurmaExcluir(); break;
				case "6": return;
			}
		}
	}

	private static Turma? PerguntarTurmaPorId()
	{
		Console.Write("Qual o ID Turma: ");
		if (!int.TryParse(Console.ReadLine(), out var id)) return null;
		return escola.Series.SelectMany(s => s.Turmas).FirstOrDefault(t => t.ID == id);
	}

	private static void TurmaListar()
	{
		Console.Clear();
		Console.WriteLine("----TURMA -> LISTAR----");
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

	private static void TurmaDetalhes()
	{
		Console.Clear();
		Console.WriteLine("----TURMA -> DETALHES----");
		var turma = PerguntarTurmaPorId();
		if (turma is null)
		{
			Console.WriteLine("Turma não encontrada.");
			Pause();
			return;
		}

		MenuTurmaDetalhes(turma);
	}

	private static void TurmaCadastrar()
	{
		Console.Clear();
		Console.WriteLine("----TURMA -> CADASTRO----");
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
			Nome = nome
		};
		serie.Turmas.Add(nova);
		SaveJSON.Save(escola);
		Console.WriteLine("Turma cadastrada!");
		Pause();
	}

	private static void TurmaAlterar()
	{
		Console.Clear();
		Console.WriteLine("----TURMA -> ALTERAR----");
		var turma = PerguntarTurmaPorId();
		if (turma is null)
		{
			Console.WriteLine("Turma não encontrada!");
			Pause();
			return;	
		}
		
		var serie = escola.Series.FirstOrDefault(s => s.Turmas.Contains(turma));
		
		Console.WriteLine($"Turma atual: {turma.Nome} (Série: {serie?.Nome ?? "Não encontrada"})");
		Console.WriteLine($"Estudantes: {turma.Estudantes.Count}");
		Console.WriteLine();
		Console.Write("Novo nome da turma: ");
		var nome = Console.ReadLine() ?? string.Empty;
		
		if (string.IsNullOrWhiteSpace(nome))
		{
			Console.WriteLine("Nome inválido!");
			Pause();
			return;
		}
		
		turma.Nome = nome;
		SaveJSON.Save(escola);
		Console.WriteLine("Turma alterada com sucesso!");
		Pause();
	}

	private static void TurmaExcluir()
	{
		Console.Clear();
		Console.WriteLine("----TURMA -> EXCLUIR----");
		var turma = PerguntarTurmaPorId();
		if (turma is null)
		{
			Console.WriteLine("Turma não encontrada!");
			Pause();
			return;
		}

		var serie = escola.Series.FirstOrDefault(s => s.Turmas.Contains(turma));
		
		Console.WriteLine($"Turma: {turma.Nome} (Série: {serie?.Nome ?? "Não encontrada"})");
		Console.WriteLine($"Estudantes: {turma.Estudantes.Count}");
		Console.WriteLine($"Diários: {turma.Diarios.Count}");
		Console.WriteLine();
		
		Console.WriteLine("Deseja realmente excluir? (s/n): ");
		var confirmacao = Console.ReadLine()?.ToLower();
		
		if (confirmacao != "s" && confirmacao != "sim")
		{
			Console.WriteLine("Exclusão cancelada.");
			Pause();
			return;
		}
		
		// Remover a turma da série
		if (serie != null)
		{
			serie.Turmas.Remove(turma);
		}
		
		SaveJSON.Save(escola);
		Console.WriteLine("Turma excluída com sucesso!");
		Pause();
	}

	

	private static void MenuTurmaDetalhes(Turma turma){
		// Encontrar a série da turma
		var serie = escola.Series.FirstOrDefault(s => s.Turmas.Contains(turma));

		while (true)
		{
			Console.Clear();
			Console.WriteLine($"----Turma: {turma.Nome} ({serie?.Nome ?? "Série não encontrada"}) -> DETALHES----");
			Console.WriteLine("1- Aulas");
			Console.WriteLine("2- Diario");
			Console.WriteLine("3- Estudantes");
			Console.WriteLine("4- Voltar");
			Console.Write("Escolha: ");
			var opt = Console.ReadLine();
			switch (opt)
			{
				case "1": MenuTurmaAulas(turma); break;
				case "2": MenuTurmaDiarios(turma); break;
				case "3": MenuTurmaEstudantes(turma); break;
				case "4": return;
			}
		}
	}

	private static void MenuTurmaAulas(Turma turma)
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Aulas----");
			Console.WriteLine("1- Listar");
			Console.WriteLine("2- Cadastrar");
			Console.WriteLine("3- Alterar");
			Console.WriteLine("4- Excluir");
			Console.WriteLine("5- Voltar");
			Console.Write("Escolha: ");
			var opt = Console.ReadLine();
			switch (opt)
			{
				case "1": AulaListar(turma); break;
				case "2": AulaCadastrar(turma); break;
				case "3": AulaAlterar(turma); break;
				case "4": AulaExcluir(turma); break;
				case "5": return;
			}
		}
	}

	private static void AulaListar(Turma turma){
		Console.Clear();
		Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Aulas -> LISTAR----");
		var aulasTurma = turma.Diarios.SelectMany(d => d.Aulas)
			.OrderBy(a => a.DiaSemana).ThenBy(a => a.NumeroAula);
		Console.WriteLine("Dia | Nº | Disciplina | Professor");
		foreach (var a in aulasTurma)
		{
			var diario = turma.Diarios.FirstOrDefault(d => d.Aulas.Contains(a));
			Console.WriteLine($"{a.DiaSemana,-3} | {a.NumeroAula,1} | {diario?.Disciplina,-10} | {diario?.Educador}");
		}
		Pause();
	}

	private static void AulaCadastrar(Turma turma){
		Console.Clear();
		Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Aulas -> CADASTRO----");
		if (turma.Diarios.Count == 0)
		{
			Console.WriteLine("Não há diários para esta turma. Cadastre primeiro em 'Diario'.");
			Pause();
			return;
		}
		Console.WriteLine("Selecione o Diário (Disciplina - Professor):");
		for (var i = 0; i < turma.Diarios.Count; i++)
			Console.WriteLine($"{i + 1}- {turma.Diarios[i].Disciplina} - {turma.Diarios[i].Educador}");
		Console.Write("Escolha: ");
		if (!int.TryParse(Console.ReadLine(), out var idx) || idx < 1 || idx > turma.Diarios.Count) return;
		var diarioSel = turma.Diarios[idx - 1];
		Console.Write("Dia da semana (Dom,Seg,Ter,Qua,Qui,Sex,Sab): ");
		var dia = Console.ReadLine() ?? string.Empty;
		Console.Write("Número da aula (1..7): ");
		if (!int.TryParse(Console.ReadLine(), out var num) || num < 1 || num > 7) return;
		
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
	}

	private static void AulaAlterar(Turma turma){
		Console.Clear();
		Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Aulas -> ALTERAR----");
		// Listar todas as aulas da turma para seleção
		var todasAulas = turma.Diarios.SelectMany(d => d.Aulas)
			.OrderBy(a => a.DiaSemana).ThenBy(a => a.NumeroAula).ToList();
		
		if (todasAulas.Count == 0)
		{
			Console.WriteLine("Não há aulas cadastradas para esta turma.");
			Pause();
			return;
		}

		Console.WriteLine("Selecione a aula para alterar:");
		Console.WriteLine("ID | Dia | Nº | Disciplina | Professor");
		for (int i = 0; i < todasAulas.Count; i++)
		{
			var aula = todasAulas[i];
			var diario = turma.Diarios.FirstOrDefault(d => d.Aulas.Contains(aula));
			Console.WriteLine($"{i + 1,2} | {aula.DiaSemana,-3} | {aula.NumeroAula,1} | {diario?.Disciplina,-10} | {diario?.Educador}");
		}
		
		Console.Write("Escolha o número da aula: ");
		if (!int.TryParse(Console.ReadLine(), out var idx) || idx < 1 || idx > todasAulas.Count)
		{
			Console.WriteLine("Seleção inválida!");
			Pause();
			return;
		}

		var aulaSelecionada = todasAulas[idx - 1];
		var diarioAula = turma.Diarios.FirstOrDefault(d => d.Aulas.Contains(aulaSelecionada));
		
		Console.WriteLine();
		Console.WriteLine($"Aula atual:");
		Console.WriteLine($"Dia: {aulaSelecionada.DiaSemana}");
		Console.WriteLine($"Número: {aulaSelecionada.NumeroAula}");
		Console.WriteLine($"Disciplina: {diarioAula?.Disciplina}");
		Console.WriteLine($"Professor: {diarioAula?.Educador}");
		Console.WriteLine();
		
		// Alterar dia da semana
		Console.Write("Novo dia da semana (ou Enter para manter): ");
		var novoDia = Console.ReadLine();
		if (!string.IsNullOrWhiteSpace(novoDia))
		{
			aulaSelecionada.DiaSemana = novoDia;
		}
		
		// Alterar número da aula
		Console.Write("Novo número da aula (1-7, ou Enter para manter): ");
		var novoNumeroStr = Console.ReadLine();
		if (!string.IsNullOrWhiteSpace(novoNumeroStr) && int.TryParse(novoNumeroStr, out var novoNumero))
		{
			if (novoNumero >= 1 && novoNumero <= 7)
			{
				aulaSelecionada.NumeroAula = novoNumero;
			}
			else
			{
				Console.WriteLine("Número inválido! Mantendo o atual.");
			}
		}
		
		SaveJSON.Save(escola);
		Console.WriteLine("Aula alterada com sucesso!");
		Pause();
	}

	private static void AulaExcluir(Turma turma){
		Console.Clear();
		Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Aulas -> EXCLUIR----");
		// Listar todas as aulas da turma para seleção
		var todasAulas = turma.Diarios.SelectMany(d => d.Aulas)
			.OrderBy(a => a.DiaSemana).ThenBy(a => a.NumeroAula).ToList();
		
		if (todasAulas.Count == 0)
		{
			Console.WriteLine("Não há aulas cadastradas para esta turma.");
			Pause();
			return;
		}

		Console.WriteLine("Selecione a aula para excluir:");
		Console.WriteLine("ID | Dia | Nº | Disciplina | Professor");
		for (int i = 0; i < todasAulas.Count; i++)
		{
			var aula = todasAulas[i];
			var diario = turma.Diarios.FirstOrDefault(d => d.Aulas.Contains(aula));
			Console.WriteLine($"{i + 1,2} | {aula.DiaSemana,-3} | {aula.NumeroAula,1} | {diario?.Disciplina,-10} | {diario?.Educador}");
		}
		
		Console.Write("Escolha o número da aula: ");
		if (!int.TryParse(Console.ReadLine(), out var idx) || idx < 1 || idx > todasAulas.Count)
		{
			Console.WriteLine("Seleção inválida!");
			Pause();
			return;
		}

		var aulaSelecionada = todasAulas[idx - 1];
		var diarioAula = turma.Diarios.FirstOrDefault(d => d.Aulas.Contains(aulaSelecionada));
		
		Console.WriteLine();
		Console.WriteLine($"Aula selecionada para exclusão:");
		Console.WriteLine($"Dia: {aulaSelecionada.DiaSemana}");
		Console.WriteLine($"Número: {aulaSelecionada.NumeroAula}");
		Console.WriteLine($"Disciplina: {diarioAula?.Disciplina}");
		Console.WriteLine($"Professor: {diarioAula?.Educador}");
		Console.WriteLine();
		
		// Verificar se há registros associados à aula
		var temRegistros = diarioAula?.Registros.Any(r => diarioAula.Aulas.Contains(aulaSelecionada)) ?? false;
		
		if (temRegistros)
		{
			Console.WriteLine("⚠️  ATENÇÃO: Esta aula possui registros associados!");
			Console.WriteLine("Deseja realmente excluir? (s/n): ");
		}
		else
		{
			Console.WriteLine("Deseja excluir esta aula? (s/n): ");
		}
		
		var confirmacao = Console.ReadLine()?.ToLower();
		if (confirmacao != "s" && confirmacao != "sim")
		{
			Console.WriteLine("Exclusão cancelada.");
			Pause();
			return;
		}

		// Remover a aula do diário
		if (diarioAula != null)
		{
			diarioAula.Aulas.Remove(aulaSelecionada);
		}
		
		SaveJSON.Save(escola);
		Console.WriteLine("Aula excluída com sucesso!");
		Pause();
	}

	private static void MenuTurmaDiarios(Turma turma)
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Diarios----");
			Console.WriteLine("1- Listar");
			Console.WriteLine("2- Cadastrar");
			Console.WriteLine("3- Alterar");
			Console.WriteLine("4- Excluir");
			Console.WriteLine("5- Voltar");
			Console.Write("Escolha: ");
			var opt = Console.ReadLine();
			switch (opt)
			{
				case "1": DiarioListar(turma);	break;
				case "2": DiarioCadastrar(turma); break;
				case "3": DiarioAlterar(turma); break;
				case "4": DiarioExcluir(turma); break;
				case "5": return;
			}
		}
	}

	

	private static void DiarioListar(Turma turma){
		Console.Clear();
		Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Diarios -> LISTAR----");
		Console.WriteLine("Professor  |  Disciplina");

		if (!(turma is null))
		{
			foreach (var d in turma.Diarios)
			{
				Console.WriteLine($"{d.Educador} | {d.Disciplina,-10}");
			}
		}
		else
		{
			foreach (var s in escola.Series)
			{
				foreach (var t in s.Turmas)
				{
					foreach (var d in t.Diarios)
					{
						Console.WriteLine($"{d.Educador} | {d.Disciplina,-10}");
					}
				}
			}
		}
		
		Pause();
	}

	private static void DiarioCadastrar(Turma turma){
		Console.Clear();
		Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Diarios -> CADASTRO----");
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
	}
	private static void DiarioAlterar(Turma turma){
		Console.Clear();
		Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Diarios -> ALTERAR----");
		if (turma.Diarios.Count == 0)
		{
			Console.WriteLine("Não há diários cadastrados para esta turma.");
			Pause();
			return;
		}

		Console.WriteLine("Selecione o diário para alterar:");
		Console.WriteLine("ID | Disciplina | Professor");
		for (int i = 0; i < turma.Diarios.Count; i++)
		{
			var diario = turma.Diarios[i];
			Console.WriteLine($"{i + 1,2} | {diario.Disciplina,-10} | {diario.Educador}");
		}
		
		Console.Write("Escolha o número do diário: ");
		if (!int.TryParse(Console.ReadLine(), out var idx) || idx < 1 || idx > turma.Diarios.Count)
		{
			Console.WriteLine("Seleção inválida!");
			Pause();
			return;
		}

		var diarioSelecionado = turma.Diarios[idx - 1];
		
		Console.WriteLine();
		Console.WriteLine($"Diário atual:");
		Console.WriteLine($"Disciplina: {diarioSelecionado.Disciplina}");
		Console.WriteLine($"Professor: {diarioSelecionado.Educador}");
		Console.WriteLine($"ID: {diarioSelecionado.ID}");
		Console.WriteLine();
		
		// Alterar disciplina
		Console.Write("Nova disciplina (ou Enter para manter): ");
		var novaDisciplina = Console.ReadLine();
		if (!string.IsNullOrWhiteSpace(novaDisciplina))
		{
			diarioSelecionado.Disciplina = novaDisciplina;
		}
		
		// Alterar professor
		Console.Write("Novo professor (ou Enter para manter): ");
		var novoProfessor = Console.ReadLine();
		if (!string.IsNullOrWhiteSpace(novoProfessor))
		{
			diarioSelecionado.Educador = novoProfessor;
		}
		
		SaveJSON.Save(escola);
		Console.WriteLine("Diário alterado com sucesso!");
		Pause();
	}
	private static void DiarioExcluir(Turma turma){
		Console.Clear();
		Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Diarios -> EXCLUIR----");
		if (turma.Diarios.Count == 0)
		{
			Console.WriteLine("Não há diários cadastrados para esta turma.");
			Pause();
			return;
		}

		Console.WriteLine("Selecione o diário para excluir:");
		Console.WriteLine("ID | Disciplina | Professor | Aulas | Registros");
		for (int i = 0; i < turma.Diarios.Count; i++)
		{
			var diario = turma.Diarios[i];
			Console.WriteLine($"{i + 1,2} | {diario.Disciplina,-10} | {diario.Educador,-8} | {diario.Aulas.Count,5} | {diario.Registros.Count,8}");
		}
		
		Console.Write("Escolha o número do diário: ");
		if (!int.TryParse(Console.ReadLine(), out var idx) || idx < 1 || idx > turma.Diarios.Count)
		{
			Console.WriteLine("Seleção inválida!");
			Pause();
			return;
		}

		var diarioSelecionado = turma.Diarios[idx - 1];
		
		Console.WriteLine();
		Console.WriteLine($"Diário selecionado para exclusão:");
		Console.WriteLine($"Disciplina: {diarioSelecionado.Disciplina}");
		Console.WriteLine($"Professor: {diarioSelecionado.Educador}");
		Console.WriteLine($"ID: {diarioSelecionado.ID}");
		Console.WriteLine($"Aulas: {diarioSelecionado.Aulas.Count}");
		Console.WriteLine($"Registros: {diarioSelecionado.Registros.Count}");
		Console.WriteLine();
		
		// Verificar se há aulas ou registros associados
		if (diarioSelecionado.Aulas.Count > 0 || diarioSelecionado.Registros.Count > 0)
		{
			Console.WriteLine("⚠️  ATENÇÃO: Este diário possui aulas e/ou registros associados!");
			Console.WriteLine("Deseja realmente excluir? (s/n): ");
		}
		else
		{
			Console.WriteLine("Deseja excluir este diário? (s/n): ");
		}
		
		var confirmacao = Console.ReadLine()?.ToLower();
		if (confirmacao != "s" && confirmacao != "sim")
		{
			Console.WriteLine("Exclusão cancelada.");
			Pause();
			return;
		}

		// Remover o diário da turma
		turma.Diarios.Remove(diarioSelecionado);
		
		SaveJSON.Save(escola);
		Console.WriteLine("Diário excluído com sucesso!");
		Pause();
	}

	private static void MenuTurmaEstudantes(Turma turma)
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Estudantes----");
			Console.WriteLine("1- Listar");
			Console.WriteLine("2- Cadastrar");
			Console.WriteLine("3- Alterar");
			Console.WriteLine("4- Excluir");
			Console.WriteLine("5- Voltar");
			Console.Write("Escolha: ");
			var opt = Console.ReadLine();
			switch (opt)
			{
				case "1": EstudanteListar(turma); break;
				case "2": EstudanteCadastrar(turma); break;
				case "3": EstudanteAlterar(turma); break;
				case "4": EstudanteExcluir(turma); break;
				case "5": return;
			}
		}
	}

	private static void EstudanteListar(Turma turma){
		Console.Clear();
		Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Estudantes -> LISTAR----");
		var alunos = turma.Estudantes.OrderBy(e => e.Nome);
		Console.WriteLine("ID | Nome");
		foreach (var e in alunos)
			Console.WriteLine($"{e.ID,2} | {e.Nome}");
		Pause();
	}

	private static void EstudanteCadastrar(Turma turma){
		Console.Clear();
		Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Estudantes -> CADASTRO----");
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
	}
	private static void EstudanteAlterar(Turma turma){
		Console.Clear();
		Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Estudantes -> ALTERAR----");
		if (turma.Estudantes.Count == 0)
		{
			Console.WriteLine("Não há estudantes cadastrados para esta turma.");
			Pause();
			return;
		}

		Console.WriteLine("Selecione o estudante para alterar:");
		Console.WriteLine("ID | Nome");
		for (int i = 0; i < turma.Estudantes.Count; i++)
		{
			var estudante = turma.Estudantes[i];
			Console.WriteLine($"{i + 1,2} | {estudante.Nome}");
		}
		
		Console.Write("Escolha o número do estudante: ");
		if (!int.TryParse(Console.ReadLine(), out var idx) || idx < 1 || idx > turma.Estudantes.Count)
		{
			Console.WriteLine("Seleção inválida!");
			Pause();
			return;
		}

		var estudanteSelecionado = turma.Estudantes[idx - 1];
		
		Console.WriteLine();
		Console.WriteLine($"Estudante atual:");
		Console.WriteLine($"Nome: {estudanteSelecionado.Nome}");
		Console.WriteLine($"ID: {estudanteSelecionado.ID}");
		Console.WriteLine();
		
		// Alterar nome do estudante
		Console.Write("Novo nome do estudante (ou Enter para manter): ");
		var novoNome = Console.ReadLine();
		if (!string.IsNullOrWhiteSpace(novoNome))
		{
			estudanteSelecionado.Nome = novoNome;
		}
		
		SaveJSON.Save(escola);
		Console.WriteLine("Estudante alterado com sucesso!");
		Pause();
	}

	private static void EstudanteExcluir(Turma turma){
		Console.Clear();
		Console.WriteLine($"----Turma {turma.Nome} -> Detalhes -> Estudantes -> EXCLUIR----");
		if (turma.Estudantes.Count == 0)
		{
			Console.WriteLine("Não há estudantes cadastrados para esta turma.");
			Pause();
			return;
		}

		Console.WriteLine("Selecione o estudante para excluir:");
		Console.WriteLine("ID | Nome");
		for (int i = 0; i < turma.Estudantes.Count; i++)
		{
			var estudante = turma.Estudantes[i];
			Console.WriteLine($"{i + 1,2} | {estudante.Nome}");
		}
		
		Console.Write("Escolha o número do estudante: ");
		if (!int.TryParse(Console.ReadLine(), out var idx) || idx < 1 || idx > turma.Estudantes.Count)
		{
			Console.WriteLine("Seleção inválida!");
			Pause();
			return;
		}

		var estudanteSelecionado = turma.Estudantes[idx - 1];
		
		Console.WriteLine();
		Console.WriteLine($"Estudante selecionado para exclusão:");
		Console.WriteLine($"Nome: {estudanteSelecionado.Nome}");
		Console.WriteLine($"ID: {estudanteSelecionado.ID}");
		Console.WriteLine();
		
		Console.WriteLine("Deseja excluir este estudante? (s/n): ");
		var confirmacao = Console.ReadLine()?.ToLower();
		if (confirmacao != "s" && confirmacao != "sim")
		{
			Console.WriteLine("Exclusão cancelada.");
			Pause();
			return;
		}

		// Remover o estudante da turma
		turma.Estudantes.Remove(estudanteSelecionado);
		
		SaveJSON.Save(escola);
		Console.WriteLine("Estudante excluído com sucesso!");
		Pause();
	}

	private static void MenuDiario()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("----DIARIO----");
			Console.WriteLine("1- Detalhes");
			Console.WriteLine("2- Registrar");
			Console.WriteLine("3- Voltar");
			Console.Write("Escolha: ");
			var opt = Console.ReadLine();
			switch (opt)
			{
				case "1": ; break;
				case "2": DiarioRegistrar(); break;
				case "3": return;
			}
		}	
	}

	private static void DiarioRegistrar(){
		Console.Clear();
		Console.WriteLine("----DIARIO -> REGISTRAR----");
		// Primeiro, selecionar a turma
		var turma = PerguntarTurmaPorId();
		if (turma is null)
		{
			Console.WriteLine("Turma não encontrada!");
			Pause();
			return;
		}

		// Verificar se há diários na turma
		if (turma.Diarios.Count == 0)
		{
			Console.WriteLine("Não há diários cadastrados para esta turma.");
			Console.WriteLine("Cadastre primeiro um diário para poder registrar informações.");
			Pause();
			return;
		}

		// Selecionar o diário
		Console.WriteLine($"Turma: {turma.Nome}");
		Console.WriteLine("Selecione o diário para registrar:");
		Console.WriteLine("ID | Disciplina | Professor");
		for (int i = 0; i < turma.Diarios.Count; i++)
		{
			var diario = turma.Diarios[i];
			Console.WriteLine($"{i + 1,2} | {diario.Disciplina,-10} | {diario.Educador}");
		}
		
		Console.Write("Escolha o número do diário: ");
		if (!int.TryParse(Console.ReadLine(), out var idx) || idx < 1 || idx > turma.Diarios.Count)
		{
			Console.WriteLine("Seleção inválida!");
			Pause();
			return;
		}

		var diarioSelecionado = turma.Diarios[idx - 1];
		
		Console.WriteLine();
		Console.WriteLine($"Diário selecionado: {diarioSelecionado.Disciplina} - {diarioSelecionado.Educador}");
		Console.WriteLine();
		
		// Solicitar data do registro
		Console.Write("Data do registro (dd/mm/aaaa): ");
		var dataStr = Console.ReadLine() ?? string.Empty;
		
		if (!DateOnly.TryParse(dataStr, out var data))
		{
			Console.WriteLine("Data inválida! Usando data atual.");
			data = DateOnly.FromDateTime(DateTime.Today);
		}
		
		// Solicitar texto do registro
		Console.WriteLine("Texto do registro:");
		Console.Write("> ");
		var texto = Console.ReadLine() ?? string.Empty;
		
		if (string.IsNullOrWhiteSpace(texto))
		{
			Console.WriteLine("Texto não pode estar vazio!");
			Pause();
			return;
		}
		
		// Encontrar o próximo ID disponível
		var maxId = escola.Series.SelectMany(s => s.Turmas)
			.SelectMany(t => t.Diarios)
			.SelectMany(d => d.Registros)
			.DefaultIfEmpty(new Registro { ID = 0 })
			.Max(r => r.ID);
		
		// Criar o registro
		var novoRegistro = new Registro
		{
			ID = maxId + 1,
			Data = data,
			Texto = texto
		};
		
		// Adicionar o registro ao diário
		diarioSelecionado.Registros.Add(novoRegistro);
		
		SaveJSON.Save(escola);
		Console.WriteLine("Registro adicionado com sucesso!");
		Pause();
	}
}


