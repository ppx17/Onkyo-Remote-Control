using System;

namespace OnkyoControl
{
    class HelpPrinter
    {
        public static void PrintCopyright()
        {
            Console.WriteLine("");
            const string copyright = "* recievecontrol.exe by Niels Oostindiër, https://github.com/ *";
            Console.WriteLine(new string('*', copyright.Length));
            Console.WriteLine(copyright);
            Console.WriteLine(new string('*', copyright.Length));
            Console.WriteLine("");
        }

        public static void PrintHelp()
        {
            Console.WriteLine("Usage: recievercontrol.exe <ipaddress> <command>");
            Console.WriteLine("");
            Console.WriteLine("To see the list of commands use recievercontrol.exe -c");
            Console.WriteLine("Note, you might want to pipe that to more, like: recievercontrol.exe -c | more");
        }
        public static void InvalidIP()
        {
            Console.WriteLine("! Invalid IP address given");
            PrintHelp();
        }

        public static void PrintCommandlist()
        {
            Console.Write(_commandList);
        }

        public static void ExceptionOccured(Exception exception)
        {
            Console.WriteLine("! Something went wront while sending the command: {0}", exception.Message);
        }

        private static string _commandList =
            @"========= Power =========
PWR00     Poweroff
PWR01     Poweron

========= Muting =========
AMT00     Audio muting off
AMT01     Audio muting on

========= A/B Speakers =========
SPA00     Speaker A Off
SPA01     Speaker A On
SPB00     Speaker B Off
SPB01     Speaker B 

========= Output mode =========
LMD00     STEREO
LMD01     DIRECT
LMD02     SURROUND
LMD03     FILM
LMD04     THX
LMD05     ACTION
LMD06     MUSICAL
LMD07     MONO MOVIE
LMD08     ORCHESTRA
LMD09     UNPLUGGED
LMD0A     STUDIO-MIX
LMD0B     TV LOGIC
LMD0C     ALL CH STEREO
LMD0D     THEATER-DIMENSIONAL
LMD0E     ENHANCED 7/ENHANCE
LMD0F     MONO
LMD11     PURE AUDIO
LMD12     MULTIPLEX
LMD13     FULL MONO
LMD14     DOLBY VIRTUAL
LMD40     5.1ch Surround
LMD40     Straight Decode*1
LMD41     Dolby EX/DTS ES
LMD41     Dolby EX*2
LMD42     THX Cinema
LMD43     THX Surround EX
LMD50     U2/S2 Cinema/Cinema2
LMD51     MusicMode
LMD52     Games Mode
LMD80     PLII/PLIIx Movie
LMD81     PLII/PLIIx Music
LMD82     Neo:6 Cinema
LMD83     Neo:6 Music
LMD84     PLII/PLIIx THX Cinema
LMD85     Neo:6 THX Cinema
LMD86     PLII/PLIIx Game
LMD87     Neural Surr*3
LMD88     Neural THX
LMD89     PLII THX Games
LMD8A     Neo:6 THX Games
LMDUP     Listening Mode Wrap-Around Up
LMDDOWN   Listening Mode Wrap-Around Down

========= Input Selector =========
SLI00     VIDEO1    VCR/DVR
SLI01     VIDEO2    CBL/SAT
SLI02     VIDEO3    GAME/TV
SLI03     VIDEO4    AUX1(AUX)
SLI04     VIDEO5    AUX2
SLI05     VIDEO6
SLI06     VIDEO7
SLI10     DVD
SLI20     TAPE(1)
SLI21     TAPE2
SLI22     PHONO
SLI23     CD
SLI24     FM
SLI25     AM
SLI26     TUNER
SLI27     MUSIC SERVER
SLI28     INTERNET RADIO
SLI29     USB
SLI30     MULTI CH
SLI31     XM*1
SLI32     SIRIUS*1
SLIUP     Selector Position Wrap-Around Up
SLIDOWN   Selector Position Wrap-Around Down

This list is only intended as a guide, specific input selections may be different per reciever model";

    }
}
