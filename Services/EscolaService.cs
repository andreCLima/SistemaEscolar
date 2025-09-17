using SistemaEscolarWeb.Models;

namespace SistemaEscolarWeb.Services;

public class EscolaService
{
    private readonly Escola _escola;

    public EscolaService()
    {
        _escola = SaveJSON.Load();
        DadosTeste();
    }

    public Escola GetEscola() => _escola;

    public void Save() => SaveJSON.Save(_escola);

    private void DadosTeste()
    {
        if (_escola.Series.Count > 0) return;
        
        // Criar séries
        var s1 = new Serie { ID = 1, Nome = "1 ano" };
        var s2 = new Serie { ID = 2, Nome = "2 ano" };
        var s3 = new Serie { ID = 3, Nome = "3 ano" };
        _escola.Series.AddRange(new[] { s1, s2, s3 });

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
        var d1 = new Diario { ID = 1, Disciplina = "Matematica", Educador = "Joao" };
        var d2 = new Diario { ID = 2, Disciplina = "Portugues", Educador = "Maria" };
        var d3 = new Diario { ID = 3, Disciplina = "Geografia", Educador = "Leo" };
        
        t2.Diarios.AddRange(new[] { d1, d2 });
        t3.Diarios.Add(d3);

        // Criar aulas e adicionar aos diários
        var a1 = new Aula { ID = 1, DiaSemana = "Seg", NumeroAula = 1 };
        var a2 = new Aula { ID = 2, DiaSemana = "Qua", NumeroAula = 2 };
        var a3 = new Aula { ID = 3, DiaSemana = "Sex", NumeroAula = 3 };
        
        d1.Aulas.Add(a1);
        d2.Aulas.Add(a2);
        d3.Aulas.Add(a3);

        Save();
    }

    // Métodos para Escola
    public void CadastrarSerie(string nome)
    {
        var nova = new Serie
        {
            ID = _escola.Series.Count == 0 ? 1 : _escola.Series.Max(s => s.ID) + 1,
            Nome = nome
        };
        _escola.Series.Add(nova);
        Save();
    }

    // Métodos para Turma
    public List<Turma> TurmaListar()
    {
        return _escola.Series.SelectMany(s => s.Turmas).ToList();
    }

    public Turma? GetTurmaPorId(int id)
    {
        return _escola.Series
            .SelectMany(s => s.Turmas)
            .FirstOrDefault(t => t.ID == id);
    }

    public void CadastrarTurma(string nome, int serieId)
    {
        var serie = _escola.Series.FirstOrDefault(s => s.ID == serieId);
        if (serie == null) return;

        var maxId = _escola.Series.SelectMany(s => s.Turmas)
            .DefaultIfEmpty(new Turma { ID = 0 })
            .Max(t => t.ID);
        
        var nova = new Turma
        {
            ID = maxId + 1,
            Nome = nome
        };
        serie.Turmas.Add(nova);
        Save();
    }

    public void AlterarTurma(int id, string nome)
    {
        var turma = GetTurmaPorId(id);
        if (turma != null)
        {
            turma.Nome = nome;
            Save();
        }
    }

    public void ExcluirTurma(int id)
    {
        var turma = GetTurmaPorId(id);
        if (turma != null)
        {
            var serie = _escola.Series.FirstOrDefault(s => s.Turmas.Contains(turma));
            if (serie != null)
            {
                serie.Turmas.Remove(turma);
                Save();
            }
        }
    }

    // Métodos para Diário
    public List<Diario> DiarioListarTodos()
    {
        return _escola.Series
            .SelectMany(s => s.Turmas)
            .SelectMany(t => t.Diarios)
            .OrderBy(d => d.Educador)
            .ToList();
    }

    public Diario? GetDiarioPorId(int id)
    {
        return _escola.Series
            .SelectMany(s => s.Turmas)
            .SelectMany(t => t.Diarios)
            .FirstOrDefault(d => d.ID == id);
    }

    public void CadastrarDiario(int turmaId, string disciplina, string educador)
    {
        var turma = GetTurmaPorId(turmaId);
        if (turma == null) return;

        var maxId = _escola.Series.SelectMany(s => s.Turmas)
            .SelectMany(t => t.Diarios)
            .DefaultIfEmpty(new Diario { ID = 0 })
            .Max(d => d.ID);
        
        var novo = new Diario
        {
            ID = maxId + 1,
            Disciplina = disciplina,
            Educador = educador
        };
        turma.Diarios.Add(novo);
        Save();
    }

