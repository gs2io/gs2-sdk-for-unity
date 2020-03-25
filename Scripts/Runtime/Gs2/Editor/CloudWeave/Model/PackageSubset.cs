﻿using LitJson;

namespace Gs2.CloudWeave.Model
{
    public class PackageSubset
    {
        public string name;

        public static PackageSubset FromDict(JsonData data)
        {
            return new PackageSubset
            {
                name = data.ToString()
            };
        }
    }
}