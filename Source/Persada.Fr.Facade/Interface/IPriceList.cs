
using Newtonsoft.Json.Linq;
using Persada.Fr.CommonFunction;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Persada.Fr.Facade.Interface
{
    public interface IPriceList
    {
        List<GEMA_TM_PRICEL_LIST> GridBind();
        GEMA_TM_PRICEL_LIST Retrieve(int id);
        ResultStatus Add(GEMA_TM_PRICEL_LIST category);
        ResultStatus Edit(GEMA_TM_PRICEL_LIST category);
        ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime);
    }
}
