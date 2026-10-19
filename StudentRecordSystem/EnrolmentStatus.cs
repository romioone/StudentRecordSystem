namespace StudentRecordSystem
{
    /// <summary>
    /// Represents the enrolment status of a student.
    /// An enum is a value type that limits a variable to a fixed
    /// set of named constants, which prevents invalid values
    /// such as misspelt status strings.
    /// </summary>
    public enum EnrolmentStatus
    {
        /// <summary>The student is currently enrolled.</summary>
        Active,

        /// <summary>The student has paused or withdrawn from study.</summary>
        Inactive,

        /// <summary>The student has completed the course.</summary>
        Graduated
    }
}