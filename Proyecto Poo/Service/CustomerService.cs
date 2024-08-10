using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Clientes;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Service.Interface;
using System.Xml;

namespace Proyecto_Poo.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly PackageServiceDbContext _context;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public CustomerService(PackageServiceDbContext context, ILogger<OrderService> logger, IMapper mapper)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
        }
        public async Task<ResponseDto<List<CustomerDto>>> GetCustomerListAsync()
        {
            return new ResponseDto<List<CustomerDto>>()
            {
                StatusCode = 200,
                Status = true,
                Message = "Listado obtenido Correctamente",
                Data = new List<CustomerDto>(),
            };
        }
         public Task<ResponseDto<CustomerDto>> GetCustomersByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<ResponseDto<CustomerDto>> CreateAsync(CustomerCreateDto dto)
        {
            var customerEntity = _mapper.Map<CustomerEntity>(dto);
            customerEntity.Id = new Guid();

            _context.Customers.Add(customerEntity);
            await _context.SaveChangesAsync();

            var customerDto = _mapper.Map<CustomerDto>(dto);
            return new ResponseDto<CustomerDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "El Cliente sea creado",
                Data = customerDto,
            };
        }
        public async Task<ResponseDto<CustomerDto>> EditAsync(CustomerEditDto dto, Guid id)
        {
            var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customerEntity == null)
            {
                return new ResponseDto<CustomerDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El pedido {id} no fue encontrado"
                };
            }
            _mapper.Map(dto, customerEntity);
            
            _context.Customers.Update(customerEntity);
            await _context.SaveChangesAsync();
            var customerDto = _mapper.Map<CustomerDto>(customerEntity);
            return new ResponseDto<CustomerDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "El cliente se editado correctamente",
                Data = customerDto,
            };
                
        }

        public async Task<ResponseDto<CustomerDto>> DeleteAsync(Guid id)
        {
            var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customerEntity == null) {
                return new ResponseDto<CustomerDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El cliente {id} no fue encontrado"
                };

            }
            _context.Customers.Remove(customerEntity); ;
            await _context.SaveChangesAsync();

            return new ResponseDto<CustomerDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "El cliente fue eliminado"

            };
        }

        

       

       
    }
}