using System.Text.Json;

namespace StudentRecordSystem
{
    /// <summary>
    /// Stores student records in a JSON file.
    /// This class implements IStudentRepository, so it must provide
    /// every method the interface declares. It uses LINQ (Language
    /// Integrated Query) for searching and filtering, and async
    /// so that file reading and writing do not freeze the user interface.
    /// </summary>
    public class JsonStudentRepository : IStudentRepository
    {
        // The in-memory list of students. It is private, so other
        // classes can only change it through the methods below.
        private readonly List<Student> students = new();

        // The full path of the JSON file used for storage.
        private readonly string filePath;

        // Shared JSON settings. WriteIndented makes the file readable.
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        /// <summary>
        /// Creates a repository that reads and writes the given file.
        /// </summary>
        /// <param name="filePath">Full path to the JSON data file.</param>
        public JsonStudentRepository(string filePath)
        {
            this.filePath = filePath;
        }

        /// <inheritdoc />
        public IReadOnlyList<Student> GetAll()
        {
            // LINQ: sort the students by ID and return a new list.
            return students
                .OrderBy(student => student.StudentId)
                .ToList();
        }

        /// <inheritdoc />
        public IReadOnlyList<Student> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return GetAll();
            }

            string term = searchText.Trim();

            // LINQ: keep only students whose ID or name matches,
            // then sort them. The lambda expression (student => ...)
            // is a small anonymous function applied to each student.
            return students
                .Where(student =>
                    student.StudentId.ToString().Contains(term) ||
                    student.StudentName.Contains(
                        term, StringComparison.OrdinalIgnoreCase))
                .OrderBy(student => student.StudentId)
                .ToList();
        }

        /// <inheritdoc />
        public Student? FindById(int studentId)
        {
            // LINQ: return the first match, or null if there is none.
            return students.FirstOrDefault(
                student => student.StudentId == studentId);
        }

        /// <inheritdoc />
        public void Add(Student student)
        {
            ArgumentNullException.ThrowIfNull(student);

            // LINQ Any() replaces a manual foreach loop.
            if (students.Any(existing => existing.StudentId == student.StudentId))
            {
                throw new DuplicateStudentException(student.StudentId);
            }

            students.Add(student);
        }

        /// <inheritdoc />
        public void Update(int oldStudentId, Student updatedStudent)
        {
            ArgumentNullException.ThrowIfNull(updatedStudent);

            // The ?? operator throws an exception if FindById returns null.
            Student existing = FindById(oldStudentId)
                ?? throw new KeyNotFoundException(
                    $"No student with ID {oldStudentId} was found.");

            // The new ID must not belong to a different student.
            bool idTakenByAnother = students.Any(student =>
                student.StudentId == updatedStudent.StudentId &&
                student != existing);

            if (idTakenByAnother)
            {
                throw new DuplicateStudentException(updatedStudent.StudentId);
            }

            existing.StudentId = updatedStudent.StudentId;
            existing.StudentName = updatedStudent.StudentName;
            existing.Course = updatedStudent.Course;
            existing.Status = updatedStudent.Status;
        }

        /// <inheritdoc />
        public bool Delete(int studentId)
        {
            // RemoveAll returns how many items were removed.
            int removed = students.RemoveAll(
                student => student.StudentId == studentId);

            return removed > 0;
        }

        /// <inheritdoc />
        public async Task LoadAsync()
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            // "await using" closes the file automatically when finished.
            await using FileStream stream = File.OpenRead(filePath);

            // await pauses this method until reading is complete,
            // while the user interface stays responsive.
            List<Student>? loaded =
                await JsonSerializer.DeserializeAsync<List<Student>>(
                    stream, JsonOptions);

            students.Clear();

            if (loaded != null)
            {
                students.AddRange(loaded);
            }
        }

        /// <inheritdoc />
        public async Task SaveAsync()
        {
            string? folder = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(folder))
            {
                Directory.CreateDirectory(folder);
            }

            await using FileStream stream = File.Create(filePath);

            await JsonSerializer.SerializeAsync(
                stream, students, JsonOptions);
        }
    }
}