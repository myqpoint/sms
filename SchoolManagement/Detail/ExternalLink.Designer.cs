namespace Debono
{
    partial class ExternalLink
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExternalLink));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gridExternLink = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deletetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gvExternalLink = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repItmBtnExtLink = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GC_Icon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repIcon = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.gridColumn97 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn96 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GC_PAth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LinkFileType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView12 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridExternLink)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvExternalLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItmBtnExtLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridExternLink
            // 
            this.gridExternLink.ContextMenuStrip = this.contextMenuStrip1;
            this.gridExternLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridExternLink.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridExternLink.Location = new System.Drawing.Point(0, 0);
            this.gridExternLink.MainView = this.gvExternalLink;
            this.gridExternLink.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridExternLink.Name = "gridExternLink";
            this.gridExternLink.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit1,
            this.repositoryItemLookUpEdit2,
            this.repItmBtnExtLink,
            this.repIcon});
            this.gridExternLink.Size = new System.Drawing.Size(691, 305);
            this.gridExternLink.TabIndex = 68;
            this.gridExternLink.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvExternalLink});
            this.gridExternLink.Click += new System.EventHandler(this.gridExternLink_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deletetoolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
            // 
            // deletetoolStripMenuItem
            // 
            this.deletetoolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deletetoolStripMenuItem.Image")));
            this.deletetoolStripMenuItem.Name = "deletetoolStripMenuItem";
            this.deletetoolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deletetoolStripMenuItem.Text = "Delete";
            this.deletetoolStripMenuItem.Click += new System.EventHandler(this.deletetoolStripMenuItem_Click);
            // 
            // gvExternalLink
            // 
            this.gvExternalLink.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvExternalLink.Appearance.GroupRow.Options.UseFont = true;
            this.gvExternalLink.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvExternalLink.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvExternalLink.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvExternalLink.Appearance.Row.Options.UseFont = true;
            this.gvExternalLink.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn1,
            this.GC_Icon,
            this.gridColumn97,
            this.gridColumn96,
            this.GC_PAth,
            this.LinkFileType});
            this.gvExternalLink.GridControl = this.gridExternLink;
            this.gvExternalLink.Name = "gvExternalLink";
            this.gvExternalLink.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvExternalLink.OptionsView.ColumnAutoWidth = false;
            this.gvExternalLink.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvExternalLink.OptionsView.RowAutoHeight = true;
            this.gvExternalLink.OptionsView.ShowAutoFilterRow = true;
            this.gvExternalLink.OptionsView.ShowGroupPanel = false;
            this.gvExternalLink.DoubleClick += new System.EventHandler(this.gvExternalLink_DoubleClick);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "FileName";
            this.gridColumn4.ColumnEdit = this.repItmBtnExtLink;
            this.gridColumn4.FieldName = "LinkFile";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 200;
            // 
            // repItmBtnExtLink
            // 
            this.repItmBtnExtLink.AutoHeight = false;
            this.repItmBtnExtLink.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "Delete", 40, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, false)});
            this.repItmBtnExtLink.Name = "repItmBtnExtLink";
            this.repItmBtnExtLink.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repItmBtnExtLink_ButtonClick);
            this.repItmBtnExtLink.Click += new System.EventHandler(this.repItmBtnExtLink_Click);
            this.repItmBtnExtLink.DoubleClick += new System.EventHandler(this.repItmBtnExtLink_DoubleClick);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Description";
            this.gridColumn5.FieldName = "LinkDescription";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 198;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Last Update";
            this.gridColumn1.FieldName = "LastUpdate";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.OptionsColumn.TabStop = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            // 
            // GC_Icon
            // 
            this.GC_Icon.Caption = "Icon";
            this.GC_Icon.ColumnEdit = this.repIcon;
            this.GC_Icon.FieldName = "icon";
            this.GC_Icon.Name = "GC_Icon";
            this.GC_Icon.OptionsColumn.ReadOnly = true;
            this.GC_Icon.Visible = true;
            this.GC_Icon.VisibleIndex = 3;
            // 
            // repIcon
            // 
            this.repIcon.Name = "repIcon";
            this.repIcon.Padding = new System.Windows.Forms.Padding(2);
            this.repIcon.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.repIcon.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.repIcon_EditValueChanging);
            this.repIcon.Click += new System.EventHandler(this.repIcon_Click);
            this.repIcon.DoubleClick += new System.EventHandler(this.repIcon_DoubleClick);
            // 
            // gridColumn97
            // 
            this.gridColumn97.Caption = "OfferId";
            this.gridColumn97.FieldName = "FormId";
            this.gridColumn97.Name = "gridColumn97";
            // 
            // gridColumn96
            // 
            this.gridColumn96.Caption = "ExternalLinkId";
            this.gridColumn96.FieldName = "ExernalId";
            this.gridColumn96.Name = "gridColumn96";
            // 
            // GC_PAth
            // 
            this.GC_PAth.Caption = "Path";
            this.GC_PAth.FieldName = "Path";
            this.GC_PAth.Name = "GC_PAth";
            // 
            // LinkFileType
            // 
            this.LinkFileType.Caption = "LinkFileType";
            this.LinkFileType.FieldName = "LinkFileType";
            this.LinkFileType.Name = "LinkFileType";
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.DisplayMember = "MaterialId";
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.NullText = "";
            this.repositoryItemGridLookUpEdit1.ValueMember = "MaterialId";
            this.repositoryItemGridLookUpEdit1.View = this.gridView12;
            // 
            // gridView12
            // 
            this.gridView12.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView12.Name = "gridView12";
            this.gridView12.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView12.OptionsView.ShowAutoFilterRow = true;
            this.gridView12.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.DisplayMember = "MaterialNo";
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            this.repositoryItemLookUpEdit2.ValueMember = "MaterialId";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 305);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(691, 70);
            this.panelControl1.TabIndex = 69;
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Image = global::DEBONO.Properties.Resources.btn_Save;
            this.btnOk.Location = new System.Drawing.Point(300, 8);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(111, 50);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Save";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ExternalLink
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 375);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.gridExternLink);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ExternalLink";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Upload Document";
            this.Activated += new System.EventHandler(this.ExternalLink_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExternalLink_FormClosing);
            this.Load += new System.EventHandler(this.ExternalLink_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridExternLink)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvExternalLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItmBtnExtLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl gridExternLink;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvExternalLink;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repItmBtnExtLink;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn96;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn97;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView12;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deletetoolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn GC_Icon;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repIcon;
        private DevExpress.XtraGrid.Columns.GridColumn GC_PAth;
        private DevExpress.XtraGrid.Columns.GridColumn LinkFileType;

    }
}