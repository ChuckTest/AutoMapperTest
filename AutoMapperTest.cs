using System;
using AutoMapper;
using Newtonsoft.Json;
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
                .CreateMap<StudentDto, Student>(MemberList.Destination)
                .ForMember(x => x.CreatedOn,
                    opt => opt.MapFrom(source =>
                        Convert.ToDateTime(source.Birthday)))
                .ForMember(x => x.Teacher,
                    opt => opt.MapFrom(source =>
                        JsonConvert.SerializeObject(source.Teacher))); //Check that all destination members are mapped
        }

        [Test]
        public void Test()
        {
            try
            {
                var studentDto = GetStudentDto();

                //the backend map StudentDto to database entity Student
                //and set the value of some property
                var student = _mapper.Map<Student>(studentDto);
                Assert.AreEqual(studentDto.IdentityId, student.IdentityId);
                Assert.AreEqual(studentDto.Name, student.Name);
                Assert.AreEqual(studentDto.Birthday, student.Birthday.ToString("yyyy-MM-dd"));
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

        /// <summary>
        /// frontend pass a dto to backend
        /// </summary>
        /// <returns></returns>
        private StudentDto GetStudentDto()
        {
            var birthday = new DateTime(1989, 12, 30);
            var teacher = GetTeacher();
            StudentDto studentDto = new StudentDto
            {
                IdentityId = "320481198912305142",
                Name = "Chuck",
                Birthday = birthday.ToString("yyyy-MM-dd"),
                Teacher = teacher
            };
            return studentDto;
        }

        private Teacher GetTeacher()
        {
            Teacher teacher = new Teacher();
            teacher.IdentityId = "";
            teacher.Name = "Joan";
            teacher.Birthday = new DateTime(1980, 1, 1);
            teacher.Course = "Chinese";
            return teacher;
        }

        [Test]
        public void UpdateExistingInstance()
        {
            //student dto update student
            Student student = new Student();
            student.Guid = Guid.NewGuid();
            Console.WriteLine(student.Guid);

            StudentDto studentDto = new StudentDto();
            studentDto.IdentityId = "20200924-001";

            _mapper.Map(studentDto, student);
            Assert.AreEqual(studentDto.IdentityId, student.IdentityId);
        }
    }
}
