using System;
using System.Collections.Generic;
using System.Text;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            base.Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (base.Students.Count < 5)
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");

            double twentyPercentOfStudents = (base.Students.Count/5);

            List<double> averageGrades = new List<double>();
            foreach(Student student in base.Students)
            {
                if(!averageGrades.Exists((grade) => grade == student.AverageGrade))
                    averageGrades.Add(student.AverageGrade);
            }
            averageGrades.Sort();
            averageGrades.Reverse();
            if (averageGrades.IndexOf(averageGrade) == -1)
                return 'F';
            if (averageGrades.IndexOf(averageGrade) < (twentyPercentOfStudents))
                return 'A';
            else if (averageGrades.IndexOf(averageGrade) < (2*twentyPercentOfStudents))
                return 'B';
            else if (averageGrades.IndexOf(averageGrade) < (3*twentyPercentOfStudents))
                return 'C';
            else if (averageGrades.IndexOf(averageGrade) < (4*twentyPercentOfStudents))
                return 'D';
            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (base.Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (base.Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
