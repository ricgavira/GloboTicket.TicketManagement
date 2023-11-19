using FluentValidation;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        public CreateEventCommandValidator(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} é de preenchimento obrigatório.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} não pode exceder 50 caracteres.");

            RuleFor(p => p.Date)
                .NotEmpty().WithMessage("{PropertyName} é de preenchimento obrigatório.")
                .NotNull()
                .GreaterThan(DateTime.Now);

            RuleFor(e => e)
                .MustAsync(EventNameAndDateUnique)
                .WithMessage("Já existe um evento com este nome e data.");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} é de preenchimento obrigatório.")
                .GreaterThan(0);
            _eventRepository = eventRepository;
        }

        private async Task<bool> EventNameAndDateUnique(CreateEventCommand e, CancellationToken token)
        {
            return !(await _eventRepository.IsEventNameAndDateUnique(e.Name, e.Date));
        }
    }
}