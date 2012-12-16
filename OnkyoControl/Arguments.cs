using System;
using System.Collections;

namespace OnkyoControl
{
    class Arguments
    {
        public readonly Hashtable Table;

        //Method which will parse the string input and return a hashtable
        public Arguments(String[] args)
        {
            Table = new Hashtable();
            try
            {
                if ((null == args) || args.Length == 0)
                {
                    Table.Add("Invalid", "true");
                    return;
                }


                foreach (string s in args)
                {

                    if ((s.StartsWith("-") || s.StartsWith("/")) && s.Contains("="))
                    {
                        // Strip off - or /
                        string key = s.Substring(1, s.Length - 1);
                        string[] keyvalue = key.Split('=');
                        key = keyvalue[0];
                        string value = keyvalue[1];

                        if (value.Trim() == "")
                        {
                            Table.Add("Invalid", "true");
                            return;
                        }

                        AddKeyValuePair(key, value);

                    }
                    else
                    {
                        string param = s;
                        if (param.StartsWith("-") || param.StartsWith("/"))
                        {
                            // Strip off - or /
                            param = param.Substring(1, s.Length - 1);
                        }
                        AddKeyValuePair(param, "true");
                    }
                }
            }
            catch (ArgumentOutOfRangeException outOfRangeEx)
            {
                Console.WriteLine("Table out of range, please check error log" + outOfRangeEx.Message + Environment.NewLine + outOfRangeEx.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        //Add the arguments in the form of a keyvalue pair to Hashtable
        public void AddKeyValuePair(string key, string value)
        {
            try
            {
                if (!Table.ContainsKey(key))
                {
                    // add this to table
                    Table.Add(key, value);
                }
                else
                {
                    //substitute the value with the latest one
                    Table[key] = value;
                }
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message + Environment.NewLine + argEx.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        public bool HasArgument(string s)
        {
            return Table.ContainsKey(s);
        }

        public string GetArgument(string s)
        {
            return (string) (HasArgument(s) ? Table[s] : "");
        }
    }
}
