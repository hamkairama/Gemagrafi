using Newtonsoft.Json.Linq;
using Persada.Fr.CommonFunction;
using Persada.Fr.Facade.Interface;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using Persada.Fr.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Persada.Fr.Facade.Repository
{
    public class SubCategoryRepo : ApiResData, ISubCategory
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public SubCategoryRepo()
        {
            _ctx = new DbCtx();
        }

        public List<GEMA_TM_SUB_CATEGORY> GridBind()
        {
            try
            {
                return _ctx.GEMA_TM_SUB_CATEGORY.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<GEMA_TM_SUB_CATEGORY> GridBindTop5()
        {
            try
            {
                List<GEMA_TM_SUB_CATEGORY> subCategoryListTop5 = new List<GEMA_TM_SUB_CATEGORY>();
                int totalRandom = 5;
                List<GEMA_TM_SUB_CATEGORY> subCategoryList = _ctx.GEMA_TM_SUB_CATEGORY.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
                if (subCategoryList.Count() < 5 )
                {
                    totalRandom = subCategoryList.Count();
                }
                for (int i = 0; i < totalRandom; i++)
                {
                    subCategoryListTop5.Add(subCategoryList[i]);
                }

                return subCategoryListTop5;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GEMA_TM_SUB_CATEGORY Retrieve(int id)
        {
            try
            {
                return _ctx.GEMA_TM_SUB_CATEGORY.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(GEMA_TM_SUB_CATEGORY subCategory)
        {
            try
            {
                _ctx.GEMA_TM_SUB_CATEGORY.Add(subCategory);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();

            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(GEMA_TM_SUB_CATEGORY subCategory)
        {
            try
            {
                GEMA_TM_SUB_CATEGORY subCategoryNew = _ctx.GEMA_TM_SUB_CATEGORY.Find(subCategory.ID);
                subCategoryNew.CATEGORY_ID = subCategory.CATEGORY_ID;
                subCategoryNew.SUB_CATEGORY_NAME = subCategory.SUB_CATEGORY_NAME;
                subCategoryNew.DESCRIPTION = subCategory.DESCRIPTION;
                subCategoryNew.IMAGE_PATH = subCategory.IMAGE_PATH;
                subCategoryNew.LAST_MODIFIED_TIME = subCategory.LAST_MODIFIED_TIME;
                subCategoryNew.LAST_MODIFIED_BY = subCategory.LAST_MODIFIED_BY;
                _ctx.Entry(subCategoryNew).State = System.Data.Entity.EntityState.Modified;
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime)
        {
            try
            {
                GEMA_TM_SUB_CATEGORY subCategory = _ctx.GEMA_TM_SUB_CATEGORY.Find(id);
                subCategory.LAST_MODIFIED_TIME = modifiedTime;
                subCategory.LAST_MODIFIED_BY = modifiedBy;
                subCategory.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(subCategory).State = EntityState.Modified;
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public List<GEMA_TM_SUB_CATEGORY> GetSubCategoryByCategoryId(int categoryId)
        {
            try
            {
                return _ctx.GEMA_TM_SUB_CATEGORY.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active && x.CATEGORY_ID == categoryId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public SelectList GetCategoryList()
        {
            List<GEMA_TM_CATEGORY> categories = _ctx.GEMA_TM_CATEGORY.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            GEMA_TM_CATEGORY category = new GEMA_TM_CATEGORY { ID = 0, CATEGORY_NAME = "Select Category :" };

            categories.Add(category);
            var categoryList = categories.OrderBy(x => x.ID);

            SelectList selectList = new SelectList(categoryList, "ID", "CATEGORY_NAME");
            return selectList;
        }
    }
}
