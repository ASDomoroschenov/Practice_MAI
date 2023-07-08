namespace Task1
{

    public class Student :
        IEquatable<Student>
    {
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _group;
        private string _practiceCourse;

        public Student(
            string surname,
            string name,
            string patronymic,
            string group,
            string practiceCourse)
        {
            if (surname == null ||
                name == null ||
                patronymic == null ||
                group == null ||
                practiceCourse == null)
            {
                throw new ArgumentNullException();
            }

            _surname = surname;
            _name = name;
            _patronymic = patronymic;
            _group = group;
            _practiceCourse = practiceCourse;
        }

        public string SurnameStudent
        {
            get
            {
                return _surname;
            }
        }

        public string NameStudent
        {
            get
            {
                return _name;
            }
        }

        public string PatronymicStudent
        {
            get
            {
                return _patronymic;
            }
        }

        public string GroupStudent
        {
            get
            {
                return _group;
            }
        }

        public int CourseStudent
        {
            get
            {
                return _group[4] - '0';
            }
        }

        public string PracticeCourseStudent
        {
            get
            {
                return _practiceCourse;
            }
        }

        public bool Equals(
            Student? obj)
        {
            Console.WriteLine("IEquatable<Student>:");
            
            if (obj == null)
            {
                return false;
            }

            return _surname.Equals(obj.SurnameStudent) &&
                   _name.Equals(obj.NameStudent) &&
                   _patronymic.Equals(obj.PatronymicStudent) &&
                   _group.Equals(obj.GroupStudent) &&
                   _practiceCourse.Equals(obj.PracticeCourseStudent);
        }

        public override bool Equals(
            object? obj)
        {
            Console.WriteLine("Base.Equals(object):");
            
            if (obj == null)
            {
                return false;
            }

            if (obj is Student ObjectStudent)
            {
                return _surname.Equals(ObjectStudent.SurnameStudent) && 
                       _name.Equals(ObjectStudent.NameStudent) &&
                       _patronymic.Equals(ObjectStudent.PatronymicStudent) &&
                       _group.Equals(ObjectStudent.GroupStudent) &&
                       _practiceCourse.Equals(ObjectStudent.PracticeCourseStudent);
            }

            return false;
        }

        public override string ToString()
        {
            return $"{_surname} {_name} {_patronymic} {_group} {_practiceCourse}";
        }

        public override int GetHashCode()
        {
            return _surname.GetHashCode() +
                   _name.GetHashCode() +
                   _patronymic.GetHashCode() +
                   _group.GetHashCode() +
                   _practiceCourse.GetHashCode();
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            var AlexandrStudent = new Student(
                "Domoroschenov",
                "Alexandr",
                "Sergeevich",
                "М8О-213Б",
                "C#");
            var IlyaStudent = new Student(
                "Irbitskiy",
                "Ilya",
                "Sergeevich",
                "М8О-213Б",
                "C#");
            
            Console.WriteLine(AlexandrStudent.ToString());
            Console.WriteLine(IlyaStudent.ToString());
            
            Console.WriteLine(AlexandrStudent.Equals(IlyaStudent));
            Console.WriteLine(AlexandrStudent.Equals((object)IlyaStudent));
        }

    }

}