using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers.Meta
{
    public class MultiReducer : IReducer
    {
        private readonly IReducer[] _reducers;

        public MultiReducer(params IReducer[] reducers)
        {
            _reducers = reducers;
        }

        public Symbol Reduce(ReductionContext context, Symbol s)
        {
            Symbol current = s;

            for (var i = 0; i < _reducers.Length; i++)
            {
                Symbol newSymbol = context.Reduce(current, _reducers[i]);

                if (newSymbol.Equals(current)) continue;
                
                i = -1;
                current = newSymbol;
            }

            return current;
        }
    }
}