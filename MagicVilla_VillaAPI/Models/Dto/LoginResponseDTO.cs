﻿namespace MagicVilla.Models.Dto;

public class LoginResponseDTO
{
    public LocalUser LocalUser { get; set; }
    public string Token { get; set; }
}