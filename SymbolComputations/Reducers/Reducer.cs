using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers
{
    public interface IReducer
    {
        Symbol Reduce(ReductionContext context, Symbol s);
    }
}