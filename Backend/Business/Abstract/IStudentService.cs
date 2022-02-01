using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IStudentService : IServiceRepository<Student, int>
    {
        IDataResult<Student> UpdatePoint(int point);
        IDataResult<Student> GetByEmail(string email);
        IDataResult<List<Student>> GetByPointForLeaderBord();
        Student GetStudent();
    }
}