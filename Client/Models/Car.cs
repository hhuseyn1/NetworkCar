﻿using System;

namespace Client.Models;
[Serializable]
public class Car
{
    public int Id { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public ushort Year { get; set; }
    public string? VIN { get; set; }
    public string? Color { get; set; }

    public override string ToString()
    {
        return $"{Id} {Make ?? "null"} {Model ?? "null"} {Year} {VIN ?? "null"} {Color ?? "null"}";
    }
}

