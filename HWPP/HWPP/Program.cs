// Lachezar Dimov Kolev
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace HWPP
{

    class Program
    {             
        static void Main(string[] args)
        {
            string name = string.Empty;
            string destinationFile;
            while (true)
            {               
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1 - Nums sort.");
                Console.WriteLine("2 - Word counter.");
                Console.WriteLine("3 - Brackets check.");
                Console.WriteLine("4 - Histogram of chars.");
                Console.WriteLine("5 - Shunting yard + RPN.");
                Console.WriteLine("6 - Exit.");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "one":
                    case "1":
                        destinationFile = "nums.txt";
                        if (CheckFile(ref name, destinationFile, option))
                        {
                            NumsSort(name);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Done! Check sortednumbers.txt to see the sorted version of your numbers...");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("Returning to options menu...");
                            Console.WriteLine();
                        }
                        else
                        {
                            if (InvalidFile())
                            {
                                goto case "1";
                            }
                        }
                        break;
                    case "two":
                    case "2":
                        destinationFile = "words.txt";
                        if (CheckFile(ref name, destinationFile, option))
                        {
                            WordCounter(name);
                            Console.WriteLine("Returning to options menu...");
                            Console.WriteLine();
                        }
                        else
                        {
                            if (InvalidFile())
                            {
                                goto case "2";
                            }
                        }
                        break;
                    case "three":
                    case "3":
                        destinationFile = "program.txt";                        
                        if(CheckFile(ref name, destinationFile, option))
                        {
                            BracketsCheck(name);
                            Console.WriteLine("Returning to options menu...");
                            Console.WriteLine();
                        }
                        else
                        {
                            if (InvalidFile())
                            {
                                goto case "3";
                            }
                        }
                        break;
                    case "four":
                    case "4":
                        destinationFile = "words.txt";
                        if(CheckFile(ref name, destinationFile, option))
                        {
                            Histogram(name);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Done! Check histrogram.txt to see result...");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("Returning to options menu...");
                            Console.WriteLine();
                        }
                        else
                        {
                            if (InvalidFile())
                            {
                                goto case "4";
                            }
                        }
                        break;
                    case "five":
                    case "5":                        
                        destinationFile = "calc.txt";                       
                        if (CheckFile(ref name, destinationFile, option))
                        {
                            ShuntingYard(name);
                            Console.WriteLine("Returning to options menu...");
                            Console.WriteLine();
                        }
                        else
                        {
                            if (InvalidFile())
                            {
                                goto case "5";
                            }
                        }
                        break;
                    case "six":
                    case "6":
                        Console.WriteLine();
                        Console.WriteLine("Exiting program...");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("See ya!");
                        Console.ResetColor();
                        return;
                    default:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Not valid option!");
                        Console.ResetColor();
                        Console.WriteLine("Returning to options menu...");
                        Console.WriteLine();
                        break;
                }
            }

        }
        private static void Sort(List<double> nums)
        {
            for (int i = 0; i < nums.Count - 1; i++)
            {
                bool flag = false;
                for (int j = 0; j < nums.Count - i - 1; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        flag = true;
                        double temp = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = temp;
                    }
                }
                if (!flag)
                {
                    break;
                }
            }
        }

        private static void NumsSort(string name)
        {
            using (StreamReader reader = new StreamReader(name))
            {
                double sum = 0;
                int counter = 0;

                string readLine = reader.ReadLine();
                List<double> nums = new List<double>();

                while (readLine != null)
                {
                    double currentNum = double.Parse(readLine);
                    sum += currentNum;
                    nums.Add(currentNum);
                    counter++;
                    readLine = reader.ReadLine();
                }
                Sort(nums);
                Console.WriteLine();
                Console.WriteLine($"Sum: {sum:F2}");
                Console.WriteLine($"Average: {sum / counter:F2}");
                Console.WriteLine($"Biggest num: {nums[nums.Count - 1]:F2}");
                Console.WriteLine($"smallest num: {nums[0]:F2}");
                Console.WriteLine($"Second biggest: {nums[nums.Count - 2]:F2}");
                Console.WriteLine($"Second smallest: {nums[1]:F2}");
                Console.WriteLine();
                using (StreamWriter writer = new StreamWriter("sortednumbers.txt"))
                {
                    foreach (double num in nums)
                    {
                        writer.WriteLine(num);
                    }
                }
            }
        }

        private static void WordCounter(string name)
        {
            using (StreamReader reader = new StreamReader(name))
            {
                Console.WriteLine("Choose a word:");

                string targetWord = Console.ReadLine();
                int counterConsiderable = 0;
                int counterNotConsiderable = 0;

                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] words = line.Split(new char[] { ' ', ',', '.', ':', ';', '-', '_' });
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i] == targetWord)
                        {
                            counterConsiderable++;
                        }
                        if (words[i].ToLower() == targetWord.ToLower())
                        {
                            counterNotConsiderable++;
                        }
                    }
                    line = reader.ReadLine();
                }
                if (counterNotConsiderable > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{targetWord} met: {counterConsiderable} times with considering capital letters.");
                    Console.WriteLine($"{targetWord} met: {counterNotConsiderable} times without considering capital letters.");
                    Console.WriteLine();
                    Console.WriteLine("Try another word?");
                    Console.WriteLine("Type Y/N...");
                    string option = Console.ReadLine().ToLower();
                    Console.WriteLine();
                    OptionCheck(option, name);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\"{targetWord}\" is not present in the file! Do you want to try another word?");
                    Console.ResetColor();
                    Console.WriteLine("Type Y/N...");
                    string option = Console.ReadLine().ToLower();
                    Console.WriteLine();
                    OptionCheck(option, name);
                }
            }
        }

        private static void BracketsCheck(string name)
        {
            using (StreamReader reader = new StreamReader(name))
            {
                string line = reader.ReadLine();
                Dictionary<char, char> closingBrackets = new Dictionary<char, char>() { { ']', '[' }, { ')', '(' }, { '}', '{' } };
                Stack<char> stack = new Stack<char>();
                while (line != null)
                {
                    char[] parentheses = line.ToCharArray();
                    if (parentheses.Contains('{') || parentheses.Contains('(') || parentheses.Contains('[')
                        || parentheses.Contains('}') || parentheses.Contains(')') || parentheses.Contains(']'))
                    {
                        foreach (char bracket in parentheses)
                        {
                            if (bracket == '{' || bracket == '(' || bracket == '[' || bracket == '}' || bracket == ')' || bracket == ']')
                            {
                                if (stack.Count > 0 && closingBrackets.ContainsKey(bracket) && stack.Peek() == closingBrackets[bracket])
                                {
                                    stack.Pop();
                                }
                                else
                                {
                                    stack.Push(bracket);
                                }
                            }
                        }
                    }

                    line = reader.ReadLine();
                }
                Console.WriteLine();
                if (stack.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("All is well good job!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Fix your brackets!");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        private static void Histogram(string name)
        {
            using (StreamReader reader = new StreamReader(name))
            {
                Dictionary<char, int> symbols = new Dictionary<char, int>();
                string line = reader.ReadLine();
                while (line != null)
                {
                    foreach (char symbol in line)
                    {
                        if (symbol != ' ')
                        {
                            if (!symbols.ContainsKey(symbol))
                            {
                                symbols.Add(symbol, 1);
                            }
                            symbols[symbol]++;
                        }
                    }
                    line = reader.ReadLine();
                }
                double sum = symbols.Values.Sum();
                Console.WriteLine();
                using (StreamWriter writer = new StreamWriter("histrogram.txt"))
                {
                    foreach (KeyValuePair<char, int> kvp in symbols)
                    {
                        writer.Write($"{kvp.Key} ");
                        Console.Write($"{kvp.Key} ");
                        for (int i = 0; i < kvp.Value; i++)
                        {
                            writer.Write("-");
                            Console.Write("-");
                        }
                        writer.WriteLine($"> {(kvp.Value / sum) * 100:F2}%");
                        Console.WriteLine($"> {(kvp.Value / sum) * 100:F2}%");
                    }
                }
                Console.WriteLine();
            }
        }

        private static int EvalRPN(Queue<string> output)
        {
            Stack<int> stack = new Stack<int>();
            foreach (var item in output)
            {
                int n;
                if (int.TryParse(item, out n))
                {
                    stack.Push(n);
                }
                else
                {
                    switch (item)
                    {
                        case "+":
                            stack.Push(stack.Pop() + stack.Pop());
                            break;
                        case "-":
                            int n1 = stack.Pop();
                            int n2 = stack.Pop();
                            stack.Push(n2 - n1);
                            break;
                        case "*":
                            stack.Push(stack.Pop() * stack.Pop());
                            break;
                        case "/":
                            n1 = stack.Pop();
                            n2 = stack.Pop();
                            stack.Push(n2 / n1);
                            break;
                    }
                }
            }
            return stack.Pop();
        }

        private static void ShuntingYard(string name)
        {
            using (StreamWriter writer = new StreamWriter("resultCalc.txt"))
            {
                using (StreamReader reader = new StreamReader(name))
                {
                    string calc = reader.ReadLine();
                    Queue<string> postfix;
                    while (calc != null)
                    {
                        postfix = RPN(calc);
                        int resultCalc = EvalRPN(postfix);
                        writer.WriteLine(calc + " = " + resultCalc);
                        calc = reader.ReadLine();
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine("Calculated! Check resultCalc.txt to see result...");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
        }

        private static Queue<string> RPN(string calc)
        {
            string[] input = calc.Split();
            Queue<string> output = new Queue<string>();
            Stack<string> signs = new Stack<string>();

            for (int i = 0; i < input.Length; i++)
            {
                if (int.TryParse(input[i], out _))
                {
                    output.Enqueue(input[i]);
                }
                else
                {
                    if (signs.Count == 0)
                    {
                        signs.Push(input[i]);
                    }
                    else
                    {
                        int currentOpPr = CheckPrecedence(signs.Peek());
                        int nextOpPr = CheckPrecedence(input[i]);
                        if (currentOpPr < nextOpPr || input[i] == "(")
                        {
                            signs.Push(input[i]);
                        }
                        else if (currentOpPr > nextOpPr || currentOpPr == nextOpPr && nextOpPr == 1 || currentOpPr == nextOpPr && nextOpPr == 2)
                        {
                            output.Enqueue(signs.Pop());
                            i--;
                        }
                        if (input[i] == ")")
                        {
                            while (signs.Contains("("))
                            {                              
                                output.Enqueue(signs.Pop());
                            }
                        }
                    }
                }

            }

            foreach (string sign in signs)
            {                
                output.Enqueue(sign);              
            }                       
            return output;
        }

       private static int CheckPrecedence(string opr)
       {
            switch (opr)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
                default:
                    throw new Exception("Not valid operator!");
            }
       }
        private static bool CheckFile(ref string name, string destinationFile, string option)
        {
            Console.WriteLine();
            Console.WriteLine($"File name for option {option}:");
            name = Console.ReadLine();       
            if (!name.Contains(".txt"))
            {
                name += ".txt";
            }
            if(name == destinationFile)
            {
                return true;
            }
            return false;
        }

        private static bool InvalidFile()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid File! Try again?");
            Console.ResetColor();
            CheckAgain:
            Console.WriteLine("Type Y/N...");
            string option = Console.ReadLine();
            if(option.ToLower() == "y")
            {
                return true;
            }
            else if (option.ToLower() == "n")
            {
                Console.WriteLine();
                Console.WriteLine("Returning to options menu....");
                Console.WriteLine();
                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Try again?");
                Console.ResetColor();
                goto CheckAgain;
            }            
        }

        private static void OptionCheck(string option, string name)
        {
            if (option == "y")
            {
                WordCounter(name);
            }
            else if (option == "n")
            {
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Try again?");
                Console.ResetColor();
                Console.WriteLine("Type Y/N...");
                option = Console.ReadLine();
                Console.WriteLine();
                OptionCheck(option, name);
            }
        }

    }
}

