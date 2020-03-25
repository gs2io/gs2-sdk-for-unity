﻿using System;
 using System.Collections;
using System.Collections.Generic;
using Gs2.CloudWeave.Model;
using LitJson;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.CloudWeave
{
    public static class RepositoryClient
    {
        private const string RegistryHome = "http://upm.gs2.io/npm";
        
        public static IEnumerator List(UnityAction<List<Package>> callback)
        {
            PackageSubsets packageSubsets = null;
            {
                var request = UnityWebRequest.Get(RegistryHome);
                yield return request.SendWebRequest();
                if (request.isHttpError)
                {
                    Debug.LogError(request.error);
                    yield break;
                }
                var text = request.downloadHandler.text;
                var data = JsonMapper.ToObject(text);
                packageSubsets = PackageSubsets.FromDict(data);
            }

            var sequence = new CoroutineSequence();
            
            var packages = new List<Package>();
            foreach (var package in packageSubsets.packages)
            {
                sequence.Insert(0f, GetPackageInformation(package, packages));
            }
            
            new CoroutineSequence()
                .Append(sequence)
                .OnCompleted(() =>
                {
                    packages.Sort((package1, package2) => string.CompareOrdinal(package1.name, package2.name));
                    callback.Invoke(packages);
                })
                .Play();
        }

        private static IEnumerator GetPackageInformation(PackageSubset package, List<Package> packages)
        {
            var request = UnityWebRequest.Get(RegistryHome + "/" + package.name);
            yield return request.SendWebRequest();
            if (request.isHttpError) {
                Debug.LogError(request.error);
                yield break;
            }
            var data = JsonMapper.ToObject(request.downloadHandler.text);
            var version = data["dist-tags"]["latest"].ToString();

            var request2 = UnityWebRequest.Get(RegistryHome + "/" + package.name + "/" + version);
            yield return request2.SendWebRequest();
            if (request2.isHttpError) {
                Debug.LogError(request2.error);
                yield break;
            }
            var data2 = JsonMapper.ToObject(request2.downloadHandler.text);
            packages.Add(Package.FromDict(data2));
        }
    }
}