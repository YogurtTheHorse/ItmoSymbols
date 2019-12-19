using System.Collections.Immutable;
using System.Linq;
using SymbolComputations.Reducers.Converters;
using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers
{
    public class ReductionContext
    {
        public Scope Scope { get; private set; } = new Scope();

        public IReducer Reducer { get; }

        public IConverter<bool> BooleanConverter { get; }
        public IConverter<string> StringConverter { get; }

        public IConverter<decimal> NumbersConverter { get; }

        public ReductionContext(IReducer reducer)
        {
            Reducer = reducer;

            BooleanConverter = new BooleanConverter();
            StringConverter = new StringConverter();
            NumbersConverter = new NumbersConverter();
        }

        public Symbol Reduce(Symbol symbol) => Reduce(symbol, Reducer);


        public Symbol Reduce(Symbol symbol, IReducer reducer)
        {
            if (symbol is TailHolderSymbol t)
            {
                symbol = t.FabricWithScope(t
                    .Tail
                    .Select(tailS =>
                        tailS is Identifier i && i.Name == Std.BuiltIns.ReduceFirst.Name
                            ? Reduce(tailS)
                            : tailS
                    )
                    .ToImmutableList()
                );
            }

            EnterScope(symbol.Scope);
            Symbol result = reducer.Reduce(this, symbol);
            result.Scope = new Scope(symbol.Scope, result.Scope.ListScope());
            LeaveScope();

            return result;
        }

        public void EnterScope(Scope scope)
        {
            Scope = new Scope(Scope, scope.ListScope());
        }

        public void LeaveScope()
        {
            if (Scope.Parent != null)
            {
                Scope = Scope.Parent;
            }
        }
    }
}