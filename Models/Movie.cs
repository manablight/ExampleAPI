﻿namespace ExampleAPI.Models;

public class Movie
{
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public int YearOfRelease { get; set; }
}