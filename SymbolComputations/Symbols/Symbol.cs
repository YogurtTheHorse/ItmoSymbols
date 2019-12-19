using System;
using SymbolComputations.Reducers;

namespace SymbolComputations.Symbols
{
    public abstract class Symbol : IEquatable<Symbol>
    {
        public Scope Scope { get; set; } = new Scope();

        public Symbol this[Symbol arg] => Call(arg);

        protected abstract Symbol Call(Symbol symbol);

        public static implicit operator Symbol(string value) => new Literal<string>(value);

        public static implicit operator Symbol(int value) => new Literal<decimal>(value);

        public static implicit operator Symbol(float value) => new Literal<decimal>((decimal) value);

        public static implicit operator Symbol(decimal value) => new Literal<decimal>(value);

        public static implicit operator Symbol(bool value) => new Literal<bool>(value);

        public abstract bool Equals(Symbol other);
    }
}