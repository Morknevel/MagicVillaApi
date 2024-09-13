﻿using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.VM;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers;

public class VillaNumberController : Controller
{
    private readonly IVillaNumberService _villaNumberService;
    private readonly IMapper _mapper;
    private readonly IVillaService _villaService;

    public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService)
    {
        _villaNumberService = villaNumberService;
        _mapper = mapper;
        _villaService = villaService;
    }

    public async Task<IActionResult> IndexVillaNumber()
    {
        List<VillaNumberDTO> list = new();

        var response = await _villaNumberService.GetAllAsync<APIResponse>();
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
        }

        return View(list);
    }

    public async Task<ActionResult> CreateVillaNumber()
    {
        VillaNumberCreateVM villaNumberVM = new();
        var response = await _villaService.GetAllAsync<APIResponse>();
        if (response != null && response.IsSuccess)
        {
            villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                (Convert.ToString(response.Result)).Select(i => new SelectListItem{ Text = i.Name,
                Value = i.Id.ToString()
        });
            ;
        }
    return View(villaNumberVM);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> CreateVillaNumber(VillaNumberCreateDTO model)
{
    if (ModelState.IsValid)
    {
        var response = await _villaNumberService.CreateAsync<APIResponse>(model);
        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(IndexVillaNumber));
        }
    }

    return View(model);
}

}