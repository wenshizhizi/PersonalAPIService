


namespace PersonalAPIService.Logic
{
    using Entity;
    using System;

    public class LoginLogic
    {
        public SystemUser Login(dynamic user)
        {
            var suser = new SystemUser {
                UserName = user.name,
                Password = user.password
            };

            return DataAccess.MongodbHelper.FindSystemUser(suser);            
        }
    }
}
