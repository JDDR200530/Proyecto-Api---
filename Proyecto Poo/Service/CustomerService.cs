using Proyecto_Poo.Dtos.Clientes;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Service.Interface;
using System.Xml;

namespace Proyecto_Poo.Service
{
    public class CustomerService : ICustomerService
    {
         public async Task<ResponseDto<List<CustomerDto>>> GetClientesListAsync()
        {
            return new ResponseDto<List<CustomerDto>>()
            {
                StatusCode = 200,
                Status = true,
                Message = "Listado obtenido Correctamente",
                Data = new List<CustomerDto>(),
            };
        }
         public Task<ResponseDto<CustomerDto>> GetClientesByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseDto<CustomerDto>> CreateAsync(CustomerCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<CustomerDto>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<CustomerDto>> EditAsync(CustomerEditDto dto, Guid id)
        {
            throw new NotImplementedException();
        }

       

       
    }
}