﻿namespace LibrariesWeb.Application.Helpers;

public class JwtSettings
{
    public required string Key { get; init; }
    public required string Audience { get; init; }
    public required string Issuer { get; init; }
    public required int DurationInMinutes { get; init; }
}