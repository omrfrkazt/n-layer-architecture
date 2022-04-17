using Core.EntityFramework;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.User
{
    public class UserDal : EfRepositoryBase<GenUser, AppDbContext>, IUserDal
    {
    }
}
