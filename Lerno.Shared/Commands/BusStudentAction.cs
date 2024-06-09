namespace Lerno.Shared.Commands
{
    public class BusStudentAction : BusAction
    {
        // Create
        public const string CreateStudent = "student.create";
        public const string CreateStudents = "students.create";

        // Read
        public const string GetStudent = "student.get";
        public const string GetStudentFiltered = "student.get.filtered";
        public const string GetStudents = "students.get";
        public const string GetStudentsFiltered = "students.get.filtered";

        // Update
        public const string UpdateStudent = "student.update";

        // Delete
        public const string DeleteStudent = "student.delete";
        public const string DeleteStudents = "students.delete";
    }
}
