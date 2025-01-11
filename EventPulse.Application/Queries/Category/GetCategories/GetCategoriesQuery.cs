using EventPulse.Application.Queries.Dtos;
using FluentResults;
using MediatR;

namespace EventPulse.Application.Queries.Category.GetCategories;

public record GetCategoriesQuery() : IRequest<Result<List<CategoryDto>>>;