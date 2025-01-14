using EventPulse.Application.Interfaces;
using EventPulse.Infrastructure.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace EventPulse.Application.Commands.Event.EventCreate;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Result<int>>
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateEventCommand> _validator;

    public CreateEventCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateEventCommand> validator,
        IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<int>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Fail<int>("Validation failed")
                .WithErrors(validationResult.Errors.Select(error => error.ErrorMessage));

        if (request.EventDate.Date < DateTime.Now.Date)
            return Result.Fail<int>("Event date cannot be in the past");

        var newEvent = new Domain.Entities.Event(request.Title, request.Description,
            request.Location, request.EventDate, request.CreatorId, false,
            false, request.CategoryId);

        await _unitOfWork.EventRepository.AddAsync(newEvent);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (request.ImageStream is not null)
        {
            var filePath = _fileStorageService.SaveFile(newEvent.Id, request.ImageStream);
            newEvent.SetImagePath(filePath);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result.Ok(newEvent.Id);
    }
}