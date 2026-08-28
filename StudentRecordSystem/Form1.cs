namespace StudentRecordSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text) ||
                string.IsNullOrWhiteSpace(txtStudentName.Text) ||
                string.IsNullOrWhiteSpace(txtCourse.Text))
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }

            MessageBox.Show(
                $"Student ID: {txtStudentId.Text}\n" +
                $"Student Name: {txtStudentName.Text}\n" +
                $"Course: {txtCourse.Text}",
                "Student Record Added"
            );
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStudentId.Clear();
            txtStudentName.Clear();
            txtCourse.Clear();
            txtStudentId.Focus();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
