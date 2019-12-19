using System;
using Microsoft.Xna.Framework;
using SymbolComputations.Reducers;
using SymbolComputations.Symbols;
using static SymbolComputations.Std.Functions;
using static SymbolComputations.Std.Math;
using static SymbolComputations.Std.BuiltIns;

namespace SymbolComputations.Plots
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            Identifier point = I("Point"), x = I("x");
            
            var ctx = new ReductionContext(
                new BuiltInsReducer()
            );

            Symbol formula = Func(x)[
                point[ReduceFirst[x]][ReduceFirst[x]][ReduceFirst[x]]
            ];
            
            using (var game = new Plots(formula, ctx))
                game.Run();
        }
    }
}