using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class OwnerCreateValidation : Validation<OwnerCreateRequestModel>
    {
        public OwnerCreateValidation()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
            ValidateAddress();
            ValidateCity();
            ValidateCountry();
            ValidatePhoneNumber();
            ValidatePostalCode();
        }

        private void ValidateFirstName()
        {
            RuleFor(o => o.FirstName)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(FirstName => CustomValidations.IsInLengthInterval(3, 150, FirstName))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                ;
        }

        private void ValidateLastName()
        {
            _ = RuleFor(o => o.LastName)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(LastName => CustomValidations.IsInLengthInterval(3, 150, LastName))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                ;
        }

        private void ValidateEmail()
        {
            RuleFor(o => o.Email)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .EmailAddress()
                .WithMessage(DefaultErrorMessages.InvalidEmail)
                .Must(name => CustomValidations.IsInLengthInterval(3, 150, name))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                ;
        }

        private void ValidateAddress()
        {
            RuleFor(o => o.Address)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(Address => CustomValidations.IsInLengthInterval(3, 150, Address))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                ;
        }

        private void ValidateCity()
        {
            RuleFor(o => o.City)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(City => CustomValidations.IsInLengthInterval(3, 150, City))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                ;
        }

        private void ValidateCountry()
        {
            RuleFor(o => o.Country)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(Country => CustomValidations.IsInLengthInterval(3, 150, Country))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                ;
        }

        private void ValidatePhoneNumber()
        {
            RuleFor(o => o.PhoneNumber)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(PhoneNumber => CustomValidations.IsInLengthInterval(3, 30, PhoneNumber))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 30))
                ;
        }

        private void ValidatePostalCode()
        {
            RuleFor(o => o.PostalCode)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(PostalCode => CustomValidations.IsInLengthInterval(3, 30, PostalCode))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 30))
                ;
        }

        protected override List<PersistenceError> GetPersistenceValidations()
        {
            return new List<PersistenceError>
            {
                new PersistenceError
                {
                }
            };
        }
    }
}