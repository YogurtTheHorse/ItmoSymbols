using System.Collections.Immutable;
using System.Linq;

namespace SymbolComputations.Symbols
{
    public abstract class TailHolderSymbol : Symbol
    {
        protected TailHolderSymbol(ImmutableList<Symbol> tail)
        {
            Tail = tail;
        }

        public ImmutableList<Symbol> Tail { get; set; }

        protected abstract TailHolderSymbol Fabric(ImmutableList<Symbol> newTail);

        public Symbol FabricWithScope(ImmutableList<Symbol> newTail)
        {
            TailHolderSymbol s = Fabric(newTail);
            s.Scope = Scope;

            return s;
        }

        protected override Symbol Call(Symbol symbol) => FabricWithScope(Tail.Add(symbol));

        protected bool TailEquals(TailHolderSymbol other)
        {
            if (other.Tail.Count != Tail.Count)
            {
                return false;
            }

            return !Tail.Where((t, i) => !t.Equals(other.Tail[i])).Any();
        }
    }
}