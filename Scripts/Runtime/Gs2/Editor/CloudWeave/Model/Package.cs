﻿using System;
using LitJson;

namespace Gs2.CloudWeave.Model
{
    public class Author
    {
        public string name;
        public string url;
        public string email;

        public static Author FromDict(JsonData data)
        {
            return new Author
            {
                name = data.ContainsKey("name") ? data["name"].ToString() : null,
                url = data.ContainsKey("url") ? data["url"].ToString() : null,
                email = data.ContainsKey("email") ? data["email"].ToString() : null,
            };
        }
    }
    
    public class Package
    {
        public string name;
        public string displayName;
        public string description;
        public Author author;
        public string sampleScene;
        public string tutorialWindowClassPath;

        public bool IsIncludeKeyWord(string keyword)
        {
            if (name != null && name.IndexOf(keyword, StringComparison.Ordinal) != -1) return true;
            if (displayName != null && displayName.IndexOf(keyword, StringComparison.Ordinal) != -1) return true;
            if (description != null && description.IndexOf(keyword, StringComparison.Ordinal) != -1) return true;
            if (author != null)
            {
                if (author.name != null && author.name.IndexOf(keyword, StringComparison.Ordinal) != -1) return true;
                if (author.url != null && author.url.IndexOf(keyword, StringComparison.Ordinal) != -1) return true;
                if (author.email != null && author.email.IndexOf(keyword, StringComparison.Ordinal) != -1) return true;
            }

            if (sampleScene != null && sampleScene.IndexOf(keyword, StringComparison.Ordinal) != -1) return true;
            if (tutorialWindowClassPath != null && tutorialWindowClassPath.IndexOf(keyword, StringComparison.Ordinal) != -1) return true;
            return false;
        }

        public static Package FromDict(JsonData data)
        {
            return new Package
            {
                name = data.ContainsKey("name") ? data["name"].ToString() : null,
                displayName = data.ContainsKey("displayName") ? data["displayName"].ToString() : null,
                description = data.ContainsKey("description") ? data["description"].ToString() : null,
                author = data.ContainsKey("author") ? Author.FromDict(data["author"]) : null,
                sampleScene = data.ContainsKey("sampleScene") ? data["sampleScene"].ToString() : null,
                tutorialWindowClassPath = data.ContainsKey("tutorialWindowClassPath") ? data["tutorialWindowClassPath"].ToString() : null,
            };
        }
    }
}