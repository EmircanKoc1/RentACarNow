﻿using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.UpdateEmployee
{
    public class UpdateEmployeeCommandRequestHandler : IRequestHandler<UpdateEmployeeCommandRequest, UpdateEmployeeCommandResponse>
    {
        public Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada çalışan güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

}