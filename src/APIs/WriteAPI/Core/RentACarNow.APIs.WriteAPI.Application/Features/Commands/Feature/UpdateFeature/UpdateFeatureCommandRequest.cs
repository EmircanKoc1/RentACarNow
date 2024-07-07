using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.UpdateFeature
{
    public class UpdateFeatureCommandRequest : IRequest<UpdateFeatureCommandResponse>
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

}
