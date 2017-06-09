namespace PartnerNet.Providers.Stock
{
    public interface IStockProvider
    {
        string ProviderCode{ get; }
        string Name { get; }
    }
}
