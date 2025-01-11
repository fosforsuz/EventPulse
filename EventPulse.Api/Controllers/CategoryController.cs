using System.ComponentModel;
using Asp.Versioning;
using EventPulse.Api.Models;
using EventPulse.Application.Queries.Category.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPulse.Api.Controllers;

[ApiVersion(1)]
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-categories")]
    [AllowAnonymous]
    [Description("Get all categories")]
    [MapToApiVersion(1)]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _mediator.Send(new GetCategoriesQuery(), HttpContext.RequestAborted);

        if (result.IsSuccess)
            return Ok(ResponseModel.Success(data: result.Value ?? new object()));

        return BadRequest(ResponseModel.Error("Failed to get categories"));
    }
}