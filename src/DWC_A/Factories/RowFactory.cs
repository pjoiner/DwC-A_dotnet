using DwC_A.Config;
using DwC_A.Meta;
using System.Collections.Generic;

namespace DwC_A.Factories
{
    internal class RowFactory : IRowFactory
    {
        private readonly RowStrategy rowStrategy;

        public RowFactory(RowStrategy rowStrategy = RowStrategy.Lazy)
        {
            this.rowStrategy = rowStrategy;
        }

        public IRow CreateRow(IEnumerable<string> fields, IFieldMetaData fieldMetaData)
        {
            switch(rowStrategy)
            {
                case RowStrategy.Greedy: return new GreedyRow(fields, fieldMetaData);
                case RowStrategy.Lazy: 
                default: return new LazyRow(fields, fieldMetaData);
            }
        }
    }
}
