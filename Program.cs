using System;
using System.Drawing;
using System.Windows.Forms;

namespace MDIApplication
{
    class MainForm : Form
    {
        MenuStrip menu;
        ToolStripMenuItem fileMenu, newForm, dialog, exit;

        public MainForm()
        {
            Text = "MDI Application";
            Width = 800;
            Height = 500;
            IsMdiContainer = true;

            menu = new MenuStrip();

            fileMenu = new ToolStripMenuItem("File");
            newForm = new ToolStripMenuItem("New Form");
            dialog = new ToolStripMenuItem("Custom Dialog");
            exit = new ToolStripMenuItem("Exit");

            // New Form
            newForm.Click += (s, e) =>
            {
                ChildForm f = new ChildForm();
                f.MdiParent = this;
                f.Show();
            };

            // Custom Dialog
            dialog.Click += (s, e) =>
            {
                CustomDialog d = new CustomDialog();

                if (d.ShowDialog() == DialogResult.OK)
                {
                    ChildForm f = new ChildForm();
                    f.MdiParent = this;
                    f.SetMessage(d.MessageText);
                    f.Show();
                }
            };

            // Exit
            exit.Click += (s, e) => Application.Exit();

            // Add Menu Items
            fileMenu.DropDownItems.Add(newForm);
            fileMenu.DropDownItems.Add(dialog);
            fileMenu.DropDownItems.Add(exit);

            menu.Items.Add(fileMenu);

            Controls.Add(menu);
        }
    }

    // Child Form
    class ChildForm : Form
    {
        Label label;

        public ChildForm()
        {
            Text = "Child Form";
            Width = 400;
            Height = 250;

            label = new Label();
            label.Text = "Welcome to MDI Application";
            label.AutoSize = true;
            label.Location = new Point(100, 80);

            Controls.Add(label);
        }

        public void SetMessage(string text)
        {
            label.Text = text;
        }
    }

    // Custom Dialog Box
    class CustomDialog : Form
    {
        TextBox textBox;

        public string MessageText
        {
            get
            {
                return textBox.Text;
            }
        }

        public CustomDialog()
        {
            Text = "Custom Dialog Box";
            Width = 400;
            Height = 180;
            StartPosition = FormStartPosition.CenterParent;

            Label label = new Label();
            label.Text = "Enter your message:";
            label.Location = new Point(30, 25);
            label.AutoSize = true;

            textBox = new TextBox();
            textBox.Location = new Point(30, 50);
            textBox.Width = 300;

            Button ok = new Button();
            ok.Text = "OK";
            ok.Location = new Point(170, 90);
            ok.DialogResult = DialogResult.OK;

            Button cancel = new Button();
            cancel.Text = "Cancel";
            cancel.Location = new Point(250, 90);
            cancel.DialogResult = DialogResult.Cancel;

            Controls.Add(label);
            Controls.Add(textBox);
            Controls.Add(ok);
            Controls.Add(cancel);

            AcceptButton = ok;
            CancelButton = cancel;
        }
    }

    // Main Program
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
