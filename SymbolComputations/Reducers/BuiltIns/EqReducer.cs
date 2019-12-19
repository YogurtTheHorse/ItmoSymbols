using SymbolComputations.Reducers.Meta;
using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers.BuiltIns
{
    public class EqReducer : BuiltInIdentifierReducer
    {
        public EqReducer() : base(Std.BuiltIns.Eq, 2)
        {
        }

        protected override Symbol ReduceImplementation(ReductionContext context, Identifier identifier)
        {
            Symbol
                operandA = context.Reduce(identifier.Tail[0]),
                operandB = context.Reduce(identifier.Tail[1]);

            return new Literal<bool>(operandA.Equals(operandB));
        }
    }
}