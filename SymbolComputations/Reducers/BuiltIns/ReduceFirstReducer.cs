using SymbolComputations.Reducers.Meta;
using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers.BuiltIns
{
    public class ReduceFirstReducer : BuiltInIdentifierReducer
    {
        public ReduceFirstReducer() : base(Std.BuiltIns.ReduceFirst, 1)
        {
        }

        protected override Symbol ReduceImplementation(ReductionContext context, Identifier identifier) => identifier.Tail[0];
    }
}