namespace PartnerNet.Providers.Stock
{
    public interface IStockProduct
    {
        string ProductCode { get; }
        string Description { get; }
        string Group { get; }
        string Provider { get; }
    }
}
