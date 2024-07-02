using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Feature.UpdateFeature
{
    public class UpdateFeatureCommandRequest : IRequest<UpdateFeatureCommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

}
