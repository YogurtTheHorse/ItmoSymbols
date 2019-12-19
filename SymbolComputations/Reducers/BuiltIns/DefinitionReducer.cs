using System.Linq;
using SymbolComputations.Symbols;
using SymbolComputations.Tools;

namespace SymbolComputations.Reducers.BuiltIns
{
    public class DefinitionReducer : IReducer
    {
        public Symbol Reduce(ReductionContext context, Symbol s)
        {
            if (!(s is Definition d) || d.Tail.Count == 0)
            {
                return s;
            }

            context.Scope.Parent[d.Name] = d.Tail[0];

            return d.Tail[0].ReduceTail(d.Tail.Skip(1));
        }
    }
}