namespace CoinEx.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    public enum OrderSide
    {
        /// <summary>
        /// Either (only usable for filtering)
        /// </summary>
        Either,
        /// <summary>
        /// Buy
        /// </summary>
        Buy,
        /// <summary>
        /// Sell
        /// </summary>
        Sell
    }
    /// <summary>
    /// Order side
    /// </summary>
    public enum FuturesOrderSide
    {
        /// <summary>
        /// Either (only usable for filtering)
        /// </summary>
        Either=0,
        /// <summary>
        /// Buy
        /// </summary>
        LongBuy=2,
        /// <summary>
        /// Sell
        /// </summary>
        ShortSell=1
    }
}
