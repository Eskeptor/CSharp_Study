using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BCL_Regex
{
    class Program
    {
        static bool IsMailNotUseRegex(string _mail)
        {
            string[] split = _mail.Split('@');
            if (split.Length != 2)
                return false;
            if (!IsAlphaNumeric(split[0]))
                return false;

            split = split[1].Split('.');
            if (split.Length == 1)
                return false;

            foreach(string str in split)
            {
                if (!IsAlphaNumeric(str))
                    return false;
            }
            return true;
        }
        static bool IsAlphaNumeric(string _text)
        {
            foreach(char ch in _text)
            {
                if (!char.IsLetterOrDigit(ch))
                    return false;
            }
            return true;
        }
        static bool IsMailUseRegex(Regex _regex, string _mail)
        {
            return _regex.IsMatch(_mail);
        }
        static void Main(string[] args)
        {
            Regex regex = new Regex(@"^([0-9a-zA-Z]+)@([0-9a-zA-Z]+)(\.[0-9a-zA-Z]+){1,}$");
            string correct_email = "abcd1234@korea.com";
            string incorrect_email = "abcd1234ko";
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

            timer.Start();
            bool res = IsMailNotUseRegex(correct_email);
            timer.Stop();
            Console.WriteLine("IsMailNotUseRegex(correct_email)");
            Console.WriteLine(correct_email + ": " + res);
            Console.WriteLine("걸린시간: " + timer.ElapsedTicks);
            Console.WriteLine();

            timer.Start();
            res = IsMailNotUseRegex(incorrect_email);
            timer.Stop();
            Console.WriteLine("IsMailNotUseRegex(incorrect_email)");
            Console.WriteLine(incorrect_email + ": " + res);
            Console.WriteLine("걸린시간: " + timer.ElapsedTicks);
            Console.WriteLine();

            timer.Start();
            res = IsMailUseRegex(regex, correct_email);
            timer.Stop();
            Console.WriteLine("IsMailUseRegex(correct_email)");
            Console.WriteLine(correct_email + ": " + res);
            Console.WriteLine("걸린시간: " + timer.ElapsedTicks);
            Console.WriteLine();

            timer.Start();
            res = IsMailUseRegex(regex, incorrect_email);
            timer.Stop();
            Console.WriteLine("IsMailUseRegex(incorrect_email)");
            Console.WriteLine(incorrect_email + ": " + res);
            Console.WriteLine("걸린시간: " + timer.ElapsedTicks);
            Console.WriteLine();
        }
    }
}
