namespace DotNet6.CodeLibrary.MathRoundTest
{
    public class MathRoundTestDemo
    {
        public static void Run()
        {
            var originalNumber = (decimal)9.081;
            var nomalNumber = Math.Round(originalNumber, 2);//output:9.08
            var toEvenNumber = Math.Round(originalNumber, 2, MidpointRounding.ToEven);//output:9.08
            var awayFromZeroNumber = Math.Round(originalNumber, 2, MidpointRounding.AwayFromZero);//output:9.08
            var toZeroNumber = Math.Round(originalNumber, 2, MidpointRounding.ToZero);//output:9.08
            var toNegativeInfinityNumber = Math.Round(originalNumber, 2, MidpointRounding.ToNegativeInfinity);//output:9.08
            var toPositiveInfinityNumber = Math.Round(originalNumber, 2, MidpointRounding.ToPositiveInfinity);//output:9.09

            var originalNumber2 = (decimal)9.087;
            var nomalNumber2 = Math.Round(originalNumber2, 2);//output:9.09
            var toEvenNumber2 = Math.Round(originalNumber2, 2, MidpointRounding.ToEven);//output:9.09
            var awayFromZeroNumber2 = Math.Round(originalNumber2, 2, MidpointRounding.AwayFromZero);//output:9.09
            var toZeroNumber2 = Math.Round(originalNumber2, 2, MidpointRounding.ToZero);//output:9.08
            var toNegativeInfinityNumber2 = Math.Round(originalNumber2, 2, MidpointRounding.ToNegativeInfinity);//output:9.08
            var toPositiveInfinityNumber2 = Math.Round(originalNumber2, 2, MidpointRounding.ToPositiveInfinity);//output:9.09

            var originalNumber3 = (decimal)9.085;
            var nomalNumber3 = Math.Round(originalNumber3, 2);//output:9.08
            var toEvenNumber3 = Math.Round(originalNumber3, 2, MidpointRounding.ToEven);//output:9.08
            var awayFromZeroNumber3 = Math.Round(originalNumber3, 2, MidpointRounding.AwayFromZero);//output:9.09
            var toZeroNumber3 = Math.Round(originalNumber3, 2, MidpointRounding.ToZero);//output:9.08
            var toNegativeInfinityNumber3 = Math.Round(originalNumber3, 2, MidpointRounding.ToNegativeInfinity);//output:9.08
            var toPositiveInfinityNumber3 = Math.Round(originalNumber3, 2, MidpointRounding.ToPositiveInfinity);//output:9.09


        }
    }
}
