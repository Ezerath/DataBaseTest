using System;
using System.Data.Entity;
using System.Windows.Forms;

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

        private void SetUpDataGrid()
        {
            materialTable.Columns[0].Name = "Id";
            materialTable.Columns[1].Name = "Наименование";
            materialTable.Columns[2].Name = "Кол-во";
            materialTable.Columns[3].Name = "Тип материала";
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddMaterialForm materialForm = new AddMaterialForm();
            DialogResult result = materialForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            Material material = new Material();
            SetMaterialProperty(materialForm, material);
            db.Materials.Add(material);
            db.SaveChanges();
            MessageBox.Show("Материал добавлен в базу");
        }

        private static void SetMaterialProperty(AddMaterialForm materialForm, Material material)
        {
            material.Name = materialForm.nameBox.Text;
            material.Count = (int)materialForm.countBox.Value;
            material.Type = materialForm.typeBox.Text;
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (materialTable.SelectedRows.Count > 0)
            {
                Material material = FindMaterialById();
                db.Materials.Remove(material);
                db.SaveChanges();
            }
        }

        private Material FindMaterialById()
        {
            int materialId = GetMaterialId();
            Material material = db.Materials.Find(materialId);
            return material;
        }

        private int GetMaterialId()
        {
            int index = materialTable.SelectedRows[0].Index;
            int.TryParse(materialTable[0, index].Value.ToString(), out int id);
            return id;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (materialTable.SelectedRows.Count == 1)
            {
                int materialId = GetMaterialId();
                Material material = db.Materials.Find(materialId);
                AddMaterialForm materialForm = new AddMaterialForm();
                materialForm.nameBox.Text = material.Name;
                materialForm.countBox.Value = material.Count;
                materialForm.typeBox.Text = material.Type;
                DialogResult result = materialForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                SetMaterialProperty(materialForm, material);
                db.Materials.Add(material);
                db.SaveChanges();
                materialTable.Refresh();
            }
        }
    }
}
