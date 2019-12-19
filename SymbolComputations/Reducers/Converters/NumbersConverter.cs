using System.Collections.Generic;
using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers.Converters
{
    public class NumbersConverter : IConverter<decimal>
    {
        public decimal Convert(Symbol s) =>
            s switch
            {
                Literal<bool> b => b.Value ? 1 : 0,
                Literal<int> i => i.Value,
                Literal<float> f => (decimal)f.Value,
                Literal<decimal> f => f.Value,
                Literal<string> sl => sl.Value.Length,
                Literal<IList<Symbol>> l => l.Value.Count,

                _ => 0
            };
    }
}