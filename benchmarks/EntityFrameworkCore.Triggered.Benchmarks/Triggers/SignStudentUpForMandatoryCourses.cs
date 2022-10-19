﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Triggered.Benchmarks.Triggers
{
    public class SignStudentUpForMandatoryCourses : IBeforeSaveTrigger<Student>
    {
        readonly TriggeredApplicationContext _applicationContext;

        public SignStudentUpForMandatoryCourses(TriggeredApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void BeforeSave(ITriggerContext<Student> context)
        {
            var mandatoryCourses = _applicationContext.Courses.Where(x => x.IsMandatory).ToList();

            foreach (var mandatoryCourse in mandatoryCourses)
            {
                _applicationContext.StudentCourses.Add(new StudentCourse {
                    CourseId = mandatoryCourse.Id,
                    StudentId = context.Entity.Id
                });
            }
        }
    }
}
