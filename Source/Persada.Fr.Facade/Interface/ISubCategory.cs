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
    public interface ISubCategory
    {
        List<GEMA_TM_SUB_CATEGORY> GridBind();
        List<GEMA_TM_SUB_CATEGORY> GridBindTop5();
        GEMA_TM_SUB_CATEGORY Retrieve(int id);
        ResultStatus Add(GEMA_TM_SUB_CATEGORY subCategory);
        ResultStatus Edit(GEMA_TM_SUB_CATEGORY subCategory);
        ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime);
        List<GEMA_TM_SUB_CATEGORY> GetSubCategoryByCategoryId(int categoryId);
        SelectList GetCategoryList();
    }
}
