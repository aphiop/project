using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    internal class Article
    {
        public string PublicationID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string PageNumber { get; set; }

        public Article(string publicationID, string title, string author, string pageNumber)
        {
            PublicationID = publicationID;
            Title = title;
            Author = author;
            PageNumber = pageNumber;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    internal class Issuance
    {
        public string PublicationID { get; set; }
        public string ReaderID { get; set; }
        public string IssueDate { get; set; }
        public string ReturnDate { get; set; }
        public string RealReturnDate { get; set; }

        public Issuance(string publicationID, string readerId, string issueDate, string returnDate, string realReturnDate)
        {
            PublicationID = publicationID;
            ReaderID = readerId;
            IssueDate = issueDate;
            ReturnDate = returnDate;
            RealReturnDate = realReturnDate;
        }
    }
}
namespace WinFormsApp3
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormLogin());
        }
    }
}using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    internal class Publication
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Identifier { get; set; }
        public string Theme { get; set; }
        public string PublishingHouse { get; set; }

        public Publication(string title, string author, string identifier, string theme, string publishingHouse)
        {
            Title = title;
            Author = author;
            Identifier = identifier;
            Theme = theme;
            PublishingHouse = publishingHouse;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    internal class UserSession
    {
        public static string UserRole { get; private set; }

        public static void SetUserRole(string role)
        {
            UserRole = role;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    internal class Reader
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PassportNumber { get; set; }
        public string IsDeactivated { get; set; }

        public Reader(string fullName, string address, string phone, string passportNumber, string isDeactivated)
        {
            FullName = fullName;
            Address = address;
            Phone = phone;
            PassportNumber = passportNumber;
            IsDeactivated = isDeactivated;
        }
    }
}
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    namespace library
    {
        internal class Operations
        {
            public static SqlConnection GetConnection()
            {
                string sql = "Data Source=localhost\\MSSQLSERVER03;Initial Catalog=LibraryDB;Integrated Security=True;TrustServerCertificate=true";
                SqlConnection conn = new SqlConnection(sql);
                try
                {
                    conn.Open();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL з'єднання! \n" + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return conn;
            }

            public static void ManagePublication(Publication b, string sql, string? id = null)
            {
                SqlConnection conn = GetConnection();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                if (id != null)
                {
                    cmd.Parameters.Add("@PublicationID", SqlDbType.VarChar).Value = id;
                }
                cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = b.Title;
                cmd.Parameters.Add("@Author", SqlDbType.VarChar).Value = b.Author;
                cmd.Parameters.Add("@Identifier", SqlDbType.VarChar).Value = b.Identifier;
                cmd.Parameters.Add("@Theme", SqlDbType.VarChar).Value = b.Theme;
                cmd.Parameters.Add("@PublishingHouse", SqlDbType.VarChar).Value = b.PublishingHouse;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Помилка SQL-з'єднання! \n" + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }

            public static void ManageReader(Reader r, string sql, string? id = null)
            {
                SqlConnection conn = GetConnection();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                if (id != null)
                {
                    cmd.Parameters.Add("@ReaderID", SqlDbType.VarChar).Value = id;
                }
                cmd.Parameters.Add("@FullName", SqlDbType.VarChar).Value = r.FullName;
                cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = r.Address;
                cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = r.Phone;
                cmd.Parameters.Add("@PassportNumber", SqlDbType.VarChar).Value = r.PassportNumber;
                cmd.Parameters.Add("@IsDeactivated", SqlDbType.VarChar).Value = r.IsDeactivated;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Помилка SQL-з'єднання! \n" + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }

            public static void ManageIssuance(Issuance i, string sql, string? id = null)
            {
                SqlConnection conn = GetConnection();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                if (id != null)
                {
                    cmd.Parameters.Add("@IssuanceID", SqlDbType.VarChar).Value = id;
                }
                cmd.Parameters.Add("@PublicationID", SqlDbType.VarChar).Value = i.PublicationID;
                cmd.Parameters.Add("@ReaderID", SqlDbType.VarChar).Value = i.ReaderID;
                cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = DateTime.Parse(i.IssueDate);
                cmd.Parameters.Add("@ReturnDate", SqlDbType.DateTime).Value = DateTime.Parse(i.ReturnDate);
                cmd.Parameters.Add("@RealReturnDate", SqlDbType.DateTime).Value = i.RealReturnDate != null ? (object)DateTime.Parse(i.RealReturnDate) : DBNull.Value;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Помилка SQL-з'єднання! \n" + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }

            public static void ManageArticle(Article a, string sql, string? id = null)
            {
                SqlConnection conn = GetConnection();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                if (id != null)
                {
                    cmd.Parameters.Add("@ArticleID", SqlDbType.VarChar).Value = id;
                }
                cmd.Parameters.Add("@PublicationID", SqlDbType.VarChar).Value = a.PublicationID;
                cmd.Parameters.Add("@Title ", SqlDbType.VarChar).Value = a.Title;
                cmd.Parameters.Add("@Author ", SqlDbType.VarChar).Value = a.Author;
                cmd.Parameters.Add("@PageNumber ", SqlDbType.VarChar).Value = a.PageNumber;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Помилка SQL-з'єднання! \n" + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }

            public static void Delete(string? id, string? cl, string? table)
            {
                string sql = $"DELETE FROM {table} WHERE {cl} = @Id";
                SqlConnection conn = GetConnection();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Не видалено. \n" + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }

            public static void DisplayAndSearch(string query, DataGridView dgv)
            {
                string sql = query;
                SqlConnection conn = GetConnection();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable tbl= new DataTable();
                adp.Fill(tbl);
                dgv.DataSource = tbl;
                conn.Close();
            }

            public static void Clear(Control parent)
            {
                foreach (Control c in parent.Controls)
                {
                    if (c is TextBox)
                    {
                        ((TextBox)c).Text = string.Empty;
                    }
                    else if (c.HasChildren)
                    {
                        Clear(c);
                    }
                }
            }
        }
    }
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace library
{
    public partial class FormArticles : Form
    {
        private readonly FormPublications _parent;
        FormNewArticle article;
        public string sql = "SELECT  FROM Articles WHERE PublicationID = @PublicationID;";
        private string currentPublicationId;

        public FormArticles(FormPublications parent)
        {
            InitializeComponent();
            _parent = parent;
            article = new FormNewArticle(this);
            btnAdd.Visible = false;
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
            }
            ConfigureButtons();
            ConfigureComboBox();
        }

        private void ConfigureButtons()
        {
            string role = UserSession.UserRole;
            bool isAdmin = role == "адмін";

            btnAdd.Visible = isAdmin;
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].Visible = isAdmin;
                dataGridView1.Columns[1].Visible = isAdmin;
            }
        }

        private void ConfigureComboBox()
        {
            cmbFilter.Items.Add("ІД видання");
            cmbFilter.Items.Add("Автор");
            cmbFilter.Items.Add("Назва");
            cmbFilter.SelectedIndex = -1;
        }

        public void LoadArticlesByPublication(string publicationId)
        {
            currentPublicationId = publicationId;
            Display();
        }

        public void Display()
        {
            string query = sql.Replace("@PublicationID", currentPublicationId);
            Operations.DisplayAndSearch(query, dataGridView1);
        }

        private void FormArticles_Shown(object sender, EventArgs e)
        {
            Display();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            article.DefaultInfo(currentPublicationId);
            article.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                article.id = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                article.publicationID = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                article.title = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                article.author = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                article.pageNumber = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                article.UpdateInfo();
                article.ShowDialog();
                return;
            }
            if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("Дійсно видалити?", "Інформація", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Operations.Delete(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(), "ArticleID", "Articles");
                    Display();
                }
                return;
            }
            if (e.ColumnIndex == 2)
            {
                string annotation = dataGridView1.Rows[e.RowIndex].Cells["Annotation"].Value.ToString();
                richTextBox1.Text = annotation;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (cmbFilter.SelectedIndex == -1)
            {
                MessageBox.Show("Виберіть критерій пошуку", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string filterBy = cmbFilter.SelectedItem.ToString();
            string query = sql.Replace("@PublicationID", currentPublicationId);

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                switch (filterBy)
                {
                    case "ІД видання":
                        query += " AND PublicationID IN (SELECT PublicationID FROM Publications WHERE Theme LIKE '%" + txtSearch.Text + "%')";
                        break;
                    case "Автор":
                        query += " AND Author LIKE '%" + txtSearch.Text + "%'";
                        break;
                    case "Назва":
                        query += " AND Title LIKE '%" + txtSearch.Text + "%'";
                        break;
                }
            }

            Operations.DisplayAndSearch(query, dataGridView1);
        }
    }
}
using Microsoft.VisualBasic;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace library
{
    public partial class FormDashboard : Form
    {
        FormNewIssuance issuance;
        FormReaders readers;
        FormPublications books;

        public string sql = "select  from issuance";

        public FormDashboard()
        {
            InitializeComponent();
            issuance = new FormNewIssuance(this);
            readers = new FormReaders(this);
            books = new FormPublications(this);
            btnAdd.Visible = false;
            if (dataGridView.Columns.Count > 0)
            {
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[1].Visible = false;
            }
            ConfigureButtons();
        }

        private void ConfigureButtons()
        {
            string role = UserSession.UserRole;
            bool isAdmin = role == "адмін";

            btnAdd.Visible = isAdmin;
            if (dataGridView.Columns.Count > 0)
            {
                dataGridView.Columns[0].Visible = isAdmin;
                dataGridView.Columns[1].Visible = isAdmin;
            }
        }

        public void Display()
        {
            Operations.DisplayAndSearch(sql, dataGridView);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            issuance.DefaultInfo();
            issuance.ShowDialog();
        }

        private void FormDashboard_Shown(object sender, EventArgs e)
        {
            Display();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Operations.DisplayAndSearch(sql + " WHERE PublicationID LIKE '%" + txtSearch.Text + "%'", dataGridView);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                issuance.id = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                issuance.publication = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                issuance.reader = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                issuance.issueDate = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                issuance.returnDate = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                issuance.realReturnDate = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                issuance.UpdateInfo();
                issuance.ShowDialog();
                return;
            }
            if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("Дійсно видалити?", "Інформація", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Operations.Delete(dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), "IssuanceID", "Issuance");
                    Display();
                }
                return;
            }
        }

        private void btnReaders_Click(object sender, EventArgs e)
        {
            readers.ShowDialog();
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            books.ShowDialog();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using library;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using Microsoft.Data.SqlClient;

namespace WinFormsApp3
{
    public partial class FormLogin : Form
    {

        public FormLogin()
        {
            InitializeComponent();
        }

        private void checkBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShow.Checked)
            {
                textPassword.PasswordChar = '\0';
            }
            else
            {
                textPassword.PasswordChar = '•';
            }
        }


        private void label5_Click(object sender, EventArgs e)
        {
            new Register().Show();
            this.Hide();
        }

        private void buttonLogin_Click_1(object sender, EventArgs e)
        {
            SqlConnection conn = Operations.GetConnection();
            string loginQuery = "SELECT role FROM users WHERE login= @login AND password= @password";
            SqlCommand cmd = new SqlCommand(loginQuery, conn);
            cmd.Parameters.AddWithValue("@login", textUsername.Text);
            cmd.Parameters.AddWithValue("@password", textPassword.Text);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string? userRole = reader["role"].ToString();
                UserSession.SetUserRole(userRole);
                this.Hide();
                FormDashboard dashboard = new FormDashboard();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Неправильно введені дані!", "Вхід скасовано", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace library
{
    public partial class FormNewArticle : Form
    {
        private readonly FormArticles _parent;
        public string? id, title, author, pageNumber, publicationID;

        public FormNewArticle(FormArticles parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public void UpdateInfo()
        {
            textBox1.Text = title;
            textBox2.Text = author;
            textBox3.Text = pageNumber;
            label1.Text = "Редагування даних";
            btnAdd.Text = "Оновити";
        }

        public void DefaultInfo(string pubID)
        {
            Operations.Clear(this);
            publicationID = pubID;
            label1.Text = "Додавання даних";
            btnAdd.Text = "Зберегти";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length < 3)
            {
                MessageBox.Show("Назва статті має містити не менше 3 символів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(textBox2.Text.Trim(), "^[^0-9]+$") || textBox1.Text.Trim().Length < 4)
            {
                MessageBox.Show("Поле Автор не повинно містити цифри і повинно містити не менше 4 символів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Article b = new Article(publicationID, textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim());
            string insert = "INSERT INTO Articles (PublicationID, Title, Author, PageNumber) " +
                "VALUES (@PublicationID, @Title, @Author, @PageNumber);";
            string update = "UPDATE Articles SET PublicationID=@PublicationID, Title=@Title, Author=@Author, " +
                "PageNumber=@PageNumber WHERE ArticleID =@ArticleID;";
            if (btnAdd.Text == "Зберегти")
            {
                Operations.ManageArticle(b, insert);
            }
            if (btnAdd.Text == "Оновити")
            {
                Operations.ManageArticle(b, update, id);
            }
            Operations.Clear(this);
            _parent.Display();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace library
{
    public partial class FormNewIssuance : Form
    {
        private readonly FormDashboard _parent;
        public string? id, publication, reader, issueDate, returnDate, realReturnDate;

        public FormNewIssuance(FormDashboard parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public void UpdateInfo()
        {
            textBox5.Text = publication;
            textBox4.Text = reader;
            textBox1.Text = issueDate;
            textBox2.Text = returnDate;
            textBox3.Text = realReturnDate;
            label1.Text = "Редагування даних";
            btnAdd.Text = "Оновити";
        }

        public void DefaultInfo()
        {
            Operations.Clear(this);
            label1.Text = "Додавання даних";
            btnAdd.Text = "Зберегти";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Issuance i = new Issuance(textBox5.Text.Trim(), textBox4.Text.Trim(), textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim());
            string insert = "INSERT INTO Issuance (PublicationID, ReaderID, IssueDate, ReturnDate, RealReturnDate) " +
                "VALUES (@PublicationID, @ReaderID, @IssueDate, @ReturnDate, @RealReturnDate);";
            string update = "UPDATE Issuance SET PublicationID = @PublicationID, ReaderID = @ReaderID, IssueDate = @IssueDate, " +
                "ReturnDate = @ReturnDate, RealReturnDate = @RealReturnDate WHERE IssuanceID = @IssuanceID;";
            if (btnAdd.Text == "Зберегти")
            {
                Operations.ManageIssuance(i, insert);
            }
            if (btnAdd.Text == "Оновити")
            {
                Operations.ManageIssuance(i, update, id);
            }
            Operations.Clear(this);
            _parent.Display();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace library
{
    public partial class FormNewPublication : Form
    {
        private readonly FormPublications _parent;
        public string? id, title, author, identifier, theme, publishingHouse;

        public FormNewPublication(FormPublications parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public void UpdateInfo()
        {
            textBox1.Text = title;
            textBox2.Text = author;
            textBox3.Text = identifier;
            textBox4.Text = theme;
            textBox5.Text = publishingHouse;
            label1.Text = "Редагування даних";
            btnAdd.Text = "Оновити";
        }

        public void DefaultInfo()
        {
            Operations.Clear(this);
            label1.Text = "Додавання даних";
            btnAdd.Text = "Зберегти";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length < 4)
            {
                MessageBox.Show("Назва книги має містити не менше 4 символів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(textBox2.Text.Trim(), "^[^0-9]+$") || textBox1.Text.Trim().Length < 4)
            {
                MessageBox.Show("Поле Автор не повинно містити цифри і повинно містити не менше 4 символів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox3.Text.Trim().Length < 4)
            {
                MessageBox.Show("Ідентифікатор має містити не менше 4 символів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox4.Text.Trim().Length < 4)
            {
                MessageBox.Show("Тема має містити не менше 3 символів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox5.Text.Trim().Length < 4)
            {
                MessageBox.Show("Видавництво має містити не менше 3 символів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Publication b = new Publication(textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim());
            string insert = "INSERT INTO Publications  (Title, Author, Identifier, Theme, PublishingHouse) " +
                "VALUES (@Title, @Author, @Identifier, @Theme, @PublishingHouse);";
            string update = "UPDATE Publications  SET Title=@Title, Author=@Author, Identifier=@Identifier, " +
                "Theme=@Theme, PublishingHouse=@PublishingHouse WHERE PublicationID =@PublicationID ;";
            if (btnAdd.Text == "Зберегти")
            {
                Operations.ManagePublication(b, insert);
            }
            if (btnAdd.Text == "Оновити")
            {
                Operations.ManagePublication(b, update, id);
            }
            Operations.Clear(this);
            _parent.Display();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace library
{
    public partial class FormNewReader : Form
    {
        private readonly FormReaders _parent;
        public string? id, fullName, address, phone, passportNumber, isDeactivated;
        string phonePattern = @"^\+[0-9]{10,13}$";

        public FormNewReader(FormReaders parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public void UpdateInfo()
        {
            textBox1.Text = fullName;
            textBox2.Text = address;
            textBox3.Text = phone;
            textBox4.Text = passportNumber;
            textBox5.Text = isDeactivated;
            label1.Text = "Редагування даних";
            btnAdd.Text = "Оновити";
        }

        public void DefaultInfo()
        {
            Operations.Clear(this);
            label1.Text = "Додавання даних";
            btnAdd.Text = "Зберегти";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (!Regex.IsMatch(textBox3.Text.Trim(), phonePattern))
            {
                MessageBox.Show("Телефонний номер має починатися з '+' та містити від 10 до 13 цифр", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox4.Text.Trim().Length < 4)
            {
                MessageBox.Show("Паспортний номер має містити не менше 4 символів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox5.Text.Trim() != "1" && textBox5.Text.Trim() != "0" && textBox5.Text.Trim() != "True" && textBox5.Text.Trim() != "False")
            {
                MessageBox.Show("Відмітка про вибуття може містити значення 1 (так) або 0 (ні)", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Reader r = new Reader(textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim());
            string insert = "INSERT INTO Reader (FullName, Address, Phone, PassportNumber, IsDeactivated) " +
                "VALUES(@FullName, @Address, @Phone, @PassportNumber, @IsDeactivated);";
            string update = "UPDATE Reader SET FullName=@FullName, Address=@Address, Phone=@Phone, " +
                "PassportNumber=@PassportNumber, IsDeactivated=@IsDeactivated WHERE ReaderID=@ReaderID;";
            if (btnAdd.Text == "Зберегти")
            {
                Operations.ManageReader(r, insert);
            }
            if (btnAdd.Text == "Оновити")
            {
                Operations.ManageReader(r, update, id);
            }
            Operations.Clear(this);
            _parent.Display();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp3;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace library
{
    public partial class FormPublications : Form
    {
        private readonly FormDashboard _parent;
        FormNewPublication publication;
        FormArticles articles;
        public string sql = "SELECT B.PublicationID, B.Title, B.Author, B.Theme, B.Identifier, B.PublishingHouse, COUNT(I.PublicationID) AS TimesIssued " +
            "FROM Publications B LEFT JOIN Issuance I ON B.PublicationID = I.PublicationID GROUP BY B.PublicationID, B.Title, B.Author, B.Theme, B.Identifier, B.PublishingHouse;";

        public FormPublications(FormDashboard parent)
        {
            InitializeComponent();
            _parent = parent;
            publication = new FormNewPublication(this);
            articles = new FormArticles(this);

            btnAdd.Visible = false;
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
            }
            ConfigureButtons();
        }

        private void ConfigureButtons()
        {
            string role = UserSession.UserRole;
            bool isAdmin = role == "адмін";

            btnAdd.Visible = isAdmin;
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].Visible = isAdmin;
                dataGridView1.Columns[1].Visible = isAdmin;
            }
        }

        public void Display()
        {
            Operations.DisplayAndSearch(sql, dataGridView1);
        }

        private void FormPublications_Shown(object sender, EventArgs e)
        {
            Display();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Operations.DisplayAndSearch("SELECT B.PublicationID, B.Title, B.Author, B.Theme, B.Identifier, B.PublishingHouse, COUNT(I.PublicationID) " +
                "AS TimesIssued FROM Publications  B LEFT JOIN Issuance I ON B.PublicationID = I.PublicationID WHERE Title LIKE '%" + txtSearch.Text + "%' " +
                "GROUP BY B.PublicationID, B.Title, B.Author, B.Theme, B.Identifier, B.PublishingHouse", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                publication.id = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                publication.title = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                publication.author = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                publication.theme = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                publication.identifier = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                publication.publishingHouse = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                publication.UpdateInfo();
                publication.ShowDialog();
                return;
            }
            if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("Дійсно видалити?", "Інформація", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Operations.Delete(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(), "PublicationID", "Publications");
                    Display();
                }
                return;
            }
            if (e.ColumnIndex == 2)
            {
                string publicationId = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                articles.LoadArticlesByPublication(publicationId);
                articles.ShowDialog();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            publication.DefaultInfo();
            publication.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace library
{
    public partial class FormReaders : Form
    {
        private readonly FormDashboard _parent;
        FormNewReader reader;
        public string sql = "SELECT R.ReaderID, R.FullName, R.Address, R.Phone, R.PassportNumber, R.IsDeactivated, " +
            "COUNT(I.ReaderID) AS BooksTaken FROM Reader R LEFT JOIN Issuance I ON R.ReaderID = I.ReaderID GROUP BY " +
            "R.ReaderID, R.FullName, R.Address, R.Phone, R.PassportNumber, R.IsDeactivated";

        public FormReaders(FormDashboard parent)
        {
            InitializeComponent();
            _parent = parent;
            reader = new FormNewReader(this);
            btnAdd.Visible = false;
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
            }
            ConfigureButtons();
        }

        private void ConfigureButtons()
        {
            string role = UserSession.UserRole;
            bool isAdmin = role == "адмін";

            btnAdd.Visible = isAdmin;
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].Visible = isAdmin;
                dataGridView1.Columns[1].Visible = isAdmin;
            }
        }

        public void Display()
        {
            Operations.DisplayAndSearch(sql, dataGridView1);
        }

        private void FormReaders_Shown(object sender, EventArgs e)
        {
            Display();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Operations.DisplayAndSearch("SELECT R.ReaderID, R.FullName, R.Address, R.Phone, R.PassportNumber, R.IsDeactivated, COUNT(I.ReaderID) " +
                "AS BooksTaken FROM Reader R LEFT JOIN Issuance I ON R.ReaderID = I.ReaderID WHERE FullName LIKE '%" + txtSearch.Text + "%' " +
                "GROUP BY R.ReaderID, R.FullName, R.Address, R.Phone, R.PassportNumber, R.IsDeactivated ", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                reader.id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                reader.fullName = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                reader.address = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                reader.phone = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                reader.passportNumber = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                reader.isDeactivated = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                reader.UpdateInfo();
                reader.ShowDialog();
                return;
            }
            if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("Дійсно видалити?", "Інформація", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Operations.Delete(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), "ReaderID", "Reader");
                    Display();
                }
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            reader.DefaultInfo();
            reader.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using System.Data;
using System.Data.OleDb;
using library;
using Microsoft.Data.SqlClient;

namespace WinFormsApp3
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }


        private void buttonRegister_Click(object sender, EventArgs e)
        {
            SqlConnection conn = Operations.GetConnection();
            if (textUsername.Text == "" || textPassword.Text == "" || textConfirm.Text == "")
            {
                MessageBox.Show("Поля обов'язкові до заповнення!", "Реєстрацію скасовано", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textPassword.Text.Length < 4)
            {
                MessageBox.Show("Пароль має містити не менше 4 символів!", "Реєстрацію скасовано", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textRole.Text != "адмін" && textRole.Text != "користувач")
            {
                MessageBox.Show("Поле роль може містити значення адмін або користувач!", "Реєстрацію скасовано", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textPassword.Text == textConfirm.Text)
            {
                string register = "INSERT INTO users (login, password, role) VALUES ('" + textUsername.Text + "', '" + textPassword.Text + "', '" + textRole.Text + "')";
                SqlCommand cmd = new SqlCommand(register, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                textUsername.Text = "";
                textPassword.Text = "";
                textRole.Text = "";
                textConfirm.Text = "";

                MessageBox.Show("Обліковий запис створено!", "Реєстрація успішна", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Паролі не співпадають!", "Реєстрацію скасовано", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textPassword.Text = "";
                textConfirm.Text = "";
                textPassword.Focus();
            }
        }

        private void checkBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShow.Checked)
            {
                textPassword.PasswordChar = '\0';
                textConfirm.PasswordChar = '\0';
            }
            else
            {
                textPassword.PasswordChar = '•';
                textConfirm.PasswordChar = '•';
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FormLogin().Show();
        }
    }
}