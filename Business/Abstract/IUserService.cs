using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByMail(string email);
        //IDataResult<List<OperationClaim>> GetClaims(User user);
        //IResult Add(User user);
        //IDataResult<User> GetByMail(string email);
    }
}
