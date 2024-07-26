using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Order;
using Proyecto_Poo.Dtos.Package;
using Proyecto_Poo.Service;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Controllers
{
    [Route("api/packages")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            this._packageService = packageService;
        }


        [HttpGet]
        public async Task<ActionResult<Response<PackageDto>>> GetAll()
        {
            var response = await _packageService.GetPackageListAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]


        public async Task<ActionResult<Response<PackageDto>>> GetOneById(Guid id)
        {
            var response = await _packageService.GetPackageByIdAsync( id);
            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto<PackageDto>>> Create(PackageCreateDto dto)
        {
            var response = await _packageService.CreatePackageAsync(dto);
            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<PackageDto>>> Edit(PackageEditDto dto, Guid id)
        {
            var response = await _packageService.EditPackageAsync(dto, id);
            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var response = await _packageService.DeletePackageAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
