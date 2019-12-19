using System.Collections.Immutable;

namespace SymbolComputations.Symbols
{
    public class FunctionDefinition : Symbol
    {
        public FunctionDefinition(string argumentName)
        {
            ArgumentName = argumentName;
        }

        public string ArgumentName { get; set; }

        protected override Symbol Call(Symbol symbol) =>
            new Lambda(ArgumentName, symbol, ImmutableList<Symbol>.Empty);

        public override bool Equals(Symbol other)
        {
            if (other is FunctionDefinition f)
            {
                return f.ArgumentName == ArgumentName;
            }

            return false;
        }
    }
}