using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace KYMA
{
    class Program
    {
        static string appVersion = "1.1.0";
        static bool isRunning = false;

        static void Main(string[] args)
        {
            CommandLineParser cmd = new CommandLineParser(args);
            if (args.Length <= 0 || !(cmd.ContainsArg("n") && cmd.ContainsArg("r")))
            {
                Usage();
                return;
            }

            int checkInterval = (cmd.ContainsArg("i") ? int.Parse(cmd["i"]) : 1) * 1000;
            int waitInterval = (cmd.ContainsArg("w") ? int.Parse(cmd["w"]) : 10) * 1000;
            bool verbose = cmd.ContainsArg("v");

            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) => isRunning = false;

            StartChecking(verbose, checkInterval, waitInterval, cmd["n"], cmd["r"]);
        }

        static void Log(string text)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {text}");
        }

        static void StartChecking(bool verbose, int checkInterval, int waitInterval, string processName, string processToRun)
        {
            isRunning = true;

            Log($"KYMA (Keep Your Miner Alive) {appVersion}");
            Log($"");
            Log($"Watching for: {processName}");
            Log($"If dead, will run: {processToRun}");
            Log($"");

            bool alive;
            bool lastAlive = false;
            while (isRunning)
            {
                Thread.Sleep(checkInterval);
                alive = Check(processName);
                if (alive)
                {
                    if (!Responding(processName))
                    {
                        if (verbose)
                            Log($"{processName} is not responding and will be killed for restart.");

                        foreach (Process p in Process.GetProcesses())
                            try
                            {
                                if (new FileInfo(p.MainModule.FileName).Name.ToLowerInvariant().Trim() == processName.ToLowerInvariant().Trim())
                                    p.Kill();
                            }
                            catch { }
                    }
                    else
                    {
                        if (verbose && lastAlive != alive)
                            Log("Alive and responding.");
                    }
                }
                else
                {
                    lastAlive = false;
                    if (verbose)
                        Log($"Dead! Waiting {waitInterval / 1000}s to revive...");
                    Thread.Sleep(waitInterval);
                    alive = Check(processName);
                    if (alive)
                    {
                        if (!Responding(processName))
                        {
                            if (verbose)
                                Log($"{processName} is not responding and will be killed for restart.");

                            foreach (Process p in Process.GetProcesses())
                                try
                                {
                                    if (new FileInfo(p.MainModule.FileName).Name.ToLowerInvariant().Trim() == processName.ToLowerInvariant().Trim())
                                        p.Kill();
                                        
                                }
                                catch { }
                        }
                        else
                        {
                            if (verbose && lastAlive != alive)
                                Log("Alive and responding.");
                        }
                    }
                    else
                    {
                        Spawn(processToRun);
                        if (verbose)
                            Log($"{processToRun} spawned!");
                    }
                }
                lastAlive = alive;
            }
        }

        static void Spawn(string filepath)
        {
            FileInfo fi = new FileInfo(filepath);
            ProcessStartInfo pi = new ProcessStartInfo()
            {
                FileName = fi.Name,
                WorkingDirectory = fi.Directory.FullName
            };
            Process.Start(pi);
        }

        static bool Check(string processName)
        {
            bool alive = false;
            foreach (Process p in Process.GetProcesses())
                try
                {
                    if (new FileInfo(p.MainModule.FileName).Name.ToLowerInvariant().Trim() == processName.ToLowerInvariant().Trim())
                        alive = true;
                }
                catch { }
            return alive;
        }

        static bool Responding(string processName)
        {
            bool responding = false;
            foreach (Process p in Process.GetProcesses())
                try
                {
                    if (new FileInfo(p.MainModule.FileName).Name.ToLowerInvariant().Trim() == processName.ToLowerInvariant().Trim())
                        if (p.Responding)
                            responding = true;
                }
                catch { }

            return responding;
        }

        static void Usage()
        {
            Console.WriteLine(
                Environment.NewLine + $"KYMA (Keep Your Miner Alive) {appVersion}" +
                Environment.NewLine + $"" +
                Environment.NewLine + $"This app will keep your miner's app alive if it dies or not respond for some reason, so your mining" +
                Environment.NewLine + $"does not get interrupted!. It also works with any other kind of app, not just miners :)" +
                Environment.NewLine + $"" +
                Environment.NewLine + $"Has this project been useful for you? Please consider donating!" +
                Environment.NewLine + $"" +
                Environment.NewLine + $"Usage: kyma.exe [option] <value> ..." +
                Environment.NewLine + $"" +
                Environment.NewLine + $"Options:" +
                Environment.NewLine + $"" +
                Environment.NewLine + $"-v         (Optional)     Verbose output" +
                Environment.NewLine + $"-n <app>   (Required)     Process name to check for, i.e. miner.exe, notepad.exe, etc." +
                Environment.NewLine + $"-i <N>     (Default: 1)   Check every N seconds if process is either alive or not" +
                Environment.NewLine + $"-w <N>     (Default: 10)  Wait N seconds if the process is dead before starting it up again" +
                Environment.NewLine + $"-r <file>  (Required)     What to run if the process is dead. It can be a .exe, .cmd. .bat, whatever!" +
                Environment.NewLine + $"                          Note: If the path contains spaces, please use double quotes!" +
                Environment.NewLine + $"Example:" +
                Environment.NewLine + $"" +
                Environment.NewLine + $"Keep miner.exe alive, check every 1s and wait 10s if it's dead before starting begin.bat again:" +
                Environment.NewLine + $"" +
                Environment.NewLine + $"kyma.exe -n miner.exe -i 1 -w 10 -r \"c:\\users\\your name\\desktop\\miner\\begin.bat\"" +
                Environment.NewLine + $"" +
                Environment.NewLine + $"Have fun!" +
                Environment.NewLine + $"Made by DARKGuy" +
                Environment.NewLine + $""
            );
        }
    }
}
