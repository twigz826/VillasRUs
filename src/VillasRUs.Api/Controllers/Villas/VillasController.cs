﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VillasRUs.Application.Villas.SearchVillas;

namespace VillasRUs.Api.Controllers.Villas;

[Authorize]
[ApiController]
[Route("api/villas")]
public class VillasController : ControllerBase
{
    private readonly ISender _sender;

    public VillasController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetVillas(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var query = new SearchVillasQuery(startDate, endDate);
        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
}
