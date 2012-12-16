using System;
using System.Net;

namespace OnkyoControl
{
    class Program
    {
        private static Arguments programArguments;
        private static Receiver receiver;

        static int Main(string[] args)
        {
            programArguments = new Arguments(args);

            if (CheckCommandListHelpParams()) return 0; // List was requested and given, so exit without errorcode

            if( ! RequiredParametersAvailable())
            {
                ShowParameterHelp();
                return 1;
            }

            IPAddress ipAddress = ParseIPAddressParameter();
            if (ipAddress == null) return 2;

            var pkg = new Package(programArguments.GetArgument("command"));

            receiver = new Receiver(ipAddress);

            try
            {
                if (ExecuteVolumeCommands())
                {
                    return 0;
                }
            }catch(Exception ex)
            {
                HelpPrinter.PrintCopyright();
                HelpPrinter.ExceptionOccured(ex);
                return 3;
            }

            try
            {
                if (pkg.IsQuestion())
                {
                    string response = receiver.ExecuteQuestion(pkg, programArguments.HasArgument("outputDecimal"));
                    Console.Write(response);
                }
                else
                {
                    HelpPrinter.PrintCopyright();
                    receiver.ExecutePackage(pkg);
                }
                
            }catch(Exception ex)
            {
                HelpPrinter.PrintCopyright();
                HelpPrinter.ExceptionOccured(ex);
                return 3;
            }

            return 0;
        }

        private static void ShowParameterHelp()
        {
            HelpPrinter.PrintCopyright();
            HelpPrinter.PrintHelp();
        }

        private static bool ExecuteVolumeCommands()
        {
            if(HasVolumeParameters())
            {
                HelpPrinter.PrintCopyright();
            }
            if(programArguments.HasArgument("increaseVolume"))
            {
                int steps = int.Parse(programArguments.GetArgument("increaseVolume"));
                IncreaseVolume(steps);
                return true;
            }
            if(programArguments.HasArgument("decreaseVolume"))
            {
                int steps = int.Parse(programArguments.GetArgument("decreaseVolume"));
                DecreaseVolume(steps);
                return true;
            }
            if (programArguments.HasArgument("setVolume"))
            {
                int volume = int.Parse(programArguments.GetArgument("setVolume"));
                SetVolume(volume);
                return true;
            }

            return false;
        }

        private static void IncreaseVolume(int steps)
        {
            SetVolume(GetVolume() + steps);
        }

        private static void DecreaseVolume(int steps)
        {
            SetVolume(GetVolume() - steps);
        }

        private static int GetVolume()
        {
            Package pkg = new Package("MVLQSTN");
            return Int32.Parse(receiver.ExecuteQuestion(pkg, true));
        }

        private static void SetVolume(int volume)
        {
            if (volume < 0)
            {
                volume = 0;
            }else if(volume > 80)
            {
                volume = 80;
            }
            Package pkg = new Package("MVL" + volume.ToString("X").PadLeft(2, '0'));
            receiver.ExecutePackage(pkg);
        }

        private static bool HasVolumeParameters()
        {
            return programArguments.HasArgument("increaseVolume") || programArguments.HasArgument("decreaseVolume") || programArguments.HasArgument("setVolume");
        }

        private static bool HasCommandParameters()
        {
            return programArguments.HasArgument("command");
        }

        private static bool HasIpParameter()
        {
            return programArguments.HasArgument("ip");
        }

        private static bool RequiredParametersAvailable()
        {
            if (HasIpParameter() && (HasVolumeParameters() || HasCommandParameters()))
            {
                // All ok
                return true;
            }
            return false;
        }

        private static bool CheckCommandListHelpParams()
        {
            if (programArguments.HasArgument("c"))
            {
                HelpPrinter.PrintCopyright();
                HelpPrinter.PrintCommandlist();
                return true;
            }
            return false;
        }

        private static IPAddress ParseIPAddressParameter()
        {
            IPAddress ipAddress;
            if (!IPAddress.TryParse(programArguments.GetArgument("ip"), out ipAddress))
            {
                HelpPrinter.PrintCopyright();
                HelpPrinter.InvalidIP();
                return null;
            }

            return ipAddress;
        }
    }
}
