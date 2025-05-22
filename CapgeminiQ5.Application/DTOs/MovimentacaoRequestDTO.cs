namespace CapgeminiQ5.Application.DTOs;

public class MovimentacaoRequestDTO
{
    public string ChaveIdempotencia { get; set; } = string.Empty;
    public Guid IdContaCorrente { get; set; }
    public string TipoMovimento { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}
