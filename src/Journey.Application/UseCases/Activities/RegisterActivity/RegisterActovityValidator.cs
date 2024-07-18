﻿using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Activities.RegisterActivity
{
    public class RegisterActovityValidator : AbstractValidator<RequestRegisterActivityJson>
    {
        public RegisterActovityValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);   
        }
    }
}