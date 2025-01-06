using EventPulse.Api.Models;
using EventPulse.Application.Commands.Authenticate;
using EventPulse.Application.Commands.User.CreateEmailToken;
using EventPulse.Application.Commands.User.CreateForgotPasswordToken;
using EventPulse.Application.Commands.User.ResetPassword;
using EventPulse.Application.Commands.User.UserCreate;
using EventPulse.Application.Commands.User.VerifyEmailToken;
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
        if (await _mediator.Send(request) is not { } result)
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

    [HttpPost]
    [Route("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] CreateForgotPasswordTokenCommand request)
    {
        if (await _mediator.Send(request: request) is not { } result)
            return BadRequest(ResponseModel.Error("Failed to create forgot password token."));

        if (result.IsSuccess)
            return Ok(ResponseModel.Success(message: "Forgot password token created successfully.",
                data: result.Value));

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpPost]
    [Route("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
    {
        if (await _mediator.Send(request: request) is not { } result)
            return BadRequest(ResponseModel.Error("Failed to reset password."));

        if (result.IsSuccess)
            return Ok(ResponseModel.Success(message: "Password reset successfully.", data: result.Value));

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }


    [HttpPost]
    [Route("send-verification-email")]
    public async Task<IActionResult> SendVerificationEmail([FromBody] CreateEmailTokenCommand request)
    {
        if (await _mediator.Send(request: request) is not { } result)
            return BadRequest(ResponseModel.Error("Failed to send verification email."));

        if (result.IsSuccess)
            return Ok(ResponseModel.Success(message: "Verification email sent successfully.", data: result.Value));

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpPost]
    [Route("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailTokenCommand request)
    {
        if (await _mediator.Send(request: request) is not { } result)
            return BadRequest(ResponseModel.Error("Failed to verify email."));

        if (result.IsSuccess)
            return Ok(ResponseModel.Success(message: "Email verified successfully.", data: result.Value));

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }
}