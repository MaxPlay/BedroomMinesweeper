using System;
using System.Runtime.InteropServices;

namespace Bedroom.Minesweeper
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            // When compiling with DEBUG set, open the console
            AllocConsole();
#endif
            Debug.Log("Game started");
            using (var game = new Core())
                game.Run();
        }

#if DEBUG
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();
#endif
    }
#endif
}
