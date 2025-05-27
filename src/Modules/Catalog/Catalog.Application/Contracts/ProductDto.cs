﻿namespace Catalog.Application.Contracts;

public sealed class ProductDto
{
    public Guid Id { get; init; }
    public string? Name{ get; init; }
    public List<string>? Category{ get; init; }
    public string? Description{ get; init; }
    public string? ImageFile{ get; init; }
    public decimal Price{ get; init; }
}