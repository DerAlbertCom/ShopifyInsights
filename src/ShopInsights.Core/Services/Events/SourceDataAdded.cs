using System;
using MediatR;

namespace ShopInsights.Services.Events
{
    public class SourceDataAdded : IRequest
    {
        public SourceDataAdded(object sourceData)
        {
            SourceData = sourceData ?? throw new ArgumentNullException(nameof(sourceData));
        }

        public object SourceData { get; }
    }
}
