using Application.Features.ProductFeatures.Commands;
using Domain.Entities;
using System;
using System.Security.Cryptography;
using System.Text;

namespace XUnitTest
{
    public static class Any
    {        
        private static readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

        public static int RandomInt(int max = int.MaxValue)
        {
            var data = new byte[4];
            _randomNumberGenerator.GetNonZeroBytes(data);

            // Convert the bytes to a UInt32
            uint scale = BitConverter.ToUInt32(data, 0);

            // And use that to pick a random number >= min and < max
            return (int)(max * (scale / (uint.MaxValue + 1.0)));
        }

        public static bool RandomBool() => RandomInt(2) == 1;

        public static DateTime RandomDateTime() => new DateTime(RandomInt(2018) + 1, RandomInt(11) + 1, RandomInt(27) + 1);

        public static decimal RandomDecimal() => RandomInt(int.MaxValue);

        public static long RandomLong() => RandomInt(int.MaxValue);

        public static string RandomString(int length = 10)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[RandomInt(chars.Length)]);
            }

            return stringBuilder.ToString();
        }

        public static short RandomShort(int max = 1000) => (short)RandomInt(max);

        public static byte[] RandomByteArray(int length = 100)
        {
            var byteArray = new byte[length];

            _randomNumberGenerator.GetBytes(byteArray);

            return byteArray;
        }

        public static string[] RandomStringArray(int length = 10)
        {
            var stringArray = new string[length];

            for (var i = 0; i < length; i++)
            {
                stringArray.SetValue(RandomString(), i);
            }

            return stringArray;
        }

        public static Product ProductEntity(string name = null, 
                                            string barcode = null, 
                                            bool isActive = false, 
                                            string description = null, 
                                            decimal? rate = null, 
                                            decimal? buyingPrice = null)
        {
            return new Product
            {
                Name = name ?? RandomString(),
                Barcode = barcode ?? RandomString(),
                IsActive = isActive,
                Description = description ?? RandomString(),
                Rate = rate ?? RandomDecimal(),
                BuyingPrice = buyingPrice ?? RandomDecimal()
            };
        }

        public static CreateProductCommandDto CreateProductCommandDto() => new CreateProductCommandDto
        {
            Name = RandomString(),
            Barcode = RandomString(),
            IsActive = true,
            Description = RandomString(),
            Rate = RandomDecimal(),
            BuyingPrice = RandomDecimal(),
        };

        //public static ProductDto[] ProductDtoArray(int length = 5) => Enumerable.Range(0, length).Select(i => ProductDto()).ToArray();
    }
}
