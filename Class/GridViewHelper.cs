using DevExpress.Utils.Menu;
using DevExpress.Utils;
using DevExpress.XtraEditors.ColorPick.Picker;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualStudio2019;

namespace QuanLyBanHang.Class
{
    internal class GridViewHelper
    {
        public static string TEMP_PATH = Path.GetTempPath() + Assembly.GetExecutingAssembly().GetName().Name + "\\";
        public static void SaveAndRestoreLayout(GridControl gridControl, string FormName)
        {
            gridControl.ForceInitialize();
            var path_layout_default = TEMP_PATH + Form_Template.TaiKhoanLogin + "\\" + FormName + "\\" + gridControl.Name + "\\" + "default_layout.xml";
            var folder = TEMP_PATH + Form_Template.TaiKhoanLogin + "\\" + FormName + "\\" + gridControl.Name;
            if (!Directory.Exists(folder))
            {
                DirectoryInfo di = Directory.CreateDirectory(folder);
            }
            if (!File.Exists(path_layout_default))
            {
                gridControl.MainView.SaveLayoutToXml(path_layout_default, OptionsLayoutBase.FullLayout);
            }
            var path = TEMP_PATH + Form_Template.TaiKhoanLogin + "\\" + FormName + "\\" + gridControl.Name + "\\custom_layout.xml";
            if (File.Exists(path))
            {
                gridControl.ForceInitialize();
                gridControl.MainView.RestoreLayoutFromXml(path, OptionsLayoutBase.FullLayout);
            }
        }

        public static void NotContainItem_Click(object sender, EventArgs e, GridView gridView)
        {
            string filter = gridView.ActiveFilterString;
            string value = (sender as DXMenuItem).Caption.Split('\'')[1];
            string newFilterString = "Not Contains([" + gridView.FocusedColumn.FieldName + "], '" + value + "')";

            if (filter == String.Empty)
                gridView.ActiveFilterString = newFilterString;
            else
                gridView.ActiveFilterString += "And " + newFilterString;
        }

        public static void ContainItem_Click(object sender, EventArgs e, GridView gridView)
        {
            string filter = gridView.ActiveFilterString;
            string value = (sender as DXMenuItem).Caption.Split('\'')[1];
            string newFilterString = "Contains([" + gridView.FocusedColumn.FieldName + "], '" + value + "')";

            if (filter == String.Empty)
                gridView.ActiveFilterString = newFilterString;
            else
                gridView.ActiveFilterString += "And " + newFilterString;
        }

        public static void GridView_ShownEditor(object sender, EventArgs e, GridView gridView)
        {
            DXMenuItem containItem;
            DXMenuItem notContainItem;
            containItem = new DXMenuItem();
            containItem.Click += (ss, ee) => { ContainItem_Click(ss, ee, gridView); };
            notContainItem = new DXMenuItem();
            notContainItem.Click += (ss, ee) => { NotContainItem_Click(ss, ee, gridView); };

            if (gridView.ActiveEditor is TextEdit)
            {
                var editor = gridView.ActiveEditor as TextEdit;
                editor.Properties.BeforeShowMenu += (ss, ee) => { Properties_BeforeShowMenu(ss, ee, containItem, notContainItem); };
            }
        }

        public static void ClearMenu(DXPopupMenu menu)
        {
            foreach (DXMenuItem item in menu.Items)
                if (item.Caption.Contains("Filter by"))
                    item.Visible = false;
        }

        public static bool initializeMenu = true;
        public static void Properties_BeforeShowMenu(object sender, DevExpress.XtraEditors.Controls.BeforeShowMenuEventArgs e, DXMenuItem containItem, DXMenuItem notContainItem)
        {
            ClearMenu(e.Menu);
            string text = (sender as TextEdit).SelectedText;

            if (text != String.Empty)
            {
                containItem.Caption = "Filter by contains '" + text + "'";
                notContainItem.Caption = "Filter by not contains '" + text + "'";
                containItem.Visible = true;
                notContainItem.Visible = true;

                initializeMenu = false;
                e.Menu.Items.Add(containItem);
                e.Menu.Items.Add(notContainItem);

            }
        }

