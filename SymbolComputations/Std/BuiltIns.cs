using SymbolComputations.Symbols;

namespace SymbolComputations.Std
{
    public static class BuiltIns
    {
        public static Identifier Or = I(nameof(Or));

        public static Identifier And = I(nameof(And));
        
        public static Identifier Eq = I(nameof(Eq));

        public static Identifier List = I(nameof(List));
        
        public static Identifier Sequence = I(nameof(Sequence));

        public static Identifier ReduceFirst = I(nameof(ReduceFirst));

        public static FunctionDefinition Func(string argumentName) => new FunctionDefinition(argumentName);
        
        public static FunctionDefinition Func(Identifier i) => new FunctionDefinition(i.Name);
        
        public static Definition Let(string name) => new Definition(name);
        
        public static Definition Let(Identifier i) => new Definition(i.Name);

        public static Identifier I(string name) => new Identifier(name);
    }
}