# Documentação Técnica – Questão 5: Movimentação Bancária

##  Projeto: `CapgeminiQ5`
##  Testes Unitários: `CapgeminiQ5.Tests`
## Objetivo da funcionalidade

Implementar uma API REST para:
- Registrar movimentações bancárias (crédito ou débito)
- Respeitar regra de idempotência
- Verificar se a conta está ativa
- Retornar o ID da movimentação
- Consultar saldo da conta

---

## Tecnologias utilizadas

| Tecnologia     | Uso                                     |
|----------------|------------------------------------------|
| .NET 8         | Framework principal                      |
| SQLite         | Banco de dados local                     |
| Dapper         | Acesso a dados via SQL                   |
| MediatR        | Padrão CQRS com handlers                 |
| xUnit          | Testes unitários                         |
| NSubstitute    | Mocks das dependências nos testes        |
| Swagger        | Documentação da API                      |

---

## Testes com Mock – `MovimentarContaCommandHandlerTests`

### Objetivo

Garantir o comportamento da movimentação bancária sem depender de banco de dados real, usando mocks das interfaces:

- `IContaRepository`
- `IMovimentoRepository`

---

## Cenários de Teste

### 1. Deve registrar movimentação com sucesso

- **Condições:**
  - Conta está ativa
  - Chave de idempotência não existe

- **Mocks:**
```csharp
_movimentoRepo.IdempotenciaExiste(...).Returns(false);
_contaRepo.ContaAtiva(...).Returns(true);
_movimentoRepo.RegistrarMovimento(...).Returns(Task.FromResult(Guid.NewGuid()));
```

- **Asserts:**
  - Retorno deve ser um `Guid`
  - `RegistrarMovimento` deve ser chamado 1x

---

### 2. Não deve registrar se a conta estiver inativa

- **Condições:**
  - Conta está inativa

- **Mocks:**
```csharp
_contaRepo.ContaAtiva(...).Returns(false);
```

- **Asserts:**
  - Deve lançar `ArgumentException`

---

### 3. Deve retornar ID existente se a chamada for idempotente

- **Condições:**
  - Chave de idempotência já existe

- **Mocks:**
```csharp
_movimentoRepo.IdempotenciaExiste(...).Returns(true);
_movimentoRepo.ObterIdPorChave(...).Returns(existingId.ToString());
```

- **Asserts:**
  - Retorno deve ser o mesmo `Guid` já salvo anteriormente

---

## JSON de Entrada (POST /movimentacao)

```json
{
  "chaveIdempotencia": "abc-12345",
  "idContaCorrente": "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
  "tipoMovimento": "C",
  "valor": 200.00
}
```

## JSON de Saída (Sucesso)

```json
{
  "id": "4d632d97-c209-4890-a764-4176a8ff3e99"
}
```

## JSON de Saída (Erro - Conta inativa)

```json
{
  "error": "VALIDATION_ERROR",
  "message": "Conta inativa ou inexistente."
}
```


---

## Considerações Finais

- Toda a lógica crítica foi validada por **testes unitários com mocks**, isolando as dependências
- Testes rodam com `dotnet test` sem dependência de banco
- A API segue boas práticas de arquitetura com `CQRS + MediatR + Repositórios`
- A estrutura facilita manutenção, extensão e testes futuros
