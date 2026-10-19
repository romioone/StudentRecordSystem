using System.Text.Json.Serialization;

namespace StudentRecordSystem
{
    /// <summary>
    /// Represents a single student record in the system.
    /// The class uses encapsulation: each value is stored in a
    /// private field and can only be changed through a property
    /// whose setter validates the new value. This prevents an
    /// invalid student (for example, a negative ID or a blank name)
    /// from ever existing in memory.
    /// </summary>
    public class Student
    {
        // Private backing fields. Code outside this class cannot
        // read or change these directly.
        private int studentId;
        private string studentName = string.Empty;
        private string course = string.Empty;

        /// <summary>
        /// Parameterless constructor required by System.Text.Json
        /// so that saved records can be loaded from the JSON file.
        /// </summary>
        public Student()
        {
        }

        /// <summary>
        /// Creates a fully initialised student. Each value passes
        /// through its property setter, so validation always runs.
        /// </summary>
        /// <param name="studentId">A positive whole-number ID.</param>
        /// <param name="studentName">The student's full name.</param>
        /// <param name="course">The course the student is enrolled in.</param>
        /// <param name="status">The enrolment status (Active by default).</param>
        public Student(
            int studentId,
            string studentName,
            string course,
            EnrolmentStatus status = EnrolmentStatus.Active)
        {
            StudentId = studentId;
            StudentName = studentName;
            Course = course;
            Status = status;
        }

        /// <summary>
        /// Gets or sets the unique student ID.
        /// Throws an exception if the value is zero or negative.
        /// </summary>
        public int StudentId
        {
            get => studentId;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(StudentId),
                        "Student ID must be a positive whole number.");
                }

                studentId = value;
            }
        }

        /// <summary>
        /// Gets or sets the student's name.
        /// Blank names are rejected and extra spaces are removed.
        /// </summary>
        public string StudentName
        {
            get => studentName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(
                        "Student name cannot be empty.",
                        nameof(StudentName));
                }

                studentName = value.Trim();
            }
        }

        /// <summary>
        /// Gets or sets the course name.
        /// Blank course names are rejected and extra spaces are removed.
        /// </summary>
        public string Course
        {
            get => course;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(
                        "Course cannot be empty.",
                        nameof(Course));
                }

                course = value.Trim();
            }
        }

        /// <summary>
        /// Gets or sets the enrolment status. The attribute saves the
        /// status as readable text (e.g. "Active") in the JSON file
        /// instead of a number.
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EnrolmentStatus Status { get; set; } = EnrolmentStatus.Active;

        /// <summary>
        /// Returns a short readable description of the student.
        /// This overrides the ToString method inherited from
        /// System.Object, which is an example of polymorphism.
        /// </summary>
        public override string ToString()
        {
            return $"{StudentId} - {StudentName} ({Course}, {Status})";
        }
    }
}