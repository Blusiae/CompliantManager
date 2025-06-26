using CompliantManager.Server.Data.Entities;
using CompliantManager.Shared.Dtos;

namespace CompliantManager.Server.Extensions
{
    public static class MappingExtentions
    {
        public static ClaimDto ToDto(this Claim entity) => new()
        {
            Id = entity.Id,
            Order = entity.Order?.ToDto(),
            ExpectedAction = entity.ExpectedAction,
            Status = entity.Status,
            CreatedOn = entity.CreatedOn,
            CompletedOn = entity.CompletedOn,
            Consultant = entity.Consultant?.ToConsultantDto(),
            ConsultantId = entity.ConsultantId
        };

        public static ProductDto ToDto(this Product entity, int quantity, int faultyQuantity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Quantity = quantity,
            FaultyQuantity = faultyQuantity,
            IsFromDatabase = true
        };

        public static OrderDto ToDto(this Order entity) => new()
        {
            Id = entity.Id,
            OrderDate = entity.OrderDate,
            OrderNumber = entity.OrderNumber,
            Products = entity.OrderItems.Select(item => item.Product.ToDto(item.Quantity, item.FaultyQuantity)).ToList(),
            Customer = entity.Customer?.ToDto()
        };

        public static CustomerDto ToDto(this Customer entity) => new()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            NotificationsEnabled = entity.NotificationsEnabled,
            AddressId = entity.Address?.Id,
            Street = entity.Address?.Street,
            City = entity.Address?.City,
            HouseNumber = entity.Address?.HouseNumber,
            PostalCode = entity.Address?.PostalCode,
            Country = entity.Address?.Country
        };

        public static ConsultantDto ToConsultantDto(this ApplicationUser entity) => new()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName
        };

        public static Claim ToEntity(this ClaimDto dto) => new()
        {
            Id = dto.Id,
            Order = dto.Order?.ToEntity(),
            ExpectedAction = dto.ExpectedAction ?? string.Empty,
            Status = dto.Status,
            CreatedOn = dto.CreatedOn,
            CompletedOn = dto.CompletedOn,
            ConsultantId = dto.ConsultantId
        };

        public static Order ToEntity(this OrderDto dto) => new()
        {
            Id = dto.Id,
            OrderDate = dto.OrderDate ?? DateTime.Now,
            OrderNumber = dto.OrderNumber ?? string.Empty,
            Customer = dto.Customer?.ToEntity(),
            CustomerId = dto.CustomerId,
            OrderItems = dto.Products.Select(p => new OrderItem
            {
                Quantity = p.Quantity,
                FaultyQuantity = p.FaultyQuantity,
                Product = p.ToEntity(),
            }).ToList()
        };

        public static Product ToEntity(this ProductDto dto) => new()
        {
            Id = dto.Id,
            Name = dto.Name
        };

        public static Customer ToEntity(this CustomerDto dto) => new()
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            NotificationsEnabled = dto.NotificationsEnabled,
            AddressId = dto.AddressId ?? 0,
            Address = new()
            {
                Id = dto.AddressId ?? 0,
                Country = dto.Country,
                City = dto.City,
                PostalCode = dto.PostalCode,
                Street = dto.Street,
                HouseNumber = dto.HouseNumber
            }
        };
    }
}
