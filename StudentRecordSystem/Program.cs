namespace StudentRecordSystem
{
    /// <summary>
    /// Contains the entry point of the Student Record Management System.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application. The [STAThread]
        /// attribute is required by Windows Forms, which must run on a
        /// single-threaded apartment thread.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Applies default settings such as high-DPI scaling and fonts.
            ApplicationConfiguration.Initialize();

            // Starts the message loop and shows the main window. The
            // application keeps running until this window is closed.
            Application.Run(new MainForm());
        }
    }
}