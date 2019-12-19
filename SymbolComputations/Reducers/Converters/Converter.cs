using SymbolComputations.Symbols;

namespace SymbolComputations.Reducers.Converters
{
    public interface IConverter<T>
    {
        T Convert(Symbol s);
    }
}