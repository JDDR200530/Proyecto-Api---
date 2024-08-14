using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Clientes;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Order;
using Proyecto_Poo.Service.Interface;
using System.Text.RegularExpressions;
using System.Xml;

namespace Proyecto_Poo.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly PackageServiceDbContext _context;
        private readonly ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public CustomerService(PackageServiceDbContext context, ILogger<CustomerService> logger, IMapper mapper)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
        }
        public async Task<ResponseDto<List<Dtos.Clientes.CustomerDto>>> GetCustomerListAsync()
        {
            try
            {
                var customerEntities = await _context.Customers.ToListAsync();
                var customerDtos = _mapper.Map<List<Dtos.Clientes.CustomerDto>>(customerEntities);

                return new ResponseDto<List<Dtos.Clientes.CustomerDto>>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Listado obtenido correctamente",
                    Data = customerDtos,
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al obtener la lista de clientes");
                return new ResponseDto<List<Dtos.Clientes.CustomerDto>>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Se produjo un error al obtener la lista de clientes",
                };
            }
        }
        public async Task<ResponseDto<Dtos.Clientes.CustomerDto>> GetCustomersByIdAsync(Guid id)
        {
            try
            {
                var customerEntity = await _context.Customers.FirstOrDefaultAsync(o => o.CustomerId == id);

                if (customerEntity == null)
                {
                    return new ResponseDto<CustomerDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = $"El cliente {id} no fue encontrado"
                    };
                }

                var customerDto = _mapper.Map<CustomerDto>(customerEntity);
                return new ResponseDto<CustomerDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Cliente encontrado correctamente",
                    Data = customerDto,

                };

            }
            catch (Exception e) 
            {
                _logger.LogError(e, "Error al obtener el pedido Id");
                return new ResponseDto<CustomerDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = $"Se produjo un error al obtener el cliente",
                };
            }

        }
        public async Task<ResponseDto<Dtos.Clientes.CustomerDto>> CreateAsync(CustomerCreateDto dto)
        {
            if (!Regex.IsMatch(dto.CustomerIdentity.ToString(), @"^\d+$"))
            {
                return new ResponseDto<Dtos.Clientes.CustomerDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "La identificación debe contener solo números y no debe tener espacios ni caracteres especiales",
                };
            }

          
            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerIdentity == dto.CustomerIdentity);

            if (existingCustomer != null)
            {
                return new ResponseDto<Dtos.Clientes.CustomerDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "Ya existe un cliente con esta identificación",
                };
            }

            var customerEntity = _mapper.Map<CustomerEntity>(dto);
            customerEntity.CustomerId = Guid.NewGuid(); 

            _context.Customers.Add(customerEntity);
            await _context.SaveChangesAsync();

            var customerDto = _mapper.Map<Dtos.Clientes.CustomerDto>(customerEntity);
            return new ResponseDto<Dtos.Clientes.CustomerDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "El cliente fue creado correctamente",
                Data = customerDto,
            };
        }
        public async Task<ResponseDto<Dtos.Clientes.CustomerDto>> EditAsync(CustomerEditDto dto, Guid id)
        {
            var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
            if (customerEntity == null)
            {
                return new ResponseDto<Dtos.Clientes.CustomerDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El pedido {id} no fue encontrado"
                };
            }
            _mapper.Map(dto, customerEntity);
            
            _context.Customers.Update(customerEntity);
            await _context.SaveChangesAsync();
            var customerDto = _mapper.Map<Dtos.Clientes.CustomerDto>(customerEntity);
            return new ResponseDto<Dtos.Clientes.CustomerDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "El cliente se editado correctamente",
                Data = customerDto,
            };
                
        }

        public async Task<ResponseDto<Dtos.Clientes.CustomerDto>> DeleteAsync(Guid id)
        {
            var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
            if (customerEntity == null) {
                return new ResponseDto<Dtos.Clientes.CustomerDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El cliente {id} no fue encontrado"
                };

            }
            _context.Customers.Remove(customerEntity); ;
            await _context.SaveChangesAsync();

            return new ResponseDto<Dtos.Clientes.CustomerDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "El cliente fue eliminado"

            };
        }

        

       

       
    }
}