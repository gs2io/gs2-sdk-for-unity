﻿using System.Collections.Generic;
using System.Linq;
using LitJson;

namespace Gs2.CloudWeave.Model
{
    public class PackageSubsets
    {
        public List<PackageSubset> packages;

        public static PackageSubsets FromDict(JsonData data)
        {
            return new PackageSubsets
            {
                packages = data.Cast<JsonData>().Select(PackageSubset.FromDict).ToList(),
            };
        }
    }
}