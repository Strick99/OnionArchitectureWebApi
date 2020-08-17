using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Application.Extensions
{
    public static class StringExtensions
    {
        public static long DecodeKeyToId(this string key)
        {
            long id = 0;
            var base32Chars = "A0D23E1V456Q7SK8N9BYFGHRPCTJWMXZ".ToCharArray();
            var keyArray = key.ToCharArray();
            Array.Reverse(keyArray);

            for (int i = 0; i < keyArray.Length; i++)
            {
                id += ((Array.IndexOf(base32Chars, keyArray[i])) * (long)(Math.Pow(32, i)));
            }

            return id - 100000000000;
        }

        public static string EncodeIdToKey(this long id)
        {
            StringBuilder key = new StringBuilder();

            var value = id + 100000000000;
            var base32Chars = "A0D23E1V456Q7SK8N9BYFGHRPCTJWMXZ".ToCharArray();

            while (value > 0)
            {
                key.Insert(0, base32Chars[(value % 32)]);
                value /= 32;
            }

            return key.ToString();
        }

        internal static string NullSafeTrim(this string value)
        {
            return value?.Trim();
        }

        internal static bool AreEqualIgnoreNulls(this string str, string strToCompare)
        {
            return StringComparer.Ordinal.Equals((str ?? "").Trim(), (strToCompare ?? "").Trim());
        }

        internal static bool AreEqualIgnoreNullsAndCase(this string str, string strToCompare)
        {
            return StringComparer.OrdinalIgnoreCase.Equals((str ?? "").Trim(), (strToCompare ?? "").Trim());
        }

        internal static bool AreEqualIgnoreCase(this string str, string strToCompare) =>
            String.Equals(str, strToCompare, StringComparison.InvariantCultureIgnoreCase);

        internal static bool AreDecimalEqualIgnoreNulls(this string str, string strToCompare)
        {
            if (str.AreEqualIgnoreNullsAndCase(strToCompare))
            {
                return true;
            }

            if ((string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(strToCompare)) || (string.IsNullOrEmpty(strToCompare) && !string.IsNullOrEmpty(str)))
            {
                return false;
            }

            _ = decimal.TryParse(str, out decimal decimalValue);
            _ = decimal.TryParse(strToCompare, out decimal decimalValuetoCompare);

            return decimalValue == decimalValuetoCompare;
        }

        internal static int? ToInteger(this string str) =>
            int.TryParse(str, NumberStyles.Integer, CultureInfo.InvariantCulture, out int intValue) ? intValue : (int?)null;

        internal static bool IsValidInteger(this string str, out int? integerValue)
        {
            bool isValid = int.TryParse(str, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value);

            integerValue = isValid ? value : (int?)null;
            return isValid;
        }

        private static readonly IReadOnlyDictionary<string, string> _changeHistoryFieldNames =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
            {
                { "VendorName", "Vendor Name" },
                { "ExcludedRateCategories", "Excluded Rate Categories" },
                { "IncludedRateCategories", "Included Rate Categories" },
                { "RateCategoryName", "Category Group Name" },
                { "AddressName", "Address Name" },
                { "CountryAlpha2Code", "Country" },
                { "AddressLine1", "Address Line 1" },
                { "AddressLine2", "Address Line 2" },
                { "City", "City" },
                { "StateProvinceCode", "State/Province" },
                { "StateProvinceName", "State/Province" },
                { "Postalcode", "Postal Code" },
                { "LegacyPublisherNumber", "Legacy Publisher Number" },
                { "PublisherUrl", "Publisher URL" },
                { "PublisherCategoryCode", "Publisher Category" },
                { "ContactRoleCode", "Contact Role" },
                { "EmailAddresses", "Email Addresses" },
                { "FirstName", "First Name" },
                { "HonorificCode", "Honorific" },
                { "LastName", "Last Name" },
                { "MiddleName", "Middle Name" },
                { "Note", "Note" },
                { "Phones", "Phone Numbers" },
                { "Roles", "Vendor Roles" },
                { "ExcludedGeoGroups", "Excluded Geo Groups" },
                { "IncludedGeoGroups", "Included Geo Groups" },
                { "GeoGroupName", "Geo Group Name" },
                { "IsoCurrencyCode", "ISO Currency" },
                { "ExpirationDate", "Expiration Date" },
                { "ExpireAssociatedRates", "Associated Rates Expired" },
                { "OfferingFormatCode", "Format" },
                { "OfferingDescriptorCode", "Offering Descriptors" },
            };

        internal static string ToChangeHistoryFieldNameDisplay(this string field)
        {
            if (field == null)
                return null;

            if (_changeHistoryFieldNames.TryGetValue(field, out var displayName))
                return displayName;

            return field;
        }

        internal static bool IsNumeric(this string value)
        {
            return long.TryParse(value, out long number);
        }
    }
}
