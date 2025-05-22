namespace CapgeminiQ5.Application.DTOs;

public class SaldoResponseDTO
{
    public int Numero { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataConsulta { get; set; }
    public decimal Saldo { get; set; }
}
