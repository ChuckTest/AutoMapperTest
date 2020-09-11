using System;

namespace AutoMapperTest
{
    public class People
    {
        public string IdentityId { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }
    }

    /// <summary>
    /// map to the table in database
    /// </summary>
    public class Student : People
    {
        public Guid Guid { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Teacher { get; set; }

        public override string ToString()
        {
            var reuslt=
                $"{nameof(IdentityId)} = {IdentityId}, {nameof(Name)} = {Name}, {nameof(Birthday)} = {Birthday:yyyy-MM-dd}, {nameof(Teacher)} = {Teacher}";
            return reuslt;
        }
    }

    /// <summary>
    /// data transfer object(Dto) which used to pass data to backend
    /// </summary>
    public class StudentDto
    {
        public string IdentityId { get; set; }

        public string Name { get; set; }

        public string Birthday { get; set; }

        public Teacher Teacher { get; set; }
    }

    public class Teacher : People
    {
        public string Course { get; set; }
    }
}
