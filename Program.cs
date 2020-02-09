using System;
using System.Collections.Generic;

namespace Cordner_Assign1
{
    class Program
    {
        static void Main(string[] args)
        {
            // question1
            int n = 5;
            Program.PrintPattern(n);
            
            // question2
            int n2 = 6;
            Program.PrintSeries(n2);

            //question3
            string s = "09:15:35PM";
            string t = UsfTime(s);
            Console.WriteLine(t);

            //question4
            int n3 = 110;
            int k = 11;
            Program.UsfNumbers(n3, k);

            //quesiton5
            string[] words = new string[] { "abcd", "dcba", "lls", "s", "sssll" };
            Program.PalindromePairs(words);

            //question6
            int n4 = 23;
            Program.Stones(n4);

        }

        private static void PrintPattern(int n)
        {
            if (n == 0) return;              // stop condition
            if (n < 0) n = Math.Abs(n);      // in case negative number is entered

            for (int i = 0; i < n; i++) Console.Write(n - i);

            Console.WriteLine();
            Program.PrintPattern(n - 1);
        }

        private static void PrintSeries(int n2)
        {
            int x = 0;

            if (n2 < 0) n2 = Math.Abs(n2);      //in case a negative number is passed
            else if (n2 == 0)
            {
                Console.WriteLine("Silly Goose! you can't print zero numbers!");
                return;
            }
            for (int i = 1; i < n2 + 1; i++)
            {
                x = x + i;
                if (i == n2) Console.Write(x);
                else Console.Write(x + ",");
            }
            Console.WriteLine("");
            return;
        }

        public static string UsfTime(string s)
        {
            int[] etime = new int[3];   // array for Earth time
            int hr12;                   // varaible to convert to 24hr time
            int y;
            int unum, snum, fnum;       // var for u s f constants
            float numsec;               // num of sec in given time
            float utime, stime, ftime;  // usf time variables
            string usft;                //output string
            string merid;               // meridien

            // constants for USF time
            unum = 36;
            snum = 60;
            fnum = 45;

            y = 3;
            // in case there are no colons in the string
            if (s.Length < 10)
            {
                if (s.Contains(":") == false) y = 2;
                else
                {
                    usft = "You forgot something in your string";
                    return usft;
                }
            } 

            // break hr mm ss into array
            for (int i = 0; i < 3; i++)
            {
                int x = y * i;
                etime[i] = Convert.ToInt32(s.Substring(x, 2));
            }

            // in case there is no meridian
            if (s.Contains("PM") == true) merid = "PM";
            else if (s.Contains("AM") == true) merid = "AM";
            else
            {
                // if time entered is in military time
                if (etime[0] > 12)
                {
                    etime[0] = etime[0] - 12;
                    merid = "PM";
                }
                else merid = "AM";
            }

            // determine if afternoon, midnight, or morning
            if (merid == "PM" & etime[0] < 12) hr12 = 12;
            else if (merid == "AM" & etime[0] > 11) hr12 = -12;
            else hr12 = 0;

            // calculate total secords of input time
            numsec = ((etime[0] + hr12) * 3600) + (etime[1] * 60) + etime[2];

            // convert time to dec multipliers
            ftime = (numsec % fnum) / fnum;
            stime = (((numsec / fnum) - ftime) % snum) / snum;
            utime = (((numsec / fnum) - ftime) / snum) - stime;

            // multi by constant to get s and f amounts
            ftime = ftime * fnum;
            stime = stime * snum;

            usft = utime + ":" + stime + ":" + ftime;
            return usft;
        }

        public static void UsfNumbers(int n3, int k)
        {
            if (n3 < 2 | k < 1)
            {
                Console.WriteLine("You entered numbers incompatible with reality");
                return;
            }
            
            int counter = n3-1;
            decimal x = n3 / k;     // get total num of lines
            x = Math.Ceiling(x);    //always rounds up
            int numline = Convert.ToInt32(x);
   
            for(int i = 1; i<numline+1; i++)        //outputs sequence for n3/k # lines
            {
                for (int j = 1; j<k+1; j++)         // outputs seq of k nums per line
                {
                    int curnum = n3 - counter;
                    counter--;

                    if (counter < -2) return;

                    if (curnum % 3 == 0 & curnum % 5 == 0 & curnum % 7 == 0) Console.Write("USF ");

                    else if (curnum % 3 == 0 & curnum % 5 == 0) Console.Write("US ");

                    else if (curnum % 3 == 0 & curnum % 7 == 0) Console.Write("UF ");

                    else if (curnum % 5 == 0 & curnum % 7 == 0) Console.Write("SF ");

                    else if (curnum % 3 == 0) Console.Write("U ");

                    else if (curnum % 5 == 0) Console.Write("S ");

                    else if (curnum % 7 == 0) Console.Write("F ");

                    else Console.Write(curnum + " ");
                }

                Console.WriteLine();
            }

        }

        public static void PalindromePairs(string[] words)
        {

            if (words.Length < 2)
            {
                Console.WriteLine("You need to be more like my wife.....wordy");
                return;
            }

            int x = words.Length;
            string temp;
            Boolean space = false;

            for (int i = 0; i<x; i++)
            {
                for (int j=0; j<x; j++)
                {
                    if (i != j)  //dont' combine the same word twice
                    {
                        // combine words and then reverse order
                        temp = words[i] + words[j];
                        // corner cases
                        if(space==false) space = temp.Contains(" ");
                        temp = temp.ToLower();              // make sure all letters same case to compare
                        temp = temp.Replace(" ", "");       // remove any spaces in the string

                        char[] check = temp.ToCharArray();
                        Array.Reverse(check);
                        String revtemp = new String(check);

                        if (temp == revtemp) Console.Write("[" + i + "," + j + "]");
                    }
                }
            }

            if (space == true) Console.WriteLine("I removed spaces from your string.");
            Console.WriteLine("");
        }

        public static void Stones(int n4)
        {
            int curtake=0;
            int prevtake=0;
            Random rnd = new Random();
            Boolean p1p2 = false;                    //true denotes player 1, false is player 2
            List<int> turns = new List<int> ();

            // check if n4 is sufficientlly large enough
            if (n4 <1)
            {
                Console.WriteLine("You don't have enough stones to play this game...he he");
                return;
            }
            else if (n4 < 4)
            {
                Console.WriteLine("[" + n4 + ",0]");
                return;
            }

            // based on rules, if num stones is divisible by 4 the game is up for player 1
            if (n4 % 4 == 0)
            {
                Console.WriteLine(p1p2);
                return;
            }

            // set things up for the first turn
            p1p2 = true;
            curtake = n4 % 4;
            
            while (n4 > 0)
            {
                if (prevtake != 0 & p1p2 == true) curtake = 4 - prevtake;
                else curtake = rnd.Next(1, 3);         // random number to sim what another player may do
                n4 = n4 - curtake;
                prevtake = curtake;
                turns.Add(curtake);
                p1p2 = !p1p2;
            }

            p1p2 = !p1p2;
            Console.Write("Player 1 Wins!! with these cool moves: [");
            turns.ForEach(i => Console.Write("{0},", i));
            Console.Write("\b]");

        }
    }
}
