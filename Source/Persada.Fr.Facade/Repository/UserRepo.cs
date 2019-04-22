using Newtonsoft.Json.Linq;
using Persada.Fr.CommonFunction;
using Persada.Fr.Facade.Interface;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using Persada.Fr.ModelView;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.Facade.Repository
{
    public class UserRepo : ApiResData, IUser
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public UserRepo()
        {
            _ctx = new DbCtx();
        }

        public List<GEMA_TM_USER> GridBind()
        {
            try
            {
                return _ctx.GEMA_TM_USER.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GEMA_TM_USER Retrieve(int id)
        {
            try
            {
                return _ctx.GEMA_TM_USER.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(GEMA_TM_USER user, GEMA_TM_USER_PROFILE userProfile, List<GEMA_TM_USER_PROFILE_SOSMED> userProfileSosmed)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    int userIdId = 0, userProfileId = 0;
                    rs = AddUser(user, ref userIdId);

                    if (rs.IsSuccess && userProfile.ADDRESS != "")
                    {
                        userProfile.USER_ID_ID = userIdId;
                        rs = AddUserProfile(userProfile, ref userProfileId);
                    }

                    if (rs.IsSuccess && userProfileSosmed.Count > 0)
                    {
                        List<GEMA_TM_USER_PROFILE_SOSMED> userProfileSosmedNew = new List<GEMA_TM_USER_PROFILE_SOSMED>();
                        foreach (var item in userProfileSosmed)
                        {
                            item.USER_PROFILE_ID = userProfileId;
                            userProfileSosmedNew.Add(item);
                        }
                        rs = AddUserProfileSosmed(userProfileSosmedNew);
                    }
                    transaction.Commit();
                    rs.SetSuccessStatus();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    rs.SetErrorStatus(ex.Message);
                }
            }
            return rs;
        }

        public ResultStatus Edit(GEMA_TM_USER user, GEMA_TM_USER_PROFILE userProfile, List<GEMA_TM_USER_PROFILE_SOSMED> userProfileSosmed)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    int userProfileId = 0;
                    rs = EditUser(user, ref userProfileId);

                    if (rs.IsSuccess && userProfile.ADDRESS != "")
                    {
                        userProfile.ID = userProfileId;
                        rs = EditUserProfile(userProfile);
                    }

                    if (rs.IsSuccess && userProfileSosmed.Count > 0)
                    {
                        List<GEMA_TM_USER_PROFILE_SOSMED> userProfileSosmedNew = new List<GEMA_TM_USER_PROFILE_SOSMED>();
                        foreach (var item in userProfileSosmed)
                        {
                            item.USER_PROFILE_ID = userProfileId;
                            userProfileSosmedNew.Add(item);
                        }
                        rs = AddUserProfileSosmed(userProfileSosmedNew);
                    }
                    transaction.Commit();
                    rs.SetSuccessStatus();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    rs.SetErrorStatus(ex.Message);
                }
            }


            return rs;
        }

        public ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime)
        {
            try
            {
                GEMA_TM_USER user = _ctx.GEMA_TM_USER.Find(id);
                user.LAST_MODIFIED_TIME = modifiedTime;
                user.LAST_MODIFIED_BY = modifiedBy;
                user.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(user).State = EntityState.Modified;
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        private ResultStatus AddUser(GEMA_TM_USER user, ref int userIdId)
        {
            _ctx.GEMA_TM_USER.Add(user);
            _ctx.SaveChanges();

            userIdId = _ctx.GEMA_TM_USER.Where(x => x.USER_MAIL == user.USER_MAIL).FirstOrDefault().ID;
            rs.SetSuccessStatus();

            return rs;
        }

        private ResultStatus AddUserProfile(GEMA_TM_USER_PROFILE userProfile, ref int userProfileId)
        {
            _ctx.GEMA_TM_USER_PROFILE.Add(userProfile);
            _ctx.SaveChanges();

            userProfileId = _ctx.GEMA_TM_USER_PROFILE.Where(x => x.USER_ID_ID == userProfile.USER_ID_ID).FirstOrDefault().ID;
            rs.SetSuccessStatus();

            return rs;
        }

        private ResultStatus AddUserProfileSosmed(List<GEMA_TM_USER_PROFILE_SOSMED> userProfileSosmed)
        {
            foreach (var item in userProfileSosmed)
            {
                _ctx.GEMA_TM_USER_PROFILE_SOSMED.Add(item);
                _ctx.SaveChanges();
            }

            rs.SetSuccessStatus();

            return rs;
        }

        private ResultStatus EditUser(GEMA_TM_USER user, ref int userProfileId)
        {
            GEMA_TM_USER userNew = _ctx.GEMA_TM_USER.Find(user.ID);
            userNew.USER_ID = user.USER_ID;
            userNew.USER_NAME = user.USER_NAME;
            userNew.USER_MAIL = user.USER_MAIL;
            userNew.IS_SUPER_ADMIN = user.IS_SUPER_ADMIN;
            userNew.LAST_MODIFIED_BY = user.LAST_MODIFIED_BY;
            userNew.LAST_MODIFIED_TIME = user.LAST_MODIFIED_TIME;

            userProfileId = userNew.GEMA_TM_USER_PROFILE.FirstOrDefault().ID;

            _ctx.Entry(userNew).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            rs.SetSuccessStatus();

            return rs;
        }

        private ResultStatus EditUserProfile(GEMA_TM_USER_PROFILE userProfile)
        {
            GEMA_TM_USER_PROFILE userProfileNew = _ctx.GEMA_TM_USER_PROFILE.Find(userProfile.ID);
            userProfileNew.ADDRESS = userProfile.ADDRESS;
            userProfileNew.BORN = userProfile.BORN;
            userProfileNew.DESCRIPTION = userProfile.DESCRIPTION;
            userProfileNew.GENDER = userProfile.GENDER;
            userProfileNew.COMPANY = userProfile.COMPANY;
            if (userProfile.PHOTO_PATH != null)
            {
                userProfileNew.PHOTO_PATH = userProfile.PHOTO_PATH;
            }
            userProfileNew.HOBBY = userProfile.HOBBY;
            userProfileNew.LAST_MODIFIED_BY = userProfile.LAST_MODIFIED_BY;
            userProfileNew.LAST_MODIFIED_TIME = userProfile.LAST_MODIFIED_TIME;

            _ctx.Entry(userProfileNew).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            rs.SetSuccessStatus();

            return rs;
        }

        private ResultStatus EditUserProfileSosmed(List<GEMA_TM_USER_PROFILE_SOSMED> userProfileSosmed)
        {
            foreach (var item in userProfileSosmed)
            {
                rs = DeleteUserProfileSosmed(item);
            }

            if (rs.IsSuccess)
            {
                rs = AddUserProfileSosmed(userProfileSosmed);
            }

            return rs;
        }

        private ResultStatus DeleteUserProfileSosmed(GEMA_TM_USER_PROFILE_SOSMED userProfileSosmed)
        {
            GEMA_TM_USER_PROFILE_SOSMED userProfileSosmedDel = _ctx.GEMA_TM_USER_PROFILE_SOSMED.Find(userProfileSosmed.ID);

            _ctx.GEMA_TM_USER_PROFILE_SOSMED.Remove(userProfileSosmedDel);
            _ctx.SaveChanges();

            rs.SetSuccessStatus();

            return rs;
        }

        public GEMA_TM_USER Login(string email, string password)
        {
            GEMA_TM_USER user = new GEMA_TM_USER();
            try
            {
                user = _ctx.GEMA_TM_USER.Where(x => x.USER_MAIL == email && x.PASSWORD == password).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            return user;
        }

        public ResultStatus ChangePassword(int id, string oldPassword, string newPassword)
        {
            try
            {
                GEMA_TM_USER user = _ctx.GEMA_TM_USER.Find(id);

                if (user.PASSWORD == oldPassword)
                {
                    user.LAST_MODIFIED_TIME = DateTime.Now;
                    user.LAST_MODIFIED_BY = user.USER_ID;
                    user.PASSWORD = newPassword;

                    _ctx.Entry(user).State = EntityState.Modified;
                    _ctx.SaveChanges();
                    rs.SetSuccessStatus("Change password has been success");
                }
                else
                {
                    rs.SetErrorStatus("Old password is not match");
                }

            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }
    }
}
