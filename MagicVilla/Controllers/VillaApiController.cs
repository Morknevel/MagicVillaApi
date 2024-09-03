using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Controllers;

[Route("api/VillaAPI")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    
    public VillaAPIController(ApplicationDbContext db)
    {
        _db = db;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
    {
        return Ok(await _db.Villas.ToListAsync());
    }
    [HttpGet("{id:int}",Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public  async Task<ActionResult<VillaDTO>> GetVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
        if (villa==null)
        {
            return NotFound();
        }

        return Ok(villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO villaDTO)
    {
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }
        if (await _db.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "Villa already Exists!");
            return BadRequest(ModelState);
        }
        if (villaDTO == null)
        {
            return BadRequest(villaDTO);
        }
        Villa model = new()
        {
            Amenity = villaDTO.Amenity,
            Details = villaDTO.Details,
            ImageUrl = villaDTO.ImageUrl,
            Name = villaDTO.Name,
            Occupancy = villaDTO.Occupancy,
            Rate = villaDTO.Rate,
            Sqft = villaDTO.Sqft
        };
        _db.Villas.AddAsync(model);
        _db.SaveChangesAsync();
        return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
    }
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    public async Task<ActionResult> DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var villa =await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        _db.Villas.Remove(villa);
        await _db.SaveChangesAsync();
       return NoContent();
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDTO)
    {
        if (villaDTO == null || id != villaDTO.Id)
        {
            return BadRequest();
        }
        Villa model = new()
        {
            Amenity = villaDTO.Amenity,
            Details = villaDTO.Details,
            Id = villaDTO.Id,
            ImageUrl = villaDTO.ImageUrl,
            Name = villaDTO.Name,
            Occupancy = villaDTO.Occupancy,
            Rate = villaDTO.Rate,
            Sqft = villaDTO.Sqft
        };
        _db.Villas.Update(model);
        _db.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest();
        }

        var villa =  await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        if (villa == null)
        {
            return BadRequest();
        }
        VillaUpdateDTO villaDTO = new()
        {
            Amenity = villa.Amenity,
            Details = villa.Details,
            Id = villa.Id,
            ImageUrl = villa.ImageUrl,
            Name = villa.Name,
            Occupancy = villa.Occupancy,
            Rate = villa.Rate,
            Sqft = villa.Sqft
        }; 

        patchDTO.ApplyTo(villaDTO, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Villa model = new Villa()
        {
            Amenity = villaDTO.Amenity,
            Details = villaDTO.Details,
            Id = villaDTO.Id,
            ImageUrl = villaDTO.ImageUrl,
            Name = villaDTO.Name,
            Occupancy = villaDTO.Occupancy,
            Rate = villaDTO.Rate,
            Sqft = villaDTO.Sqft
        };
        _db.Villas.Update(model);
        _db.SaveChangesAsync();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();
    }
}