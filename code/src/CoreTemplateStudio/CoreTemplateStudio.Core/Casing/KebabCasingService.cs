﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Templates.Core.Casing
{
    public class KebabCasingService : ICasingService
    {
        public static string ParameterName => "wts.sourceName.casing.kebab";

        public string GetParameterName()
        {
            return ParameterName;
        }

        public string Transform(string value)
        {
            return value.ToKebabCase();
        }
    }
}
