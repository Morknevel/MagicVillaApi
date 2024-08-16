using MagicVilla.Models.Dto;

namespace MagicVilla.Data;

public static class VillaStore
{
    public static List<VillaDTO> villaList = new List<VillaDTO>()
    {
        new VillaDTO { Id = 1, Name = "Pool View" },
        new VillaDTO { Id = 1, Name = "Beach View" }
    };
}