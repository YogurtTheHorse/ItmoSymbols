using SymbolComputations.Reducers.Meta;
using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers.BuiltIns
{
    public class SequenceReducer : BuiltInIdentifierReducer
    {
        public SequenceReducer() : base(Std.BuiltIns.Sequence, 0, RestArgumentsAction.Ignore)
        {
        }

        protected override Symbol ReduceImplementation(ReductionContext context, Identifier identifier)
        {
            Symbol result = null;
            foreach (Symbol symbol in identifier.Tail)
            {
                result = context.Reduce(symbol);
            }

            return result;
        }
    }
}