using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordCounterTest
{
    public partial class Main : Form
    {
        WordCounter.WordCounter _wordCounter;

        public Main()
        {
            try
            {
                InitializeComponent();

                _wordCounter = new WordCounter.WordCounter();

                ClearForm();
            }
            catch (Exception e)
            {
                ShowError(e.ToString());
            }
        }

        /// <summary>Resets the form to a starting state</summary>
        private void ClearForm()
        {
            txtKeywords.Clear();
            txtURL.Clear();
            lblTest.Text = "Enter a comma-delimited list (no spaces) of keywords and a valid URL, then click Test.";
            ClearThreadLabels();
            txtKeywords.Focus();
        }

        /// <summary>Resets only the Thread labels</summary>
        private void ClearThreadLabels()
        {
            lblThread1.Text = string.Empty;
            lblThread2.Text = string.Empty;
            lblThread3.Text = string.Empty;
            lblThread4.Text = string.Empty;
        }

        /// <summary>Displays an error message in a MessageBox</summary>
        /// <param name="msg">The error details</param>
        private void ShowError(string msg)
        {
            MessageBox.Show(string.Format("An error occurred.  Details: {0}", msg), 
                "WordCounter", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #region ControlEvents
        private void cmdTest_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> keywords = GetKeywords();
                string url = GetURL();
                int count = _wordCounter.GetKeywordCount(keywords, url);

                DisplayResults(count, keywords.Count);
            }
            catch (Exception ex)
            {
                ShowError(ex.ToString());

                ClearForm();
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void cmdMultiTest_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> keywords = GetKeywords();
                string url = GetURL();

                var multithread = WordCounterWithDelayAsync(keywords, url);
            }
            catch (Exception ex)
            {
                ShowError(ex.ToString());

                ClearForm();
            }
        }
        #endregion ControlEvents

        #region AsyncMethods
        /// <summary>Creates four threads and makes the same WordCounter call with each</summary>
        /// <param name="keywords">The list of keywords to search for in the URL</param>
        /// <param name="url">A URL pointing to a local file resource or a web resource</param>
        /// <returns>A Task</returns>
        private async Task WordCounterWithDelayAsync(List<string> keywords, string url)
        {   // Create four threads to make the same call.
            // Add async delays to force the threads to work simultaneously.
            ClearThreadLabels();

            int count1 = _wordCounter.GetKeywordCount(keywords, url);
            await Task.Delay(1000);

            int count2 = _wordCounter.GetKeywordCount(keywords, url);
            await Task.Delay(1000);

            int count3 = _wordCounter.GetKeywordCount(keywords, url);
            await Task.Delay(1000);

            int count4 = _wordCounter.GetKeywordCount(keywords, url);
            await Task.Delay(1000);

            lblTest.Text = "Multi-threaded test results displayed below.";
            lblThread1.Text = string.Format("Thread 1 result: {0} occurrences of the keywords found", count1.ToString());
            lblThread2.Text = string.Format("Thread 2 result: {0} occurrences of the keywords found", count2.ToString());
            lblThread3.Text = string.Format("Thread 3 result: {0} occurrences of the keywords found", count3.ToString());
            lblThread4.Text = string.Format("Thread 4 result: {0} occurrences of the keywords found", count4.ToString());
        }
        #endregion AsyncMethods

        /// <summary>Gets the list of keywords entered into the keywords textbox</summary>
        /// <returns>A list of keywords in List format</returns>
        private List<string> GetKeywords()
        {
            List<string> keywords = new List<string>();
            if (txtKeywords.Text.Trim().Length > 0)
            {
                keywords = txtKeywords.Text.Trim().Split(',').ToList<string>();
            }

            return keywords;
        }

        /// <summary>Gets the value entered into the URL textbox</summary>
        /// <returns>The URL</returns>
        private string GetURL()
        {
            return txtURL.Text.Trim();
        }

        /// <summary>Displays the results of a successful WordCounter search</summary>
        /// <param name="hitCount">The number of keyword matches in the search</param>
        /// <param name="keywordCount">The number of keywords</param>
        private void DisplayResults(int hitCount, int keywordCount)
        {
            lblTest.Text = string.Format("{0} occurrence{1} of {2} {3} found in this URL.",
                    hitCount.ToString(), (hitCount == 1 ? string.Empty : "s"),
                    (keywordCount == 1 ? "this keyword" : "these keywords"),
                    (hitCount == 1 ? "was" : "were"));
        }
    }
}
