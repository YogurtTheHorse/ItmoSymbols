using System;
using SymbolComputations.Reducers;
using SymbolComputations.Symbols;
using static SymbolComputations.Std.Functions;
using static SymbolComputations.Std.Math;
using static SymbolComputations.Std.BuiltIns;

namespace SymbolComputations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ctx = new ReductionContext(
                new BuiltInsReducer()
            );

            Identifier n = I("n"), Fact = I("Fact");

            Symbol res = ctx.Reduce(
                Let(Fact)[
                    Func(n)[
                        If[n][
                            Mul[n][
                                Fact[
                                    ReduceFirst[Sub[n][1]]
                                ]
                            ]
                        ][1]
                    ]
                ][10]
            );

            Console.WriteLine(res);
        }
    }
}