using SymbolComputations.Reducers.Meta;
using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers.BuiltIns
{
    public class AndReducer : BuiltInIdentifierReducer
    {
        public AndReducer() : base(Std.BuiltIns.And, 2)
        {
        }

        protected override Symbol ReduceImplementation(ReductionContext context, Identifier identifier)
        {
            Symbol reducedFirstSymbol = context.Reduce(identifier.Tail[0]);
            bool boolean = context.BooleanConverter.Convert(reducedFirstSymbol);

            return boolean
                ? identifier.Tail[1]
                : reducedFirstSymbol;
        }
    }
}