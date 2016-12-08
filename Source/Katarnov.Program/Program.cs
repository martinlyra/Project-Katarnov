using System;
using System.Diagnostics;
using System.Reflection;

namespace Katarnov
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        private static ProgramArgs ParseArgs(string[] args)
        {
            var pargs = new ProgramArgs();
            for (int i = 0; i < args.Length; i++)
            {
                var s = args[i];

                if (s.Contains("server"))
                    pargs.ServerMode = true;
                if (s.Contains("headless"))
                    pargs.Headless = true;
                if (s.Contains("localhost"))
                {
                    pargs.ServerMode = true;
                    pargs.Headless = true;
                    pargs.LocalHost = true;
                }
            }
            return pargs;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var pargs = ParseArgs(args);
            Console.WriteLine(Assembly.GetExecutingAssembly().FullName);

            Process p = null;
            if (pargs.LocalHost)
                p = Process.Start("PKatarnov.exe", ""); // TODO: Start a slave process to act as the local admin client for server debugging

            using (var game = new Katarnov.Game1(args: pargs))
                game.Run();

            if (p != null)
                p.Kill();
        }
    }
}
