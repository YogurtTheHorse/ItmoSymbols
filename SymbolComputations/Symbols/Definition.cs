using System.Collections.Immutable;

namespace SymbolComputations.Symbols
{
    public class Definition : TailHolderSymbol
    {
        public Definition(string name) : this(name, ImmutableList<Symbol>.Empty)
        {
        }

        private Definition(string name, ImmutableList<Symbol> tail) : base(tail)
        {
            Name = name;
        }

        public string Name { get; }

        public override bool Equals(Symbol other) => other is Definition d && d.Name == Name && TailEquals(d);

        protected override TailHolderSymbol Fabric(ImmutableList<Symbol> newTail) => new Definition(Name, newTail);
    }
}