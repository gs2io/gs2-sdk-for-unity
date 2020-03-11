using UnityEngine.Networking;

namespace Gs2.Core.Model
{
    public class ProjectTokenGs2Credential : IGs2Credential
    {
        /** クライアントID */
        public string ClientId { get; set; }
        
        public string ClientSecret { get; set; }

        /** プロジェクトトークン */
        public string ProjectToken  { get; set; }

        public ProjectTokenGs2Credential(string clientId, string projectToken)
        {
            ClientId = clientId;
            ProjectToken = projectToken;
        }

        public void Authorized(UnityWebRequest request)
        {
            request.SetRequestHeader("X-GS2-CLIENT-ID", ClientId);
            if (ProjectToken != null)
            {
                request.SetRequestHeader("Authorization", "Bearer " + ProjectToken);
            }
        }
    }
}