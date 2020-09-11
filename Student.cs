using System;

namespace AutoMapperTest
{
    /// <summary>
    /// map to the table in database
    /// </summary>
    public class Student
    {
        public Guid Guid { get; set; }

        public string IdentityId { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public override string ToString()
        {
            return $"{nameof(IdentityId)} = {IdentityId}, {nameof(Name)} = {Name}, {nameof(Birthday)} = {Birthday:yyyy-MM-dd}";
        }
    }

    /// <summary>
    /// data transfer object(Dto) which used to pass data to backend
    /// </summary>
    public class StudentDto
    {
        public string IdentityId { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }
    }
}
