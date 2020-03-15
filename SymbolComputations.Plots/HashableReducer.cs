using System.Collections.Generic;
using SymbolComputations.Reducers;
using SymbolComputations.Symbols;

namespace SymbolComputations.Plots
{
    public class HashableReducer : IReducer
    {
        private readonly IReducer _reducer;
        private Dictionary<Symbol, Symbol> _hash;

        public HashableReducer(IReducer reducer)
        {
            _reducer = reducer;
            _hash = new Dictionary<Symbol, Symbol>();
        }

        public Symbol Reduce(ReductionContext context, Symbol s)
        {
            if (!_hash.ContainsKey(s))
            {
                _hash[s] = _reducer.Reduce(context, s);
            }

            return _hash[s];
        }
    }
}