        public static void GridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e, GridControl gridControl, GridView gridView)
        {
            if (!gridView.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                    gridControl.BeginInvoke(new MethodInvoker(delegate { cal(_Width, gridView); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1));
                SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                gridControl.BeginInvoke(new MethodInvoker(delegate { cal(_Width, gridView); }));
            }
        }

        public static bool cal(Int32 _Width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _Width ? _Width : _View.IndicatorWidth;
            return true;
        }

        public static Image createImage(Color color)
        {
            Bitmap flag = new Bitmap(16, 16);
            Graphics flagGraphics = Graphics.FromImage(flag);
            Pen blackPen = new Pen(Color.Black, 2);
            Rectangle rect = new Rectangle(0, 0, 16, 16);
            flagGraphics.DrawRectangle(blackPen, rect);
            flagGraphics.FillRectangle(new SolidBrush(color), 1, 1, 14, 14);
            return flag;
        }

      

        private static void GridView_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {

            e.Info.Caption = string.Empty;
            e.Painter.DrawObject(e.Info);

            e.Handled = true;
            // DrawEditorHelper.DrawColumnInplaceEditor(e, _Item, EditValue, GetRightIndent());
            //DrawEditorHelper.DrawColumnInplaceEditor(e, _Item, _Column.Caption, GetRightIndent());

        }

        //Create a menu item 
        public static DXMenuCheckItem CreateCheckItem(string caption, GridColumn column, FixedStyle style, Color color)
        {
            Image image = createImage(color);
            DXMenuCheckItem item = new DXMenuCheckItem(caption, column.Fixed == style, image, new EventHandler(OnFixedClick));
            item.Tag = new MenuInfo(column, style);
            return item;
        }

        //Menu item click handler 
        public static void OnFixedClick(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            MenuInfo info = item.Tag as MenuInfo;
            if (info == null) return;

            if (item.Caption.Substring(0, 3) == "Col")
            {
                if (item.Caption == "Color Customize...")
                {
                    ColorPickEdit colorPickerEdit = new ColorPickEdit();
                    FrmColorPicker frm = new FrmColorPicker(colorPickerEdit.Properties);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.TopMost = true;
                    if (frm.ShowDialog(colorPickerEdit.FindForm()) == DialogResult.OK)
                    {
                        info.Column.AppearanceCell.BackColor = frm.SelectedColor;
                    }

                }
                else
                {
                    info.Column.AppearanceCell.BackColor = ((Bitmap)item.Image).GetPixel(5, 5);
                }
            }
            else if (item.Caption.Substring(0, 4) == "Font")
            {
                FontDialog fontDialog = new FontDialog();
                fontDialog.ShowDialog();
                info.Column.AppearanceCell.Font = fontDialog.Font;
            }
            else
            {
                if (item.Caption == "ForeColor Customize...")
                {
                    ColorPickEdit colorPickerEdit = new ColorPickEdit();
                    FrmColorPicker frm = new FrmColorPicker(colorPickerEdit.Properties);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.TopMost = true;
                    if (frm.ShowDialog(colorPickerEdit.FindForm()) == System.Windows.Forms.DialogResult.OK)
                    {
                        info.Column.AppearanceCell.ForeColor = frm.SelectedColor;
                    }

                }
                else
                {
                    info.Column.AppearanceCell.ForeColor = ((Bitmap)item.Image).GetPixel(5, 5);
                }
            }


        }
        class MenuInfo
        {
            public MenuInfo(GridColumn column, FixedStyle style)
            {
                this.Column = column;
                this.Style = style;
            }
            public FixedStyle Style;
            public GridColumn Column;
        }

        //private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    gridView2.SaveLayoutToXml("data.xml", OptionsLayoutBase.FullLayout);
        //}
    }
}
