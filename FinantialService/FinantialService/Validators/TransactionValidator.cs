using FinantialService.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.Validators
{
    public class TransactionValidator : AbstractValidator<Transaction>
    {
        public TransactionValidator()
        {
            RuleFor(t => t.BuyerId).NotNull().NotEmpty().WithMessage("You must enter buyerId");
            RuleFor(t => t.ProductId).NotNull().NotEmpty().WithMessage("You must enter productId");
            RuleFor(t => t.TransportTypeId).NotNull().NotEmpty().WithMessage("You must enter transportTypeId");
            RuleFor(t => t.ProductsQuantity).GreaterThan(0).WithMessage("Product quantity must be bigger then zero");
            RuleFor(t => t.DeliveryAddress).NotEmpty().WithMessage("You must enter deliveryAddress");
            RuleFor(t => t.DeliveryCity).NotEmpty().WithMessage("You must enter deliveryCity");
        }
    }
}
