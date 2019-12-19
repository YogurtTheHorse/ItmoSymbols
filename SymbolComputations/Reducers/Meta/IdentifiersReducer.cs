using SymbolComputations.Symbols;
using SymbolComputations.Tools;

namespace SymbolComputations.Reducers.Meta
{
    public class IdentifiersReducer : IReducer
    {
        public Symbol Reduce(ReductionContext context, Symbol s)
        {
            if (!(s is Identifier i)) return s;

            return context.Scope[i.Name]?.ReduceTail(i.Tail) ?? s;
        }
    }
}