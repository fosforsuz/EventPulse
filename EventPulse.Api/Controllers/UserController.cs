using EventPulse.Api.Models;
using EventPulse.Application.Commands.User.UserCreate;
using EventPulse.Application.Commands.User.UserDelete;
using EventPulse.Application.Commands.User.UserUpdate;
using EventPulse.Application.Queries.Dtos;
using EventPulse.Application.Queries.FindUserById;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventPulse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request)
    {
        if (await _mediator.Send(request) is not Result<int> result)
            return BadRequest(ResponseModel.Error("An error occurred while creating the user."));

        if (result.IsSuccess)
        {
            return CreatedAtAction(
                nameof(GetUserById),
                new { id = result.Value, message = "User created successfully." }
            );
        }

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand request)
    {
        var result = await _mediator.Send(request);

        if (result.IsSuccess)
            return NoContent();

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpPut]
    [Route("delete-user/{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (await _mediator.Send(new DeleteUserCommand(Id: id)) is not { } result)
            return BadRequest(ResponseModel.Error("An error occurred while deleting the user."));

        if (result.IsSuccess)
            return NoContent();

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetUserById(int id, string message = "")
    {
        if (await _mediator.Send(new FindUserByIdQuery(Id: id)) is not Result<UserDto> result)
            return BadRequest(ResponseModel.Error("An error occurred while fetching the user."));

        if (result.IsSuccess)
            return Ok(string.IsNullOrWhiteSpace(message)
                ? ResponseModel.Success(data: result.Value)
                : ResponseModel.Success(data: result.Value, message: message));


        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(message: errorMessage));
    }
}