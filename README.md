# Global Solution — Microservice: API de Versionamento de Prompts de IA

## Desenvolvedores
| Nome | RM |
|------|----|
| Cintia Cristina Braga Angelo | RM552253 |
| Henrique Mosseri | RM552240 |

---

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

| Coluna       | Tipo SQL                                 | Obrigatório |
|--------------|-------------------------------------------|-------------|
| IdPrompt     | INT AUTO_INCREMENT                        | Sim         |
| Nome         | VARCHAR(100)                              | Sim         |
| Descricao    | TEXT                                      | Não         |
| Versao       | INT                                       | Sim         |
| DataCriacao  | DATETIME                                  | Sim         |
| Autor        | VARCHAR(100)                              | Sim         |
| TipoModelo   | ENUM('Texto','Imagem','Audio','Video')    | Sim         |
| StatusPrompt | ENUM('Ativo','Inativo','Arquivado')       | Sim         |

---

### Branch Criada
`feature/modelagem-prompt`

Essa branch contém:
- Script SQL de criação do banco (`/scripts/create_database.sql`)
- Models (`/Models/Prompt.cs`)
- Contexto de banco (`/Data/PromptContext.cs`)
- Arquivo README documentando esta etapa.

---

#  Tratamento de Exceções - Camada Service

##  Contexto
Durante a implementação da camada **Service** do projeto de versionamento de prompts, foi adicionado um **tratamento centralizado de exceções** com o objetivo de:
- Garantir respostas consistentes para erros de execução;
- Evitar propagação de exceções não tratadas até a Controller;
- Facilitar o rastreamento e o log de falhas.

---

##  Estratégia Utilizada

Cada método da `PromptService` foi encapsulado em **blocos `try-catch`**, garantindo que qualquer exceção seja capturada e devidamente tratada antes de retornar para a Controller.

A Service comunica o resultado à Controller através de **retornos nulos** ou **exceções customizadas** (dependendo do tipo de erro), permitindo que a Controller responda com o status HTTP adequado (`BadRequest`, `NotFound`, `Ok`, etc).

---

#  Etapa 2 — Camada Service e Controllers

### Estrutura Implementada
- Controller: `PromptController`
- Service: `PromptService`
- Interface: `IPromptService`
- Contexto: `AppPromptContext`

A arquitetura segue o padrão **Service Layer**, garantindo **responsabilidade única** e **injeção de dependência** configurada no `Program.cs`.

### Métodos REST
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET    | /api/prompt | Retorna todos os prompts |
| GET    | /api/prompt/{id} | Retorna um prompt específico |
| POST   | /api/prompt | Cria um novo prompt |
| PUT    | /api/prompt/{id} | Atualiza um prompt existente |
| DELETE | /api/prompt/{id} | Remove um prompt |

Todos os métodos foram implementados de forma **assíncrona (`async/await`)** com o **Entity Framework Core**.

---

## Etapa 3 — Testes com JSONs

### Requisições de Teste (POST)

**Exemplo 1**
```json
{
  "idPrompt": 0,
  "nome": "Gerador de Ideias Criativas",
  "descricao": "Prompt para geração de ideias de produtos inovadores utilizando IA generativa.",
  "versao": 1,
  "dataCriacao": "2025-11-12T23:37:11.561Z",
  "autor": "Cintia Cristina Braga Angelo",
  "tipoModelo": 0,
  "statusPrompt": 0
}
```

**Exemplo 2**
```json
{
  "idPrompt": 0,
  "nome": "Analisador de Sentimentos",
  "descricao": "Prompt para análise de sentimento em textos de redes sociais e avaliações de produtos.",
  "versao": 3,
  "dataCriacao": "2025-11-12T20:45:22.871Z",
  "autor": "Henrique Mosseri",
  "tipoModelo": 0,
  "statusPrompt": 0
}
```

---

##  Testes adicionais (GET / PUT / DELETE)
- `GET /api/prompt` — deve retornar lista de prompts (200 OK).
- `GET /api/prompt/{id}` — deve retornar 200 OK ou 404 Not Found.
- `POST /api/prompt` — deve retornar 201 Created com `Location` apontando para `GET /api/prompt/{id}`.
- `PUT /api/prompt/{id}` — deve retornar 200 OK ou 404 Not Found.
- `DELETE /api/prompt/{id}` — deve retornar 200 OK ou 404 Not Found.

---

##  Resumo Técnico
- **Linguagem:** C#
- **Framework:** .NET 8.0
- **Banco de Dados:** MySQL
- **ORM:** Entity Framework Core (compatível com Dapper)
- **Padrão de Projeto:** Service Layer + Dependency Injection
- **Tratamento de Erros:** Try-Catch + Logging
- **Testes:** JSONs compatíveis com os endpoints

---

## Estrutura do Repositório
```
/PromptAPI
  /Controllers
    PromptController.cs
  /Services
    IPromptService.cs
    PromptService.cs
  /Data
    AppPromptContext.cs
  /Models
    Prompt.cs
  /scripts
    create_database.sql
  README_GLOBAL_SOLUTION.md
```

---

## Observações Finais
O projeto está pronto para ser executado localmente após configuração da connection string em `appsettings.json` e execução das migrações. Para executar:
```bash
dotnet ef database update
dotnet run
```

---

