using System.Collections.Generic;
using SymbolComputations.Reducers.Meta;
using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers.BuiltIns
{
    public class ListReducer : BuiltInIdentifierReducer
    {
        public ListReducer() : base(Std.BuiltIns.List, 0)
        {
        }

        protected override Symbol ReduceImplementation(ReductionContext context, Identifier identifier) =>
            new Literal<IList<Symbol>>(identifier.Tail);
    }
}