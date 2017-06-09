namespace PartnerNet.Providers.Stock
{
    public interface IStockProductTransaction
    {
        string Product { get; }
        int Purchase { get; }
        int Sale { get; }
        int Stock { get; }
        int Month { get; }
        int Year { get; }
    }
}
