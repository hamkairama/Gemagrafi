
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
    public interface ICategory
    {
        List<GEMA_TM_CATEGORY> GridBind();
        GEMA_TM_CATEGORY Retrieve(int id);
        ResultStatus Add(GEMA_TM_CATEGORY category);
        ResultStatus Edit(GEMA_TM_CATEGORY category);
        ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime);
    }
}
