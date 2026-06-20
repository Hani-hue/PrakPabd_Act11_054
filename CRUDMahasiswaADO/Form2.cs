using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRUDMahasiswaADO
{
    public partial class Form2 : Form
    {
        static string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=DBAkademikADO;" +
            "User ID=sa;" +
            "Password=11nov2004;" +
            "TrustServerCertificate=True";

        SqlConnection conn = new SqlConnection(connectionString);


        string prodi { get; set; }
        DateTime tglmasuk { get; set; }

        public Form2(string Prodi, DateTime TglMasuk)
        {
            InitializeComponent();

            prodi = Prodi;
            tglmasuk = TglMasuk;

            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("sp_Report", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@inProdi", prodi);
                cmd.Parameters.AddWithValue("@inTglMsuk", tglmasuk.Year);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                conn.Close();

                MessageBox.Show("Rows = " + dt.Rows.Count);

                ListMahasiswa rpt = new ListMahasiswa();

                rpt.SetDataSource(dt);

                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}