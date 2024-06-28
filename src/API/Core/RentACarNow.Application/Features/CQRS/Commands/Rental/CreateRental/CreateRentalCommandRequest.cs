using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Rental.CreateRental
{
    public class CreateRentalCommandRequest : IRequest<CreateRentalCommandResponse>
    {
        // Buraya kiralama oluşturma için gerekli alanlar eklenebilir, örneğin rentalId, yeni bilgiler gibi
    }

}
