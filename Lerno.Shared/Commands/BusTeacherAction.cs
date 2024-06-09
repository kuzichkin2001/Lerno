namespace Lerno.Shared.Commands
{
    public class BusTeacherAction : BusAction
    {
        // Create
        public const string CreateTeacher = "teacher.create";
        public const string CreateTeachers = "teachers.create";

        // Read
        public const string GetTeacher = "teacher.get";
        public const string GetTeacherFiltered = "teacher.get.filtered";
        public const string GetTeachers = "teachers.get";
        public const string GetTeachersFiltered = "teachers.get.filtered";

        // Update
        public const string UpdateTeacher = "teacher.update";
        
        // Delete
        public const string DeleteTeacher = "teacher.delete";
        public const string DeleteTeachers = "teachers.delete";
    }
}
