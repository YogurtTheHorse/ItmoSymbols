using System;
using System.Linq;
using SymbolComputations.Symbols;
using SymbolComputations.Tools;

namespace SymbolComputations.Reducers.Meta
{
    public abstract class BuiltInIdentifierReducer : IReducer
    {
        private readonly Identifier _identifier;
        private readonly int _expectedArgsLength;
        private readonly RestArgumentsAction _action;

        protected BuiltInIdentifierReducer(
            Identifier i,
            int expectedArgsLength,
            RestArgumentsAction action = RestArgumentsAction.Reduce
        )
        {
            _identifier = i;
            _expectedArgsLength = expectedArgsLength;
            _action = action;
        }

        public Symbol Reduce(ReductionContext context, Symbol s)
        {
            if (!(s is Identifier i))
            {
                return s;
            }

            if (_identifier != null && (i.Name != _identifier.Name || i.Tail.Count < _expectedArgsLength))
            {
                return s;
            }

            Symbol primarySymbol = ReduceImplementation(context, i);

            if (primarySymbol is null)
            {
                return s;
            }

            return _action switch
            {
                RestArgumentsAction.Ignore => primarySymbol,
                RestArgumentsAction.Reduce => primarySymbol.ReduceTail(i.Tail.Skip(_expectedArgsLength)),

                _ => throw new NotImplementedException()
            };
        }

        protected abstract Symbol ReduceImplementation(ReductionContext context, Identifier identifier);
    }
}