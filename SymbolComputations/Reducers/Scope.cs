using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers
{
    public class Scope
    {
        private readonly Dictionary<string, Symbol> _symbols;
        public Scope Parent { get; }

        public Scope(Scope parent = null, IEnumerable<(string, Symbol)> scope = null)
        {
            _symbols = new Dictionary<string, Symbol>();
            Parent = parent;

            if (scope == null) return;
            
            foreach ((string name, Symbol value) in scope)
            {
                _symbols[name] = value;
            }
        }

        public ImmutableList<(string, Symbol)> ListScope()
        {
            ImmutableList<(string, Symbol)> parentScope = Parent is null
                ? ImmutableList<(string, Symbol)>.Empty
                : Parent.ListScope();

            return ImmutableList<(string, Symbol)>
                .Empty
                .AddRange(_symbols.Select(kv => (kv.Key, kv.Value)))
                .AddRange(parentScope);
        }

        public Symbol GetSymbol(string name) =>
            _symbols.TryGetValue(name, out Symbol s)
                ? s
                : Parent?.GetSymbol(name); // TODO: Add undefined symbol.

        public Symbol this[string name]
        {
            get => GetSymbol(name);
            set => _symbols[name] = value;
        }
    }
}