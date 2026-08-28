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
            lblStudentId = new Label();
            lblStudenName = new Label();
            lblCourse = new Label();
            txtStudentId = new TextBox();
            txtStudentName = new TextBox();
            txtCourse = new TextBox();
            btnAdd = new Button();
            btnClear = new Button();
            btnexit = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(312, 27);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(254, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Student Record Management System";
            // 
            // lblStudentId
            // 
            lblStudentId.AutoSize = true;
            lblStudentId.Location = new Point(31, 111);
            lblStudentId.Name = "lblStudentId";
            lblStudentId.Size = new Size(82, 20);
            lblStudentId.TabIndex = 1;
            lblStudentId.Text = "Student ID:";
            lblStudentId.Click += label1_Click;
            // 
            // lblStudenName
            // 
            lblStudenName.AutoSize = true;
            lblStudenName.Location = new Point(31, 168);
            lblStudenName.Name = "lblStudenName";
            lblStudenName.Size = new Size(107, 20);
            lblStudenName.TabIndex = 2;
            lblStudenName.Text = "Student Name:";
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.Location = new Point(31, 230);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(57, 20);
            lblCourse.TabIndex = 3;
            lblCourse.Text = "Course:";
            // 
            // txtStudentId
            // 
            txtStudentId.Location = new Point(154, 104);
            txtStudentId.MaxLength = 10;
            txtStudentId.Name = "txtStudentId";
            txtStudentId.Size = new Size(125, 27);
            txtStudentId.TabIndex = 4;
            // 
            // txtStudentName
            // 
            txtStudentName.Location = new Point(154, 168);
            txtStudentName.MaxLength = 50;
            txtStudentName.Name = "txtStudentName";
            txtStudentName.Size = new Size(125, 27);
            txtStudentName.TabIndex = 5;
            // 
            // txtCourse
            // 
            txtCourse.Location = new Point(154, 227);
            txtCourse.MaxLength = 50;
            txtCourse.Name = "txtCourse";
            txtCourse.Size = new Size(125, 27);
            txtCourse.TabIndex = 6;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(28, 298);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(110, 35);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Add student";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(173, 298);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(90, 35);
            btnClear.TabIndex = 8;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnexit
            // 
            btnexit.Location = new Point(312, 298);
            btnexit.Name = "btnexit";
            btnexit.Size = new Size(90, 35);
            btnexit.TabIndex = 9;
            btnexit.Text = "Exit";
            btnexit.UseVisualStyleBackColor = true;
            btnexit.Click += btnexit_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 553);
            Controls.Add(btnexit);
            Controls.Add(btnClear);
            Controls.Add(btnAdd);
            Controls.Add(txtCourse);
            Controls.Add(txtStudentName);
            Controls.Add(txtStudentId);
            Controls.Add(lblCourse);
            Controls.Add(lblStudenName);
            Controls.Add(lblStudentId);
            Controls.Add(lblTitle);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Student Record Management System";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblStudentId;
        private Label lblStudenName;
        private Label lblCourse;
        private TextBox txtStudentId;
        private TextBox txtStudentName;
        private TextBox txtCourse;
        private Button btnAdd;
        private Button btnClear;
        private Button btnexit;
    }
}
