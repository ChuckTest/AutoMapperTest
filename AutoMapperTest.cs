using System;
using AutoMapper;
using NUnit.Framework;

namespace AutoMapperTest
{
    public class Person
    {
        public string Gender { get; set; }
    }

    public class Student : Person
    {
        public Student(Person source)
        {
            Mapper.Map(source, this);
        }

        public int Age { get; set; }

        public override string ToString()
        {
            return $"{nameof(Gender)} = {Gender}, {nameof(Age)} = {Age}";
        }
    }

    [TestFixture]
    public class AutoMapperTest
    {
        [SetUp]
        public void Init()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Person, Student>();
            });
        }

        /// <summary>
        /// https://stackoverflow.com/a/12653899
        /// </summary>
        [Test]
        public void Test()
        {
            try
            {
                Person male = new Person();
                male.Gender = "male";

                Student student = new Student(male);
                student.Age = 10;

                Console.WriteLine(student);
            }
            catch (Exception ex)
            {
                while (ex != null)
                {
                    Console.WriteLine(ex);
                    ex = ex.InnerException;
                }
            }
        }
    }
}
