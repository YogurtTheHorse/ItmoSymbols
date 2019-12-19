using System;
using SymbolComputations.Reducers.Meta;
using SymbolComputations.Symbols;
using static SymbolComputations.Std.Math;

namespace SymbolComputations.Reducers.BuiltIns
{
    public class MathOperatorsReducer : BuiltInIdentifierReducer
    {
        public MathOperatorsReducer() : base(null, 2)
        {
        }

        protected override Symbol ReduceImplementation(ReductionContext context, Identifier identifier)
        {
            decimal GetOperand(int index) => context.NumbersConverter.Convert(context.Reduce(identifier.Tail[index]));

            Lazy<decimal>
                operandA = new Lazy<decimal>(() => GetOperand(0)),
                operandB = new Lazy<decimal>(() => GetOperand(1));

            decimal? value = identifier.Name switch
            {
                nameof(Add) => (decimal?) operandA.Value + operandB.Value,
                nameof(Sub) => operandA.Value - operandB.Value,
                nameof(Mul) => operandA.Value * operandB.Value,
                nameof(Div) => operandA.Value / operandB.Value,

                _ => null
            };

            return value is null
                ? null
                : new Literal<decimal>(value.Value);
        }
    }
}
