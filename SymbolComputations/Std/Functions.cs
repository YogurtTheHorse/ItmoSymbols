using SymbolComputations.Symbols;
using static SymbolComputations.Std.BuiltIns;

namespace SymbolComputations.Std
{
    public static class Functions
    {
        public static Symbol If =
            Func("cond")[
                Func("expr1")[
                    Or[And[I("cond")][I("expr1")]]
                ]
            ];
    }
}