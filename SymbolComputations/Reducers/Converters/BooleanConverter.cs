using System.Collections.Generic;
using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers.Converters
{
    public class BooleanConverter : IConverter<bool>
    {
        public bool Convert(Symbol s) =>
            s switch
            {
                Literal<bool> b => b.Value,
                Literal<int> i => i.Value != 0,
                Literal<decimal> i => i.Value != 0,
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                Literal<float> f => f.Value != 0,
                Literal<string> sl => sl.Value.Length != 0,
                Literal<IList<Symbol>> l => l.Value.Count > 0,
                _ => false
            };
    }
}