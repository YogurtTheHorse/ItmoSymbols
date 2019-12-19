using System.Linq;
using SymbolComputations.Symbols;
using SymbolComputations.Tools;

namespace SymbolComputations.Reducers.BuiltIns
{
    public class LambdaReducer : IReducer
    {
        public Symbol Reduce(ReductionContext context, Symbol s)
        {
            if (!(s is Lambda lambda) || lambda.Tail.Count == 0)
            {
                return s;
            }

            lambda.Body.Scope = new Scope(lambda.Scope, new[] {(lambda.ArgumentName, lambda.Tail[0])});
            
            Symbol reducedBody = context.Reduce(lambda.Body);

            return reducedBody.ReduceTail(lambda.Tail.Skip(1));
        }
    }
}