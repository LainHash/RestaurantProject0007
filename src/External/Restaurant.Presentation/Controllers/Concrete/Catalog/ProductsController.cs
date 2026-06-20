using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Catalog.Products.Commands.Create;
using Restaurant.Application.Features.Catalog.Products.Commands.Delete;
using Restaurant.Application.Features.Catalog.Products.Commands.Restore;
using Restaurant.Application.Features.Catalog.Products.Commands.Update;
using Restaurant.Application.Features.Catalog.Products.Commands.UploadImage;
using Restaurant.Application.Features.Catalog.Products.Queries.GetAll;
using Restaurant.Application.Features.Catalog.Products.Queries.GetOne;
using Restaurant.Contracts.DTOs.Catalog.Misc;
using Restaurant.Contracts.DTOs.Catalog.Products;
using Restaurant.Presentation.Controllers.Abstraction;

namespace Restaurant.Presentation.Controllers.Concrete.Catalog
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new GetProductByIdQuery(id), cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new CreateProductCommand(request), cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellation)
        {
            var result = await Sender.Send(new UpdateProductCommand(id, request), cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellation)
        {
            var result = await Sender.Send(new DeleteProductCommand(id), cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id, CancellationToken cancellation)
        {
            var result = await Sender.Send(new RestoreProductCommand(id), cancellation);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>Upload ảnh cho product (multipart/form-data). Tối đa 5 ảnh, 1 primary.</summary>
        [HttpPost("{id}/images")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage(
            [FromRoute] Guid id,
            IFormFile file,
            [FromForm] UploadImageRequest metadata,
            CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0)
                return BadRequest("File ảnh không được để trống.");

            await using var stream = file.OpenReadStream();

            var command = new UploadProductImageCommand(
                id,
                stream,
                file.FileName,
                metadata);

            var result = await Sender.Send(command, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
