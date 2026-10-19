namespace StudentRecordSystem
{
    /// <summary>
    /// A custom exception thrown when a student ID is already in use.
    /// It inherits from System.Exception, which demonstrates
    /// inheritance: the class reuses all the behaviour of Exception
    /// (message, stack trace) and adds its own StudentId property.
    /// </summary>
    public class DuplicateStudentException : Exception
    {
        /// <summary>
        /// Gets the student ID that caused the conflict.
        /// </summary>
        public int StudentId { get; }

        /// <summary>
        /// Creates the exception with a readable error message.
        /// The ": base(...)" call passes the message to the
        /// constructor of the parent Exception class.
        /// </summary>
        /// <param name="studentId">The duplicate student ID.</param>
        public DuplicateStudentException(int studentId)
            : base($"A student with ID {studentId} already exists.")
        {
            StudentId = studentId;
        }
    }
}