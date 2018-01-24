using System;

namespace CwCodeLib.FileHandlers
{
    /// <summary>
    /// Presents as CSV file as DataRow (one row at a time)
    /// </summary>
    /// <remarks>Remarks</remarks>
    internal class StreamedCSVReader : IDisposable
    {
        private System.IO.StreamReader fso;

        private string delimiter = ",";

        private bool allowMultiLineQuotes = true;

        // To detect redundant calls
        private bool disposedValue = false;

        public StreamedCSVReader(string sFilename)
            : this()
        {
            this.fso = new System.IO.StreamReader(sFilename);
        }

        public StreamedCSVReader(System.IO.Stream stream)
            : this()
        {
            this.fso = new System.IO.StreamReader(stream);
        }

        public StreamedCSVReader(string sFilename, System.Text.Encoding encoding)
            : this()
        {
            this.fso = new System.IO.StreamReader(sFilename, encoding);
        }

        public StreamedCSVReader(System.IO.Stream stream, System.Text.Encoding encoding)
            : this()
        {
            this.fso = new System.IO.StreamReader(stream, encoding);
        }

        private StreamedCSVReader()
        {
        }

        public string Delimiter
        {
            get { return this.delimiter; }
            set { this.delimiter = value; }
        }

        public bool AllowMultiLineQuotes
        {
            get { return this.allowMultiLineQuotes; }
            set { this.allowMultiLineQuotes = value; }
        }

        public string[] ReadLine()
        {
            string line = null;
            string[] items = null;

            long check = this.fso.BaseStream.Position;

            if (this.fso != null)
            {
                line = this.fso.ReadLine();

                if (line != null)
                {
                    // convert the line (CSV) into an array of items - allowing quoted identifiers
                    System.Text.RegularExpressions.Regex rgx_quotID = new System.Text.RegularExpressions.Regex(string.Format("[^\"{0}]?\"(?<mainpart>[^\"]*?{0}*[^\"]*?)\"[^\"{0}]?", "[\\^$.|?*+()".Contains(this.Delimiter) ? "\\" + this.Delimiter : this.Delimiter), System.Text.RegularExpressions.RegexOptions.Multiline);

                    if (this.AllowMultiLineQuotes)
                    {
                        // make sure we have a full line (incase of quoted CrLf breaking the fso.ReadLine) - i.e. if there are any " then there should be an equal number of them
                        while (System.Text.RegularExpressions.Regex.Matches(line, "[\"]").Count % 2 != 0)
                        {
                            // uneven number, line continuation?
                            line += "\r\n" + this.fso.ReadLine();
                        }
                    }

                    // first removed the quoted commas
                    string testLine = line;
                    foreach (System.Text.RegularExpressions.Match rgxMatch in rgx_quotID.Matches(testLine))
                    {
                        line = line.Remove(rgxMatch.Groups["mainpart"].Index, rgxMatch.Groups["mainpart"].Length);
                        line = line.Insert(rgxMatch.Groups["mainpart"].Index, rgxMatch.Groups["mainpart"].Value.Replace(this.Delimiter, ((char)255).ToString()));
                    }

                    // swap out escaped quotes
                    line = line.Replace("\"\"", ((char)254).ToString());

                    // remove identifiers
                    line = line.Replace("\"", string.Empty);

                    // swap in escaped quotes
                    line = line.Replace(((char)254).ToString(), "\"");

                    items = line.Split(Convert.ToChar(this.Delimiter));

                    // swap the delimiter back in
                    for (int i = 0; i <= items.Length - 1; i++)
                    {
                        items[i] = items[i].Replace(((char)255).ToString(), this.Delimiter);
                    }
                }
            }

            return items;
        }

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.CloseStreamReader();
                }
            }

            this.disposedValue = true;
        }

        private void CloseStreamReader()
        {
            if (this.fso != null)
            {
                this.fso.Close();
                this.fso.Dispose();
                this.fso = null;
            }
        }
    }
}