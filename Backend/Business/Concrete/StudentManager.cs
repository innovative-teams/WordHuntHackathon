using Business.Abstract;
using Core.Business;
using Core.Utilities.Exceptions;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{

    public class StudentManager : BusinessService, IStudentService
    {
        private readonly IStudentDal _studentDal;

        public StudentManager(IStudentDal studentDal)
        {
            _studentDal = studentDal;
        }

        public IResult Add(Student entity)
        {
            IResult result = BusinessRules.Run();

            if (!result.Success)
                return result;

            _studentDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(DeleteModel entity)
        {
            IResult result = BusinessRules.Run();

            if (!result.Success)
                return result;

            Student entityToDelete = GetById(entity.ID).Data;
            _studentDal.Delete(entityToDelete);
            return new SuccessResult();
        }

        public IResult Update(Student entity)
        {
            IResult result = BusinessRules.Run();

            if (!result.Success)
                return result;

            _studentDal.Update(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Student>> GetAll()
        {
            return new SuccessDataResult<List<Student>>(_studentDal.GetAll());
        }

        public IDataResult<Student> GetById(int id)
        {
            return new SuccessDataResult<Student>(_studentDal.Get(e => e.UserId == id));
        }

        public IDataResult<List<Student>> GetByPointForLeaderBord()
        {
            return new SuccessDataResult<List<Student>>(_studentDal.GetAll().OrderBy(s => -s.Point).ToList());
        }
        public IDataResult<Student> GetByEmail(string email)
        {
            return new SuccessDataResult<Student>(_studentDal.Get(s => s.Email == email));
        }

        public IDataResult<Student> UpdatePoint(int point)
        {
            var student = GetStudent();
            student.Point += point;

            var operation = Update(student);
            if (!operation.Success)
                return new ErrorDataResult<Student>(operation.Message);

            return new SuccessDataResult<Student>(student, operation.Message);
        }

        public Student GetStudent()
        {
            var requestUser = RequestUserService.GetRequestUser().Data;
            var student = GetById(requestUser.Id).Data;

            if (student.UserId == 0)
                throw new LoginRequiredException(CoreMessages.LoginRequired(), "");

            return student;
        }
    }
}
