namespace StudentRecordSystem
{
    /// <summary>
    /// Defines the operations for storing and retrieving students.
    /// An interface describes WHAT a class can do without saying
    /// HOW it does it (abstraction). The form depends only on this
    /// interface, so the JSON storage could later be replaced with
    /// SQLite by writing a new class, without changing the form.
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>Returns every student, sorted by ID.</summary>
        IReadOnlyList<Student> GetAll();

        /// <summary>Returns students whose ID or name contains the search text.</summary>
        IReadOnlyList<Student> Search(string searchText);

        /// <summary>Returns the student with the given ID, or null if none exists.</summary>
        Student? FindById(int studentId);

        /// <summary>Adds a new student. Throws DuplicateStudentException if the ID is taken.</summary>
        void Add(Student student);

        /// <summary>Replaces the details of the student whose current ID is oldStudentId.</summary>
        void Update(int oldStudentId, Student updatedStudent);

        /// <summary>Deletes a student. Returns true if a record was removed.</summary>
        bool Delete(int studentId);

        /// <summary>Loads all students from storage asynchronously.</summary>
        Task LoadAsync();

        /// <summary>Saves all students to storage asynchronously.</summary>
        Task SaveAsync();
    }
}