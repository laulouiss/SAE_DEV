using System;

namespace SAE1._01_2
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new FuryFlash())
                game.Run();
        }
    }
}
