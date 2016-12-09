using System;
using System.Text;

namespace DotNet.Globbing.Generation
{
    public static class GlobMatchGeneratorExtensions
    {
        public static void AppendRandomLiteralCharacter(this StringBuilder builder, Random random)
        {
            var typeOfCharacterToAppend = random.Next(0, 3);
            switch (typeOfCharacterToAppend)
            {
                case 0:
                    builder.Append(random.GetRandomCharacterBetween('a', 'z'));
                    break;
                case 1:
                    builder.Append(random.GetRandomCharacterBetween('A', 'Z'));
                    break;
                case 2:
                    builder.Append(random.GetRandomCharacterBetween('0', '9'));
                    break;
                case 3:
                    builder.Append(random.GetRandomCharacterBetween('-', '.'));
                    break;
                default:
                    throw new InvalidOperationException();
                // break;
            }
        }

        public static void AppendRandomLiteralString(this StringBuilder builder, Random random, int maxLength = 10)
        {
            // append 0 or many, random literal characters
            var numberToAppend = random.Next(0, maxLength);
            if (numberToAppend > 0)
            {
                for (int i = 1; i <= numberToAppend; i++)
                {
                    builder.AppendRandomLiteralCharacter(random);
                }
            }
        }

        public static char GetRandomCharacterBetween(this Random random, char start, char end)
        {
            return (char)random.Next((int)start, (int)end);
        }


    }
}