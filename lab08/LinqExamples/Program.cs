using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExamples
{


    public class Department
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public Department(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return $"{Id,2}), {Name,11}";
        }

    }

    public enum Gender
    {
        Female,
        Male
    }

    public class StudentWithTopics
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender {get;set; }
        public bool Active { get; set; }
        public int  DepartmentId { get; set; }

        public List<string> Topics { get; set; }
        public StudentWithTopics(int id, int index, string name, Gender gender,bool active, 
            int departmentId,List<string> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.Topics = topics;
        }

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }
    }

    public static class Generator
    {
        public static int[] GenerateIntsEasy()
        {
            return new int[] { 5, 3, 9, 7, 1, 2, 6, 7, 8 };
        }

        public static int[] GenerateIntsMany()
        {
            var result = new int[10000];
            Random random = new Random();
            for (int i = 0; i < result.Length; i++)
                result[i] = random.Next(1000);
            return result;
        }

        public static List<string> GenerateNamesEasy()
        {
            return new List<string>() {
                "Nowak",
                "Kowalski",
                "Schmidt",
                "Newman",
                "Bandingo",
                "Miniwiliger"
            };
        }
        public static List<StudentWithTopics> GenerateStudentsWithTopicsEasy()
        {
            return new List<StudentWithTopics>() {
            new StudentWithTopics(1,12345,"Nowak", Gender.Female,true,1,
                    new List<string>{"C#","PHP","algorithms"}),
            new StudentWithTopics(2, 13235, "Kowalski", Gender.Male, true,1,
                    new List<string>{"C#","C++","fuzzy logic"}),
            new StudentWithTopics(3, 13444, "Schmidt", Gender.Male, false,2,
                    new List<string>{"Basic","Java"}),
            new StudentWithTopics(4, 14000, "Newman", Gender.Female, false,3,
                    new List<string>{"JavaScript","neural networks"}),
            new StudentWithTopics(5, 14001, "Bandingo", Gender.Male, true,3,
                    new List<string>{"Java","C#"}),
            new StudentWithTopics(6, 14100, "Miniwiliger", Gender.Male, true,2,
                    new List<string>{"algorithms","web programming"}),
            new StudentWithTopics(11,22345,"Nowak", Gender.Female,true,2,
                    new List<string>{"C#","PHP","algorithms"}),
            new StudentWithTopics(12, 23235, "Nowak", Gender.Male, true,1,
                    new List<string>{"C#","C++","fuzzy logic"}),
            new StudentWithTopics(13, 23444, "Schmidt", Gender.Male, false,1,
                    new List<string>{"Basic","Java"}),
            new StudentWithTopics(14, 24000, "Newman", Gender.Female, false,1,
                    new List<string>{"JavaScript","neural networks"}),
            new StudentWithTopics(15, 24001, "Bandingo", Gender.Male, true,3,
                    new List<string>{"Java","C#"}),
            new StudentWithTopics(16, 24100, "Bandingo", Gender.Male, true,2,
                    new List<string>{"algorithms","web programming"}),
            };
        }

        public static List<Department> GenerateDepartmentsEasy()
        {
            return new List<Department>() {
            new Department(1,"Computer Science"),
            new Department(2,"Electronics"),
            new Department(3,"Mathematics"),
            new Department(4,"Mechanics")
            };
        }

    }

    // zad 3
    class Topic {
        public string Name { get; set; }
        public int Id { get; set; }

        public Topic(string name, int id) {
            Name = name;
            Id = id;
        }
    }

    class Student {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender {get;set; }
        public bool Active { get; set; }
        public int  DepartmentId { get; set; }

        public List<int> Topics { get; set; }
        public Student(int id, int index, string name, Gender gender,bool active, 
            int departmentId,List<int> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.Topics = topics;
        }

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }        
    }
    class Program
    {

        static void Main()
        {
            // var studs = Generator.GenerateStudentsWithTopicsEasy();
            // GroupStudents(studs, 3);
            // System.Console.WriteLine();
            // GroupStudents(studs, 5);

            var topics = Generator.GenerateStudentsWithTopicsEasy();
            SortTopicsGenderless(topics);
            System.Console.WriteLine();
            SortTopicsGender(topics);

            // List<Topic> uniqueTopics = GenerateTopics();
            // var studs = Generator.GenerateStudentsWithTopicsEasy();
            // List<Student> newStuds = SplitStudentsAndTopics(studs, uniqueTopics);
            // foreach (var stud in newStuds) {
            //     System.Console.WriteLine(stud);
            // }

            // var studs = Generator.GenerateStudentsWithTopicsEasy();
            // var newTopics = GenerateTopicsFromStudents(studs);
            // foreach (var topic in newTopics) {
            //     System.Console.WriteLine($"{topic.Id} - {topic.Name}");
            // }
        }

        // zad 1
        static void GroupStudents(List<StudentWithTopics> students, int n) {
            
            if (n <= 0) {
                return;
            }
            
            var result = students.Select(x => new { Name = x.Name, StudID = x.Index })
                                .OrderBy(x => x.Name).ThenBy(x => x.StudID)
                                .Select((x, i) => new { Index = i, Name = x.Name, StudID = x.StudID })
                                .GroupBy(x => x.Index / n);

            foreach (var res in result) {
                System.Console.WriteLine($"Grupa {res.Key}:");
                res.ToList().ForEach(s => System.Console.WriteLine($" {s.Name} {s.StudID}"));
            }
        }

        // zad 2a
        static void SortTopicsGenderless(List<StudentWithTopics> students) {
            var result = students.SelectMany(s => s.Topics)
                                .GroupBy(row => row)
                                .Select(group => new { Topic = group.Key, Count = group.Count() })
                                .OrderByDescending(g => g.Count);

            foreach (var res in result) {
                System.Console.WriteLine($"{res.Topic} - {res.Count}");
            }
        }

        // zad 2b
        static void SortTopicsGender(List<StudentWithTopics> students) {
            var result = students.SelectMany(s => s.Topics.Select(t => new { Topic=t, Gender=s.Gender} ))
                                .GroupBy(record => new {record.Gender, record.Topic })
                                .Select(group => new { Value = group.Key, Count = group.Count() })
                                .OrderBy(g => g.Value.Gender).ThenByDescending(g => g.Count);

            foreach (var res in result) {
                System.Console.WriteLine($"{res.Value.Gender}: {res.Value.Topic} - {res.Count}");
            }
        }

        // zad 3
        static List<Topic> GenerateTopics() {
            List<Topic> topics = new List<Topic>();
            topics.Add(new Topic("C#", 1));
            topics.Add(new Topic("C++", 3));
            topics.Add(new Topic("PHP", 4));
            topics.Add(new Topic("algorithms", 2));
            topics.Add(new Topic("fuzzy logic", 12));
            topics.Add(new Topic("Java", 7));
            topics.Add(new Topic("Basic", 10));
            topics.Add(new Topic("JavaScript", 11));
            topics.Add(new Topic("neural networks", 9));
            topics.Add(new Topic("web programming", 13));
            return topics;
        }

        static List<Student> SplitStudentsAndTopics(List<StudentWithTopics> students, List<Topic> topics) {
            var result = students
                .SelectMany(s => s.Topics.Select(t => new { Topic = t, s.Name, s.Active, s.Gender, s.Index, s.Id, s.DepartmentId}))
                .Join(topics,
                    s => s.Topic,
                    t => t.Name,
                    (s, t) => new {
                        s.Id,
                        s.Name,
                        s.Gender,
                        s.Active,
                        s.Index,
                        s.DepartmentId,
                        TopicName=t.Name,
                        TopicId=t.Id
                    })
                .GroupBy(g => new {g.Id, g.Active, g.DepartmentId, g.Gender, g.Index, g.Name})
                .Select(group => new { Student=group.Key, Topics=group.Select(x => x.TopicId).ToList()});

            List<Student> newStuds = new List<Student>();

            foreach (var res in result) {
                newStuds.Add(new Student(res.Student.Id, res.Student.Index, res.Student.Name, 
                    res.Student.Gender, res.Student.Active, res.Student.DepartmentId, res.Topics));
            }

            return newStuds;
        }

        // zad 3a
        static List<Topic> GenerateTopicsFromStudents(List<StudentWithTopics> students) {
            var result = students.SelectMany(s => s.Topics.Select(t => new { Topic = t, StudId=s.Id }))
                                .GroupBy(group => group.Topic).Select(s => new { Topic=s.Key });

            List<Topic> newTopics = new List<Topic>();
            
            int i=1;
            foreach(var res in result) {
                newTopics.Add(new Topic(res.Topic, i));
                i += 1;
            }

            return newTopics;
        }
    }
}
