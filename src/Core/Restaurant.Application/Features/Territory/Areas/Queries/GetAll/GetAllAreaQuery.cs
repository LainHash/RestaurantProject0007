using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Territory.Areas;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetAll
{
    public record GetAllAreaQuery : IQuery<DataResult<IEnumerable<AreaResponse>>>
    {
        public string? AreaType { get; set; }
    }
}
