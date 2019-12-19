using System.Collections.Generic;
using System.Linq;
using SymbolComputations.Symbols;

namespace SymbolComputations.Tools
{
    public static class SymbolsTools
    {
        public static Symbol ReduceTail(this Symbol symbol, IEnumerable<Symbol> tail) =>
            tail.Aggregate(symbol, (current, next) => current[next]);
    }
}