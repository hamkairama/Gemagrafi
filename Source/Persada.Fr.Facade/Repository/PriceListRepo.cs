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
    public class PriceListRepo : ApiResData, IPriceList
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public PriceListRepo()
        {
            _ctx = new DbCtx();
        }

        public List<GEMA_TM_PRICEL_LIST> GridBind()
        {
            try
            {
                return _ctx.GEMA_TM_PRICEL_LIST.Where(x=>x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GEMA_TM_PRICEL_LIST Retrieve(int id)
        {
            try
            {
                return _ctx.GEMA_TM_PRICEL_LIST.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(GEMA_TM_PRICEL_LIST priceList)
        {
            try
            {
                _ctx.GEMA_TM_PRICEL_LIST.Add(priceList);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
                
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(GEMA_TM_PRICEL_LIST priceList)
        {
            try
            {
                GEMA_TM_PRICEL_LIST priceListNew = _ctx.GEMA_TM_PRICEL_LIST.Find(priceList.ID);
                priceListNew.TYPE = priceList.TYPE;
                priceListNew.DESCRIPTION = priceList.DESCRIPTION;
                priceListNew.PRICE = priceList.PRICE;
                priceListNew.IMAGE_BIT = priceList.IMAGE_BIT;
                priceListNew.IMAGE_PATH = priceList.IMAGE_PATH;
                priceListNew.LAST_MODIFIED_TIME = priceList.LAST_MODIFIED_TIME;
                priceListNew.LAST_MODIFIED_BY = priceList.LAST_MODIFIED_BY;
                _ctx.Entry(priceListNew).State = System.Data.Entity.EntityState.Modified;
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
                GEMA_TM_PRICEL_LIST priceList = _ctx.GEMA_TM_PRICEL_LIST.Find(id);
                priceList.LAST_MODIFIED_TIME = modifiedTime;
                priceList.LAST_MODIFIED_BY = modifiedBy;
                priceList.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(priceList).State = EntityState.Modified;
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

    }
}
