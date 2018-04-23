using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
                private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Number", typeof(int));
            tbl.Columns.Add("Date", typeof(DateTime));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) });
            return tbl;
        }
        

        public Form1()
        {
            InitializeComponent();
            gridControl1.DataSource = CreateTable(20);
            new HotTrackCellHelper(gridView1);
        }
    }


    public class HotTrackCellHelper
    {
        
        public HotTrackCellHelper(GridView view)
        {
            _View = view;
            view.GridControl.Paint += new PaintEventHandler(GridControl_Paint);
            view.MouseMove += new MouseEventHandler(view_MouseMove);
        }


        private int _BorderWidth = 4;
        private GridCell _HotTrackedCell;
        private readonly GridView _View;
        public GridCell HotTrackedCell
        {
            get { return _HotTrackedCell; }
            set 
            {
                RefreshCell(_HotTrackedCell);
                _HotTrackedCell = value;
            }
        }

        public Rectangle GetCellBounds(GridCell cell)
        {
            if (cell == null)
                return Rectangle.Empty;
            GridViewInfo info = _View.GetViewInfo() as GridViewInfo;
            GridCellInfo cellInfo = info.GetGridCellInfo(cell.RowHandle, cell.Column);
            return cellInfo.Bounds;
        }


        private void UpdateHotTrackedCell(Point location)
        {
            GridHitInfo hi = _View.CalcHitInfo(location);
            if (hi.HitTest == GridHitTest.Row || hi.HitTest == GridHitTest.RowEdge)
                return;
            if (hi.InRowCell)
            {
                if (_View.IsRowVisible(hi.RowHandle) == RowVisibleState.Visible)
                {
                    HotTrackedCell = new GridCell(hi.RowHandle, hi.Column);
                    return;
                }
            }
            HotTrackedCell = null;
        }

    
        void view_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateHotTrackedCell(e.Location);
        }


        private void RefreshCell(GridCell cell)
        {
            if (cell == null)
                return;
            Rectangle rect = GetCellBounds(cell);
            rect.Inflate(_BorderWidth, _BorderWidth);
            _View.InvalidateRect(rect);
        }

        void GridControl_Paint(object sender, PaintEventArgs e)
        {
            DrawHotTrackedCell(e);
        }

        private void DrawHotTrackedCell(PaintEventArgs e)
        {
            Rectangle bounds = GetCellBounds(HotTrackedCell);
            e.Graphics.DrawRectangle(new Pen(Brushes.Black, _BorderWidth), bounds);
        }
    }
}