namespace Lerno.Shared.Commands
{
    public class BusTeacherAction : BusAction
    {
        // Create
        public const string CreateTeacher = "teacher.create";

        // Read
        public const string GetTeacher = "teacher.get";
        public const string GetTeachers = "teachers.get";

        // Update
        public const string UpdateTeacher = "teacher.update";
        
        // Delete
        public const string DeleteTeacher = "teacher.delete";
        public const string DeleteTeachers = "teachers.delete";
    }
}
