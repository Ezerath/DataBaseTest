using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace DataBaseTest
{
    public partial class MainForm : Form
    {
        MaterialContext db;
        public MainForm()
        {
            InitializeComponent();
            db = new MaterialContext();
            db.Materials.Load();
            materialTable.DataSource = db.Materials.Local.ToBindingList();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            MaterialForm materialForm = new MaterialForm();
            DialogResult result = materialForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            Material material = new Material();
            material.Name = materialForm.nameBox.Text;
            material.Count = (int)materialForm.countBox.Value;
            material.Type = materialForm.typeBox.Text;
            db.Materials.Add(material);
            db.SaveChanges();
            MessageBox.Show("Материал добавлен в базу");
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (materialTable.SelectedRows.Count > 0)
            {
                int index = materialTable.SelectedRows[0].Index;
                bool converted = int.TryParse(materialTable[0, index].Value.ToString(), out int id);
                Material material = db.Materials.Find(id);
                db.Materials.Remove(material);
                db.SaveChanges();
            }
        }
    }
}
