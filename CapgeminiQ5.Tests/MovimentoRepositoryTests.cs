//using Xunit;
//using CapgeminiQ5.Infrastructure.Repositories;
//using CapgeminiQ5.Application.DTOs;
//using System;

//namespace CapgeminiQ5.Tests.Repositories;

//public class MovimentoRepositoryTests
//{
//    [Fact]
//    public void Deve_Registrar_Credito_E_Debito_E_Calcular_Saldo()
//    {
//        // Arrange
//        var repo = new MovimentoRepository();
//        var idConta = Guid.NewGuid();

//        var credito = new MovimentacaoRequestDTO
//        {
//            ChaveIdempotencia = "teste-cred",
//            IdContaCorrente = idConta,
//            TipoMovimento = "C",
//            Valor = 300
//        };

//        var debito = new MovimentacaoRequestDTO
//        {
//            ChaveIdempotencia = "teste-deb",
//            IdContaCorrente = idConta,
//            TipoMovimento = "D",
//            Valor = 100
//        };

//        // Act
//        repo.RegistrarMovimento(credito);
//        repo.RegistrarMovimento(debito);
//        var saldo = repo.CalcularSaldo(idConta);

//        // Assert
//        Assert.Equal(200, saldo);
//    }

//    [Fact]
//    public void Deve_Retornar_Mesmo_Id_Para_Mesma_Chave()
//    {
//        var repo = new MovimentoRepository();
//        var idConta = Guid.NewGuid();

//        var dto = new MovimentacaoRequestDTO
//        {
//            ChaveIdempotencia = "1234-abc",
//            IdContaCorrente = idConta,
//            TipoMovimento = "C",
//            Valor = 100
//        };

//        var id1 = repo.RegistrarMovimento(dto);
//        var existe = repo.IdempotenciaExiste("1234-abc");
//        var id2 = repo.ObterIdPorChave("1234-abc");

//        Assert.True(existe);
//        Assert.Equal(id1, id2);
//    }
//}
