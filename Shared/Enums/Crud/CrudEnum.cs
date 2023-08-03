using System.ComponentModel;
using System.Runtime.Serialization;

namespace WebAdmin.Shared.Enums
{
    public enum CrudEnum : long
    {
        [EnumMember]
        [Description("inserir")]
        Insert = 1,

        [EnumMember]
        [Description("atualizar")]
        Update = 2,

        [EnumMember]
        [Description("deletar")]
        Delete = 3
    }
}