    public void AlterarDiario(int id, string disciplina, string educador)
    {
        var diario = GetDiarioPorId(id);
        if (diario != null)
        {
            diario.Disciplina = disciplina;
            diario.Educador = educador;
            Save();
        }
    }

    public void ExcluirDiario(int id)
    {
        var diario = GetDiarioPorId(id);
        if (diario != null)
        {
            var turma = _escola.Series
                .SelectMany(s => s.Turmas)
                .FirstOrDefault(t => t.Diarios.Contains(diario));
            
            if (turma != null)
            {
                turma.Diarios.Remove(diario);
                Save();
            }
        }
    }

    // Métodos para Aula
    public void CadastrarAula(int diarioId, string diaSemana, int numeroAula)
    {
        var diario = GetDiarioPorId(diarioId);
        if (diario == null) return;

        var maxId = _escola.Series.SelectMany(s => s.Turmas)
            .SelectMany(t => t.Diarios)
            .SelectMany(d => d.Aulas)
            .DefaultIfEmpty(new Aula { ID = 0 })
            .Max(a => a.ID);
        
        var novaAula = new Aula
        {
            ID = maxId + 1,
            DiaSemana = diaSemana,
            NumeroAula = numeroAula
        };
        diario.Aulas.Add(novaAula);
        Save();
    }

    public void AlterarAula(int id, int diarioId, string diaSemana, int numeroAula)
    {
        var aula = _escola.Series
            .SelectMany(s => s.Turmas)
            .SelectMany(t => t.Diarios)
            .SelectMany(d => d.Aulas)
            .FirstOrDefault(a => a.ID == id);
        
        if (aula != null)
        {
            // Remover a aula do diário atual
            var diarioAtual = _escola.Series
                .SelectMany(s => s.Turmas)
                .SelectMany(t => t.Diarios)
                .FirstOrDefault(d => d.Aulas.Contains(aula));
            
            if (diarioAtual != null)
            {
                diarioAtual.Aulas.Remove(aula);
            }
            
            // Adicionar a aula ao novo diário
            var novoDiario = GetDiarioPorId(diarioId);
            if (novoDiario != null)
            {
                aula.DiaSemana = diaSemana;
                aula.NumeroAula = numeroAula;
                novoDiario.Aulas.Add(aula);
                Save();
            }
        }
    }

    public void ExcluirAula(int id)
    {
        var aula = _escola.Series
            .SelectMany(s => s.Turmas)
            .SelectMany(t => t.Diarios)
            .SelectMany(d => d.Aulas)
            .FirstOrDefault(a => a.ID == id);
        
        if (aula != null)
        {
            var diario = _escola.Series
                .SelectMany(s => s.Turmas)
                .SelectMany(t => t.Diarios)
                .FirstOrDefault(d => d.Aulas.Contains(aula));
            
            if (diario != null)
            {
                diario.Aulas.Remove(aula);
                Save();
            }
        }
    }

    // Métodos para Estudante
    public void CadastrarEstudante(int turmaId, string nome)
    {
        var turma = GetTurmaPorId(turmaId);
        if (turma == null) return;

        var maxId = _escola.Series.SelectMany(s => s.Turmas)
            .SelectMany(t => t.Estudantes)
            .DefaultIfEmpty(new Estudante { ID = 0 })
            .Max(e => e.ID);
        
        var novo = new Estudante
        {
            ID = maxId + 1,
            Nome = nome
        };
        turma.Estudantes.Add(novo);
        Save();
    }

    public void AlterarEstudante(int id, string nome)
    {
        var estudante = _escola.Series
            .SelectMany(s => s.Turmas)
            .SelectMany(t => t.Estudantes)
            .FirstOrDefault(e => e.ID == id);
        
        if (estudante != null)
        {
            estudante.Nome = nome;
            Save();
        }
    }

    public void ExcluirEstudante(int id)
    {
        var estudante = _escola.Series
            .SelectMany(s => s.Turmas)
            .SelectMany(t => t.Estudantes)
            .FirstOrDefault(e => e.ID == id);
        
        if (estudante != null)
        {
            var turma = _escola.Series
                .SelectMany(s => s.Turmas)
                .FirstOrDefault(t => t.Estudantes.Contains(estudante));
            
            if (turma != null)
            {
                turma.Estudantes.Remove(estudante);
                Save();
            }
        }
    }
}
