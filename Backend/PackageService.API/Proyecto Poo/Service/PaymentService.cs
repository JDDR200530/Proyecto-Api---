using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Payments;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly PackageServiceDbContext context;
        private readonly ILogger<PaymentService> logger;
        private readonly IMapper mapper;
        private readonly IAudtiService audtiService;

        public PaymentService(
            PackageServiceDbContext context,
            ILogger<PaymentService> logger,
            IMapper mapper,
            IAudtiService audtiService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.audtiService = audtiService ?? throw new ArgumentNullException(nameof(audtiService));
        }

        public async Task<ResponseDto<PaymentDto>> CreatePaymentWithDebitCardAsync(PaymentCreateDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return new ResponseDto<PaymentDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "Datos del pago no válidos."
                    };
                }

                // Buscar la orden asociada
                var orderEntity = await context.Orders.FirstOrDefaultAsync(o => o.Id == dto.OrderId);
                if (orderEntity == null)
                {
                    return new ResponseDto<PaymentDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = "La orden no fue encontrada."
                    };
                }

                // Validar método de pago y número de tarjeta
                if (dto.PaymentMethod != "Debit Card" || dto.CardNumber <= 0)
                {
                    return new ResponseDto<PaymentDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "El pago con tarjeta de débito requiere un número de tarjeta válido."
                    };
                }

                if (!IsValidCardNumber(dto.CardNumber))
                {
                    return new ResponseDto<PaymentDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "Número de tarjeta inválido."
                    };
                }

                // Validar fecha de expiración de la tarjeta
              if (IsCardExpired(dto.ExpirationYear, dto.ExpirationMonth))
                    {
                         return new ResponseDto<PaymentDto>
                         {
                      StatusCode = 400,
                  Status = false,
                           Message = "La tarjeta está caducada."
                     };
}


                // Calcular el monto basado en peso y distancia
                double amount;
                try
                {
                    amount = CalculateAmount(orderEntity.TotalWeight, orderEntity.Distance);
                }
                catch (ArgumentException ex)
                {
                    logger.LogError(ex, "Error al calcular el monto para la orden.");
                    return new ResponseDto<PaymentDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "Datos inválidos para calcular el monto del pago."
                    };
                }

                // Crear y guardar la entidad de pago
                var paymentEntity = new PaymentEntity
                {
                    Id = Guid.NewGuid(),
                    Amount = amount,
                    PaymentDate = DateTime.UtcNow,
                    OrderId = dto.OrderId,
                    PaymentMethod = dto.PaymentMethod
                };

                context.Payments.Add(paymentEntity);

                orderEntity.PaymentStatus = true;
                context.Orders.Update(orderEntity);
                await context.SaveChangesAsync();

                logger.LogInformation("Pago creado con éxito. ID del Pago: {PaymentId}", paymentEntity.Id);
                logger.LogInformation("Pago creado con éxito para la orden {OrderId}", dto.OrderId);

                // Mapear la entidad a DTO y devolverla
                var paymentDto = mapper.Map<PaymentDto>(paymentEntity);
                paymentDto.CardNumber = dto.CardNumber;

                return new ResponseDto<PaymentDto>
                {
                    StatusCode = 201,
                    Status = true,
                    Message = $"Pago con tarjeta de débito creado correctamente. ID del Pago: {paymentEntity.Id}",
                    Data = paymentDto
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al procesar el pago con tarjeta de débito.");
                return new ResponseDto<PaymentDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Error interno al procesar el pago."
                };
            }
        }

        public bool IsCardExpired(int expirationYear, int expirationMonth)
        {
            var currentDate = DateTime.UtcNow;

            // Si el año es menor al actual, la tarjeta está caducada
            if (expirationYear < currentDate.Year)
            {
                return true;
            }

            // Si el año es el actual, validar el mes
            if (expirationYear == currentDate.Year && expirationMonth < currentDate.Month)
            {
                return true;
            }

            return false; // La tarjeta no está caducada
        }


        public bool IsValidCardNumber(long cardNumber)
        {
            string cardNumberString = cardNumber.ToString();
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumberString.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(cardNumberString[i])) return false;

                int n = int.Parse(cardNumberString[i].ToString());
                if (alternate)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }
                sum += n;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }

        private double CalculateAmount(double totalWeight, double distance)
        {
            if (totalWeight <= 0 || distance <= 0)
            {
                throw new ArgumentException("Peso total o distancia inválidos.");
            }

            // Cálculo del monto: 0.5 por kilo y 0.2 por kilómetro
            return Math.Round(totalWeight * 0.5 + distance * 0.2, 2);
        }
        public async Task<ResponseDto<PaymentDto>> CreatePaymentWithPayPalAsync(PaymentPayPalCreatedDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return new ResponseDto<PaymentDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "Datos del pago no válidos."
                    };
                }

                // Buscar la orden asociada
                var orderEntity = await context.Orders.FirstOrDefaultAsync(o => o.Id == dto.OrderId);
                if (orderEntity == null)
                {
                    return new ResponseDto<PaymentDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = "La orden no fue encontrada."
                    };
                }

                // Validar método de pago y correo de PayPal
                if (dto.PaymentMethod != "PayPal" || string.IsNullOrEmpty(dto.PayPalEmail))
                {
                    return new ResponseDto<PaymentDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "El pago con PayPal requiere un correo válido."
                    };
                }

                if (!IsValidEmail(dto.PayPalEmail))
                {
                    return new ResponseDto<PaymentDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "El correo de PayPal es inválido."
                    };
                }

                // Calcular el monto basado en peso y distancia
                double amount;
                try
                {
                    amount = CalculateAmount(orderEntity.TotalWeight, orderEntity.Distance);
                }
                catch (ArgumentException ex)
                {
                    logger.LogError(ex, "Error al calcular el monto para la orden.");
                    return new ResponseDto<PaymentDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "Datos inválidos para calcular el monto del pago."
                    };
                }

                // Crear y guardar la entidad de pago
                var paymentEntity = new PaymentEntity
                {
                    Id = Guid.NewGuid(),
                    Amount = amount,
                    PaymentDate = DateTime.UtcNow,
                    OrderId = dto.OrderId,
                    PaymentMethod = dto.PaymentMethod,
                    PayPalEmail = dto.PayPalEmail // Agregar el correo de PayPal a la entidad
                };

                context.Payments.Add(paymentEntity);

                orderEntity.PaymentStatus = true;
                context.Orders.Update(orderEntity);
                await context.SaveChangesAsync();

                logger.LogInformation("Pago creado con éxito para la orden {OrderId} usando PayPal", dto.OrderId);

                // Mapear la entidad a DTO y devolverla
                var paymentDto = mapper.Map<PaymentDto>(paymentEntity);

                return new ResponseDto<PaymentDto>
                {
                    StatusCode = 201,
                    Status = true,
                    Message = "Pago con PayPal creado correctamente.",
                    Data = paymentDto
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al procesar el pago con PayPal.");
                return new ResponseDto<PaymentDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Error interno al procesar el pago."
                };
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var emailCheck = new System.Net.Mail.MailAddress(email);
                return emailCheck.Address == email;
            }
            catch
            {
                return false;
            }
        }




    }


}
