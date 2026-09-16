namespace CustomerManagement.WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            nameLabel = new Label();
            emailLabel = new Label();
            statusLabel = new Label();
            nameTextBox = new TextBox();
            emailTextBox = new TextBox();
            addCustomerButton = new Button();
            deleteSelectedButton = new Button();
            customersDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)customersDataGridView).BeginInit();
            SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(30, 30);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(160, 30);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Customer name";
            // 
            // emailLabel
            // 
            emailLabel.AutoSize = true;
            emailLabel.Location = new Point(30, 130);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(140, 30);
            emailLabel.TabIndex = 1;
            emailLabel.Text = "Email address";
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(30, 577);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(133, 30);
            statusLabel.TabIndex = 2;
            statusLabel.Text = "Customers: 0";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(30, 70);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(450, 35);
            nameTextBox.TabIndex = 3;
            // 
            // emailTextBox
            // 
            emailTextBox.Location = new Point(30, 170);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(450, 35);
            emailTextBox.TabIndex = 4;
            // 
            // addCustomerButton
            // 
            addCustomerButton.Location = new Point(30, 240);
            addCustomerButton.Name = "addCustomerButton";
            addCustomerButton.Size = new Size(220, 50);
            addCustomerButton.TabIndex = 5;
            addCustomerButton.Text = "Add Customer";
            addCustomerButton.UseVisualStyleBackColor = true;
            // 
            // deleteSelectedButton
            // 
            deleteSelectedButton.Location = new Point(260, 240);
            deleteSelectedButton.Name = "deleteSelectedButton";
            deleteSelectedButton.Size = new Size(220, 50);
            deleteSelectedButton.TabIndex = 6;
            deleteSelectedButton.Text = "Delete Selected";
            deleteSelectedButton.UseVisualStyleBackColor = true;
            // 
            // customersDataGridView
            // 
            customersDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            customersDataGridView.BackgroundColor = SystemColors.Control;
            customersDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            customersDataGridView.Location = new Point(30, 312);
            customersDataGridView.Name = "customersDataGridView";
            customersDataGridView.RowHeadersWidth = 72;
            customersDataGridView.Size = new Size(910, 262);
            customersDataGridView.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 616);
            Controls.Add(customersDataGridView);
            Controls.Add(deleteSelectedButton);
            Controls.Add(addCustomerButton);
            Controls.Add(emailTextBox);
            Controls.Add(nameTextBox);
            Controls.Add(statusLabel);
            Controls.Add(emailLabel);
            Controls.Add(nameLabel);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Customer Management";
            ((System.ComponentModel.ISupportInitialize)customersDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label nameLabel;
        private Label emailLabel;
        private Label statusLabel;
        private TextBox nameTextBox;
        private TextBox emailTextBox;
        private Button addCustomerButton;
        private Button deleteSelectedButton;
        private DataGridView customersDataGridView;
    }
}
