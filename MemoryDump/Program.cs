using System;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MemoryDump
{
    internal class Program
    {
        static string tempPath = Path.GetTempPath();
        static long totalSize = 0;
        static int totalProcess = 0;
        static void Main(string[] args)
        {
        starter:

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("MemoryDump | LukeCreater | 1.0");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Execute como ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("administrador");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(".\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.WriteLine("> 1 | Apenas apagar arquivos inúteis (melhora o desempenho no geral)");
            Console.WriteLine("> 2 | Apaga todos os arquivos inúteis, desabilita registros e melhora diminui a RAM em segundo plano.");
            Console.WriteLine("");
            Console.Write("> ");
            string info = Console.ReadLine();

            if (info == "1")
            {
                AdvancedOptimize();
            }
            else if (info == "2")
            {
                AdvancedOptimize();
                KillProcessByName("Microsoft Edge WebView2");
                KillProcessByName("Java Update Scheduler");
                DisableStartupProcess("SecurityHealthSystray");
                DisableStartupProcess("Adobe Creative Cloud");
                DisableStartupProcess("Skype");
                DisableStartupProcess("iTunes");
                DisableStartupProcess("Dropbox");
                DisableStartupProcess("Spotify");
                DisableStartupProcess("Java Update Scheduler");
                DisableStartupProcess("CyberLink PowerDVD");
                DisableStartupProcess("LogitechSetPointEventManager");
                DisableStartupProcess("Microsoft Teams");
                DisableStartupProcess("MicrosoftTeams");
                DisableStartupProcess("Adobe Reader and Acrobat Manager");
                DisableBackgroundApp("Skype");
                DisableBackgroundApp("WindowsStore");
                DisableBackgroundApp("YourPhone");
                DisableBackgroundApp("People");
                DisableBackgroundApp("Paint3D");
                DisableBackgroundApp("OneNote");
                DisableBackgroundApp("Maps");
                DisableBackgroundApp("Camera");
                DisableBackgroundApp("ZuneMusic");
                DisableBackgroundApp("ZuneVideo");
                DisableBackgroundApp("Calculator");
                DisableBackgroundApp("Windows.Calendar");
                DisableBackgroundApp("windowscommunicationapps");
                DisableBackgroundApp("MicrosoftSolitaireCollection");
                DisableBackgroundApp("GetHelp");
                DisableBackgroundApp("WindowsFeatureExperiencePack");
            }
            else
            {
                Console.WriteLine("");
                Console.Write("Comando incorreto.");
                Thread.Sleep(1000);
                Console.Clear();
                goto starter;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("");
            Console.WriteLine($"Despejo total de memória: {totalSize / 1024 / 1024} MB");
            Console.WriteLine($"Processos desabilitados da inicialização: {totalProcess}");
            Console.ReadKey();
        }
        static void AdvancedOptimize()
        {
            Console.WriteLine("");
            Console.Clear();
            Console.WriteLine("Iniciando processo de otimização avançada em 5 segundos...");
            Thread.Sleep(1000); Console.Clear();
            Console.WriteLine("Iniciando processo de otimização avançada em 4 segundos...");
            Thread.Sleep(1000); Console.Clear();
            Console.WriteLine("Iniciando processo de otimização avançada em 3 segundos...");
            Thread.Sleep(1000); Console.Clear();
            Console.WriteLine("Iniciando processo de otimização avançada em 2 segundos...");
            Thread.Sleep(1000); Console.Clear();
            Console.WriteLine("Iniciando processo de otimização avançada em 1 segundo...");
            Thread.Sleep(1000); Console.Clear();

            try
            {
                string tempPath = Path.GetTempPath();
                string[] tempFiles = Directory.GetFiles(tempPath);

                foreach (string file in tempFiles)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        totalSize += fileInfo.Length;
                        File.Delete(file);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Despejando memória: " + Path.GetFileName(file));
                    }
                    catch (IOException ioEx)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Arquivo em uso: " + Path.GetFileName(file));
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro inesperado: " + Path.GetFileName(file));
                    }
                }

                string[] tempDirs = Directory.GetDirectories(tempPath);

                foreach (string dir in tempDirs)
                {
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(dir);
                        totalSize += GetDirectorySize(dirInfo);
                        Directory.Delete(dir, true);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Despejando memória: " + Path.GetFileName(dir));
                    }
                    catch (IOException ioEx)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Arquivo em uso: " + Path.GetFileName(dir));
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro inesperado: " + Path.GetFileName(dir));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possível otimizar: " + ex.Message);
            }
        }

        static void StartupOptimize()
        {
            Console.Clear();
        }

        static void DisableBackgroundApp(string appName)
        {
            string command = $@"
$package = Get-AppxPackage -Name *{appName}*
if ($package -ne $null) {{
    $packageFamilyName = $package.PackageFamilyName
    # Desativa a execução em segundo plano no registro
    $regPath = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications'
    Set-ItemProperty -Path $regPath -Name $packageFamilyName -Value 2
    $capabilitiesPath = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\ApplicationCapability\CapabilityClasses\BackgroundTasks'
    Set-ItemProperty -Path $capabilitiesPath -Name $packageFamilyName -Value 0
    # Desativa a execução em segundo plano via política de grupo
    New-Item -Path 'HKCU:\Software\Policies\Microsoft\Windows\System' -Force | Out-Null
    Set-ItemProperty -Path 'HKCU:\Software\Policies\Microsoft\Windows\System' -Name 'DisableBackgroundApps' -Value 1
    Write-Host ""O aplicativo {appName} foi desativado em segundo plano.""
}} else {{
    Write-Host ""O aplicativo {appName} não foi encontrado.""
}}";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tentando desativar: " + appName);
            ExecutePowerShellCommand(command);
        }

        static void ExecutePowerShellCommand(string command)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = "powershell.exe";
            processInfo.Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"";
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            processInfo.UseShellExecute = false;
            processInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = processInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();
            if (!string.IsNullOrEmpty(output))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(output);
            }

            if (!string.IsNullOrEmpty(error))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro desconhecido encontrado ao desativar processo..");
            }
        }

        static void KillProcessByName(string processName)
        {
            string processTitle = "";
            try
            {
                var processes = Process.GetProcessesByName(processName);
                if (processes.Length == 0)
                {
                    processTitle = processName.ToString();
                    return;
                }

                foreach (var process in processes)
                {
                    process.Kill();
                    process.WaitForExit();
                    processTitle = processName.ToString();
                }
            }
            catch (Exception ex)
            {
                processTitle = processName.ToString();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Finalizando processo inútil: {processTitle}");
        }

        static void DisableStartupProcess(string processName)
        {
            if (string.IsNullOrEmpty(processName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nome do processo não pode ser vazio.");
                return;
            }
            if (DisableStartupProcess(Registry.CurrentUser, processName))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"O processo '{processName}' foi desativado da inicialização.");
                totalProcess++;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"O processo '{processName}' não foi encontrado na inicialização.");
            }
            if (DisableStartupProcess(Registry.LocalMachine, processName))
            {
                totalProcess++;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
        }

        static bool DisableStartupProcess(RegistryKey root, string processName)
        {
            bool disabled = false;
            string runKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            using (RegistryKey runKey = root.OpenSubKey(runKeyPath, true))
            {
                if (runKey != null)
                {
                    object value = runKey.GetValue(processName);
                    if (value != null)
                    {
                        runKey.SetValue(processName, "", RegistryValueKind.String);
                        disabled = true;
                    }
                }
            }

            return disabled;
        }
        static long GetDirectorySize(DirectoryInfo directoryInfo)
        {
            long size = 0;
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                size += file.Length;
            }
            foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
            {
                size += GetDirectorySize(dir);
            }
            return size;
        }
    }
}