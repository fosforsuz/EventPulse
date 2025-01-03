using EventPulse.Api.Models;
using EventPulse.Application.Commands.Authenticate;
using EventPulse.Application.Commands.User.UserCreate;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventPulse.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticateCommand request)
    {
        if (await _mediator.Send(request: request) is not { } result)
            return BadRequest(ResponseModel.Error("Failed to login user."));

        if (result.IsSuccess)
            return Ok(ResponseModel.Success(message: "User logged in successfully.", data: result.Value));

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserCommand request)
    {
        if (await _mediator.Send(request) is not Result<int> result)
            return BadRequest(ResponseModel.Error("Failed to create user."));

        if (result.IsSuccess)
            return Ok(ResponseModel.Success(message: "User created successfully.", data: result.Value));

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }
}