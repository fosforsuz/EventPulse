using System.ComponentModel;
using Asp.Versioning;
using EventPulse.Api.Models;
using EventPulse.Application.Commands.Authenticate;
using EventPulse.Application.Commands.User.CreateEmailToken;
using EventPulse.Application.Commands.User.CreateForgotPasswordToken;
using EventPulse.Application.Commands.User.ResetPassword;
using EventPulse.Application.Commands.User.UserCreate;
using EventPulse.Application.Commands.User.VerifyEmailToken;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPulse.Api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    private IActionResult HandleResult<T>(Result<T> result, string successMessage)
    {
        if (result.IsSuccess)
            return Ok(ResponseModel.Success(message: successMessage, data: result.Value ?? new object()));

        if (result.Errors.Any(error => error.Message.Contains("Unauthorized")))
            return Unauthorized(ResponseModel.Error("Unauthorized"));

        var errorMessage = string.Join(", ", result.Errors.Select(error => error.Message));
        return BadRequest(ResponseModel.Error(errorMessage));
    }

    [HttpPost]
    [Route("login")]
    [Description("Authenticate user")]
    [MapToApiVersion(1)]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] AuthenticateCommand request)
    {
        _logger.LogInformation("Login attempt for user: {Email}", request.Email);
        var result = await _mediator.Send(request, HttpContext.RequestAborted);
        return HandleResult(result, "User logged in successfully.");
    }

    [HttpPost]
    [Route("register")]
    [Description("Register user")]
    [MapToApiVersion(1)]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] CreateUserCommand request)
    {
        _logger.LogInformation("Registering user: {Email}", request.Email);
        return await _mediator.Send(request, HttpContext.RequestAborted) is not Result<int> result
            ? BadRequest(ResponseModel.Error("An error occurred while creating user."))
            : HandleResult(result, "User created successfully.");
    }

    [HttpPost]
    [Route("forgot-password")]
    [Description("Create forgot password token")]
    [MapToApiVersion(1)]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPassword([FromBody] CreateForgotPasswordTokenCommand request)
    {
        _logger.LogInformation("Creating forgot password token for user: {Email}", request.Email);
        var result = await _mediator.Send(request, HttpContext.RequestAborted);
        return HandleResult(result, "Forgot password token created successfully.");
    }

    [HttpPost]
    [Route("reset-password")]
    [Description("Reset user password")]
    [MapToApiVersion(1)]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
    {
        _logger.LogInformation("Resetting password for user: {Email}", request.Token);
        var result = await _mediator.Send(request, HttpContext.RequestAborted);
        return HandleResult(result, "Password reset successfully.");
    }

    [HttpPost]
    [Route("send-verification-email")]
    [Description("Send verification email")]
    [MapToApiVersion(1)]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SendVerificationEmail([FromBody] CreateEmailTokenCommand request)
    {
        _logger.LogInformation("Sending verification email to user: {Email}", request.Id);
        var result = await _mediator.Send(request, HttpContext.RequestAborted);
        return HandleResult(result, "Verification email sent successfully.");
    }

    [HttpPost]
    [Route("verify-email")]
    [Description("Verify email")]
    [MapToApiVersion(1)]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailTokenCommand request)
    {
        _logger.LogInformation("Verifying email for user: {Email}", request.Token);
        var result = await _mediator.Send(request, HttpContext.RequestAborted);
        return HandleResult(result, "Email verified successfully.");
    }
}