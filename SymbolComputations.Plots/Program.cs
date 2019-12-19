using System;

namespace SymbolComputations.Plots
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Plots())
                game.Run();
        }
    }
}