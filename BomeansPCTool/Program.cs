using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BomeansPCTool
{
    static class Program
    {
        public const string SoftwareName = "Bomeans PC Tool";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        public static DialogResult ShowError(Exception ex)
        {
            return ShowError(ex.Message);
        }

        internal static DialogResult ShowError(string text)
        {
            DialogResult result = MessageBox.Show(text, SoftwareName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return result;
        }

        public static void ShowWarning(string text)
        {
            MessageBox.Show(text, SoftwareName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        public static void ShowMessage(string text)
        {
            MessageBox.Show(text, SoftwareName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        public static DialogResult ShowQuestion(string text)
        {
            DialogResult result = MessageBox.Show(text, SoftwareName,
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);
            return result;
        }

        public static DialogResult ShowQuestionWithCancel(string text)
        {
            DialogResult result = MessageBox.Show(text, SoftwareName,
               MessageBoxButtons.YesNoCancel,
               MessageBoxIcon.Question);
            return result;
        }
    }
}
