﻿namespace CapgeminiQ5.Domain.Entities;

public class ContaCorrente
{
    public Guid Id { get; set; }
    public int Numero { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Ativa { get; set; }
}
