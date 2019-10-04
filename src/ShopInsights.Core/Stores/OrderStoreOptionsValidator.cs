using Microsoft.Extensions.Options;

namespace ShopInsights.Core.Stores
{
    public class OrderStoreOptionsValidator : IValidateOptions<OrderStoreOptions>
    {
        public ValidateOptionsResult Validate(string name, OrderStoreOptions options)
        {
            if (string.IsNullOrEmpty(options.ImportPath))
            {
                return ValidateOptionsResult.Fail($"Import Path is missing");
            }
            return ValidateOptionsResult.Success;
        }
    }
}
