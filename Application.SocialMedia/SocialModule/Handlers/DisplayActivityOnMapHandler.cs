using System;
using Swaksoft.Domain.Seedwork.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Events.Streaming;
using Swaksoft.Infrastructure.Crosscutting.Extensions;

namespace Swaksoft.Application.SocialMedia.SocialModule.Handlers
{
    public class DisplayActivityOnMapHandler : HandlerBase<DisplayActivityOnMap>
    {
        private readonly IDisplayActivityOnMapHandler handler;

        public DisplayActivityOnMapHandler(IDisplayActivityOnMapHandler handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            this.handler = handler;
        }
        public override void Handle(DisplayActivityOnMap args)
        {
            var dto = args.ProjectedAs<Dto.DisplayActivityOnMap>();
            handler.Handle(dto);
        }
    }
}