//Developer:            Conner Henry
//Description:          A program used to edit and create text files, with the capability to save and open new ones.
//Date:                 October 9th, 2016
//File:                 MainWindow.xaml.cs
//Assignment:           Assignment 2 for Windows and Mobile Development
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;


namespace Assignment_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Declare variables
        string FileName = @"*.txt"; //File Name
        string OriginalText = "";   //Original text, used to decide if user has edited text at all

        public MainWindow()
        {
            InitializeComponent();
        }

        //Update status bar with the character count
        private void UserText_TextChanged(object sender, TextChangedEventArgs e)
        {
            CharacterCount.Content = UserText.Text.Length;
        }


        //Event used for opening a file
        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            //Checks SavePrompt, incase the user should possibly save before opening a new file
            SavePrompt();
            //Create OpenFileDialog window to open a new file
            OpenFileDialog OpenFileWindow = new OpenFileDialog();
            OpenFileWindow.FileName = @"*.txt";
            //Set up filter
            OpenFileWindow.Filter = "Text Files|*.txt|All Files|*.*";
            //If the user opened a new file, load data into textbox and set text as OrignalText
            if (OpenFileWindow.ShowDialog()==true)
            {

                UserText.Text = File.ReadAllText(OpenFileWindow.FileName);
                OriginalText = UserText.Text;

            }
            //Set file name as the new opened one
            FileName = OpenFileWindow.FileName;
        }


        //Event used for saving the current text as a file
        private void MenuItem_SaveAs(object sender, RoutedEventArgs e)
        {
            //Create and open a save file dialog window
            SaveFileDialog SaveFileWindow = new SaveFileDialog();
            //Set the file name as the current file name, if a file is already opened and being saved, it shall show this name first
            SaveFileWindow.FileName = FileName;
            //Set up filters
            SaveFileWindow.Filter = "Text Files|*.txt|All Files|*.*";
            //If the user saved the file, write data to the file
            if (SaveFileWindow.ShowDialog()==true)
            {
                File.WriteAllText(SaveFileWindow.FileName, UserText.Text);
                FileName = SaveFileWindow.FileName;
            }
        }

        //Closing the application
        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            //Call SavePrompt, to see if file should be saved
            SavePrompt();
            //Exit program
            Close();
        }

        //Start a new file
        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            //Check to see if save should be prompt
            SavePrompt();
            //Reset the filename, and empty the text box (UserText)
            FileName = @"*.txt";
            UserText.Text = "";
        }

        //Function:         SavePrompt
        //Purpose:          Call the Save_As option if the UserText text box has been edited at all
        private void SavePrompt()
        {
            if (OriginalText != UserText.Text)
            {
                if (MessageBox.Show("Would you like to save your current progress?", "Save Prompt", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MenuItem_SaveAs(null, null);
                }
            }
        }

           
        //Event for when wordwrap is clicked on
        private void MenuItem_WordWrap(object sender, RoutedEventArgs e)
        {
            //Enable wordwrap if it is checked
            if (WordWrap.IsChecked == true)
            {
                UserText.TextWrapping = TextWrapping.Wrap;
                return;
            }
            //Disable wordwrap if the option is not checked
            else
            {
                UserText.TextWrapping = TextWrapping.NoWrap;
                return;
            }
        }

        //About option under help
        private void MenuItem_About(object sender, RoutedEventArgs e)
        {
            //Once clicked on, show the about window
            AboutSetPad AboutWindow = new AboutSetPad();
            AboutWindow.ShowDialog();
        }
    }
}