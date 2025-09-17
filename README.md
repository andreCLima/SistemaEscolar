Um sistema de gestão escolar desenvolvido em **Blazor Server** com **.NET 8** 
para controle de séries, turmas, estudantes e diários de classe.

## ✨ Funcionalidades

### 📚 Gestão de Séries
- Visualizar todas as séries cadastradas
- Cadastrar novas séries
- Alterar informações das séries

### 🎓 Gestão de Turmas
- Visualizar turmas por série
- Cadastrar novas turmas
- Alterar informações das turmas
- Excluir turmas

### 👥 Gestão de Estudantes
- Visualizar estudantes por turma
- Cadastrar novos estudantes
- Alterar dados dos estudantes
- Excluir estudantes

### 📖 Gestão de Diários
- Visualizar diários por turma
- Cadastrar novos diários
- Gerenciar registros de aula

## 🛠️ Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **Blazor Server** - Interface web interativa
- **Bootstrap** - Estilização responsiva
- **JSON** - Persistência de dados

## 🎯 Principais Páginas

- **Escola** - Visão geral das séries
- **Turmas** - Gerenciamento de turmas por série
- **Detalhes da Turma** - Visualização completa da turma
- **Diários** - Controle de diários de classe
- **Detalhes do Diário** - Registros de aula

## 🔧 Funcionalidades Técnicas

### Validação de Dados
- Inputs de nome aceitam apenas letras sem acentuação
- Remoção automática de caracteres especiais

### Persistência
- Dados salvos em arquivo JSON (`escola.json`)
- Carregamento automático dos dados na inicialização
- Dados de teste incluídos para demonstração

### 📊 Dados de Teste

O sistema inclui dados de exemplo para testes

## 🚀 Como Executar

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 ou VS Code

### Instalação

1. **Clone o repositório**
   ```bash
   git clone https://github.com/andreCLima/SistemaEscolar.git
   cd SistemaEscolarWeb
   ```

2. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

3. **Execute o projeto**
   ```bash
   dotnet run
   ```

4. **Acesse no navegador**
   ```
   http://localhost:5177
   ```
   
## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 👨‍💻 Autor

André Lima

---

**Sistema Escolar Web** - Transformando a gestão educacional com tecnologia moderna! 🚀
