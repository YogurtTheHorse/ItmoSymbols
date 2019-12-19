using SymbolComputations.Reducers.Meta;
using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers.BuiltIns
{
    public class OrReducer : BuiltInIdentifierReducer
    {
        public OrReducer() : base(Std.BuiltIns.Or, 2)
        {
        }

        protected override Symbol ReduceImplementation(ReductionContext context, Identifier identifier)
        {
            Symbol reducedFirstSymbol = context.Reduce(identifier.Tail[0]);
            bool boolean = context.BooleanConverter.Convert(reducedFirstSymbol);

            return boolean
                ? reducedFirstSymbol
                : identifier.Tail[1];
        }
    }
}