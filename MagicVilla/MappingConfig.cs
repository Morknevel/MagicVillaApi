﻿using AutoMapper;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla.Models;
using MagicVilla.Models.Dto;

namespace MagicVilla;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Villa, VillaDTO>().ReverseMap();
      
        CreateMap<Villa, VillaCreateDTO>().ReverseMap();
        CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
    }
}