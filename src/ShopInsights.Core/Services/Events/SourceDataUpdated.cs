using System;
using MediatR;

namespace ShopInsights.Services.Events
{
    public class SourceDataUpdated : IRequest
    {
        public SourceDataUpdated(object sourceData)
        {
            SourceData = sourceData ?? throw new ArgumentNullException(nameof(sourceData));
        }

        public object SourceData { get; }
    }
}