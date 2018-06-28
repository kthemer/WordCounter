using System;
using System.Collections.Generic;
using System.Linq;
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
            txtKeywords.Focus();
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
        #endregion ControlEvents

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
