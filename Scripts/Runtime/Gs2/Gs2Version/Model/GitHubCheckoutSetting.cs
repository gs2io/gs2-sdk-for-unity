/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Version.Model
{
	[Preserve]
	public class GitHubCheckoutSetting
	{

        /** リソースの取得に使用するGitHub のAPIキー のGRN */
        public string gitHubApiKeyId { set; get; }

        /**
         * リソースの取得に使用するGitHub のAPIキー のGRNを設定
         *
         * @param gitHubApiKeyId リソースの取得に使用するGitHub のAPIキー のGRN
         * @return this
         */
        public GitHubCheckoutSetting WithGitHubApiKeyId(string gitHubApiKeyId) {
            this.gitHubApiKeyId = gitHubApiKeyId;
            return this;
        }

        /** リポジトリ名 */
        public string repositoryName { set; get; }

        /**
         * リポジトリ名を設定
         *
         * @param repositoryName リポジトリ名
         * @return this
         */
        public GitHubCheckoutSetting WithRepositoryName(string repositoryName) {
            this.repositoryName = repositoryName;
            return this;
        }

        /** ソースコードのファイルパス */
        public string sourcePath { set; get; }

        /**
         * ソースコードのファイルパスを設定
         *
         * @param sourcePath ソースコードのファイルパス
         * @return this
         */
        public GitHubCheckoutSetting WithSourcePath(string sourcePath) {
            this.sourcePath = sourcePath;
            return this;
        }

        /** コードの取得元 */
        public string referenceType { set; get; }

        /**
         * コードの取得元を設定
         *
         * @param referenceType コードの取得元
         * @return this
         */
        public GitHubCheckoutSetting WithReferenceType(string referenceType) {
            this.referenceType = referenceType;
            return this;
        }

        /** コミットハッシュ */
        public string commitHash { set; get; }

        /**
         * コミットハッシュを設定
         *
         * @param commitHash コミットハッシュ
         * @return this
         */
        public GitHubCheckoutSetting WithCommitHash(string commitHash) {
            this.commitHash = commitHash;
            return this;
        }

        /** ブランチ名 */
        public string branchName { set; get; }

        /**
         * ブランチ名を設定
         *
         * @param branchName ブランチ名
         * @return this
         */
        public GitHubCheckoutSetting WithBranchName(string branchName) {
            this.branchName = branchName;
            return this;
        }

        /** タグ名 */
        public string tagName { set; get; }

        /**
         * タグ名を設定
         *
         * @param tagName タグ名
         * @return this
         */
        public GitHubCheckoutSetting WithTagName(string tagName) {
            this.tagName = tagName;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.gitHubApiKeyId != null)
            {
                writer.WritePropertyName("gitHubApiKeyId");
                writer.Write(this.gitHubApiKeyId);
            }
            if(this.repositoryName != null)
            {
                writer.WritePropertyName("repositoryName");
                writer.Write(this.repositoryName);
            }
            if(this.sourcePath != null)
            {
                writer.WritePropertyName("sourcePath");
                writer.Write(this.sourcePath);
            }
            if(this.referenceType != null)
            {
                writer.WritePropertyName("referenceType");
                writer.Write(this.referenceType);
            }
            if(this.commitHash != null)
            {
                writer.WritePropertyName("commitHash");
                writer.Write(this.commitHash);
            }
            if(this.branchName != null)
            {
                writer.WritePropertyName("branchName");
                writer.Write(this.branchName);
            }
            if(this.tagName != null)
            {
                writer.WritePropertyName("tagName");
                writer.Write(this.tagName);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static GitHubCheckoutSetting FromDict(JsonData data)
        {
            return new GitHubCheckoutSetting()
                .WithGitHubApiKeyId(data.Keys.Contains("gitHubApiKeyId") && data["gitHubApiKeyId"] != null ? data["gitHubApiKeyId"].ToString() : null)
                .WithRepositoryName(data.Keys.Contains("repositoryName") && data["repositoryName"] != null ? data["repositoryName"].ToString() : null)
                .WithSourcePath(data.Keys.Contains("sourcePath") && data["sourcePath"] != null ? data["sourcePath"].ToString() : null)
                .WithReferenceType(data.Keys.Contains("referenceType") && data["referenceType"] != null ? data["referenceType"].ToString() : null)
                .WithCommitHash(data.Keys.Contains("commitHash") && data["commitHash"] != null ? data["commitHash"].ToString() : null)
                .WithBranchName(data.Keys.Contains("branchName") && data["branchName"] != null ? data["branchName"].ToString() : null)
                .WithTagName(data.Keys.Contains("tagName") && data["tagName"] != null ? data["tagName"].ToString() : null);
        }
	}
}