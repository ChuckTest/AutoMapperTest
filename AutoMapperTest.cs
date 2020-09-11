using System;
using AutoMapper;
using NUnit.Framework;

namespace AutoMapperTest
{

    [TestFixture]
    public class AutoMapperTest
    {
        private IMapper _mapper;

        [SetUp]
        public void Init()
        {
            var config = new MapperConfiguration(CreateMap);
            _mapper = config.CreateMapper();
        }

        private void CreateMap(IMapperConfigurationExpression mapperConfigurationExpression)
        {
            mapperConfigurationExpression
                .CreateMap<StudentDto, Student>(MemberList.Destination); //Check that all destination members are mapped
        }

        [Test]
        public void Test()
        {
            try
            {
                //frontend pass a dto to backend
                StudentDto studentDto = new StudentDto
                {
                    IdentityId = "320481198912305142",
                    Name = "Chuck",
                    Birthday = "1989-12-30"
                };

                //the backend map StudentDto to database entity Student
                //and set the value of some property
                var student = _mapper.Map<Student>(studentDto);
                Assert.AreEqual(studentDto.IdentityId, student.IdentityId);
                Assert.AreEqual(studentDto.Name, student.Name);
                Assert.AreEqual(studentDto.Birthday, student.Birthday);
                Console.WriteLine(student);

                student.Guid = Guid.NewGuid();
                student.CreatedBy = "admin";
                student.CreatedOn = DateTime.UtcNow;

                //then pass the student to database operation helper
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
