using System;
using System.Net;

namespace OnkyoControl
{
    class Program
    {
        static int Main(string[] args)
        {
            HelpPrinter.PrintCopyright();

            if(args.Length == 1 && args[0] == "-c")
            {
                HelpPrinter.PrintCommandlist();
                return 0;
            }

            if (args.Length != 2)
            {
                HelpPrinter.PrintHelp();
                return 1;
            }

            IPAddress ipAddress;
            if( ! IPAddress.TryParse(args[0], out ipAddress))
            {
                HelpPrinter.InvalidIP();
                return 2;
            }


            var pkg = new Package(args[1]);

            var receiver = new Receiver(ipAddress);

            try
            {
                receiver.ExecutePackage(pkg);
            }catch(Exception ex)
            {
                HelpPrinter.ExceptionOccured(ex);
                return 3;
            }

            return 0;
        }
    }
}
