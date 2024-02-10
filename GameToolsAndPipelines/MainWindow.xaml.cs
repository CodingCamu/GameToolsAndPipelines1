using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Management.Automation;

namespace GameToolsAndPipelines
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Event handler for Add button
        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            // Execute 'git add' command for the specified folder or file
            string folderOrFile = ""; // Get the folder or file path from your UI elements
            ExecuteGitCommand($"add \"{folderOrFile}\"");
        }

        // Event handler for Commit button
        private void bCommit_Click(object sender, RoutedEventArgs e)
        {
            // Get message and author information from your UI elements
            string message = ""; // Get the commit message from your UI elements
            string authorInfo = ""; // Get the author information from your UI elements
            ExecuteGitCommand($"commit -m \"{message}\" --author=\"{authorInfo}\"");
        }

        // Event handler for Push button
        private void bPush_Click(object sender, RoutedEventArgs e)
        {
            // Execute 'git push' command
            ExecuteGitCommand("push");
        }

        // Event handler for Fetch button
        private void bFetch_Click(object sender, RoutedEventArgs e)
        {
            // Execute 'git fetch' command
            ExecuteGitCommand("fetch");
        }

        // Method to execute git commands
        // Method to execute git commands
        private void ExecuteGitCommand(string command)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string gitExecutable = "D:\\Code\\GameToolsAndPipelines"; // Assumes 'git' is in the system PATH

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = gitExecutable;
            startInfo.Arguments = command;
            startInfo.WorkingDirectory = currentDirectory;

            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            process.WaitForExit();

           
            if (process.ExitCode != 0)
            {
                string gitPath = @"C:\Program Files\Git\cmd\git.exe"; 
                startInfo.FileName = gitPath;
                process.StartInfo = startInfo;
                process.Start();
                output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
                process.WaitForExit();
            }
        }

    }
}
