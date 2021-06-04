﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Templates.Core.Resources;

namespace Microsoft.Templates.Core
{
    public static class StringExtensions
    {
        public static string Obfuscate(this string data)
        {
            string result = data;
            byte[] b64data = Encoding.UTF8.GetBytes(data);

            using (SHA512 sha2 = SHA512.Create())
            {
                result = GetHash(sha2, b64data);
            }

            return result.ToUpperInvariant();
        }

        private static string GetHash(HashAlgorithm md5Hash, byte[] inputData)
        {
            byte[] data = md5Hash.ComputeHash(inputData);

            var sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }

            return sb.ToString();
        }

        public static string[] GetMultiValue(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value.Trim()))
            {
                return new string[0];
            }

            var values = value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (values.Any(v => v != v.Trim()))
            {
                throw new InvalidDataException(string.Format(StringRes.ErrorExtraWhitespacesInMultiValues, value));
            }

            return values;
        }

        public static bool IsMultiValue(this string value)
        {
            return value.GetMultiValue().Length > 1;
        }

        public static int GetLeadingTrivia(this string statement)
        {
            return statement.TakeWhile(char.IsWhiteSpace).Count();
        }

        public static string WithLeadingTrivia(this string statement, int triviaCount)
        {
            if (triviaCount < 1)
            {
                return statement;
            }
            else
            {
                return string.Concat(new string(' ', triviaCount), statement);
            }
        }
    }
}
