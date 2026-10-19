namespace StudentRecordSystem
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            pnlDetails = new Panel();
            lblDetailsHeading = new Label();
            lblStudentId = new Label();
            pnlStudentIdBox = new Panel();
            txtStudentId = new TextBox();
            lblStudentName = new Label();
            pnlStudentNameBox = new Panel();
            txtStudentName = new TextBox();
            lblCourse = new Label();
            pnlCourseBox = new Panel();
            txtCourse = new TextBox();
            lblStatus = new Label();
            cboStatus = new ComboBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            lblDivider = new Label();
            btnExit = new Button();
            pnlRecords = new Panel();
            lblRecordsHeading = new Label();
            pnlSearchBox = new Panel();
            lblSearchIcon = new Label();
            txtSearch = new TextBox();
            dgvStudents = new DataGridView();
            pnlDetails.SuspendLayout();
            pnlStudentIdBox.SuspendLayout();
            pnlStudentNameBox.SuspendLayout();
            pnlCourseBox.SuspendLayout();
            pnlRecords.SuspendLayout();
            pnlSearchBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.Location = new Point(0, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1180, 56);
            lblTitle.TabIndex = 20;
            lblTitle.Text = "Student Record Management System";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            //
            // pnlDetails
            //
            pnlDetails.Controls.Add(lblDetailsHeading);
            pnlDetails.Controls.Add(lblStudentId);
            pnlDetails.Controls.Add(pnlStudentIdBox);
            pnlDetails.Controls.Add(lblStudentName);
            pnlDetails.Controls.Add(pnlStudentNameBox);
            pnlDetails.Controls.Add(lblCourse);
            pnlDetails.Controls.Add(pnlCourseBox);
            pnlDetails.Controls.Add(lblStatus);
            pnlDetails.Controls.Add(cboStatus);
            pnlDetails.Controls.Add(btnAdd);
            pnlDetails.Controls.Add(btnUpdate);
            pnlDetails.Controls.Add(btnDelete);
            pnlDetails.Controls.Add(btnClear);
            pnlDetails.Controls.Add(lblDivider);
            pnlDetails.Controls.Add(btnExit);
            pnlDetails.Location = new Point(24, 90);
            pnlDetails.Name = "pnlDetails";
            pnlDetails.Size = new Size(400, 606);
            pnlDetails.TabIndex = 0;
            //
            // lblDetailsHeading
            //
            lblDetailsHeading.AutoSize = true;
            lblDetailsHeading.Location = new Point(24, 22);
            lblDetailsHeading.Name = "lblDetailsHeading";
            lblDetailsHeading.Size = new Size(140, 28);
            lblDetailsHeading.TabIndex = 30;
            lblDetailsHeading.Text = "Student details";
            //
            // lblStudentId
            //
            lblStudentId.AutoSize = true;
            lblStudentId.Location = new Point(24, 72);
            lblStudentId.Name = "lblStudentId";
            lblStudentId.Size = new Size(82, 20);
            lblStudentId.TabIndex = 31;
            lblStudentId.Text = "Student ID:";
            //
            // pnlStudentIdBox
            //
            pnlStudentIdBox.Controls.Add(txtStudentId);
            pnlStudentIdBox.Location = new Point(24, 98);
            pnlStudentIdBox.Name = "pnlStudentIdBox";
            pnlStudentIdBox.Size = new Size(352, 42);
            pnlStudentIdBox.TabIndex = 0;
            //
            // txtStudentId
            //
            txtStudentId.BorderStyle = BorderStyle.None;
            txtStudentId.Location = new Point(12, 9);
            txtStudentId.MaxLength = 10;
            txtStudentId.Name = "txtStudentId";
            txtStudentId.Size = new Size(328, 24);
            txtStudentId.TabIndex = 0;
            //
            // lblStudentName
            //
            lblStudentName.AutoSize = true;
            lblStudentName.Location = new Point(24, 152);
            lblStudentName.Name = "lblStudentName";
            lblStudentName.Size = new Size(107, 20);
            lblStudentName.TabIndex = 32;
            lblStudentName.Text = "Student Name:";
            //
            // pnlStudentNameBox
            //
            pnlStudentNameBox.Controls.Add(txtStudentName);
            pnlStudentNameBox.Location = new Point(24, 178);
            pnlStudentNameBox.Name = "pnlStudentNameBox";
            pnlStudentNameBox.Size = new Size(352, 42);
            pnlStudentNameBox.TabIndex = 1;
            //
            // txtStudentName
            //
            txtStudentName.BorderStyle = BorderStyle.None;
            txtStudentName.Location = new Point(12, 9);
            txtStudentName.MaxLength = 50;
            txtStudentName.Name = "txtStudentName";
            txtStudentName.Size = new Size(328, 24);
            txtStudentName.TabIndex = 0;
            //
            // lblCourse
            //
            lblCourse.AutoSize = true;
            lblCourse.Location = new Point(24, 232);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(57, 20);
            lblCourse.TabIndex = 33;
            lblCourse.Text = "Course:";
            //
            // pnlCourseBox
            //
            pnlCourseBox.Controls.Add(txtCourse);
            pnlCourseBox.Location = new Point(24, 258);
            pnlCourseBox.Name = "pnlCourseBox";
            pnlCourseBox.Size = new Size(352, 42);
            pnlCourseBox.TabIndex = 2;
            //
            // txtCourse
            //
            txtCourse.BorderStyle = BorderStyle.None;
            txtCourse.Location = new Point(12, 9);
            txtCourse.MaxLength = 50;
            txtCourse.Name = "txtCourse";
            txtCourse.Size = new Size(328, 24);
            txtCourse.TabIndex = 0;
            //
            // lblStatus
            //
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(24, 312);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(52, 20);
            lblStatus.TabIndex = 34;
            lblStatus.Text = "Status:";
            //
            // cboStatus
            //
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.FormattingEnabled = true;
            cboStatus.Location = new Point(24, 338);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(352, 33);
            cboStatus.TabIndex = 3;
            //
            // btnAdd
            //
            btnAdd.Location = new Point(24, 396);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(170, 44);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Add Student";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            //
            // btnUpdate
            //
            btnUpdate.Location = new Point(206, 396);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(170, 44);
            btnUpdate.TabIndex = 5;
            btnUpdate.Text = "Update Selected";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            //
            // btnDelete
            //
            btnDelete.Location = new Point(24, 452);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(170, 44);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete Selected";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            //
            // btnClear
            //
            btnClear.Location = new Point(206, 452);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(170, 44);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            //
            // lblDivider
            //
            lblDivider.Location = new Point(24, 514);
            lblDivider.Name = "lblDivider";
            lblDivider.Size = new Size(352, 1);
            lblDivider.TabIndex = 35;
            //
            // btnExit
            //
            btnExit.Location = new Point(24, 530);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(352, 44);
            btnExit.TabIndex = 8;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            //
            // pnlRecords
            //
            pnlRecords.Controls.Add(lblRecordsHeading);
            pnlRecords.Controls.Add(pnlSearchBox);
            pnlRecords.Controls.Add(dgvStudents);
            pnlRecords.Location = new Point(444, 90);
            pnlRecords.Name = "pnlRecords";
            pnlRecords.Size = new Size(712, 606);
            pnlRecords.TabIndex = 1;
            //
            // lblRecordsHeading
            //
            lblRecordsHeading.AutoSize = true;
            lblRecordsHeading.Location = new Point(24, 22);
            lblRecordsHeading.Name = "lblRecordsHeading";
            lblRecordsHeading.Size = new Size(150, 28);
            lblRecordsHeading.TabIndex = 40;
            lblRecordsHeading.Text = "Student records";
            //
            // pnlSearchBox
            //
            pnlSearchBox.Controls.Add(lblSearchIcon);
            pnlSearchBox.Controls.Add(txtSearch);
            pnlSearchBox.Location = new Point(352, 16);
            pnlSearchBox.Name = "pnlSearchBox";
            pnlSearchBox.Size = new Size(336, 42);
            pnlSearchBox.TabIndex = 0;
            //
            // lblSearchIcon
            //
            lblSearchIcon.Location = new Point(10, 9);
            lblSearchIcon.Name = "lblSearchIcon";
            lblSearchIcon.Size = new Size(26, 24);
            lblSearchIcon.TabIndex = 41;
            lblSearchIcon.Text = "";
            lblSearchIcon.TextAlign = ContentAlignment.MiddleCenter;
            //
            // txtSearch
            //
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.Location = new Point(44, 9);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(280, 24);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            //
            // dgvStudents
            //
            dgvStudents.Location = new Point(24, 80);
            dgvStudents.Name = "dgvStudents";
            dgvStudents.RowHeadersWidth = 51;
            dgvStudents.Size = new Size(664, 502);
            dgvStudents.TabIndex = 1;
            dgvStudents.CellClick += dgvStudents_CellClick;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1180, 720);
            Controls.Add(pnlRecords);
            Controls.Add(pnlDetails);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 10.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Student Record Management System";
            Load += MainForm_Load;
            pnlDetails.ResumeLayout(false);
            pnlDetails.PerformLayout();
            pnlStudentIdBox.ResumeLayout(false);
            pnlStudentIdBox.PerformLayout();
            pnlStudentNameBox.ResumeLayout(false);
            pnlStudentNameBox.PerformLayout();
            pnlCourseBox.ResumeLayout(false);
            pnlCourseBox.PerformLayout();
            pnlRecords.ResumeLayout(false);
            pnlRecords.PerformLayout();
            pnlSearchBox.ResumeLayout(false);
            pnlSearchBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Panel pnlDetails;
        private Label lblDetailsHeading;
        private Label lblStudentId;
        private Panel pnlStudentIdBox;
        private TextBox txtStudentId;
        private Label lblStudentName;
        private Panel pnlStudentNameBox;
        private TextBox txtStudentName;
        private Label lblCourse;
        private Panel pnlCourseBox;
        private TextBox txtCourse;
        private Label lblStatus;
        private ComboBox cboStatus;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Label lblDivider;
        private Button btnExit;
        private Panel pnlRecords;
        private Label lblRecordsHeading;
        private Panel pnlSearchBox;
        private Label lblSearchIcon;
        private TextBox txtSearch;
        private DataGridView dgvStudents;
    }
}