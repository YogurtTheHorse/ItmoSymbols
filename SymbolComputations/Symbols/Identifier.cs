using System.Collections.Immutable;

namespace SymbolComputations.Symbols
{
    public class Identifier : TailHolderSymbol
    {
        public Identifier(string name) : this(name, ImmutableList<Symbol>.Empty)
        {
        }

        public Identifier(string name, ImmutableList<Symbol> tail) : base(tail)
        {
            Name = name;
        }

        public string Name { get; set; }

        protected override TailHolderSymbol Fabric(ImmutableList<Symbol> newTail) =>
            new Identifier(Name, newTail);

        public override bool Equals(Symbol other)
        {
            if (other is Identifier i)
            {
                return Name == i.Name && TailEquals(i);
            }

            return false;
        }
    }
}