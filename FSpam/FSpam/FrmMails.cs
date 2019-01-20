using FSpam.classifier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSpam
{
    public partial class FrmMails : Form
    {
        private List<string> titles;
        private List<string> summaries;
        private List<string> senders;
        private BayesClassifier bayesClassifier;
        DataTable dt = new DataTable();

        public FrmMails(List<string> titl, List<string> summary, List<string> authors)
        {
            InitializeComponent();

            titles = titl;
            summaries = summary;
            senders = authors;
            bayesClassifier = new BayesClassifier();
        }

        private void FrmMails_Load(object sender, EventArgs e)
        {
            dt.Columns.Add(new DataColumn("Pošiljatelj", typeof(string)));
            dt.Columns.Add(new DataColumn("Naslov", typeof(string)));   
            dt.Columns.Add(new DataColumn("Bayes vrijednost", typeof(float)));
            dt.Columns.Add(new DataColumn("SPAM", typeof(string))); //- DA ILI NE
         
            for(int i=0; i<titles.Count; i++)
            {
                DataRow row = dt.NewRow();
                Tuple<double, string> tuple = bayesClassifier.CalculateProbabilityOfTokens(summaries[i].Split(' ').ToList());

                row["Pošiljatelj"] = senders[i];
                row["Naslov"] = titles[i];
                row["Bayes vrijednost"] = tuple.Item1;
                row["SPAM"] = tuple.Item2;

                dt.Rows.Add(row);
            }
            

            dgvMails.DataSource = dt;
            dgvMails.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvMails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvMails.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            foreach (DataGridViewRow row in dgvMails.Rows)
            {
                if(row.Cells[3].Value.ToString() == "DA")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void dgvMails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRowIndex = dgvMails.CurrentCell.RowIndex;

            MessageBox.Show(summaries[selectedRowIndex], "Dodatne informacije");
        }
    }
}
