using System.Drawing.Drawing2D;
using System.Text.Json;

namespace StudentRecordSystem
{
    /// <summary>
    /// The main window of the Student Record Management System.
    /// This class is the presentation layer only. It reads user input,
    /// displays results and responds to events such as button clicks,
    /// typing in the search box and selecting a row. All storage work
    /// is delegated to an IStudentRepository, which keeps the user
    /// interface separate from the data-access logic.
    /// </summary>
    public partial class MainForm : Form
    {
        // Location of the JSON data file, for example:
        // C:\Users\<name>\AppData\Local\StudentRecordSystem\students.json
        private static readonly string DataFilePath = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
            "StudentRecordSystem",
            "students.json");

        // Shared fonts, created once and reused by every control.
        private static readonly Font RegularFont = new("Segoe UI", 10.5F);
        private static readonly Font InputFont = new("Segoe UI", 11F);
        private static readonly Font ButtonFont = new("Segoe UI", 10.5F, FontStyle.Bold);
        private static readonly Font HeaderFont = new("Segoe UI", 10.5F, FontStyle.Bold);
        private static readonly Font HeadingFont = new("Segoe UI", 15F, FontStyle.Bold);
        private static readonly Font TitleFont = new("Segoe UI", 22F, FontStyle.Bold);
        private static readonly Font IconFont = new("Segoe MDL2 Assets", 12F);

        // Shared colours used across the whole window.
        private static readonly Color PageColor = Color.FromArgb(243, 245, 248);
        private static readonly Color CardColor = Color.White;
        private static readonly Color BorderColor = Color.FromArgb(222, 226, 232);
        private static readonly Color FocusColor = Color.FromArgb(37, 99, 235);
        private static readonly Color DarkText = Color.FromArgb(17, 34, 64);
        private static readonly Color MutedText = Color.FromArgb(75, 85, 99);

        // The form depends only on the interface, not on the JSON class.
        // This is abstraction: the storage could be swapped for SQLite
        // without changing any code in this form.
        private readonly IStudentRepository repository;

        // ID of the student the user clicked in the table.
        // int? is a nullable value type, so null means "nothing selected".
        private int? selectedStudentId;

        /// <summary>
        /// Creates the main window, the repository and the table layout.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            repository = new JsonStudentRepository(DataFilePath);

