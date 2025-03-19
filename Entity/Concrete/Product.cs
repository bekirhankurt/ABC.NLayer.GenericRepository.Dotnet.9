﻿using Core.Entities;

namespace Entity.Concrete;

public class Product : IEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
}