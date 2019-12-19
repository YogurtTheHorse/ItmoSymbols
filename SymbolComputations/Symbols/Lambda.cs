using System.Collections.Immutable;

namespace SymbolComputations.Symbols
{
    public class Lambda : TailHolderSymbol
    {
        public string ArgumentName { get; }
        public Symbol Body { get; }

        public Lambda(string argumentName, Symbol body, ImmutableList<Symbol> tail) : base(tail)
        {
            ArgumentName = argumentName;
            Body = body;
        }

        protected override TailHolderSymbol Fabric(ImmutableList<Symbol> newTail) =>
            new Lambda(ArgumentName, Body, newTail);

        public override bool Equals(Symbol other)
        {
            return other is Lambda l && ArgumentName == l.ArgumentName && Body.Equals(l.Body) && TailEquals(l);
        }
    }
}