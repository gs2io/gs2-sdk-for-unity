using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Gs2Account.Model;

namespace Gs2.Unity.Util
{
    // UnauthorizedException の派生にしないのは、Gs2Connection.RunAsync が UnauthorizedException を再認証で直せるものとして扱い、拒まれると分かっている Authentication をもう一度呼ぶため。
    // BadRequestException の派生にしているのは、これまで LoginBySignature の入力検証エラー（BadRequestException）を catch していた呼び出し元が、そのまま受けられるようにするため。
    public class AccountAuthenticationRefusedException : BadRequestException
    {
        // 削除の予定日時などを個別のプロパティに写さないのは、Account のモデルにフィールドが増えれば Item からそのまま読めるため。
        public Account Item { get; }
        public BanStatus[] BanStatuses { get; }

        public AccountAuthenticationRefusedException(
            Account item,
            BanStatus[] banStatuses
        ) : base(
            new[] {
                new RequestError {
                    Component = "account",
                    Message = "account.authentication.refused",
                }
            }
        ) {
            Item = item;
            BanStatuses = banStatuses;
        }
    }
}
