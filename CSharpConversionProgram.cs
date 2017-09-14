﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloatingPointConversion
{
    class BinaryToDenary
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Enter the mantissa including the implied binary point");
            Console.WriteLine(" if the binary point is not enetered it will be assumed");
            Console.WriteLine(" that the binary point is one right of the MSB");
            Console.WriteLine(" E.g. 0100010 -> 0.100010");
            Console.WriteLine("");
            string mantissa = Console.ReadLine();
            bool pointPresent = false;

            for (int i = 0; i < mantissa.Length; i++)
            {
                if (mantissa[i] == Convert.ToChar("."))
                {
                    pointPresent = true;
                }
            }

            if (!pointPresent)
            {
                string tempstring = Convert.ToString(mantissa[0]);
                tempstring = tempstring + ".";
                for (int x = 1; x < mantissa.Length; x++)
                {
                    tempstring = tempstring + mantissa[x];
                }
                Console.WriteLine("Mantissa is now {0}", tempstring);
                mantissa = tempstring;
                //convert to denary
            }
            char FirstChar = mantissa[0];
            char SecondChar = mantissa[1];
            decimal denaryMantissa = 0;

            if (FirstChar == Convert.ToChar("1") || FirstChar == Convert.ToChar(".") && SecondChar == Convert.ToChar("1"))
            {
                //call twos compliment negative
                denaryMantissa = ConvertToDenaryNegative(mantissa);
                Console.WriteLine(" number enetered is negative");
            }
            else
            {
                denaryMantissa = ConvertToDenaryPositive(mantissa);
            }

            Console.WriteLine(" Enter your exponent in binary");
            Console.WriteLine(" Including the binary point");
            Console.WriteLine("");
            string Exponent = Console.ReadLine();
            decimal DenaryExponent = 0;

            if (Exponent.Length == 1)
            {
                Exponent = Exponent + ".";
            }

            if (Convert.ToString(Exponent[0]) == "0")
            {
                DenaryExponent = ConvertToDenaryPositive(Exponent);
            }

            if (Convert.ToString(Exponent[0]) == "1")
            {
                DenaryExponent = ConvertToDenaryNegative(Exponent);
            }

            Console.WriteLine("");
            double multiplyBy = (Math.Pow(2, Convert.ToDouble(DenaryExponent)));
            Console.WriteLine(" 2 to the power of {0} is {1}", DenaryExponent, multiplyBy);
            double DoubleMantissa = Convert.ToDouble(denaryMantissa);
            double OutPut = (DoubleMantissa * multiplyBy);
            Console.WriteLine(" Mutliplying {0} by {1} gives {2}", denaryMantissa, multiplyBy, OutPut);
            Console.WriteLine(" Mantissa is {0}, Exponent is {1}", DoubleMantissa, DenaryExponent);
            Console.WriteLine(" The denary equivalent of the floating point entered is {0}", OutPut);
            Console.ReadKey();

        }
        static decimal ConvertToDenaryPositive(string input)
        {
            decimal returnDec = 0;
            //find the index of the binary point
            int index = 0;
            for (int x = 0; x < input.Length; x++)
            {
                if (Convert.ToString(input[x]) == ("."))
                {
                    index = x;
                }
            }
            //convert by power 2 of relative to position left or right of binary point
            for (int x = 0; x < input.Length; x++)
            {

                if (x != index)
                {
                    if (x < index)
                    {
                        if (Convert.ToString(input[x]) == "1")
                        {
                            returnDec = returnDec + Convert.ToDecimal(Math.Pow(2, (index - x) - 1));
                        }
                    }
                    if (x > index)
                    {
                        if (Convert.ToString(input[x]) == "1")
                        {
                            returnDec = returnDec + Convert.ToDecimal(Math.Pow(2, (index - x)));
                        }
                    }
                }
            }
            return returnDec;
        }
        static decimal ConvertToDenaryNegative(string input)
        {
            decimal returnDec = 0;
            //find the index of the binary point
            int index = 0;
            for (int x = 0; x < input.Length; x++)
            {
                if (Convert.ToString(input[x]) == ("."))
                {
                    index = x;
                }
            }
            //convert by power 2 of relative to position left or right of binary point
            bool HasSubtractedFirst = false;
            for (int x = 0; x < input.Length; x++)
            {
                if (x != index)
                {
                    if (x < index)
                    {
                        if (Convert.ToString(input[x]) == "1")
                        {
                            if (HasSubtractedFirst)
                            {
                                returnDec = returnDec + Convert.ToDecimal(Math.Pow(2, (index - x) - 1));
                            }
                            if (!HasSubtractedFirst)
                            {
                                returnDec = returnDec - Convert.ToDecimal(Math.Pow(2, (index - x) - 1));
                                HasSubtractedFirst = true;
                            }
                        }
                    }

                    if (x > index)
                    {
                        if (Convert.ToString(input[x]) == "1")
                        {
                            if (HasSubtractedFirst)
                            {
                                returnDec = returnDec + Convert.ToDecimal(Math.Pow(2, (index - x)));
                            }
                            if (!HasSubtractedFirst)
                            {
                                returnDec = returnDec - Convert.ToDecimal(Math.Pow(2, (index - x) - 1));
                                HasSubtractedFirst = true;
                            }
                        }
                    }
                }
            }
            return returnDec;
        }
    }
}