using System.Collections.Generic;
using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers.Meta
{
    public class CacheReducer : IReducer
    {
        public CacheReducer(IReducer reducer)
        {
            _reducer = reducer;
        }

        private readonly Dictionary<int, Symbol> _cache = new Dictionary<int, Symbol>();
        private readonly IReducer _reducer;

        public Symbol Reduce(ReductionContext context, Symbol s)
        {
            int hashCode = s.GetHashCode();

            if (!_cache.ContainsKey(hashCode))
            {
                _cache[hashCode] = _reducer.Reduce(context, s);
            }

            return _cache[hashCode];
        }
    }
}