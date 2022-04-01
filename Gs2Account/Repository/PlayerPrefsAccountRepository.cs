using Gs2.Gs2Account.Model;
using Gs2.Unity.Gs2Account.Model;
using Gs2.Util.LitJson;
using Gs2.Util.WebSocketSharp;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Account
{
    [AddComponentMenu("GS2 UIKit/Account/PlayerPrefsAccountRepository")]
    public class PlayerPrefsAccountRepository : AbstractAccountRepository
    {
        public string playerPrefsKey = "Gs2.Account";
        
        public override EzAccount Load()
        {
            var account = PlayerPrefs.GetString(playerPrefsKey);
            if (account.IsNullOrEmpty())
            {
                return null;
            }
            return EzAccount.FromModel(Account.FromJson(JsonMapper.ToObject(account)));
        }

        public override void Save(EzAccount account)
        {
            PlayerPrefs.SetString(playerPrefsKey, account.ToModel().ToJson().ToJson());
        }

        public override void Delete()
        {
            PlayerPrefs.DeleteKey(playerPrefsKey);
        }
    }
}