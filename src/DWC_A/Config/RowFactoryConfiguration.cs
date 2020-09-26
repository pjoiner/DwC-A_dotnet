namespace DwC_A.Config
{
    /// <summary>
    /// Determines the row strategy of the RowFactory
    /// </summary>
    public enum RowStrategy
    {
        /// <summary>
        /// Reads fields as they are consumed.  Use this when consuming fields in index order
        /// or only reading some of the fields in a row.
        /// </summary>
        Lazy,
        /// <summary>
        /// Parses all fields in a row and stores them in an array.  Use this strategy when
        /// consuming all fields in random or reverse index order.
        /// </summary>
        Greedy
    };

    /// <summary>
    /// Configuration options for the RowFactory
    /// </summary>
    public partial class RowFactoryConfiguration
    {
        /// <summary>
        /// Specify which strategy to consume rows
        /// </summary>
        /// <seealso cref="RowStrategy"/>
        /// <default>RowStrategy.Lazy</default>
        public RowStrategy Strategy { get; set; } = RowStrategy.Lazy;
    }
}
