using System;
using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers.Converters
{
    public class StringConverter : IConverter<string>
    {
        public string Convert(Symbol s) =>
            s switch
            {
                Literal<string> sl => sl.Value,
                Literal<int> i => i.Value.ToString(),
                Literal<bool> b => b.Value.ToString(),
                Identifier i => i.Name,

                _ => throw new NotImplementedException()
    };
    }
}