using System;
using MediatR;

namespace ShopInsights.Services.Events
{
    public class SourceDataRemoved : IRequest
    {
        public SourceDataRemoved(object sourceData)
        {
            SourceData = sourceData ?? throw new ArgumentNullException(nameof(sourceData));
        }

        public object SourceData { get; }
    }
}