namespace SymbolComputations.Symbols
{
    public class Literal<T> : Symbol
    {
        public Literal(T value)
        {
            Value = value;
        }

        public T Value { get; set; }

        protected override Symbol Call(Symbol symbol) => this;

        public override bool Equals(Symbol other) =>
            other is Literal<T> l && (Value?.Equals(l.Value) ?? false);


        public override string ToString() => $"Literal[{Value}]";
    }
}