            ConfigureGrid();
            ConfigureStatusList();
            ApplyModernStyle();
        }

        #region Form set-up

        /// <summary>
        /// Adds the table columns and sets the table to read-only,
        /// single full-row selection.
        /// </summary>
        private void ConfigureGrid()
        {
            dgvStudents.Columns.Add("StudentId", "Student ID");
            dgvStudents.Columns.Add("StudentName", "Student Name");
            dgvStudents.Columns.Add("Course", "Course");
            dgvStudents.Columns.Add("Status", "Status");

            dgvStudents.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.ReadOnly = true;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.MultiSelect = false;
            dgvStudents.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Fills the Status drop-down list with every value of the
        /// EnrolmentStatus enum and selects Active by default.
        /// </summary>
        private void ConfigureStatusList()
        {
            // Enum.GetValues<T>() is a generic method that returns
            // every named value declared in the enum.
            foreach (EnrolmentStatus status in Enum.GetValues<EnrolmentStatus>())
            {
                cboStatus.Items.Add(status);
            }

            cboStatus.SelectedItem = EnrolmentStatus.Active;
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Runs when the window first loads. The saved students are read
        /// asynchronously, so the window stays responsive while the file
        /// is being read. "async void" is only used for event handlers.
        /// </summary>
        private async void MainForm_Load(object? sender, EventArgs e)
        {
            try
            {
                await repository.LoadAsync();
            }
            catch (JsonException ex)
            {
                // The file exists but does not contain valid JSON.
                ShowError(
                    "The saved data file is damaged and could not be read.\n\n" +
                    ex.Message,
                    "Load Failed");
            }
            catch (ArgumentException ex)
            {
                // A saved record broke one of the Student validation rules.
                ShowError(
                    "The saved data contains an invalid student record.\n\n" +
                    ex.Message,
                    "Load Failed");
            }
            catch (Exception ex) when (
                ex is IOException or UnauthorizedAccessException)
            {
                // An exception filter ("when") catches only file-access problems.
                ShowError(
                    "The saved data file could not be opened.\n\n" + ex.Message,
                    "Load Failed");
            }

            RefreshStudentGrid();
        }

        /// <summary>
        /// Adds a new student using the values in the input boxes.
        /// </summary>
        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            Student? student = ReadStudentFromInputs();

            if (student is null)
            {
                return;
            }

            try
            {
                repository.Add(student);
            }
            catch (DuplicateStudentException ex)
            {
                // Our custom exception, thrown by the repository.
                ShowWarning(ex.Message, "Duplicate Student ID");
                txtStudentId.Focus();
                return;
            }

            if (!await TrySaveAsync())
            {
                return;
            }

            txtSearch.Clear();
            RefreshStudentGrid();
            ClearForm();

            ShowInfo("Student added successfully.", "Student Added");
        }

        /// <summary>
        /// Updates the student that was clicked in the table with the
        /// values currently in the input boxes.
        /// </summary>
        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
            // Pattern matching: only continue if a student is selected.
            if (selectedStudentId is not int oldId)
            {
                ShowInfo(
                    "Please click a student in the table first.",
                    "No Student Selected");
                return;
            }

            Student? updatedStudent = ReadStudentFromInputs();

            if (updatedStudent is null)
            {
                return;
            }

            try
            {
                repository.Update(oldId, updatedStudent);
            }
            catch (DuplicateStudentException ex)
            {
                ShowWarning(ex.Message, "Duplicate Student ID");
                txtStudentId.Focus();
                return;
            }
            catch (KeyNotFoundException ex)
            {
                // The selected student was removed before the update.
                ShowWarning(ex.Message, "Student Not Found");
                RefreshStudentGrid();
                return;
            }

            if (!await TrySaveAsync())
            {
                return;
            }

            RefreshStudentGrid();
            ClearForm();

            ShowInfo("Student updated successfully.", "Student Updated");
        }

        /// <summary>
        /// Deletes the student that was clicked in the table after the
        /// user confirms the action.
        /// </summary>
        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (selectedStudentId is not int studentId)
            {
                ShowInfo(
                    "Please click a student in the table first.",
                    "No Student Selected");
                return;
            }

            // The ?. and ?? operators safely handle a missing student.
            string studentName =
                repository.FindById(studentId)?.StudentName
                ?? "this student";

            DialogResult answer = MessageBox.Show(
                $"Are you sure you want to delete {studentName} " +
                $"(ID: {studentId})?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            if (!repository.Delete(studentId))
            {
                ShowWarning(
                    "The selected student no longer exists.",
                    "Student Not Found");
                RefreshStudentGrid();
                return;
            }

            if (!await TrySaveAsync())
            {
                return;
            }

            RefreshStudentGrid();
            ClearForm();

            ShowInfo("Student deleted successfully.", "Student Deleted");
        }

        /// <summary>
        /// Clears the input boxes and the current selection.
        /// </summary>
        private void btnClear_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        private void btnExit_Click(object? sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Filters the table each time the search text changes.
        /// </summary>
        private void txtSearch_TextChanged(object? sender, EventArgs e)
        {
            RefreshStudentGrid();
        }

        /// <summary>
        /// Copies the clicked student's details into the input boxes
        /// and remembers which student was selected.
        /// </summary>
        private void dgvStudents_CellClick(
            object? sender, DataGridViewCellEventArgs e)
        {
            // A row index of -1 means the column header was clicked.
            if (e.RowIndex < 0)
            {
                return;
            }

            // DataGridView cells store values as "object", so pattern
            // matching is needed to get the int back safely.
            object? cellValue =
                dgvStudents.Rows[e.RowIndex].Cells["StudentId"].Value;

            if (cellValue is not int studentId)
            {
                return;
            }

            Student? student = repository.FindById(studentId);

            if (student is null)
            {
                return;
            }

            selectedStudentId = student.StudentId;

            txtStudentId.Text = student.StudentId.ToString();
            txtStudentName.Text = student.StudentName;
            txtCourse.Text = student.Course;
            cboStatus.SelectedItem = student.Status;
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Reads and validates the input boxes and returns a new Student,
        /// or null if any value is invalid. The Student class itself
        /// enforces the name and course rules through its property setters.
        /// </summary>
        private Student? ReadStudentFromInputs()
        {
            if (!int.TryParse(txtStudentId.Text.Trim(), out int studentId)
                || studentId <= 0)
            {
                ShowWarning(
                    "Enter a positive whole number for Student ID.",
                    "Invalid Student ID");
                txtStudentId.Focus();
                return null;
            }

            EnrolmentStatus status =
                cboStatus.SelectedItem is EnrolmentStatus selected
                    ? selected
                    : EnrolmentStatus.Active;

            try
            {
                return new Student(
                    studentId,
                    txtStudentName.Text,
                    txtCourse.Text,
                    status);
            }
            catch (ArgumentException)
            {
                // Thrown by the Student setters when the name or
                // course is blank.
                ShowWarning(
                    "Please enter both the student name and the course.",
                    "Missing Details");
                return null;
            }
        }

        /// <summary>
        /// Saves all students and reports any file error to the user.
        /// Returns true when the save succeeded.
        /// </summary>
        private async Task<bool> TrySaveAsync()
        {
            try
            {
                await repository.SaveAsync();
                return true;
            }
            catch (Exception ex) when (
                ex is IOException or UnauthorizedAccessException)
            {
                ShowError(
                    "The student data could not be saved.\n\n" + ex.Message,
                    "Save Failed");
                return false;
            }
        }

        /// <summary>
        /// Reloads the table from the repository, applying the search text.
        /// The selection is cleared so that no student is changed by accident.
        /// </summary>
        private void RefreshStudentGrid()
        {
            dgvStudents.Rows.Clear();

            foreach (Student student in repository.Search(txtSearch.Text))
            {
                dgvStudents.Rows.Add(
                    student.StudentId,
                    student.StudentName,
                    student.Course,
                    student.Status);
            }

            dgvStudents.ClearSelection();
            selectedStudentId = null;
        }

        /// <summary>
        /// Empties the input boxes and resets the selection.
        /// </summary>
        private void ClearForm()
        {
            txtStudentId.Clear();
            txtStudentName.Clear();
            txtCourse.Clear();
            cboStatus.SelectedItem = EnrolmentStatus.Active;

            selectedStudentId = null;
            dgvStudents.ClearSelection();
            txtStudentId.Focus();
        }

        /// <summary>Shows an information message.</summary>
        private static void ShowInfo(string message, string title)
        {
            MessageBox.Show(
                message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>Shows a warning message.</summary>
        private static void ShowWarning(string message, string title)
        {
            MessageBox.Show(
                message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>Shows an error message.</summary>
        private static void ShowError(string message, string title)
        {
            MessageBox.Show(
                message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region Visual styling

        /// <summary>
        /// Applies the card layout, colours, fonts and rounded shapes
        /// to the whole window.
        /// </summary>
        private void ApplyModernStyle()
        {
            BackColor = PageColor;
            ForeColor = DarkText;

            StyleCards();
            StyleLabels();
            StyleInputBoxes();
            StyleGrid();

            StyleButton(btnAdd, Color.FromArgb(37, 99, 235), Color.FromArgb(29, 78, 216), Color.White);
            StyleButton(btnUpdate, Color.FromArgb(37, 99, 235), Color.FromArgb(29, 78, 216), Color.White);
            StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.FromArgb(185, 28, 45), Color.White);
            StyleButton(btnClear, Color.FromArgb(100, 116, 139), Color.FromArgb(71, 85, 105), Color.White);
            StyleButton(btnExit, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), DarkText);

            cboStatus.Font = InputFont;
            cboStatus.BackColor = CardColor;
            cboStatus.ForeColor = DarkText;
        }

        /// <summary>
        /// Gives the two main panels a white rounded "card" appearance.
        /// The panels are drawn in their Paint events below.
        /// </summary>
        private void StyleCards()
        {
            pnlDetails.BackColor = PageColor;
            pnlRecords.BackColor = PageColor;

            pnlDetails.Paint += Card_Paint;
            pnlRecords.Paint += RecordsCard_Paint;

            // Redraw the cards whenever their size changes.
            pnlDetails.Resize += (_, _) => pnlDetails.Invalidate();
            pnlRecords.Resize += (_, _) => pnlRecords.Invalidate();
        }

        /// <summary>
        /// Styles every label in the window. GetAllControls is an iterator
        /// method and OfType&lt;Label&gt;() is a LINQ method that keeps only labels.
        /// </summary>
        private void StyleLabels()
        {
            foreach (Label label in GetAllControls(this).OfType<Label>())
            {
                label.Font = RegularFont;
                label.ForeColor = MutedText;

                // Labels inside a card sit on a white background.
                label.BackColor = label.Parent == this ? PageColor : CardColor;
            }

            lblTitle.Font = TitleFont;
            lblTitle.ForeColor = DarkText;

            lblDetailsHeading.Font = HeadingFont;
            lblDetailsHeading.ForeColor = DarkText;

            lblRecordsHeading.Font = HeadingFont;
            lblRecordsHeading.ForeColor = DarkText;

            lblDivider.BackColor = BorderColor;

            lblSearchIcon.Font = IconFont;
            lblSearchIcon.ForeColor = MutedText;
        }

        /// <summary>
        /// Turns each small panel that holds a text box into a rounded
        /// input box. The border turns blue while the text box has focus.
        /// </summary>
        private void StyleInputBoxes()
        {
            Panel[] inputBoxes =
            {
                pnlStudentIdBox, pnlStudentNameBox, pnlCourseBox, pnlSearchBox
            };

            foreach (Panel box in inputBoxes)
            {
                box.BackColor = CardColor;
                box.Paint += InputBox_Paint;
                box.Cursor = Cursors.IBeam;

                foreach (TextBox textBox in box.Controls.OfType<TextBox>())
                {
                    textBox.BorderStyle = BorderStyle.None;
                    textBox.BackColor = CardColor;
                    textBox.ForeColor = DarkText;
                    textBox.Font = InputFont;

                    // Lambda expressions that redraw the border on focus changes.
                    textBox.Enter += (_, _) => box.Invalidate();
                    textBox.Leave += (_, _) => box.Invalidate();

                    // Clicking anywhere in the box moves focus to the text box.
                    box.Click += (_, _) => textBox.Focus();
                }
            }

            lblSearchIcon.Click += (_, _) => txtSearch.Focus();
            txtSearch.PlaceholderText = "Search by ID or student name...";
        }

        /// <summary>
        /// Applies the table colours, fonts, padding and rounded corners.
        /// </summary>
        private void StyleGrid()
        {
            dgvStudents.BackgroundColor = CardColor;
            dgvStudents.BorderStyle = BorderStyle.None;
            dgvStudents.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;
            dgvStudents.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dgvStudents.GridColor = Color.FromArgb(236, 239, 243);
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvStudents.ColumnHeadersHeight = 50;
            dgvStudents.RowTemplate.Height = 46;

            Padding cellPadding = new(12, 0, 0, 0);

            dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStudents.ColumnHeadersDefaultCellStyle.Font = HeaderFont;
            dgvStudents.ColumnHeadersDefaultCellStyle.Padding = cellPadding;
            dgvStudents.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvStudents.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 41, 59);

            dgvStudents.DefaultCellStyle.Font = RegularFont;
            dgvStudents.DefaultCellStyle.Padding = cellPadding;
            dgvStudents.DefaultCellStyle.ForeColor = DarkText;
            dgvStudents.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvStudents.DefaultCellStyle.SelectionForeColor = DarkText;
            dgvStudents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            ApplyRoundedRegion(dgvStudents, 10);
        }

        /// <summary>
        /// Gives a button a flat coloured look, a hover colour and
        /// rounded corners.
        /// </summary>
        private static void StyleButton(
            Button button, Color backgroundColor, Color hoverColor, Color textColor)
        {
            button.BackColor = backgroundColor;
            button.ForeColor = textColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = hoverColor;
            button.Font = ButtonFont;
            button.Cursor = Cursors.Hand;

            ApplyRoundedRegion(button, 10);
        }

        /// <summary>
        /// Draws a white card with a smooth rounded border. Used as the
        /// Paint event handler for the two main panels.
        /// </summary>
        private void Card_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control card)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = new(0, 0, card.Width - 1, card.Height - 1);

            using GraphicsPath path = CreateRoundedPath(bounds, 16);
            using SolidBrush fill = new(CardColor);
            using Pen border = new(BorderColor);

            e.Graphics.FillPath(fill, path);
            e.Graphics.DrawPath(border, path);
        }

        /// <summary>
        /// Draws the records card and a light border around the table.
        /// </summary>
        private void RecordsCard_Paint(object? sender, PaintEventArgs e)
        {
            Card_Paint(sender, e);

            Rectangle tableBounds = dgvStudents.Bounds;
            tableBounds.Inflate(1, 1);

            using GraphicsPath path = CreateRoundedPath(tableBounds, 11);
            using Pen border = new(BorderColor);

            e.Graphics.DrawPath(border, path);
        }

        /// <summary>
        /// Draws the rounded border of an input box. The border is blue
        /// while the text box inside it has keyboard focus.
        /// </summary>
        private void InputBox_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control box)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = new(0, 0, box.Width - 1, box.Height - 1);

            // The conditional operator picks the border colour.
            Color borderColor = box.ContainsFocus ? FocusColor : BorderColor;

            using GraphicsPath path = CreateRoundedPath(bounds, 8);
            using Pen border = new(borderColor, box.ContainsFocus ? 2F : 1F);

            e.Graphics.DrawPath(border, path);
        }

        /// <summary>
        /// Clips any control to a rounded rectangle. The shape is
        /// recalculated whenever the control changes size.
        /// </summary>
        private static void ApplyRoundedRegion(Control control, int radius)
        {
            // A local function: a method declared inside another method.
            // It can use the control and radius variables directly.
            void ApplyShape(object? sender, EventArgs e)
            {
                if (control.Width <= 0 || control.Height <= 0)
                {
                    return;
                }

                using GraphicsPath path = CreateRoundedPath(
                    new Rectangle(0, 0, control.Width, control.Height), radius);

                control.Region?.Dispose();
                control.Region = new Region(path);
            }

            // Subscribing a method to an event (event-driven programming).
            control.SizeChanged += ApplyShape;
            ApplyShape(control, EventArgs.Empty);
        }

        /// <summary>
        /// Builds a rectangle shape with rounded corners.
        /// </summary>
        private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
        {
            int diameter = Math.Min(
                radius * 2, Math.Min(bounds.Width, bounds.Height));

            GraphicsPath path = new();

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Returns every control inside a parent, including controls
        /// nested inside panels. "yield return" makes this an iterator:
        /// each control is produced one at a time as the loop asks for it.
        /// </summary>
        private static IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                yield return child;

                foreach (Control grandChild in GetAllControls(child))
                {
                    yield return grandChild;
                }
            }
        }

        #endregion
    }
}