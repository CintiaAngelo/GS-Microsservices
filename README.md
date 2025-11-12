# API de Versionamento de Prompts de IA

##  Objetivo
Sistema para versionar, rastrear e gerenciar diferentes versões de prompts usados no treinamento de modelos de IA.

---

##  Etapa 1 — Modelagem e Banco de Dados

### Implementações
- Criação da tabela **Prompt** em MySQL.
- Criação da classe **Prompt** com atributos e enums equivalentes ao SQL.
- Criação do contexto **PromptContext** configurado para MySQL.
- Todos os nomes de propriedades e colunas são **idênticos**.
- Projeto preparado para integração com **Dapper** ou **Entity Framework Core**.

---

### Estrutura da Tabela

| Coluna        | Tipo SQL                                  | Obrigatório |
|----------------|--------------------------------------------|--------------|
| IdPrompt       | INT AUTO_INCREMENT                         | Sim |
| Nome           | VARCHAR(100)                               | Sim |
| Descricao      | TEXT                                       | Não |
| Versao         | INT                                        | Sim |
| DataCriacao    | DATETIME                                   | Sim |
| Autor          | VARCHAR(100)                               | Sim |
| TipoModelo     | ENUM('Texto','Imagem','Audio','Video')     | Sim |
| StatusPrompt   | ENUM('Ativo','Inativo','Arquivado')        | Sim |

---

### Branch Criada
`feature/modelagem-prompt`

Essa branch contém:
- Script SQL de criação do banco (`/scripts/create_database.sql`)
- Models (`/Models/Prompt.cs`)
- Contexto de banco (`/Data/PromptContext.cs`)
- Arquivo README documentando esta etapa.

---

### Próximos Passos
- Implementar repositório com **Dapper** para CRUD de `Prompt`.
- Criar endpoints da API (`GET`, `POST`, `PUT`, `DELETE`).
