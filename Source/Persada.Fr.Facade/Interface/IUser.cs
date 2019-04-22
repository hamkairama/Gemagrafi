using Newtonsoft.Json.Linq;
using Persada.Fr.CommonFunction;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.Facade.Interface
{
    public interface IUser
    {
        List<GEMA_TM_USER> GridBind();
        GEMA_TM_USER Retrieve(int id);
        ResultStatus Add(GEMA_TM_USER user, GEMA_TM_USER_PROFILE userProfile, List<GEMA_TM_USER_PROFILE_SOSMED> userProfileSosmed);
        ResultStatus Edit(GEMA_TM_USER user, GEMA_TM_USER_PROFILE userProfile, List<GEMA_TM_USER_PROFILE_SOSMED> userProfileSosmed);
        ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime);
        GEMA_TM_USER Login(string email, string password);
        ResultStatus ChangePassword(int id, string oldPassword, string newPassword);
    }
}
