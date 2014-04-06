// EventLogClearer.cs
// Written by Ryan Ries, September 2012
// This app clears event logs on remote systems

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;
using Microsoft.Win32;
using System.DirectoryServices;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace EventLogClearer
{
    public partial class MainForm : Form
    {        
        public static string computerToAddManually = string.Empty;
        static List<string> thread1Computers = new List<string>();
        static List<string> thread2Computers = new List<string>();
        static int threadsFinished = 0;
        static System.Security.SecureString securePW = new System.Security.SecureString();
        
        public MainForm()
        {
            InitializeComponent();
            this.autoPopulateButton.MouseEnter += autoPopulateButton_MouseEnter;
            this.autoPopulateButton.MouseLeave += button_MouseLeave;
            this.clearListButton.MouseEnter += clearListButton_MouseEnter;
            this.clearListButton.MouseLeave += button_MouseLeave;
            this.removeComputerButton.MouseEnter += removeComputerButton_MouseEnter;
            this.removeComputerButton.MouseLeave += button_MouseLeave;
            this.addComputerButton.MouseEnter += addComputerButton_MouseEnter;
            this.addComputerButton.MouseLeave += button_MouseLeave;
            this.computersListBox.MouseEnter += computersListBox_MouseEnter;
            this.computersListBox.MouseLeave += button_MouseLeave;
            this.goButton.MouseEnter += goButton_MouseEnter;
            this.goButton.MouseLeave += button_MouseLeave;
            this.statusMessagesListBox.MouseEnter += statusMessagesListBox_MouseEnter;
            this.statusMessagesListBox.MouseLeave += button_MouseLeave;
            this.logTypesGroupBox.MouseEnter += logTypesGroupBox_MouseEnter;
            this.logTypesGroupBox.MouseLeave += button_MouseLeave;
            this.linkLabel1.MouseEnter += linkLabel1_MouseEnter;
            this.linkLabel1.MouseLeave += button_MouseLeave;
            this.helpLinkLabel.MouseEnter += helpLinkLabel_MouseEnter;
            this.helpLinkLabel.MouseLeave += button_MouseLeave;
            this.usernameTextBox.MouseEnter += usernameTextBox_MouseEnter;
            this.usernameTextBox.MouseLeave += button_MouseLeave;
            this.passwordTextBox.MouseEnter += passwordTextBox_MouseEnter;
            this.passwordTextBox.MouseLeave += button_MouseLeave;
            this.usernameTextBox.GotFocus += usernameTextBox_GotFocus;
            this.passwordTextBox.GotFocus += passwordTextBox_GotFocus;
            this.Disposed += MainForm_Disposed;
        }

        void MainForm_Disposed(object sender, EventArgs e)
        {
            try
            {
                RegistryKey myotherpcisacloudRegKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Myotherpcisacloud.com");
                RegistryKey eventLogClearerRegKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Myotherpcisacloud.com\\EventLogClearer");
                eventLogClearerRegKey.Close();
                myotherpcisacloudRegKey.Close();
                eventLogClearerRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Myotherpcisacloud.com\\EventLogClearer", true);
                string computers = string.Empty;
                foreach (string comp in computersListBox.Items)
                    computers += comp + ",";
                if(computers.EndsWith(","))
                    computers = computers.Remove(computers.Length - 1);
                eventLogClearerRegKey.SetValue("Computers", computers);
                string logs = string.Empty;
                if (applicationEventLogCheckBox.Checked)
                    logs += "Application,";
                if (securityEventLogCheckBox.Checked)
                    logs += "Security,";
                if (setupEventLogCheckBox.Checked)
                    logs += "Setup,";
                if (systemEventLogCheckBox.Checked)
                    logs += "System,";
                if (appsAndSvcsLogsCheckBox.Checked)
                    logs += "AppsAndSvcs,";
                if(logs.EndsWith(","))
                    logs = logs.Remove(logs.Length - 1);
                eventLogClearerRegKey.SetValue("Logs", logs);
                eventLogClearerRegKey.Close();
            }
            catch
            {
                // No point in doing a MessageBox here since it seems they will not be displayed while the main form is being disposed.
                //MessageBox.Show("There was a problem saving your settings to the registry:\n\n" + ex.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.evti;
            this.Text = Application.ProductName + " v" + Application.ProductVersion;
            usernameTextBox.Text = "username";
            usernameTextBox.ForeColor = System.Drawing.Color.Gray;
            passwordTextBox.Text = "password";
            passwordTextBox.ForeColor = System.Drawing.Color.Gray;
            try
            {                
                RegistryKey eventLogClearerRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Myotherpcisacloud.com\\EventLogClearer");
                string computers = eventLogClearerRegKey.GetValue("Computers").ToString();
                foreach (string comp in eventLogClearerRegKey.GetValue("Computers").ToString().Split(','))
                    if(comp.Length > 0)
                        computersListBox.Items.Add(comp);
                foreach (string log in eventLogClearerRegKey.GetValue("Logs").ToString().Split(','))
                {
                    if (log.ToLower().Trim() == "application")
                        applicationEventLogCheckBox.Checked = true;
                    if (log.ToLower().Trim() == "security")
                        securityEventLogCheckBox.Checked = true;
                    if (log.ToLower().Trim() == "setup")
                        setupEventLogCheckBox.Checked = true;
                    if (log.ToLower().Trim() == "system")
                        systemEventLogCheckBox.Checked = true;
                    if (log.ToLower().Trim() == "appsandsvcs")
                        appsAndSvcsLogsCheckBox.Checked = true;
                }
                eventLogClearerRegKey.Close();
                if (computers.Length > 2048)
                    statusMessagesListBox.Items.Add("You have a large list of computers stored in the registry.");
            }
            catch { }
            if (computersListBox.Items.Count > 0)
            {
                computersListBox.SelectedIndex = 0;
                removeComputerButton.Enabled = true;
            }
            if(computersListBox.Items.Count > 0 && (applicationEventLogCheckBox.Checked || securityEventLogCheckBox.Checked || setupEventLogCheckBox.Checked || systemEventLogCheckBox.Checked || appsAndSvcsLogsCheckBox.Checked))
                goButton.Enabled = true;
            else
                goButton.Enabled = false;
        }

        private void autoPopulateButton_Click(object sender, EventArgs e)
        {
            statusMessagesListBox.Items.Add("Connecting to Active Directory...");
            BackgroundWorker populateFromADWorker = new BackgroundWorker();
            populateFromADWorker.WorkerReportsProgress = true;
            populateFromADWorker.ProgressChanged += new ProgressChangedEventHandler(populateFromADWorker_ProgressChanged);
            populateFromADWorker.DoWork += new DoWorkEventHandler(populateFromADWorker_DoWork);
            populateFromADWorker.RunWorkerAsync();             
        }

        void populateFromADWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int i = 0;
                List<string> computerNames = new List<string>();
                DirectoryEntry domain;
                if (usernameTextBox.Text.Length > 0 && usernameTextBox.Text != "username")
                {
                    ((BackgroundWorker)sender).ReportProgress(0, "Using username " + usernameTextBox.Text + ".");
                    domain = new DirectoryEntry("LDAP://" + Environment.UserDomainName, usernameTextBox.Text, passwordTextBox.Text);
                }
                else
                {
                    domain = new DirectoryEntry("LDAP://" + Environment.UserDomainName);
                }                
                DirectorySearcher searcher = new DirectorySearcher(domain);
                searcher.Filter = ("(objectClass=computer)");
                searcher.SizeLimit = int.MaxValue;
                searcher.PageSize = int.MaxValue;
                foreach (SearchResult resEnt in searcher.FindAll())
                {
                    if (resEnt.GetDirectoryEntry().Name.ToUpper().StartsWith("CN="))
                        computerNames.Add(resEnt.GetDirectoryEntry().Name.Remove(0, "CN=".Length));
                }
                searcher.Dispose();
                domain.Dispose();
                foreach (string comp in computerNames)
                {
                    bool alreadyExists = false;
                    foreach (string x in computersListBox.Items)
                    {
                        if (comp.Trim().ToUpper() == x.Trim().ToUpper())
                            alreadyExists = true;
                    }
                    if (!alreadyExists)
                    {
                        i += 1;
                        ((BackgroundWorker)sender).ReportProgress(1, comp);                        
                    }
                }
                ((BackgroundWorker)sender).ReportProgress(0, i.ToString() + " computers added from Active Directory.");
                
            }
            catch (Exception ex)
            {
                ((BackgroundWorker)sender).ReportProgress(0, "An error occured while getting computers from AD: " + ex.Message);
            }
        }

        void populateFromADWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage == 1)
                computersListBox.Items.Add(e.UserState.ToString());
            else
                statusMessagesListBox.Items.Add(e.UserState.ToString());

            if (computersListBox.Items.Count > 0 && (applicationEventLogCheckBox.Checked || securityEventLogCheckBox.Checked || setupEventLogCheckBox.Checked || systemEventLogCheckBox.Checked || appsAndSvcsLogsCheckBox.Checked))
                goButton.Enabled = true;
            else
                goButton.Enabled = false;

            if (computersListBox.Items.Count > 0)
            {
                computersListBox.SelectedIndex = computersListBox.Items.Count - 1;
                removeComputerButton.Enabled = true;
            }

            if(statusMessagesListBox.Items.Count > 0)
                statusMessagesListBox.SelectedIndex = statusMessagesListBox.Items.Count - 1;
        }

        void button_MouseLeave(object sender, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = " ";
        }

        private void clearListButton_Click(object sender, EventArgs e)
        {
            computersListBox.Items.Clear();
            removeComputerButton.Enabled = false;
            statusMessagesListBox.Items.Add("Computers list cleared.");
            goButton.Enabled = false;
            if (statusMessagesListBox.Items.Count > 0)
                statusMessagesListBox.SelectedIndex = statusMessagesListBox.Items.Count - 1;
        }

        void linkLabel1_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = "Visit me on the web!";
        }

        void logTypesGroupBox_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = "Select the Event Logs you want to clear.";
        }
        
        void statusMessagesListBox_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = "Status messages, error messages, and progress will be listed here after hitting the Go button.";
        }

        void autoPopulateButton_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = "Scan Active Directory as " + WindowsIdentity.GetCurrent().Name.ToString().ToLower() + " and populate the list with all computers found.";
        }

        void goButton_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = "Commit your changes and go.";
        }

        void clearListButton_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = "Clear all computers from the list.";
        }

        void removeComputerButton_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = "Removes the selected computer from the list.";
        }

        void addComputerButton_MouseEnter(object seonder, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = "Adds a computer to the list.";
        }

        void computersListBox_MouseEnter(object seonder, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = "These computers will have their event logs cleared.";
        }

        void helpLinkLabel_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = "Click for more detailed info.";
        }

        void usernameTextBox_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = "Leave this alone to run as currently logged on user. DO NOT specify domain name.";
        }

        void passwordTextBox_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel3.Text = "Leave this alone to run as currently logged on user. DO NOT specify domain name.";
        }

        private void goButton_Click(object sender, EventArgs e)        
        {
            threadsFinished = 0;
            thread1Computers.Clear();
            thread2Computers.Clear();
            securePW.Clear();
            statusMessagesListBox.Items.Add("Clearing event logs on " + computersListBox.Items.Count + " computers...");
            if (usernameTextBox.Text.Length > 0 && usernameTextBox.Text != "username")
            {                
                statusMessagesListBox.Items.Add("Using alternate user " + usernameTextBox.Text + ".");
                foreach (char c in passwordTextBox.Text)
                    securePW.AppendChar(c);
            }
            autoPopulateButton.Enabled = false;
            removeComputerButton.Enabled = false;
            addComputerButton.Enabled = false;
            clearListButton.Enabled = false;
            applicationEventLogCheckBox.Enabled = false;
            securityEventLogCheckBox.Enabled = false;
            setupEventLogCheckBox.Enabled = false;
            systemEventLogCheckBox.Enabled = false;
            appsAndSvcsLogsCheckBox.Enabled = false;
            goButton.Enabled = false;

            // Divide the work in half so two threads can do half at the same time
            List<string> allComputers = new List<string>();
            foreach (string s in computersListBox.Items)
                allComputers.Add(s);
            int i = 0;
            do
            {
                if (i == 0)
                {
                    i = 1;
                    thread1Computers.Add(allComputers[allComputers.Count - 1]);
                    allComputers.RemoveAt(allComputers.Count - 1);
                }
                else
                {
                    i = 0;
                    thread2Computers.Add(allComputers[allComputers.Count - 1]);
                    allComputers.RemoveAt(allComputers.Count - 1);
                }
            }
            while (allComputers.Count() > 0);

            BackgroundWorker thread1 = new BackgroundWorker();
            thread1.WorkerReportsProgress = true;
            thread1.ProgressChanged += new ProgressChangedEventHandler(EventLogThread_ProgressChanged);
            thread1.DoWork += new DoWorkEventHandler(thread1_DoWork);
            thread1.RunWorkerAsync();

            BackgroundWorker thread2 = new BackgroundWorker();
            thread2.WorkerReportsProgress = true;
            thread2.ProgressChanged += new ProgressChangedEventHandler(EventLogThread_ProgressChanged);
            thread2.DoWork += new DoWorkEventHandler(thread2_DoWork);
            thread2.RunWorkerAsync();
        }

        void thread1_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (string computer in thread1Computers)
            {
                if (applicationEventLogCheckBox.Checked)
                {
                    var eventLog = new EventLogSession();
                    try
                    {                        
                        if (securePW.Length > 0)                        
                            eventLog = new EventLogSession(computer, Environment.UserDomainName, usernameTextBox.Text, securePW, SessionAuthentication.Negotiate);                        
                        else                        
                            eventLog = new EventLogSession(computer);                        
                        eventLog.ClearLog("Application");
                        eventLog.Dispose();
                        ((BackgroundWorker)sender).ReportProgress(0, "Application log cleared on " + computer + ".");
                    }
                    catch (Exception ex)
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Failed to clear Application log on " + computer + ": " + ex.Message);
                    }
                }
                if (securityEventLogCheckBox.Checked)
                {
                    var eventLog = new EventLogSession();
                    try
                    {
                        if (securePW.Length > 0)
                            eventLog = new EventLogSession(computer, Environment.UserDomainName, usernameTextBox.Text, securePW, SessionAuthentication.Negotiate);
                        else
                            eventLog = new EventLogSession(computer);
                        eventLog.ClearLog("Security");
                        eventLog.Dispose();
                        ((BackgroundWorker)sender).ReportProgress(0, "Security log cleared on " + computer + ".");
                    }
                    catch (Exception ex)
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Failed to clear Security log on " + computer + ": " + ex.Message);
                    }
                }
                if (setupEventLogCheckBox.Checked)
                {
                    var eventLog = new EventLogSession();
                    try
                    {
                        if (securePW.Length > 0)
                            eventLog = new EventLogSession(computer, Environment.UserDomainName, usernameTextBox.Text, securePW, SessionAuthentication.Negotiate);
                        else
                            eventLog = new EventLogSession(computer);
                        eventLog.ClearLog("Setup");
                        eventLog.Dispose();
                        ((BackgroundWorker)sender).ReportProgress(0, "Setup log cleared on " + computer + ".");
                    }
                    catch (Exception ex)
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Failed to clear Setup log on " + computer + ": " + ex.Message);
                    }
                }
                if (systemEventLogCheckBox.Checked)
                {
                    var eventLog = new EventLogSession();
                    try
                    {
                        if (securePW.Length > 0)
                            eventLog = new EventLogSession(computer, Environment.UserDomainName, usernameTextBox.Text, securePW, SessionAuthentication.Negotiate);
                        else
                            eventLog = new EventLogSession(computer);
                        eventLog.ClearLog("System");
                        eventLog.Dispose();
                        ((BackgroundWorker)sender).ReportProgress(0, "System log cleared on " + computer + ".");
                    }
                    catch (Exception ex)
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Failed to clear System log on " + computer + ": " + ex.Message);
                    }
                }
                if (appsAndSvcsLogsCheckBox.Checked)
                {
                    var eventLog = new EventLogSession();
                    try
                    {
                        if (securePW.Length > 0)
                            eventLog = new EventLogSession(computer, Environment.UserDomainName, usernameTextBox.Text, securePW, SessionAuthentication.Negotiate);
                        else
                            eventLog = new EventLogSession(computer);
                        foreach (string log in eventLog.GetLogNames())
                        {
                            if (!(log.ToLower() == "application" || log.ToLower() == "setup" || log.ToLower() == "system" || log.ToLower() == "security"))
                            {
                                eventLog.ClearLog(log);
                                ((BackgroundWorker)sender).ReportProgress(0, log + " log cleared on " + computer + ".");
                            }
                        }
                        eventLog.Dispose();
                    }
                    catch (Exception ex)
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Failed to clear misc. log on " + computer + ": " + ex.Message);
                    }
                }
            }
            ((BackgroundWorker)sender).ReportProgress(0, "Thread 1 finished.");
        }

        void thread2_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (string computer in thread2Computers)
            {
                if (applicationEventLogCheckBox.Checked)
                {
                    var eventLog = new EventLogSession();
                    try
                    {
                        if (securePW.Length > 0)
                            eventLog = new EventLogSession(computer, Environment.UserDomainName, usernameTextBox.Text, securePW, SessionAuthentication.Negotiate);
                        else
                            eventLog = new EventLogSession(computer);
                        eventLog.ClearLog("Application");
                        eventLog.Dispose();
                        ((BackgroundWorker)sender).ReportProgress(0, "Application log cleared on " + computer + ".");
                    }
                    catch (Exception ex)
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Failed to clear Application log on " + computer + ": " + ex.Message);
                    }
                }
                if (securityEventLogCheckBox.Checked)
                {
                    var eventLog = new EventLogSession();
                    try
                    {
                        if (securePW.Length > 0)
                            eventLog = new EventLogSession(computer, Environment.UserDomainName, usernameTextBox.Text, securePW, SessionAuthentication.Negotiate);
                        else
                            eventLog = new EventLogSession(computer);
                        eventLog.ClearLog("Security");
                        eventLog.Dispose();
                        ((BackgroundWorker)sender).ReportProgress(0, "Security log cleared on " + computer + ".");
                    }
                    catch (Exception ex)
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Failed to clear Security log on " + computer + ": " + ex.Message);
                    }
                }
                if (setupEventLogCheckBox.Checked)
                {
                    var eventLog = new EventLogSession();
                    try
                    {
                        if (securePW.Length > 0)
                            eventLog = new EventLogSession(computer, Environment.UserDomainName, usernameTextBox.Text, securePW, SessionAuthentication.Negotiate);
                        else
                            eventLog = new EventLogSession(computer);
                        eventLog.ClearLog("Setup");
                        eventLog.Dispose();
                        ((BackgroundWorker)sender).ReportProgress(0, "Setup log cleared on " + computer + ".");
                    }
                    catch (Exception ex)
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Failed to clear Setup log on " + computer + ": " + ex.Message);
                    }
                }
                if (systemEventLogCheckBox.Checked)
                {
                    var eventLog = new EventLogSession();
                    try
                    {
                        if (securePW.Length > 0)
                            eventLog = new EventLogSession(computer, Environment.UserDomainName, usernameTextBox.Text, securePW, SessionAuthentication.Negotiate);
                        else
                            eventLog = new EventLogSession(computer);
                        eventLog.ClearLog("System");
                        eventLog.Dispose();
                        ((BackgroundWorker)sender).ReportProgress(0, "System log cleared on " + computer + ".");
                    }
                    catch (Exception ex)
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Failed to clear System log on " + computer + ": " + ex.Message);
                    }
                }
                if (appsAndSvcsLogsCheckBox.Checked)
                {
                    var eventLog = new EventLogSession();
                    try
                    {
                        if (securePW.Length > 0)
                            eventLog = new EventLogSession(computer, Environment.UserDomainName, usernameTextBox.Text, securePW, SessionAuthentication.Negotiate);
                        else
                            eventLog = new EventLogSession(computer);
                        foreach (string log in eventLog.GetLogNames())
                        {
                            if (!(log.ToLower() == "application" || log.ToLower() == "setup" || log.ToLower() == "system" || log.ToLower() == "security"))
                            {
                                eventLog.ClearLog(log);
                                ((BackgroundWorker)sender).ReportProgress(0, log + " log cleared on " + computer + ".");
                            }
                        }
                        eventLog.Dispose();
                    }
                    catch (Exception ex)
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Failed to clear misc. log on " + computer + ": " + ex.Message);
                    }
                }
            }
            ((BackgroundWorker)sender).ReportProgress(0, "Thread 2 finished.");
        }

        void EventLogThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusMessagesListBox.Items.Add(e.UserState.ToString());
            statusMessagesListBox.SelectedIndex = statusMessagesListBox.Items.Count - 1;
            if (e.UserState.ToString() == "Thread 1 finished.")
                threadsFinished++;
            if (e.UserState.ToString() == "Thread 2 finished.")
                threadsFinished++;
            if (threadsFinished > 1)
            {                
                autoPopulateButton.Enabled = true;
                removeComputerButton.Enabled = true;
                addComputerButton.Enabled = true;
                clearListButton.Enabled = true;
                applicationEventLogCheckBox.Enabled = true;
                securityEventLogCheckBox.Enabled = true;
                setupEventLogCheckBox.Enabled = true;
                systemEventLogCheckBox.Enabled = true;
                appsAndSvcsLogsCheckBox.Enabled = true;
                goButton.Enabled = true;                
                statusMessagesListBox.Items.Add("All work completed.");
                statusMessagesListBox.Items.Add("---------------------------------------------");
                computersListBox.SelectedIndex = computersListBox.Items.Count - 1;
            }
            statusMessagesListBox.SelectedIndex = statusMessagesListBox.Items.Count - 1;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.myotherpcisacloud.com");
        }

        private void addComputerButton_Click(object sender, EventArgs e)
        {
            AddComputerForm addComputerForm = new AddComputerForm();
            if (addComputerForm.ShowDialog(this) == DialogResult.OK)
            {
                // Wait for the pop up to complete
            }
            addComputerForm.Dispose();
            bool computerAlreadyExists = false;
            foreach (string comp in computersListBox.Items)
            {               
                if (comp.ToUpper().Trim() == computerToAddManually.ToUpper().Trim())
                    computerAlreadyExists = true;
            }

            if (computerAlreadyExists)
            {
                MessageBox.Show("That computer is already in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (computerToAddManually.Length > 0)
            {
                computersListBox.Items.Add(computerToAddManually);
                statusMessagesListBox.Items.Add(computerToAddManually + " added to list.");
                if (computersListBox.Items.Count > 0)
                {
                    computersListBox.SelectedIndex = computersListBox.Items.Count - 1;
                    removeComputerButton.Enabled = true;
                }
            }
            computerToAddManually = string.Empty;
            if (computersListBox.Items.Count > 0 && (applicationEventLogCheckBox.Checked || securityEventLogCheckBox.Checked || setupEventLogCheckBox.Checked || systemEventLogCheckBox.Checked || appsAndSvcsLogsCheckBox.Checked))
                goButton.Enabled = true;
            else
                goButton.Enabled = false;
            if (statusMessagesListBox.Items.Count > 0)
                statusMessagesListBox.SelectedIndex = statusMessagesListBox.Items.Count - 1;
        }

        private void removeComputerButton_Click(object sender, EventArgs e)
        {
            statusMessagesListBox.Items.Add("Removing " + computersListBox.SelectedItem.ToString() + ".");
            computersListBox.Items.Remove(computersListBox.SelectedItem);

            if (computersListBox.Items.Count < 1)
                removeComputerButton.Enabled = false;
            if (computersListBox.Items.Count > 0)
                computersListBox.SelectedIndex = computersListBox.Items.Count - 1;
            if (computersListBox.Items.Count > 0 && (applicationEventLogCheckBox.Checked || securityEventLogCheckBox.Checked || setupEventLogCheckBox.Checked || systemEventLogCheckBox.Checked || appsAndSvcsLogsCheckBox.Checked))
                goButton.Enabled = true;
            else
                goButton.Enabled = false;
            if (statusMessagesListBox.Items.Count > 0)
                statusMessagesListBox.SelectedIndex = statusMessagesListBox.Items.Count - 1;
        }

        private void applicationEventLogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (computersListBox.Items.Count > 0 && (applicationEventLogCheckBox.Checked || securityEventLogCheckBox.Checked || setupEventLogCheckBox.Checked || systemEventLogCheckBox.Checked || appsAndSvcsLogsCheckBox.Checked))
                goButton.Enabled = true;
            else
                goButton.Enabled = false;
        }

        private void securityEventLogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (computersListBox.Items.Count > 0 && (applicationEventLogCheckBox.Checked || securityEventLogCheckBox.Checked || setupEventLogCheckBox.Checked || systemEventLogCheckBox.Checked || appsAndSvcsLogsCheckBox.Checked))
                goButton.Enabled = true;
            else
                goButton.Enabled = false;
        }

        private void setupEventLogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (computersListBox.Items.Count > 0 && (applicationEventLogCheckBox.Checked || securityEventLogCheckBox.Checked || setupEventLogCheckBox.Checked || systemEventLogCheckBox.Checked || appsAndSvcsLogsCheckBox.Checked))
                goButton.Enabled = true;
            else
                goButton.Enabled = false;
        }

        private void systemEventLogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (computersListBox.Items.Count > 0 && (applicationEventLogCheckBox.Checked || securityEventLogCheckBox.Checked || setupEventLogCheckBox.Checked || systemEventLogCheckBox.Checked || appsAndSvcsLogsCheckBox.Checked))
                goButton.Enabled = true;
            else
                goButton.Enabled = false;
        }

        private void appsAndSvcsLogsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (computersListBox.Items.Count > 0 && (applicationEventLogCheckBox.Checked || securityEventLogCheckBox.Checked || setupEventLogCheckBox.Checked || systemEventLogCheckBox.Checked || appsAndSvcsLogsCheckBox.Checked))
                goButton.Enabled = true;
            else
                goButton.Enabled = false;
        }

        private void helpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HelpAboutForm helpAboutForm = new HelpAboutForm();
            if (helpAboutForm.ShowDialog(this) == DialogResult.OK)
            {
                // Wait for the pop up to complete
            }
            helpAboutForm.Dispose();
        }

        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        void usernameTextBox_GotFocus(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "username")
            {
                usernameTextBox.Text = string.Empty;
                usernameTextBox.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        void passwordTextBox_GotFocus(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "password")
            {
                passwordTextBox.Text = string.Empty;
                passwordTextBox.ForeColor = System.Drawing.Color.Black;
                passwordTextBox.UseSystemPasswordChar = true;
            }
        }
    }
}
