using SymbolComputations.Symbols;
using static SymbolComputations.Std.BuiltIns;

namespace SymbolComputations.Std
{
    public static class Math
    {
        public static Identifier Mul = I(nameof(Mul));

        public static Identifier Div = I(nameof(Div));

        public static Identifier Add = I(nameof(Add));

        public static Identifier Sub = I(nameof(Sub));
    }
}