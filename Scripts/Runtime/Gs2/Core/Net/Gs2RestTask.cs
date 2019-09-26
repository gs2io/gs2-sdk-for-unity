using System.Collections;
using UnityEngine.Networking;

namespace Gs2.Core.Net
{
    public abstract class Gs2RestTask
    {
        public UnityWebRequest UnityWebRequest { get; } = new UnityWebRequest();

        public Gs2RestTask()
        {
        }

        public IEnumerator Send()
        {
            UnityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return UnityWebRequest.SendWebRequest();

            Callback(
                new Gs2RestResponse(
                    !UnityWebRequest.isNetworkError || UnityWebRequest.isHttpError ? UnityWebRequest.downloadHandler.text : UnityWebRequest.error,
                    UnityWebRequest.responseCode
                )
            );
        }

        public abstract void Callback(Gs2RestResponse gs2RestResponse);
    }
}