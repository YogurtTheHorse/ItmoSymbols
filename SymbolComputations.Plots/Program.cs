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
            Identifier point = I("Point"), x = I("x"), y = I("y");

            var ctx = new ReductionContext(
                new BuiltInsReducer()
            );

            using var game = new Plots(new[]
            {
                Func(x)[
                    Func(y)[
                        Add[Mul[x][x]][Mul[y][y]]
                    ]
                ]
            }, ctx);
            game.Run();
        }
    }
}