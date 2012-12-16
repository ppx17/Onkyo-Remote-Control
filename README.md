Onkyo Remote Controller
=======================

This is a simple, commandline based remote controller for the Onkyo NR series based on the ISCP over ethernet protocol.

Usage
-----

<pre>
OnkyoControl.exe /ip=[ip] (/command=[command]) (/c) (/increaseVolume=[steps]) (/decreaseVolume=[steps]) (/outputDecimal)

/ip=[ip]                 This is the only required argument, replace [ip] with the receivers IP Address
/command=[command]       The ISCP command you wish to execute, see /c for a list of suggested parameters.
/c                       When this command is set, a suggested list of commands is shown
                          Use Google to find a full list of commands supported by your receiver.
/increaseVolume=[steps]  Increase the master volume by [steps] steps.
/decreaseVolume=[steps]  Decrease the master volume by [steps] steps.
/setVolume=[volume]      Set the volume to [volume]
/outputDecimal           When asing a question (A command ending in QSTN), this option converts
                          the response to a directly usable decimal in stead of the Hex codes.
  **NOTE** Only one of increase/decrease/setVolume will be executed when both are given in the same command.
  **NOTE** When a increase/decrease/setVolume volume command is specified, no other commands are executed.
  **NOTE** The increase/decrease/setVolume arguments are hardlimited to a max of 80.

Errorcodes:
0   All OK
1   Not all required parameters are given
2   Invalid IP-Address
3   Some exception occured, receiver is probably unreachable
</pre>
