using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace GameToolsAndPipelines
{
    public partial class MainWindow : Window
    {
        // Added InputText property
        public string InputText { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ExecuteGitCommand(string command)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.FileName = "cmd.exe";
                startInfo.CreateNoWindow = true;

                process.StartInfo = startInfo;
                process.Start();

                StreamWriter streamWriter = process.StandardInput;
                streamWriter.WriteLine(command);
                streamWriter.WriteLine("exit");
                streamWriter.Close();

                string output = await process.StandardOutput.ReadToEndAsync();
                Console.WriteLine(output);

                process.WaitForExit();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing Git command: {ex.Message}");
            }
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select file or folder to add";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                string files = string.Join(" ", openFileDialog.FileNames);
                ExecuteGitCommand($"cd /d \"{Directory.GetCurrentDirectory()}\" && git add {files}");
            }
        }

        private void bCommit_Click(object sender, RoutedEventArgs e)
        {
            
            string commitMessage = InputTextBox.Text;

            if (!string.IsNullOrWhiteSpace(commitMessage))
            {
                ExecuteGitCommand($"cd /d \"{Directory.GetCurrentDirectory()}\" && git commit -m \"{commitMessage}\"");
                InputTextBox.Text = "Done";
            }


        }

        private void bPush_Click(object sender, RoutedEventArgs e)
        {
            ExecuteGitCommand($"cd /d \"{Directory.GetCurrentDirectory()}\" && git push");
        }
        private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputTextBox.Text = "";
        }

        private void bFetch_Click(object sender, RoutedEventArgs e)
        {
            ExecuteGitCommand($"cd /d \"{Directory.GetCurrentDirectory()}\" && git fetch");
        }

        private void bClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
