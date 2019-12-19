using SymbolComputations.Reducers.BuiltIns;
using SymbolComputations.Reducers.Meta;

namespace SymbolComputations.Reducers
{
    public class BuiltInsReducer : MultiReducer
    {
        public BuiltInsReducer() : base(
            new IdentifiersReducer(),
            new ListReducer(),
            new EqReducer(),
            new AndReducer(),
            new OrReducer(),
            new SequenceReducer(),
            new LambdaReducer(),
            new DefinitionReducer(),
            new MathOperatorsReducer(),
            new ReduceFirstReducer())
        {
        }
    }
}