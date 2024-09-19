namespace Flat_UI
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Collections;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using Microsoft.VisualBasic.CompilerServices;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    internal class FormSkin : ContainerControl
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private Color _BaseLight;
        private Color _BorderColor;
        private Color _HeaderColor;
        private Color _HeaderLight;
        private bool _HeaderMaximize;
        private bool Cap;
        private int H;
        private Point MousePoint;
        private object MoveHeight;
        private Color TextColor;
        public Color TextLight;
        private int W;

        public FormSkin()
        {
            base.MouseDoubleClick += new MouseEventHandler(this.FormSkin_MouseDoubleClick);
            __ENCAddToList(this);
            this.Cap = false;
            this._HeaderMaximize = false;
            this.MousePoint = new Point(0, 0);
            this.MoveHeight = 50;
            this._HeaderColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._BaseColor = Color.FromArgb(60, 70, 0x49);
            this._BorderColor = Color.FromArgb(0x35, 0x3a, 60);
            this.TextColor = Color.FromArgb(0xea, 0xea, 0xea);
            this._HeaderLight = Color.FromArgb(0xab, 0xab, 0xac);
            this._BaseLight = Color.FromArgb(0xc4, 0xc7, 200);
            this.TextLight = Color.FromArgb(0x2d, 0x2f, 0x31);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 12f);
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        private void FormSkin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.HeaderMaximize)
            {
                Rectangle rectangle2 = new Rectangle(0, 0, this.Width, Conversions.ToInteger(this.MoveHeight));
                if ((e.Button == MouseButtons.Left) & rectangle2.Contains(e.Location))
                {
                    if (this.FindForm().WindowState == FormWindowState.Normal)
                    {
                        this.FindForm().WindowState = FormWindowState.Maximized;
                        this.FindForm().Refresh();
                    }
                    else if (this.FindForm().WindowState == FormWindowState.Maximized)
                    {
                        this.FindForm().WindowState = FormWindowState.Normal;
                        this.FindForm().Refresh();
                    }
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.ParentForm.FormBorderStyle = FormBorderStyle.None;
            this.ParentForm.AllowTransparency = false;
            this.ParentForm.TransparencyKey = Color.Fuchsia;
            this.ParentForm.FindForm().StartPosition = FormStartPosition.CenterScreen;
            this.Dock = DockStyle.Fill;
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Rectangle rectangle2 = new Rectangle(0, 0, this.Width, Conversions.ToInteger(this.MoveHeight));
            if ((e.Button == MouseButtons.Left) & rectangle2.Contains(e.Location))
            {
                this.Cap = true;
                this.MousePoint = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.Cap)
            {
                this.Parent.Location = Control.MousePosition - ((Size)this.MousePoint);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.Cap = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width;
            this.H = this.Height;
            Rectangle rect = new Rectangle(0, 0, this.W, this.H);
            Rectangle rectangle2 = new Rectangle(0, 0, this.W, 50);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            g.FillRectangle(new SolidBrush(this._BaseColor), rect);
            g.FillRectangle(new SolidBrush(this._HeaderColor), rectangle2);
            Rectangle rectangle3 = new Rectangle(8, 0x10, 4, 0x12);
            g.FillRectangle(new SolidBrush(Color.FromArgb(0xf3, 0xf3, 0xf3)), rectangle3);
            g.FillRectangle(new SolidBrush(Helpers._FlatColor), 0x10, 0x10, 4, 0x12);
            rectangle3 = new Rectangle(0x1a, 15, this.W, this.H);
            g.DrawString(this.Text, this.Font, new SolidBrush(this.TextColor), rectangle3, Helpers.NearSF);
            g.DrawRectangle(new Pen(this._BorderColor), rect);
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get
            {
                return this._BaseColor;
            }
            set
            {
                this._BaseColor = value;
            }
        }

        [Category("Colors")]
        public Color BorderColor
        {
            get
            {
                return this._BorderColor;
            }
            set
            {
                this._BorderColor = value;
            }
        }

        [Category("Colors")]
        public Color FlatColor
        {
            get
            {
                return Helpers._FlatColor;
            }
            set
            {
                Helpers._FlatColor = value;
            }
        }

        [Category("Colors")]
        public Color HeaderColor
        {
            get
            {
                return this._HeaderColor;
            }
            set
            {
                this._HeaderColor = value;
            }
        }

        [Category("Options")]
        public bool HeaderMaximize
        {
            get
            {
                return this._HeaderMaximize;
            }
            set
            {
                this._HeaderMaximize = value;
            }
        }
    }
    internal class FlatTreeView : TreeView
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private Color _LineColor;
        private TreeNodeStates State;

        public FlatTreeView()
        {
            __ENCAddToList(this);
            this._BaseColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._LineColor = Color.FromArgb(0x19, 0x1b, 0x1d);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = this._BaseColor;
            this.ForeColor = Color.White;
            this.LineColor = this._LineColor;
            this.DrawMode = TreeViewDrawMode.OwnerDrawAll;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            try
            {
                Rectangle bounds = e.Bounds;
                Rectangle rect = new Rectangle(e.Bounds.Location.X, e.Bounds.Location.Y, e.Bounds.Width, bounds.Height);
                switch (this.State)
                {
                    case TreeNodeStates.Default:
                        e.Graphics.FillRectangle(Brushes.Red, rect);
                        bounds = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height);
                        e.Graphics.DrawString(e.Node.Text, new Font("Segoe UI", 8f), Brushes.LimeGreen, bounds, Helpers.NearSF);
                        this.Invalidate();
                        goto Label_0216;

                    case TreeNodeStates.Checked:
                        e.Graphics.FillRectangle(Brushes.Green, rect);
                        bounds = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height);
                        e.Graphics.DrawString(e.Node.Text, new Font("Segoe UI", 8f), Brushes.Black, bounds, Helpers.NearSF);
                        this.Invalidate();
                        break;

                    case TreeNodeStates.Selected:
                        e.Graphics.FillRectangle(Brushes.Green, rect);
                        bounds = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height);
                        e.Graphics.DrawString(e.Node.Text, new Font("Segoe UI", 8f), Brushes.Black, bounds, Helpers.NearSF);
                        this.Invalidate();
                        break;
                }
            }
            catch (Exception exception1)
            {
                Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox(exception.Message, MsgBoxStyle.ApplicationModal, null);
                Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError();
            }
        Label_0216:
            base.OnDrawNode(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            g.FillRectangle(new SolidBrush(this._BaseColor), rect);
            Rectangle layoutRectangle = new Rectangle(this.Bounds.X + 2, this.Bounds.Y + 2, this.Bounds.Width, this.Bounds.Height);
            g.DrawString(this.Text, new Font("Segoe UI", 8f), Brushes.Black, layoutRectangle, Helpers.NearSF);
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }
    }
    [DefaultEvent("Scroll")]
    internal class FlatTrackBar : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _HatchColor;
        private int _Maximum;
        private int _Minimum;
        private bool _ShowValue;
        private Color _TrackColor;
        private int _Value;
        private Color BaseColor;
        private bool Bool;
        private int H;
        private Rectangle Knob;
        private Color SliderColor;
        private _Style Style_;
        private Rectangle Track;
        private int Val;
        private int W;

        public event ScrollEventHandler Scroll;

        public FlatTrackBar()
        {
            __ENCAddToList(this);
            this._Maximum = 10;
            this._ShowValue = false;
            this.BaseColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._TrackColor = Helpers._FlatColor;
            this.SliderColor = Color.FromArgb(0x19, 0x1b, 0x1d);
            this._HatchColor = Color.FromArgb(0x17, 0x94, 0x5c);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.Height = 0x12;
            this.BackColor = Color.FromArgb(60, 70, 0x49);
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Subtract)
            {
                if (this.Value != 0)
                {
                    this.Value--;
                }
            }
            else if ((e.KeyCode == Keys.Add) && (this.Value != this._Maximum))
            {
                this.Value++;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Val = (int)Math.Round((double)((((double)(this._Value - this._Minimum)) / ((double)(this._Maximum - this._Minimum))) * (this.Width - 11)));
                this.Track = new Rectangle(this.Val, 0, 10, 20);
                this.Bool = this.Track.Contains(e.Location);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if ((this.Bool && (e.X > -1)) && (e.X < (this.Width + 1)))
            {
                this.Value = this._Minimum + ((int)Math.Round((double)((this._Maximum - this._Minimum) * (((double)e.X) / ((double)this.Width)))));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.Bool = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width - 1;
            this.H = this.Height - 1;
            Rectangle rect = new Rectangle(1, 6, this.W - 2, 8);
            GraphicsPath path = new GraphicsPath();
            GraphicsPath path2 = new GraphicsPath();
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            this.Val = (int)Math.Round((double)((((double)(this._Value - this._Minimum)) / ((double)(this._Maximum - this._Minimum))) * (this.W - 10)));
            this.Track = new Rectangle(this.Val, 0, 10, 20);
            this.Knob = new Rectangle(this.Val, 4, 11, 14);
            path.AddRectangle(rect);
            g.SetClip(path);
            Rectangle rectangle2 = new Rectangle(0, 7, this.W, 8);
            g.FillRectangle(new SolidBrush(this.BaseColor), rectangle2);
            rectangle2 = new Rectangle(0, 7, this.Track.X + this.Track.Width, 8);
            g.FillRectangle(new SolidBrush(this._TrackColor), rectangle2);
            g.ResetClip();
            HatchBrush brush = new HatchBrush(HatchStyle.Plaid, this.HatchColor, this._TrackColor);
            rectangle2 = new Rectangle(-10, 7, this.Track.X + this.Track.Width, 8);
            g.FillRectangle(brush, rectangle2);
            switch (this.Style)
            {
                case _Style.Slider:
                    path2.AddRectangle(this.Track);
                    g.FillPath(new SolidBrush(this.SliderColor), path2);
                    break;

                case _Style.Knob:
                    path2.AddEllipse(this.Knob);
                    g.FillPath(new SolidBrush(this.SliderColor), path2);
                    break;
            }
            if (this.ShowValue)
            {
                rectangle2 = new Rectangle(1, 6, this.W, this.H);
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Far
                };
                g.DrawString(Conversions.ToString(this.Value), new Font("Segoe UI", 8f), Brushes.White, rectangle2, format);
            }
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Height = 0x17;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate();
        }

        [Category("Colors")]
        public Color HatchColor
        {
            get
            {
                return this._HatchColor;
            }
            set
            {
                this._HatchColor = value;
            }
        }

        public int Maximum
        {
            get
            {
                return this._Maximum;
            }
            set
            {
                if (value < 0)
                {
                }
                this._Maximum = value;
                if (value < this._Value)
                {
                    this._Value = value;
                }
                if (value < this._Minimum)
                {
                    this._Minimum = value;
                }
                this.Invalidate();
            }
        }

        public int Minimum
        {
            get
            {
                int num = new int();
                return num;
            }
            set
            {
                if (value < 0)
                {
                }
                this._Minimum = value;
                if (value > this._Value)
                {
                    this._Value = value;
                }
                if (value > this._Maximum)
                {
                    this._Maximum = value;
                }
                this.Invalidate();
            }
        }

        public bool ShowValue
        {
            get
            {
                return this._ShowValue;
            }
            set
            {
                this._ShowValue = value;
            }
        }

        public _Style Style
        {
            get
            {
                return this.Style_;
            }
            set
            {
                this.Style_ = value;
            }
        }

        [Category("Colors")]
        public Color TrackColor
        {
            get
            {
                return this._TrackColor;
            }
            set
            {
                this._TrackColor = value;
            }
        }

        public int Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                if (value != this._Value)
                {
                    if ((value > this._Maximum) || (value < this._Minimum))
                    {
                    }
                    this._Value = value;
                    this.Invalidate();
                    ScrollEventHandler scrollEvent = Scroll;
                    if (scrollEvent != null)
                    {
                        scrollEvent(this);
                    }
                }
            }
        }

        [Flags]
        public enum _Style
        {
            Slider,
            Knob
        }

        public delegate void ScrollEventHandler(object sender);
    }
    [DefaultEvent("CheckedChanged")]
    internal class FlatToggle : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private bool _Checked;
        private Color BaseColor;
        private Color BaseColorRed;
        private Color BGColor;
        private int H;
        private _Options O;
        private MouseState State;
        private Color TextColor;
        private Color ToggleColor;
        private int W;

        public event CheckedChangedEventHandler CheckedChanged;

        public FlatToggle()
        {
            __ENCAddToList(this);
            this._Checked = false;
            this.State = MouseState.None;
            this.BaseColor = Helpers._FlatColor;
            this.BaseColorRed = Color.FromArgb(220, 0x55, 0x60);
            this.BGColor = Color.FromArgb(0x54, 0x55, 0x56);
            this.ToggleColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this.TextColor = Color.FromArgb(0xf3, 0xf3, 0xf3);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;
            Size size2 = new Size(0x2c, this.Height + 1);
            this.Size = size2;
            this.Cursor = Cursors.Hand;
            this.Font = new Font("Segoe UI", 10f);
            size2 = new Size(0x4c, 0x21);
            this.Size = size2;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this._Checked = !this._Checked;
            CheckedChangedEventHandler checkedChangedEvent = CheckedChanged;
            if (checkedChangedEvent != null)
            {
                checkedChangedEvent(this);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.State = MouseState.Down;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.State = MouseState.None;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rectangle3;
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width - 1;
            this.H = this.Height - 1;
            GraphicsPath path = new GraphicsPath();
            GraphicsPath path2 = new GraphicsPath();
            Rectangle rectangle = new Rectangle(0, 0, this.W, this.H);
            Rectangle rectangle2 = new Rectangle(this.W / 2, 0, 0x26, this.H);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            switch (this.O)
            {
                case _Options.Style1:
                    path = Helpers.RoundRec(rectangle, 6);
                    path2 = Helpers.RoundRec(rectangle2, 6);
                    g.FillPath(new SolidBrush(this.BGColor), path);
                    g.FillPath(new SolidBrush(this.ToggleColor), path2);
                    rectangle3 = new Rectangle(0x13, 1, this.W, this.H);
                    g.DrawString("OFF", this.Font, new SolidBrush(this.BGColor), rectangle3, Helpers.CenterSF);
                    if (this.Checked)
                    {
                        path = Helpers.RoundRec(rectangle, 6);
                        rectangle3 = new Rectangle(this.W / 2, 0, 0x26, this.H);
                        path2 = Helpers.RoundRec(rectangle3, 6);
                        g.FillPath(new SolidBrush(this.ToggleColor), path);
                        g.FillPath(new SolidBrush(this.BaseColor), path2);
                        rectangle3 = new Rectangle(8, 7, this.W, this.H);
                        g.DrawString("ON", this.Font, new SolidBrush(this.BaseColor), rectangle3, Helpers.NearSF);
                    }
                    break;

                case _Options.Style2:
                    path = Helpers.RoundRec(rectangle, 6);
                    rectangle2 = new Rectangle(4, 4, 0x24, this.H - 8);
                    path2 = Helpers.RoundRec(rectangle2, 4);
                    g.FillPath(new SolidBrush(this.BaseColorRed), path);
                    g.FillPath(new SolidBrush(this.ToggleColor), path2);
                    g.DrawLine(new Pen(this.BGColor), 0x12, 20, 0x12, 12);
                    g.DrawLine(new Pen(this.BGColor), 0x16, 20, 0x16, 12);
                    g.DrawLine(new Pen(this.BGColor), 0x1a, 20, 0x1a, 12);
                    rectangle3 = new Rectangle(0x13, 2, this.Width, this.Height);
                    g.DrawString("r", new Font("Marlett", 8f), new SolidBrush(this.TextColor), rectangle3, Helpers.CenterSF);
                    if (this.Checked)
                    {
                        path = Helpers.RoundRec(rectangle, 6);
                        rectangle2 = new Rectangle((this.W / 2) - 2, 4, 0x24, this.H - 8);
                        path2 = Helpers.RoundRec(rectangle2, 4);
                        g.FillPath(new SolidBrush(this.BaseColor), path);
                        g.FillPath(new SolidBrush(this.ToggleColor), path2);
                        g.DrawLine(new Pen(this.BGColor), (this.W / 2) + 12, 20, (this.W / 2) + 12, 12);
                        g.DrawLine(new Pen(this.BGColor), (this.W / 2) + 0x10, 20, (this.W / 2) + 0x10, 12);
                        g.DrawLine(new Pen(this.BGColor), (this.W / 2) + 20, 20, (this.W / 2) + 20, 12);
                        rectangle3 = new Rectangle(8, 7, this.Width, this.Height);
                        g.DrawString("\x00fc", new Font("Wingdings", 14f), new SolidBrush(this.TextColor), rectangle3, Helpers.NearSF);
                    }
                    break;

                case _Options.Style3:
                    path = Helpers.RoundRec(rectangle, 0x10);
                    rectangle2 = new Rectangle(this.W - 0x1c, 4, 0x16, this.H - 8);
                    path2.AddEllipse(rectangle2);
                    g.FillPath(new SolidBrush(this.ToggleColor), path);
                    g.FillPath(new SolidBrush(this.BaseColorRed), path2);
                    rectangle3 = new Rectangle(-12, 2, this.W, this.H);
                    g.DrawString("OFF", this.Font, new SolidBrush(this.BaseColorRed), rectangle3, Helpers.CenterSF);
                    if (this.Checked)
                    {
                        path = Helpers.RoundRec(rectangle, 0x10);
                        rectangle2 = new Rectangle(6, 4, 0x16, this.H - 8);
                        path2.Reset();
                        path2.AddEllipse(rectangle2);
                        g.FillPath(new SolidBrush(this.ToggleColor), path);
                        g.FillPath(new SolidBrush(this.BaseColor), path2);
                        rectangle3 = new Rectangle(12, 2, this.W, this.H);
                        g.DrawString("ON", this.Font, new SolidBrush(this.BaseColor), rectangle3, Helpers.CenterSF);
                    }
                    break;

                case _Options.Style4:
                    if (this.Checked)
                    {
                    }
                    break;

                case _Options.Style5:
                    if (this.Checked)
                    {
                    }
                    break;
            }
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = 0x4c;
            this.Height = 0x21;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate();
        }

        [Category("Options")]
        public bool Checked
        {
            get
            {
                return this._Checked;
            }
            set
            {
                this._Checked = value;
            }
        }

        [Category("Options")]
        public _Options Options
        {
            get
            {
                return this.O;
            }
            set
            {
                this.O = value;
            }
        }

        [Flags]
        public enum _Options
        {
            Style1,
            Style2,
            Style3,
            Style4,
            Style5
        }

        public delegate void CheckedChangedEventHandler(object sender);
    }
    [DefaultEvent("TextChanged")]
    internal class FlatTextBox : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private Color _BorderColor;
        private int _MaxLength;
        private bool _Multiline;
        private bool _ReadOnly;
        [AccessedThroughProperty("TB")]
        private TextBox _TB;
        private HorizontalAlignment _TextAlign;
        private Color _TextColor;
        private bool _UseSystemPasswordChar;
        private int H;
        private MouseState State;
        private int W;

        public FlatTextBox()
        {
            __ENCAddToList(this);
            this.State = MouseState.None;
            this._TextAlign = HorizontalAlignment.Left;
            this._MaxLength = 0x7fff;
            this._BaseColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._TextColor = Color.FromArgb(0xc0, 0xc0, 0xc0);
            this._BorderColor = Helpers._FlatColor;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;
            this.TB = new TextBox();
            this.TB.Font = new System.Drawing.Font("Segoe UI", 10f);
            this.TB.Text = this.Text;
            this.TB.BackColor = this._BaseColor;
            this.TB.ForeColor = this._TextColor;
            this.TB.MaxLength = this._MaxLength;
            this.TB.Multiline = this._Multiline;
            this.TB.ReadOnly = this._ReadOnly;
            this.TB.UseSystemPasswordChar = this._UseSystemPasswordChar;
            this.TB.BorderStyle = BorderStyle.None;
            Point point2 = new Point(5, 5);
            this.TB.Location = point2;
            this.TB.Width = this.Width - 10;
            this.TB.Cursor = Cursors.IBeam;
            if (this._Multiline)
            {
                this.TB.Height = this.Height - 11;
            }
            else
            {
                this.Height = this.TB.Height + 11;
            }
            this.TB.TextChanged += new EventHandler(this.OnBaseTextChanged);
            this.TB.KeyDown += new KeyEventHandler(this.OnBaseKeyDown);
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        private void OnBaseKeyDown(object s, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A))
            {
                this.TB.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && (e.KeyCode == Keys.C))
            {
                this.TB.Copy();
                e.SuppressKeyPress = true;
            }
        }

        private void OnBaseTextChanged(object s, EventArgs e)
        {
            this.Text = this.TB.Text;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!this.Controls.Contains(this.TB))
            {
                this.Controls.Add(this.TB);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.State = MouseState.Down;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.State = MouseState.Over;
            this.TB.Focus();
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.State = MouseState.None;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.State = MouseState.Over;
            this.TB.Focus();
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width - 1;
            this.H = this.Height - 1;
            Rectangle rect = new Rectangle(0, 0, this.W, this.H);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            this.TB.BackColor = this._BaseColor;
            this.TB.ForeColor = this._TextColor;
            g.FillRectangle(new SolidBrush(this._BaseColor), rect);
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            Point point2 = new Point(5, 5);
            this.TB.Location = point2;
            this.TB.Width = this.Width - 10;
            if (this._Multiline)
            {
                this.TB.Height = this.Height - 11;
            }
            else
            {
                this.Height = this.TB.Height + 11;
            }
            base.OnResize(e);
        }

        [Category("Options")]
        public override System.Drawing.Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                if (this.TB != null)
                {
                    this.TB.Font = value;
                    Point point2 = new Point(3, 5);
                    this.TB.Location = point2;
                    this.TB.Width = this.Width - 6;
                    if (!this._Multiline)
                    {
                        this.Height = this.TB.Height + 11;
                    }
                }
            }
        }

        public override Color ForeColor
        {
            get
            {
                return this._TextColor;
            }
            set
            {
                this._TextColor = value;
            }
        }

        [Category("Options")]
        public int MaxLength
        {
            get
            {
                return this._MaxLength;
            }
            set
            {
                this._MaxLength = value;
                if (this.TB != null)
                {
                    this.TB.MaxLength = value;
                }
            }
        }

        [Category("Options")]
        public bool Multiline
        {
            get
            {
                return this._Multiline;
            }
            set
            {
                this._Multiline = value;
                if (this.TB != null)
                {
                    this.TB.Multiline = value;
                    if (value)
                    {
                        this.TB.Height = this.Height - 11;
                    }
                    else
                    {
                        this.Height = this.TB.Height + 11;
                    }
                }
            }
        }

        [Category("Options")]
        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;
                if (this.TB != null)
                {
                    this.TB.ReadOnly = value;
                }
            }
        }

        private TextBox TB
        {
            [DebuggerNonUserCode]
            get
            {
                return this._TB;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                this._TB = value;
            }
        }

        [Category("Options")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (this.TB != null)
                {
                    this.TB.Text = value;
                }
            }
        }

        [Category("Options")]
        public HorizontalAlignment TextAlign
        {
            get
            {
                return this._TextAlign;
            }
            set
            {
                this._TextAlign = value;
                if (this.TB != null)
                {
                    this.TB.TextAlign = value;
                }
            }
        }

        [Category("Colors")]
        public Color TextColor
        {
            get
            {
                return this._TextColor;
            }
            set
            {
                this._TextColor = value;
            }
        }

        [Category("Options")]
        public bool UseSystemPasswordChar
        {
            get
            {
                return this._UseSystemPasswordChar;
            }
            set
            {
                this._UseSystemPasswordChar = value;
                if (this.TB != null)
                {
                    this.TB.UseSystemPasswordChar = value;
                }
            }
        }
    }
    internal class FlatTabControl : TabControl
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _ActiveColor;
        private Color _BaseColor;
        private Color BGColor;
        private int H;
        private int W;

        public FlatTabControl()
        {
            __ENCAddToList(this);
            this.BGColor = Color.FromArgb(60, 70, 0x49);
            this._BaseColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._ActiveColor = Helpers._FlatColor;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.FromArgb(60, 70, 0x49);
            this.Font = new Font("Segoe UI", 10f);
            this.SizeMode = TabSizeMode.Fixed;
            Size size2 = new Size(120, 40);
            this.ItemSize = size2;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            this.Alignment = TabAlignment.Top;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width - 1;
            this.H = this.Height - 1;
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this._BaseColor);
            try
            {
                this.SelectedTab.BackColor = this.BGColor;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            int num2 = this.TabCount - 1;
            for (int i = 0; i <= num2; i++)
            {
                Point location = this.GetTabRect(i).Location;
                Point point3 = new Point(location.X + 2, this.GetTabRect(i).Location.Y);
                Size size = new Size(this.GetTabRect(i).Width, this.GetTabRect(i).Height);
                Rectangle rectangle = new Rectangle(point3, size);
                size = new Size(rectangle.Width, rectangle.Height);
                Rectangle rect = new Rectangle(rectangle.Location, size);
                if (i == this.SelectedIndex)
                {
                    g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                    g.FillRectangle(new SolidBrush(this._ActiveColor), rect);
                    if (this.ImageList != null)
                    {
                        try
                        {
                            if (this.ImageList.Images[this.TabPages[i].ImageIndex] != null)
                            {
                                point3 = rect.Location;
                                location = new Point(point3.X + 8, rect.Location.Y + 6);
                                g.DrawImage(this.ImageList.Images[this.TabPages[i].ImageIndex], location);
                                g.DrawString("      " + this.TabPages[i].Text, this.Font, Brushes.White, rect, Helpers.CenterSF);
                            }
                            else
                            {
                                g.DrawString(this.TabPages[i].Text, this.Font, Brushes.White, rect, Helpers.CenterSF);
                            }
                        }
                        catch (Exception exception3)
                        {
                            ProjectData.SetProjectError(exception3);
                            Exception exception = exception3;
                            throw new Exception(exception.Message);
                            ProjectData.ClearProjectError();
                        }
                    }
                    else
                    {
                        g.DrawString(this.TabPages[i].Text, this.Font, Brushes.White, rect, Helpers.CenterSF);
                    }
                }
                else
                {
                    StringFormat format;
                    g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                    if (this.ImageList != null)
                    {
                        try
                        {
                            if (this.ImageList.Images[this.TabPages[i].ImageIndex] != null)
                            {
                                point3 = rect.Location;
                                location = new Point(point3.X + 8, rect.Location.Y + 6);
                                g.DrawImage(this.ImageList.Images[this.TabPages[i].ImageIndex], location);
                                format = new StringFormat
                                {
                                    LineAlignment = StringAlignment.Center,
                                    Alignment = StringAlignment.Center
                                };
                                g.DrawString("      " + this.TabPages[i].Text, this.Font, new SolidBrush(Color.White), rect, format);
                            }
                            else
                            {
                                format = new StringFormat
                                {
                                    LineAlignment = StringAlignment.Center,
                                    Alignment = StringAlignment.Center
                                };
                                g.DrawString(this.TabPages[i].Text, this.Font, new SolidBrush(Color.White), rect, format);
                            }
                        }
                        catch (Exception exception4)
                        {
                            ProjectData.SetProjectError(exception4);
                            Exception exception2 = exception4;
                            throw new Exception(exception2.Message);
                            ProjectData.ClearProjectError();
                        }
                    }
                    else
                    {
                        format = new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        };
                        g.DrawString(this.TabPages[i].Text, this.Font, new SolidBrush(Color.White), rect, format);
                    }
                }
            }
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        [Category("Colors")]
        public Color ActiveColor
        {
            get
            {
                return this._ActiveColor;
            }
            set
            {
                this._ActiveColor = value;
            }
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get
            {
                return this._BaseColor;
            }
            set
            {
                this._BaseColor = value;
            }
        }
    }
    internal class FlatStickyButton : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private bool _Rounded;
        private Color _TextColor;
        private int H;
        private MouseState State;
        private int W;

        public FlatStickyButton()
        {
            __ENCAddToList(this);
            this.State = MouseState.None;
            this._Rounded = false;
            this._BaseColor = Helpers._FlatColor;
            this._TextColor = Color.FromArgb(0xf3, 0xf3, 0xf3);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            Size size2 = new Size(0x6a, 0x20);
            this.Size = size2;
            this.BackColor = Color.Transparent;
            this.Font = new Font("Segoe UI", 12f);
            this.Cursor = Cursors.Hand;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        private bool[] GetConnectedSides()
        {
            IEnumerator enumerator = null;
            bool[] flagArray = new bool[] { false, false, false, false };
            try
            {
                enumerator = this.Parent.Controls.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Control current = (Control)enumerator.Current;
                    if ((current is FlatStickyButton) && !((current == this) | !this.Rect.IntersectsWith(this.Rect)))
                    {
                        double a = (Math.Atan2((double)(this.Left - current.Left), (double)(this.Top - current.Top)) * 2.0) / 3.1415926535897931;
                        if ((((long)Math.Round(a)) / 1L) == a)
                        {
                            flagArray[(int)Math.Round((double)(a + 1.0))] = true;
                        }
                    }
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }
            return flagArray;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.State = MouseState.Down;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.State = MouseState.None;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width;
            this.H = this.Height;
            GraphicsPath path = new GraphicsPath();
            bool[] connectedSides = this.GetConnectedSides();
            GraphicsPath path2 = Helpers.RoundRect(0f, 0f, (float)this.W, (float)this.H, 0.3f, !(connectedSides[2] | connectedSides[1]), !(connectedSides[1] | connectedSides[0]), !(connectedSides[3] | connectedSides[0]), !(connectedSides[3] | connectedSides[2]));
            Rectangle rect = new Rectangle(0, 0, this.W, this.H);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            switch (this.State)
            {
                case MouseState.None:
                    if (!this.Rounded)
                    {
                        g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                        g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rect, Helpers.CenterSF);
                        break;
                    }
                    path = path2;
                    g.FillPath(new SolidBrush(this._BaseColor), path);
                    g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rect, Helpers.CenterSF);
                    break;

                case MouseState.Over:
                    if (!this.Rounded)
                    {
                        g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.White)), rect);
                        g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rect, Helpers.CenterSF);
                        break;
                    }
                    path = path2;
                    g.FillPath(new SolidBrush(this._BaseColor), path);
                    g.FillPath(new SolidBrush(Color.FromArgb(20, Color.White)), path);
                    g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rect, Helpers.CenterSF);
                    break;

                case MouseState.Down:
                    if (!this.Rounded)
                    {
                        g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.Black)), rect);
                        g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rect, Helpers.CenterSF);
                        break;
                    }
                    path = path2;
                    g.FillPath(new SolidBrush(this._BaseColor), path);
                    g.FillPath(new SolidBrush(Color.FromArgb(20, Color.Black)), path);
                    g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rect, Helpers.CenterSF);
                    break;
            }
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get
            {
                return this._BaseColor;
            }
            set
            {
                this._BaseColor = value;
            }
        }

        private Rectangle Rect
        {
            get
            {
                return new Rectangle(this.Left, this.Top, this.Width, this.Height);
            }
        }

        [Category("Options")]
        public bool Rounded
        {
            get
            {
                return this._Rounded;
            }
            set
            {
                this._Rounded = value;
            }
        }

        [Category("Colors")]
        public Color TextColor
        {
            get
            {
                return this._TextColor;
            }
            set
            {
                this._TextColor = value;
            }
        }
    }
    internal class FlatStatusBar : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private Color _RectColor;
        private bool _ShowTimeDate;
        private Color _TextColor;
        private int H;
        private int W;

        public FlatStatusBar()
        {
            __ENCAddToList(this);
            this._ShowTimeDate = false;
            this._BaseColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._TextColor = Color.White;
            this._RectColor = Helpers._FlatColor;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.Font = new Font("Segoe UI", 8f);
            this.ForeColor = Color.White;
            Size size2 = new Size(this.Width, 20);
            this.Size = size2;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            this.Dock = DockStyle.Bottom;
        }

        public string GetTimeDate()
        {
            return (Conversions.ToString(DateTime.Now.Date) + " " + Conversions.ToString(DateTime.Now.Hour) + ":" + Conversions.ToString(DateTime.Now.Minute));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width;
            this.H = this.Height;
            Rectangle rect = new Rectangle(0, 0, this.W, this.H);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BaseColor);
            g.FillRectangle(new SolidBrush(this.BaseColor), rect);
            Rectangle layoutRectangle = new Rectangle(10, 4, this.W, this.H);
            g.DrawString(this.Text, this.Font, Brushes.White, layoutRectangle, Helpers.NearSF);
            layoutRectangle = new Rectangle(4, 4, 4, 14);
            g.FillRectangle(new SolidBrush(this._RectColor), layoutRectangle);
            if (this.ShowTimeDate)
            {
                layoutRectangle = new Rectangle(-4, 2, this.W, this.H);
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(this.GetTimeDate(), this.Font, new SolidBrush(this._TextColor), layoutRectangle, format);
            }
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate();
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get
            {
                return this._BaseColor;
            }
            set
            {
                this._BaseColor = value;
            }
        }

        [Category("Colors")]
        public Color RectColor
        {
            get
            {
                return this._RectColor;
            }
            set
            {
                this._RectColor = value;
            }
        }

        public bool ShowTimeDate
        {
            get
            {
                return this._ShowTimeDate;
            }
            set
            {
                this._ShowTimeDate = value;
            }
        }

        [Category("Colors")]
        public Color TextColor
        {
            get
            {
                return this._TextColor;
            }
            set
            {
                this._TextColor = value;
            }
        }
    }
    internal class FlatProgressBar : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private Color _DarkerProgress;
        private int _Maximum;
        private Color _ProgressColor;
        private int _Value;
        private int H;
        private int W;

        public FlatProgressBar()
        {
            __ENCAddToList(this);
            this._Value = 0;
            this._Maximum = 100;
            this._BaseColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._ProgressColor = Helpers._FlatColor;
            this._DarkerProgress = Color.FromArgb(0x17, 0x94, 0x5c);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.FromArgb(60, 70, 0x49);
            this.Height = 0x2a;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            this.Height = 0x2a;
        }

        public void Increment(int Amount)
        {
            this.Value += Amount;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rectangle3;
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width - 1;
            this.H = this.Height - 1;
            Rectangle rect = new Rectangle(0, 0x18, this.W, this.H);
            GraphicsPath path = new GraphicsPath();
            GraphicsPath path2 = new GraphicsPath();
            GraphicsPath path3 = new GraphicsPath();
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            int num = (int)Math.Round((double)((((double)this._Value) / ((double)this._Maximum)) * this.Width));
            int num2 = this.Value;
            if (num2 == 0)
            {
                g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                rectangle3 = new Rectangle(0, 0x18, num - 1, this.H - 1);
                g.FillRectangle(new SolidBrush(this._ProgressColor), rectangle3);
            }
            else if (num2 == 100)
            {
                g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                rectangle3 = new Rectangle(0, 0x18, num - 1, this.H - 1);
                g.FillRectangle(new SolidBrush(this._ProgressColor), rectangle3);
            }
            else
            {
                g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                rectangle3 = new Rectangle(0, 0x18, num - 1, this.H - 1);
                path.AddRectangle(rectangle3);
                g.FillPath(new SolidBrush(this._ProgressColor), path);
                HatchBrush brush = new HatchBrush(HatchStyle.Plaid, this._DarkerProgress, this._ProgressColor);
                rectangle3 = new Rectangle(0, 0x18, num - 1, this.H - 1);
                g.FillRectangle(brush, rectangle3);
                Rectangle rectangle = new Rectangle(num - 0x12, 0, 0x22, 0x10);
                path2 = Helpers.RoundRec(rectangle, 4);
                g.FillPath(new SolidBrush(this._BaseColor), path2);
                path3 = Helpers.DrawArrow(num - 9, 0x10, true);
                g.FillPath(new SolidBrush(this._BaseColor), path3);
                rectangle3 = new Rectangle(num - 11, -2, this.W, this.H);
                g.DrawString(Conversions.ToString(this.Value), new Font("Segoe UI", 10f), new SolidBrush(this._ProgressColor), rectangle3, Helpers.NearSF);
            }
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Height = 0x2a;
        }

        [Category("Colors")]
        public Color DarkerProgress
        {
            get
            {
                return this._DarkerProgress;
            }
            set
            {
                this._DarkerProgress = value;
            }
        }

        [Category("Control")]
        public int Maximum
        {
            get
            {
                return this._Maximum;
            }
            set
            {
                if (value < this._Value)
                {
                    this._Value = value;
                }
                this._Maximum = value;
                this.Invalidate();
            }
        }

        [Category("Colors")]
        public Color ProgressColor
        {
            get
            {
                return this._ProgressColor;
            }
            set
            {
                this._ProgressColor = value;
            }
        }

        [Category("Control")]
        public int Value
        {
            get
            {
                int num;
                if (this._Value == 0)
                {
                    return 0;
                }
                return this._Value;
                this.Invalidate();
                return num;
            }
            set
            {
                if (value > this._Maximum)
                {
                    value = this._Maximum;
                    this.Invalidate();
                }
                this._Value = value;
                this.Invalidate();
            }
        }
    }
    internal class FlatNumeric : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private Color _ButtonColor;
        private long _Max;
        private long _Min;
        private long _Value;
        private bool Bool;
        private int H;
        private MouseState State;
        private int W;
        private int x;
        private int y;

        public FlatNumeric()
        {
            __ENCAddToList(this);
            this.State = MouseState.None;
            this._BaseColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._ButtonColor = Helpers._FlatColor;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.Font = new Font("Segoe UI", 10f);
            this.BackColor = Color.FromArgb(60, 70, 0x49);
            this.ForeColor = Color.White;
            this._Min = 0L;
            this._Max = 0x98967fL;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Back)
            {
                this.Value = 0L;
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            try
            {
                if (this.Bool)
                {
                    this._Value = Conversions.ToLong(Conversions.ToString(this._Value) + e.KeyChar.ToString());
                }
                if (this._Value > this._Max)
                {
                    this._Value = this._Max;
                }
                this.Invalidate();
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if ((this.x > (this.Width - 0x15)) && (this.x < (this.Width - 3)))
            {
                if (this.y < 15)
                {
                    if ((this.Value + 1L) <= this._Max)
                    {
                        this._Value += 1L;
                    }
                }
                else if ((this.Value - 1L) >= this._Min)
                {
                    this._Value -= 1L;
                }
            }
            else
            {
                this.Bool = !this.Bool;
                this.Focus();
            }
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.x = e.Location.X;
            this.y = e.Location.Y;
            this.Invalidate();
            if (e.X < (this.Width - 0x17))
            {
                this.Cursor = Cursors.IBeam;
            }
            else
            {
                this.Cursor = Cursors.Hand;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width;
            this.H = this.Height;
            Rectangle rect = new Rectangle(0, 0, this.W, this.H);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            g.FillRectangle(new SolidBrush(this._BaseColor), rect);
            Rectangle rectangle2 = new Rectangle(this.Width - 0x18, 0, 0x18, this.H);
            g.FillRectangle(new SolidBrush(this._ButtonColor), rectangle2);
            Point point = new Point(this.Width - 12, 8);
            g.DrawString("+", new Font("Segoe UI", 12f), Brushes.White, (PointF)point, Helpers.CenterSF);
            point = new Point(this.Width - 12, 0x16);
            g.DrawString("-", new Font("Segoe UI", 10f, FontStyle.Bold), Brushes.White, (PointF)point, Helpers.CenterSF);
            rectangle2 = new Rectangle(5, 1, this.W, this.H);
            StringFormat format = new StringFormat
            {
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(Conversions.ToString(this.Value), this.Font, Brushes.White, rectangle2, format);
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Height = 30;
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get
            {
                return this._BaseColor;
            }
            set
            {
                this._BaseColor = value;
            }
        }

        [Category("Colors")]
        public Color ButtonColor
        {
            get
            {
                return this._ButtonColor;
            }
            set
            {
                this._ButtonColor = value;
            }
        }

        public long Maximum
        {
            get
            {
                return this._Max;
            }
            set
            {
                if (value > this._Min)
                {
                    this._Max = value;
                }
                if (this._Value > this._Max)
                {
                    this._Value = this._Max;
                }
                this.Invalidate();
            }
        }

        public long Minimum
        {
            get
            {
                return this._Min;
            }
            set
            {
                if (value < this._Max)
                {
                    this._Min = value;
                }
                if (this._Value < this._Min)
                {
                    this._Value = this.Minimum;
                }
                this.Invalidate();
            }
        }

        public long Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                if ((value <= this._Max) & (value >= this._Min))
                {
                    this._Value = value;
                }
                this.Invalidate();
            }
        }
    }
    internal class FlatMini : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private Color _TextColor;
        private MouseState State;
        private int x;

        public FlatMini()
        {
            __ENCAddToList(this);
            this.State = MouseState.None;
            this._BaseColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._TextColor = Color.FromArgb(0xf3, 0xf3, 0xf3);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            Size size2 = new Size(0x12, 0x12);
            this.Size = size2;
            this.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.Font = new Font("Marlett", 12f);
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            switch (this.FindForm().WindowState)
            {
                case FormWindowState.Normal:
                    this.FindForm().WindowState = FormWindowState.Minimized;
                    break;

                case FormWindowState.Maximized:
                    this.FindForm().WindowState = FormWindowState.Minimized;
                    break;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.State = MouseState.Down;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.State = MouseState.None;
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.x = e.X;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap image = new Bitmap(this.Width, this.Height);
            Graphics graphics = Graphics.FromImage(image);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Graphics graphics2 = graphics;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.Clear(this.BackColor);
            graphics2.FillRectangle(new SolidBrush(this._BaseColor), rect);
            Rectangle layoutRectangle = new Rectangle(2, 1, this.Width, this.Height);
            graphics2.DrawString("0", this.Font, new SolidBrush(this.TextColor), layoutRectangle, Helpers.CenterSF);
            switch (((byte)(((int)this.State) - 1)))
            {
                case 0:
                    graphics2.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.White)), rect);
                    break;

                case 1:
                    graphics2.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), rect);
                    break;
            }
            graphics2 = null;
            base.OnPaint(e);
            graphics.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(image, 0, 0);
            image.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size size2 = new Size(0x12, 0x12);
            this.Size = size2;
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get
            {
                return this._BaseColor;
            }
            set
            {
                this._BaseColor = value;
            }
        }

        [Category("Colors")]
        public Color TextColor
        {
            get
            {
                return this._TextColor;
            }
            set
            {
                this._TextColor = value;
            }
        }
    }
    internal class FlatMax : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private Color _TextColor;
        private MouseState State;
        private int x;

        public FlatMax()
        {
            __ENCAddToList(this);
            this.State = MouseState.None;
            this._BaseColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._TextColor = Color.FromArgb(0xf3, 0xf3, 0xf3);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            Size size2 = new Size(0x12, 0x12);
            this.Size = size2;
            this.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.Font = new Font("Marlett", 12f);
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            switch (this.FindForm().WindowState)
            {
                case FormWindowState.Normal:
                    this.FindForm().WindowState = FormWindowState.Maximized;
                    break;

                case FormWindowState.Maximized:
                    this.FindForm().WindowState = FormWindowState.Normal;
                    break;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.State = MouseState.Down;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.State = MouseState.None;
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.x = e.X;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rectangle2;
            Bitmap image = new Bitmap(this.Width, this.Height);
            Graphics graphics = Graphics.FromImage(image);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Graphics graphics2 = graphics;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.Clear(this.BackColor);
            graphics2.FillRectangle(new SolidBrush(this._BaseColor), rect);
            if (this.FindForm().WindowState == FormWindowState.Maximized)
            {
                rectangle2 = new Rectangle(1, 1, this.Width, this.Height);
                graphics2.DrawString("1", this.Font, new SolidBrush(this.TextColor), rectangle2, Helpers.CenterSF);
            }
            else if (this.FindForm().WindowState == FormWindowState.Normal)
            {
                rectangle2 = new Rectangle(1, 1, this.Width, this.Height);
                graphics2.DrawString("2", this.Font, new SolidBrush(this.TextColor), rectangle2, Helpers.CenterSF);
            }
            switch (((byte)(((int)this.State) - 1)))
            {
                case 0:
                    graphics2.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.White)), rect);
                    break;

                case 1:
                    graphics2.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), rect);
                    break;
            }
            graphics2 = null;
            base.OnPaint(e);
            graphics.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(image, 0, 0);
            image.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size size2 = new Size(0x12, 0x12);
            this.Size = size2;
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get
            {
                return this._BaseColor;
            }
            set
            {
                this._BaseColor = value;
            }
        }

        [Category("Colors")]
        public Color TextColor
        {
            get
            {
                return this._TextColor;
            }
            set
            {
                this._TextColor = value;
            }
        }
    }
    internal class FlatListBox : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private string[] _items;
        [AccessedThroughProperty("ListBx")]
        private ListBox _ListBx;
        private Color _SelectedColor;
        private Color BaseColor;

        public FlatListBox()
        {
            __ENCAddToList(this);
            this.ListBx = new ListBox();
            this._items = new string[] { "" };
            this.BaseColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._SelectedColor = Helpers._FlatColor;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.ListBx.DrawMode = DrawMode.OwnerDrawFixed;
            this.ListBx.ScrollAlwaysVisible = false;
            this.ListBx.HorizontalScrollbar = false;
            this.ListBx.BorderStyle = BorderStyle.None;
            this.ListBx.BackColor = this.BaseColor;
            this.ListBx.ForeColor = Color.White;
            Point point2 = new Point(3, 3);
            this.ListBx.Location = point2;
            this.ListBx.Font = new Font("Segoe UI", 8f);
            this.ListBx.ItemHeight = 20;
            this.ListBx.Items.Clear();
            this.ListBx.IntegralHeight = false;
            Size size2 = new Size(0x83, 0x65);
            this.Size = size2;
            this.BackColor = this.BaseColor;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        public void AddItem(object item)
        {
            this.ListBx.Items.Remove("");
            this.ListBx.Items.Add(RuntimeHelpers.GetObjectValue(item));
        }

        public void AddRange(object[] items)
        {
            this.ListBx.Items.Remove("");
            this.ListBx.Items.AddRange(items);
        }

        public void Clear()
        {
            this.ListBx.Items.Clear();
        }

        public void ClearSelected()
        {
            int num = this.ListBx.SelectedItems.Count - 1;
            while (true)
            {
                int num2 = 0;
                if (num < num2)
                {
                    return;
                }
                this.ListBx.Items.Remove(RuntimeHelpers.GetObjectValue(this.ListBx.SelectedItems[num]));
                num += -1;
            }
        }

        public void Drawitem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                if (Strings.InStr(e.State.ToString(), "Selected,", CompareMethod.Binary) > 0)
                {
                    Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                    e.Graphics.FillRectangle(new SolidBrush(this._SelectedColor), rect);
                    e.Graphics.DrawString(" " + this.ListBx.Items[e.Index].ToString(), new Font("Segoe UI", 8f), Brushes.White, (float)e.Bounds.X, (float)(e.Bounds.Y + 2));
                }
                else
                {
                    Rectangle rectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0x33, 0x35, 0x37)), rectangle);
                    e.Graphics.DrawString(" " + this.ListBx.Items[e.Index].ToString(), new Font("Segoe UI", 8f), Brushes.White, (float)e.Bounds.X, (float)(e.Bounds.Y + 2));
                }
                e.Graphics.Dispose();
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!this.Controls.Contains(this.ListBx))
            {
                this.Controls.Add(this.ListBx);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            Size size2 = new Size(this.Width - 6, this.Height - 2);
            this.ListBx.Size = size2;
            g.FillRectangle(new SolidBrush(this.BaseColor), rect);
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        [Category("Options")]
        public string[] items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
                this.ListBx.Items.Clear();
                this.ListBx.Items.AddRange(value);
                this.Invalidate();
            }
        }

        private ListBox ListBx
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ListBx;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                DrawItemEventHandler handler = new DrawItemEventHandler(this.Drawitem);
                if (this._ListBx != null)
                {
                    this._ListBx.DrawItem -= handler;
                }
                this._ListBx = value;
                if (this._ListBx != null)
                {
                    this._ListBx.DrawItem += handler;
                }
            }
        }

        [Category("Colors")]
        public Color SelectedColor
        {
            get
            {
                return this._SelectedColor;
            }
            set
            {
                this._SelectedColor = value;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this.ListBx.SelectedIndex;
            }
        }

        public string SelectedItem
        {
            get
            {
                return Conversions.ToString(this.ListBx.SelectedItem);
            }
        }
    }
    internal class FlatLabel : Label
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        public FlatLabel()
        {
            __ENCAddToList(this);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Font = new Font("Segoe UI", 8f);
            this.ForeColor = Color.White;
            this.BackColor = Color.Transparent;
            this.Text = this.Text;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate();
        }
    }
    internal class FlatGroupBox : ContainerControl
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private bool _ShowText;
        private int H;
        private int W;

        public FlatGroupBox()
        {
            __ENCAddToList(this);
            this._ShowText = true;
            this._BaseColor = Color.FromArgb(60, 70, 0x49);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;
            Size size2 = new Size(240, 180);
            this.Size = size2;
            this.Font = new Font("Segoe ui", 10f);
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width - 1;
            this.H = this.Height - 1;
            GraphicsPath path = new GraphicsPath();
            GraphicsPath path2 = new GraphicsPath();
            GraphicsPath path3 = new GraphicsPath();
            Rectangle rectangle = new Rectangle(8, 8, this.W - 0x10, this.H - 0x10);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            path = Helpers.RoundRec(rectangle, 8);
            g.FillPath(new SolidBrush(this._BaseColor), path);
            path2 = Helpers.DrawArrow(0x1c, 2, false);
            g.FillPath(new SolidBrush(this._BaseColor), path2);
            path3 = Helpers.DrawArrow(0x1c, 8, true);
            g.FillPath(new SolidBrush(Color.FromArgb(60, 70, 0x49)), path3);
            if (this.ShowText)
            {
                Rectangle layoutRectangle = new Rectangle(0x10, 0x10, this.W, this.H);
                g.DrawString(this.Text, this.Font, new SolidBrush(Helpers._FlatColor), layoutRectangle, Helpers.NearSF);
            }
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get
            {
                return this._BaseColor;
            }
            set
            {
                this._BaseColor = value;
            }
        }

        public bool ShowText
        {
            get
            {
                return this._ShowText;
            }
            set
            {
                this._ShowText = value;
            }
        }
    }
    internal class FlatContextMenuStrip : ContextMenuStrip
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        public FlatContextMenuStrip()
        {
            __ENCAddToList(this);
            this.Renderer = new ToolStripProfessionalRenderer(new TColorTable());
            this.ShowImageMargin = false;
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 8f);
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate();
        }

        public class TColorTable : ProfessionalColorTable
        {
            private Color BackColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            private Color BorderColor = Color.FromArgb(0x35, 0x3a, 60);
            private Color CheckedColor = Helpers._FlatColor;

            [Category("Colors")]
            public Color _BackColor
            {
                get
                {
                    return this.BackColor;
                }
                set
                {
                    this.BackColor = value;
                }
            }

            [Category("Colors")]
            public Color _BorderColor
            {
                get
                {
                    return this.BorderColor;
                }
                set
                {
                    this.BorderColor = value;
                }
            }

            [Category("Colors")]
            public Color _CheckedColor
            {
                get
                {
                    return this.CheckedColor;
                }
                set
                {
                    this.CheckedColor = value;
                }
            }

            public override Color ButtonSelectedBorder
            {
                get
                {
                    return this.BackColor;
                }
            }

            public override Color CheckBackground
            {
                get
                {
                    return this.CheckedColor;
                }
            }

            public override Color CheckPressedBackground
            {
                get
                {
                    return this.CheckedColor;
                }
            }

            public override Color CheckSelectedBackground
            {
                get
                {
                    return this.CheckedColor;
                }
            }

            public override Color ImageMarginGradientBegin
            {
                get
                {
                    return this.CheckedColor;
                }
            }

            public override Color ImageMarginGradientEnd
            {
                get
                {
                    return this.CheckedColor;
                }
            }

            public override Color ImageMarginGradientMiddle
            {
                get
                {
                    return this.CheckedColor;
                }
            }

            public override Color MenuBorder
            {
                get
                {
                    return this.BorderColor;
                }
            }

            public override Color MenuItemBorder
            {
                get
                {
                    return this.BorderColor;
                }
            }

            public override Color MenuItemSelected
            {
                get
                {
                    return this.CheckedColor;
                }
            }

            public override Color SeparatorDark
            {
                get
                {
                    return this.BorderColor;
                }
            }

            public override Color ToolStripDropDownBackground
            {
                get
                {
                    return this.BackColor;
                }
            }
        }
    }
    internal class FlatComboBox : ComboBox
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private Color _BGColor;
        private Color _HoverColor;
        private int _StartIndex;
        private int H;
        private MouseState State;
        private int W;
        private int x;
        private int y;

        public FlatComboBox()
        {
            base.DrawItem += new DrawItemEventHandler(this.DrawItem_);
            __ENCAddToList(this);
            this._StartIndex = 0;
            this.State = MouseState.None;
            this._BaseColor = Color.FromArgb(0x19, 0x1b, 0x1d);
            this._BGColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._HoverColor = Color.FromArgb(0x23, 0xa8, 0x6d);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.BackColor = Color.FromArgb(0x2d, 0x2d, 0x30);
            this.ForeColor = Color.White;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Cursor = Cursors.Hand;
            this.StartIndex = 0;
            this.ItemHeight = 0x12;
            this.Font = new Font("Segoe UI", 8f, FontStyle.Regular);
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        public void DrawItem_(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(this._HoverColor), e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(this._BaseColor), e.Bounds);
                }
                Rectangle layoutRectangle = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[e.Index])), new Font("Segoe UI", 8f), Brushes.White, layoutRectangle);
                e.Graphics.Dispose();
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.Invalidate();
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            this.Invalidate();
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                this.Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.State = MouseState.Down;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.State = MouseState.None;
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.x = e.Location.X;
            this.y = e.Location.Y;
            this.Invalidate();
            if (e.X < (this.Width - 0x29))
            {
                this.Cursor = Cursors.IBeam;
            }
            else
            {
                this.Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width;
            this.H = this.Height;
            Rectangle rect = new Rectangle(0, 0, this.W, this.H);
            Rectangle rectangle2 = new Rectangle(this.W - 40, 0, this.W, this.H);
            GraphicsPath path = new GraphicsPath();
            GraphicsPath path2 = new GraphicsPath();
            Graphics g = Helpers.G;
            g.Clear(Color.FromArgb(0x2d, 0x2d, 0x30));
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(new SolidBrush(this._BGColor), rect);
            path.Reset();
            path.AddRectangle(rectangle2);
            g.SetClip(path);
            g.FillRectangle(new SolidBrush(this._BaseColor), rectangle2);
            g.ResetClip();
            g.DrawLine(Pens.White, this.W - 10, 6, this.W - 30, 6);
            g.DrawLine(Pens.White, this.W - 10, 12, this.W - 30, 12);
            g.DrawLine(Pens.White, this.W - 10, 0x12, this.W - 30, 0x12);
            Point point = new Point(4, 6);
            g.DrawString(this.Text, this.Font, Brushes.White, (PointF)point, Helpers.NearSF);
            g = null;
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Height = 0x12;
        }

        [Category("Colors")]
        public Color HoverColor
        {
            get
            {
                return this._HoverColor;
            }
            set
            {
                this._HoverColor = value;
            }
        }

        private int StartIndex
        {
            get
            {
                return this._StartIndex;
            }
            set
            {
                this._StartIndex = value;
                try
                {
                    base.SelectedIndex = value;
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    ProjectData.ClearProjectError();
                }
                this.Invalidate();
            }
        }
    }
    internal class FlatClose : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private Color _TextColor;
        private MouseState State;
        private int x;

        public FlatClose()
        {
            __ENCAddToList(this);
            this.State = MouseState.None;
            this._BaseColor = Color.FromArgb(0xa8, 0x23, 0x23);
            this._TextColor = Color.FromArgb(0xf3, 0xf3, 0xf3);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            Size size2 = new Size(0x12, 0x12);
            this.Size = size2;
            this.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.Font = new Font("Marlett", 10f);
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Environment.Exit(0);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.State = MouseState.Down;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.State = MouseState.None;
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.x = e.X;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap image = new Bitmap(this.Width, this.Height);
            Graphics graphics = Graphics.FromImage(image);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Graphics graphics2 = graphics;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.Clear(this.BackColor);
            graphics2.FillRectangle(new SolidBrush(this._BaseColor), rect);
            Rectangle layoutRectangle = new Rectangle(0, 0, this.Width, this.Height);
            graphics2.DrawString("r", this.Font, new SolidBrush(this.TextColor), layoutRectangle, Helpers.CenterSF);
            switch (((byte)(((int)this.State) - 1)))
            {
                case 0:
                    graphics2.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.White)), rect);
                    break;

                case 1:
                    graphics2.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), rect);
                    break;
            }
            graphics2 = null;
            base.OnPaint(e);
            graphics.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(image, 0, 0);
            image.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size size2 = new Size(0x12, 0x12);
            this.Size = size2;
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get
            {
                return this._BaseColor;
            }
            set
            {
                this._BaseColor = value;
            }
        }

        [Category("Colors")]
        public Color TextColor
        {
            get
            {
                return this._TextColor;
            }
            set
            {
                this._TextColor = value;
            }
        }
    }
    [DefaultEvent("CheckedChanged")]
    internal class FlatCheckBox : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private Color _BorderColor;
        private bool _Checked;
        private Color _TextColor;
        private int H;
        private _Options O;
        private MouseState State;
        private int W;

        public event CheckedChangedEventHandler CheckedChanged;

        public FlatCheckBox()
        {
            __ENCAddToList(this);
            this.State = MouseState.None;
            this._BaseColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._BorderColor = Helpers._FlatColor;
            this._TextColor = Color.FromArgb(0xf3, 0xf3, 0xf3);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.FromArgb(60, 70, 0x49);
            this.Cursor = Cursors.Hand;
            this.Font = new Font("Segoe UI", 10f);
            Size size2 = new Size(0x70, 0x16);
            this.Size = size2;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnClick(EventArgs e)
        {
            this._Checked = !this._Checked;
            CheckedChangedEventHandler checkedChangedEvent = CheckedChanged;
            if (checkedChangedEvent != null)
            {
                checkedChangedEvent(this);
            }
            base.OnClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.State = MouseState.Down;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.State = MouseState.None;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rectangle2;
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width - 1;
            this.H = this.Height - 1;
            Rectangle rect = new Rectangle(0, 2, this.Height - 5, this.Height - 5);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            switch (this.O)
            {
                case _Options.Style1:
                    g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                    switch (((byte)(((int)this.State) - 1)))
                    {
                        case 0:
                            g.DrawRectangle(new Pen(this._BorderColor), rect);
                            goto Label_00F6;

                        case 1:
                            g.DrawRectangle(new Pen(this._BorderColor), rect);
                            goto Label_00F6;
                    }
                    break;

                case _Options.Style2:
                    g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                    switch (((byte)(((int)this.State) - 1)))
                    {
                        case 0:
                            g.DrawRectangle(new Pen(this._BorderColor), rect);
                            g.FillRectangle(new SolidBrush(Color.FromArgb(0x76, 0xd5, 170)), rect);
                            break;

                        case 1:
                            g.DrawRectangle(new Pen(this._BorderColor), rect);
                            g.FillRectangle(new SolidBrush(Color.FromArgb(0x76, 0xd5, 170)), rect);
                            break;
                    }
                    if (this.Checked)
                    {
                        rectangle2 = new Rectangle(5, 7, this.H - 9, this.H - 9);
                        g.DrawString("\x00fc", new Font("Wingdings", 18f), new SolidBrush(this._BorderColor), rectangle2, Helpers.CenterSF);
                    }
                    if (!this.Enabled)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(0x36, 0x3a, 0x3d)), rect);
                        rectangle2 = new Rectangle(20, 2, this.W, this.H);
                        g.DrawString(this.Text, this.Font, new SolidBrush(Color.FromArgb(0x30, 0x77, 0x5b)), rectangle2, Helpers.NearSF);
                    }
                    rectangle2 = new Rectangle(20, 2, this.W, this.H);
                    g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rectangle2, Helpers.NearSF);
                    goto Label_03B0;

                default:
                    goto Label_03B0;
            }
        Label_00F6:
            if (this.Checked)
            {
                rectangle2 = new Rectangle(5, 7, this.H - 9, this.H - 9);
                g.DrawString("\x00fc", new Font("Wingdings", 18f), new SolidBrush(this._BorderColor), rectangle2, Helpers.CenterSF);
            }
            if (!this.Enabled)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(0x36, 0x3a, 0x3d)), rect);
                rectangle2 = new Rectangle(20, 2, this.W, this.H);
                g.DrawString(this.Text, this.Font, new SolidBrush(Color.FromArgb(140, 0x8e, 0x8f)), rectangle2, Helpers.NearSF);
            }
            rectangle2 = new Rectangle(20, 2, this.W, this.H);
            g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rectangle2, Helpers.NearSF);
        Label_03B0:
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Height = 0x16;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate();
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get
            {
                return this._BaseColor;
            }
            set
            {
                this._BaseColor = value;
            }
        }

        [Category("Colors")]
        public Color BorderColor
        {
            get
            {
                return this._BorderColor;
            }
            set
            {
                this._BorderColor = value;
            }
        }

        public bool Checked
        {
            get
            {
                return this._Checked;
            }
            set
            {
                this._Checked = value;
                this.Invalidate();
            }
        }

        [Category("Options")]
        public _Options Options
        {
            get
            {
                return this.O;
            }
            set
            {
                this.O = value;
            }
        }

        [Flags]
        public enum _Options
        {
            Style1,
            Style2
        }

        public delegate void CheckedChangedEventHandler(object sender);
    }
    internal class FlatButton : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private bool _Rounded;
        private Color _TextColor;
        private int H;
        private MouseState State;
        private int W;

        public FlatButton()
        {
            __ENCAddToList(this);
            this._Rounded = false;
            this.State = MouseState.None;
            this._BaseColor = Helpers._FlatColor;
            this._TextColor = Color.FromArgb(0xf3, 0xf3, 0xf3);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            Size size2 = new Size(0x6a, 0x20);
            this.Size = size2;
            this.BackColor = Color.Transparent;
            this.Font = new Font("Segoe UI", 12f);
            this.Cursor = Cursors.Hand;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.State = MouseState.Down;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.State = MouseState.None;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width - 1;
            this.H = this.Height - 1;
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.W, this.H);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            switch (this.State)
            {
                case MouseState.None:
                    if (!this.Rounded)
                    {
                        g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                        g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rect, Helpers.CenterSF);
                        break;
                    }
                    path = Helpers.RoundRec(rect, 6);
                    g.FillPath(new SolidBrush(this._BaseColor), path);
                    g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rect, Helpers.CenterSF);
                    break;

                case MouseState.Over:
                    if (!this.Rounded)
                    {
                        g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.White)), rect);
                        g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rect, Helpers.CenterSF);
                        break;
                    }
                    path = Helpers.RoundRec(rect, 6);
                    g.FillPath(new SolidBrush(this._BaseColor), path);
                    g.FillPath(new SolidBrush(Color.FromArgb(20, Color.White)), path);
                    g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rect, Helpers.CenterSF);
                    break;

                case MouseState.Down:
                    if (!this.Rounded)
                    {
                        g.FillRectangle(new SolidBrush(this._BaseColor), rect);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.Black)), rect);
                        g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rect, Helpers.CenterSF);
                        break;
                    }
                    path = Helpers.RoundRec(rect, 6);
                    g.FillPath(new SolidBrush(this._BaseColor), path);
                    g.FillPath(new SolidBrush(Color.FromArgb(20, Color.Black)), path);
                    g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rect, Helpers.CenterSF);
                    break;
            }
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get
            {
                return this._BaseColor;
            }
            set
            {
                this._BaseColor = value;
            }
        }

        [Category("Options")]
        public bool Rounded
        {
            get
            {
                return this._Rounded;
            }
            set
            {
                this._Rounded = value;
            }
        }

        [Category("Colors")]
        public Color TextColor
        {
            get
            {
                return this._TextColor;
            }
            set
            {
                this._TextColor = value;
            }
        }
    }
    internal class FlatAlertBox : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        [AccessedThroughProperty("T")]
        private Timer _T;
        private string _Text;
        private Color ErrorColor;
        private Color ErrorText;
        private int H;
        private Color InfoColor;
        private Color InfoText;
        private _Kind K;
        private MouseState State;
        private Color SuccessColor;
        private Color SuccessText;
        private int W;
        private int X;

        public FlatAlertBox()
        {
            __ENCAddToList(this);
            this.State = MouseState.None;
            this.SuccessColor = Color.FromArgb(60, 0x55, 0x4f);
            this.SuccessText = Color.FromArgb(0x23, 0xa9, 110);
            this.ErrorColor = Color.FromArgb(0x57, 0x47, 0x47);
            this.ErrorText = Color.FromArgb(0xfe, 0x8e, 0x7a);
            this.InfoColor = Color.FromArgb(70, 0x5b, 0x5e);
            this.InfoText = Color.FromArgb(0x61, 0xb9, 0xba);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.FromArgb(60, 70, 0x49);
            Size size2 = new Size(0x240, 0x2a);
            this.Size = size2;
            Point point2 = new Point(10, 0x3d);
            this.Location = point2;
            this.Font = new Font("Segoe UI", 10f);
            this.Cursor = Cursors.Hand;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.Visible = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.State = MouseState.Down;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.State = MouseState.None;
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.X = e.X;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rectangle2;
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width - 1;
            this.H = this.Height - 1;
            Rectangle rect = new Rectangle(0, 0, this.W, this.H);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            switch (this.K)
            {
                case _Kind.Success:
                    g.FillRectangle(new SolidBrush(this.SuccessColor), rect);
                    rectangle2 = new Rectangle(8, 9, 0x18, 0x18);
                    g.FillEllipse(new SolidBrush(this.SuccessText), rectangle2);
                    rectangle2 = new Rectangle(10, 11, 20, 20);
                    g.FillEllipse(new SolidBrush(this.SuccessColor), rectangle2);
                    rectangle2 = new Rectangle(7, 7, this.W, this.H);
                    g.DrawString("\x00fc", new Font("Wingdings", 22f), new SolidBrush(this.SuccessText), rectangle2, Helpers.NearSF);
                    rectangle2 = new Rectangle(0x30, 12, this.W, this.H);
                    g.DrawString(this.Text, this.Font, new SolidBrush(this.SuccessText), rectangle2, Helpers.NearSF);
                    rectangle2 = new Rectangle(this.W - 30, this.H - 0x1d, 0x11, 0x11);
                    g.FillEllipse(new SolidBrush(Color.FromArgb(0x23, Color.Black)), rectangle2);
                    rectangle2 = new Rectangle(this.W - 0x1c, 0x10, this.W, this.H);
                    g.DrawString("r", new Font("Marlett", 8f), new SolidBrush(this.SuccessColor), rectangle2, Helpers.NearSF);
                    if (this.State == MouseState.Over)
                    {
                        rectangle2 = new Rectangle(this.W - 0x1c, 0x10, this.W, this.H);
                        g.DrawString("r", new Font("Marlett", 8f), new SolidBrush(Color.FromArgb(0x19, Color.White)), rectangle2, Helpers.NearSF);
                    }
                    break;

                case _Kind.Error:
                    g.FillRectangle(new SolidBrush(this.ErrorColor), rect);
                    rectangle2 = new Rectangle(8, 9, 0x18, 0x18);
                    g.FillEllipse(new SolidBrush(this.ErrorText), rectangle2);
                    rectangle2 = new Rectangle(10, 11, 20, 20);
                    g.FillEllipse(new SolidBrush(this.ErrorColor), rectangle2);
                    rectangle2 = new Rectangle(6, 11, this.W, this.H);
                    g.DrawString("r", new Font("Marlett", 16f), new SolidBrush(this.ErrorText), rectangle2, Helpers.NearSF);
                    rectangle2 = new Rectangle(0x30, 12, this.W, this.H);
                    g.DrawString(this.Text, this.Font, new SolidBrush(this.ErrorText), rectangle2, Helpers.NearSF);
                    rectangle2 = new Rectangle(this.W - 0x20, this.H - 0x1d, 0x11, 0x11);
                    g.FillEllipse(new SolidBrush(Color.FromArgb(0x23, Color.Black)), rectangle2);
                    rectangle2 = new Rectangle(this.W - 30, 0x11, this.W, this.H);
                    g.DrawString("r", new Font("Marlett", 8f), new SolidBrush(this.ErrorColor), rectangle2, Helpers.NearSF);
                    if (this.State == MouseState.Over)
                    {
                        rectangle2 = new Rectangle(this.W - 30, 15, this.W, this.H);
                        g.DrawString("r", new Font("Marlett", 8f), new SolidBrush(Color.FromArgb(0x19, Color.White)), rectangle2, Helpers.NearSF);
                    }
                    break;

                case _Kind.Info:
                    g.FillRectangle(new SolidBrush(this.InfoColor), rect);
                    rectangle2 = new Rectangle(8, 9, 0x18, 0x18);
                    g.FillEllipse(new SolidBrush(this.InfoText), rectangle2);
                    rectangle2 = new Rectangle(10, 11, 20, 20);
                    g.FillEllipse(new SolidBrush(this.InfoColor), rectangle2);
                    rectangle2 = new Rectangle(12, -4, this.W, this.H);
                    g.DrawString("\x00a1", new Font("Segoe UI", 20f, FontStyle.Bold), new SolidBrush(this.InfoText), rectangle2, Helpers.NearSF);
                    rectangle2 = new Rectangle(0x30, 12, this.W, this.H);
                    g.DrawString(this.Text, this.Font, new SolidBrush(this.InfoText), rectangle2, Helpers.NearSF);
                    rectangle2 = new Rectangle(this.W - 0x20, this.H - 0x1d, 0x11, 0x11);
                    g.FillEllipse(new SolidBrush(Color.FromArgb(0x23, Color.Black)), rectangle2);
                    rectangle2 = new Rectangle(this.W - 30, 0x11, this.W, this.H);
                    g.DrawString("r", new Font("Marlett", 8f), new SolidBrush(this.InfoColor), rectangle2, Helpers.NearSF);
                    if (this.State == MouseState.Over)
                    {
                        rectangle2 = new Rectangle(this.W - 30, 0x11, this.W, this.H);
                        g.DrawString("r", new Font("Marlett", 8f), new SolidBrush(Color.FromArgb(0x19, Color.White)), rectangle2, Helpers.NearSF);
                    }
                    break;
            }
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Height = 0x2a;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate();
        }

        public void ShowControl(_Kind Kind, string Str, int Interval)
        {
            this.K = Kind;
            this.Text = Str;
            this.Visible = true;
            this.T = new Timer();
            this.T.Interval = Interval;
            this.T.Enabled = true;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            this.Visible = false;
            this.T.Enabled = false;
            this.T.Dispose();
        }

        [Category("Options")]
        public _Kind kind
        {
            get
            {
                return this.K;
            }
            set
            {
                this.K = value;
            }
        }

        private Timer T
        {
            [DebuggerNonUserCode]
            get
            {
                return this._T;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                EventHandler handler = new EventHandler(this.T_Tick);
                if (this._T != null)
                {
                    this._T.Tick -= handler;
                }
                this._T = value;
                if (this._T != null)
                {
                    this._T.Tick += handler;
                }
            }
        }

        [Category("Options")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (this._Text != null)
                {
                    this._Text = value;
                }
            }
        }

        [Category("Options")]
        public bool Visible
        {
            get
            {
                return !base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        [Flags]
        public enum _Kind
        {
            Success,
            Error,
            Info
        }
    }
    [DefaultEvent("CheckedChanged")]
    internal class RadioButton : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _BaseColor;
        private Color _BorderColor;
        private bool _Checked;
        private Color _TextColor;
        private int H;
        private _Options O;
        private MouseState State;
        private int W;

        public event CheckedChangedEventHandler CheckedChanged;

        public RadioButton()
        {
            __ENCAddToList(this);
            this.State = MouseState.None;
            this._BaseColor = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._BorderColor = Helpers._FlatColor;
            this._TextColor = Color.FromArgb(0xf3, 0xf3, 0xf3);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.Cursor = Cursors.Hand;
            Size size2 = new Size(100, 0x16);
            this.Size = size2;
            this.BackColor = Color.FromArgb(60, 70, 0x49);
            this.Font = new Font("Segoe UI", 10f);
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        private void InvalidateControls()
        {
            if (this.IsHandleCreated && this._Checked)
            {
                IEnumerator enumerator = null;
                try
                {
                    enumerator = this.Parent.Controls.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        Control current = (Control)enumerator.Current;
                        if ((current != this) && (current is Flat_UI.RadioButton))
                        {
                            ((Flat_UI.RadioButton)current).Checked = false;
                            this.Invalidate();
                        }
                    }
                }
                finally
                {
                    if (enumerator is IDisposable)
                    {
                        (enumerator as IDisposable).Dispose();
                    }
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (!this._Checked)
            {
                this.Checked = true;
            }
            base.OnClick(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.InvalidateControls();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.State = MouseState.Down;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.State = MouseState.None;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.State = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rectangle3;
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width - 1;
            this.H = this.Height - 1;
            Rectangle rect = new Rectangle(0, 2, this.Height - 5, this.Height - 5);
            Rectangle rectangle2 = new Rectangle(4, 6, this.H - 12, this.H - 12);
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            switch (this.O)
            {
                case _Options.Style1:
                    g.FillEllipse(new SolidBrush(this._BaseColor), rect);
                    switch (((byte)(((int)this.State) - 1)))
                    {
                        case 0:
                            g.DrawEllipse(new Pen(this._BorderColor), rect);
                            goto Label_0112;

                        case 1:
                            g.DrawEllipse(new Pen(this._BorderColor), rect);
                            goto Label_0112;
                    }
                    break;

                case _Options.Style2:
                    g.FillEllipse(new SolidBrush(this._BaseColor), rect);
                    switch (((byte)(((int)this.State) - 1)))
                    {
                        case 0:
                            g.DrawEllipse(new Pen(this._BorderColor), rect);
                            g.FillEllipse(new SolidBrush(Color.FromArgb(0x76, 0xd5, 170)), rect);
                            break;

                        case 1:
                            g.DrawEllipse(new Pen(this._BorderColor), rect);
                            g.FillEllipse(new SolidBrush(Color.FromArgb(0x76, 0xd5, 170)), rect);
                            break;
                    }
                    if (this.Checked)
                    {
                        g.FillEllipse(new SolidBrush(this._BorderColor), rectangle2);
                    }
                    goto Label_01EF;

                default:
                    goto Label_01EF;
            }
        Label_0112:
            if (this.Checked)
            {
                g.FillEllipse(new SolidBrush(this._BorderColor), rectangle2);
            }
        Label_01EF:
            rectangle3 = new Rectangle(20, 2, this.W, this.H);
            g.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rectangle3, Helpers.NearSF);
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Height = 0x16;
        }

        public bool Checked
        {
            get
            {
                return this._Checked;
            }
            set
            {
                this._Checked = value;
                this.InvalidateControls();
                CheckedChangedEventHandler checkedChangedEvent = CheckedChanged;
                if (checkedChangedEvent != null)
                {
                    checkedChangedEvent(this);
                }
                this.Invalidate();
            }
        }

        [Category("Options")]
        public _Options Options
        {
            get
            {
                return this.O;
            }
            set
            {
                this.O = value;
            }
        }

        [Flags]
        public enum _Options
        {
            Style1,
            Style2
        }

        public delegate void CheckedChangedEventHandler(object sender);
    }
    internal enum MouseState : byte
    {
        Block = 3,
        Down = 2,
        None = 0,
        Over = 1
    }
    internal sealed class Helpers
    {
        internal static Color _FlatColor = Color.FromArgb(255, 12, 64, 166);
        internal static Bitmap B;
        internal static StringFormat CenterSF;
        internal static Graphics G;
        internal static StringFormat NearSF;

        static Helpers()
        {
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            };
            NearSF = format;
            format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            CenterSF = format;
        }

        public static GraphicsPath DrawArrow(int x, int y, bool flip)
        {
            GraphicsPath path2 = new GraphicsPath();
            int num2 = 12;
            int num = 6;
            if (flip)
            {
                path2.AddLine(x + 1, y, (x + num2) + 1, y);
                path2.AddLine(x + num2, y, x + num, (y + num) - 1);
            }
            else
            {
                path2.AddLine(x, y + num, x + num2, y + num);
                path2.AddLine(x + num2, y + num, x + num, y);
            }
            path2.CloseFigure();
            return path2;
        }

        public static GraphicsPath RoundRec(Rectangle Rectangle, int Curve)
        {
            GraphicsPath path = new GraphicsPath();
            int width = Curve * 2;
            Rectangle rect = new Rectangle(Rectangle.X, Rectangle.Y, width, width);
            path.AddArc(rect, -180f, 90f);
            rect = new Rectangle((Rectangle.Width - width) + Rectangle.X, Rectangle.Y, width, width);
            path.AddArc(rect, -90f, 90f);
            rect = new Rectangle((Rectangle.Width - width) + Rectangle.X, (Rectangle.Height - width) + Rectangle.Y, width, width);
            path.AddArc(rect, 0f, 90f);
            rect = new Rectangle(Rectangle.X, (Rectangle.Height - width) + Rectangle.Y, width, width);
            path.AddArc(rect, 90f, 90f);
            Point point = new Point(Rectangle.X, (Rectangle.Height - width) + Rectangle.Y);
            Point point2 = new Point(Rectangle.X, Curve + Rectangle.Y);
            path.AddLine(point, point2);
            return path;
        }

        public static GraphicsPath RoundRect(float x, float y, float w, float h, float r = 0.3f, bool TL = true, bool TR = true, bool BR = true, bool BL = true)
        {
            float width = Math.Min(w, h) * r;
            float num2 = x + w;
            float num3 = y + h;
            GraphicsPath path = new GraphicsPath();
            GraphicsPath path2 = path;
            if (TL)
            {
                path2.AddArc(x, y, width, width, 180f, 90f);
            }
            else
            {
                path2.AddLine(x, y, x, y);
            }
            if (TR)
            {
                path2.AddArc(num2 - width, y, width, width, 270f, 90f);
            }
            else
            {
                path2.AddLine(num2, y, num2, y);
            }
            if (BR)
            {
                path2.AddArc(num2 - width, num3 - width, width, width, 0f, 90f);
            }
            else
            {
                path2.AddLine(num2, num3, num2, num3);
            }
            if (BL)
            {
                path2.AddArc(x, num3 - width, width, width, 90f, 90f);
            }
            else
            {
                path2.AddLine(x, num3, x, num3);
            }
            path2.CloseFigure();
            path2 = null;
            return path;
        }
    }
    internal class FlatColorPalette : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private Color _Black;
        private Color _Blue;
        private Color _Cyan;
        private Color _Gray;
        private Color _LimeGreen;
        private Color _Orange;
        private Color _Purple;
        private Color _Red;
        private Color _White;
        private int H;
        private int W;

        public FlatColorPalette()
        {
            __ENCAddToList(this);
            this._Red = Color.FromArgb(220, 0x55, 0x60);
            this._Cyan = Color.FromArgb(10, 0x9a, 0x9d);
            this._Blue = Color.FromArgb(0, 0x80, 0xff);
            this._LimeGreen = Color.FromArgb(0x23, 0xa8, 0x6d);
            this._Orange = Color.FromArgb(0xfd, 0xb5, 0x3f);
            this._Purple = Color.FromArgb(0x9b, 0x58, 0xb5);
            this._Black = Color.FromArgb(0x2d, 0x2f, 0x31);
            this._Gray = Color.FromArgb(0x3f, 70, 0x49);
            this._White = Color.FromArgb(0xf3, 0xf3, 0xf3);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.FromArgb(60, 70, 0x49);
            Size size2 = new Size(160, 80);
            this.Size = size2;
            this.Font = new Font("Segoe UI", 12f);
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Helpers.B = new Bitmap(this.Width, this.Height);
            Helpers.G = Graphics.FromImage(Helpers.B);
            this.W = this.Width - 1;
            this.H = this.Height - 1;
            Graphics g = Helpers.G;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(this.BackColor);
            Rectangle rect = new Rectangle(0, 0, 20, 40);
            g.FillRectangle(new SolidBrush(this._Red), rect);
            rect = new Rectangle(20, 0, 20, 40);
            g.FillRectangle(new SolidBrush(this._Cyan), rect);
            rect = new Rectangle(40, 0, 20, 40);
            g.FillRectangle(new SolidBrush(this._Blue), rect);
            rect = new Rectangle(60, 0, 20, 40);
            g.FillRectangle(new SolidBrush(this._LimeGreen), rect);
            rect = new Rectangle(80, 0, 20, 40);
            g.FillRectangle(new SolidBrush(this._Orange), rect);
            rect = new Rectangle(100, 0, 20, 40);
            g.FillRectangle(new SolidBrush(this._Purple), rect);
            rect = new Rectangle(120, 0, 20, 40);
            g.FillRectangle(new SolidBrush(this._Black), rect);
            rect = new Rectangle(140, 0, 20, 40);
            g.FillRectangle(new SolidBrush(this._Gray), rect);
            rect = new Rectangle(160, 0, 20, 40);
            g.FillRectangle(new SolidBrush(this._White), rect);
            rect = new Rectangle(0, 0x16, this.W, this.H);
            g.DrawString("Color Palette", this.Font, new SolidBrush(this._White), rect, Helpers.CenterSF);
            g = null;
            base.OnPaint(e);
            Helpers.G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
            Helpers.B.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = 180;
            this.Height = 80;
        }

        [Category("Colors")]
        public Color Black
        {
            get
            {
                return this._Black;
            }
            set
            {
                this._Black = value;
            }
        }

        [Category("Colors")]
        public Color Blue
        {
            get
            {
                return this._Blue;
            }
            set
            {
                this._Blue = value;
            }
        }

        [Category("Colors")]
        public Color Cyan
        {
            get
            {
                return this._Cyan;
            }
            set
            {
                this._Cyan = value;
            }
        }

        [Category("Colors")]
        public Color Gray
        {
            get
            {
                return this._Gray;
            }
            set
            {
                this._Gray = value;
            }
        }

        [Category("Colors")]
        public Color LimeGreen
        {
            get
            {
                return this._LimeGreen;
            }
            set
            {
                this._LimeGreen = value;
            }
        }

        [Category("Colors")]
        public Color Orange
        {
            get
            {
                return this._Orange;
            }
            set
            {
                this._Orange = value;
            }
        }

        [Category("Colors")]
        public Color Purple
        {
            get
            {
                return this._Purple;
            }
            set
            {
                this._Purple = value;
            }
        }

        [Category("Colors")]
        public Color Red
        {
            get
            {
                return this._Red;
            }
            set
            {
                this._Red = value;
            }
        }

        [Category("Colors")]
        public Color White
        {
            get
            {
                return this._White;
            }
            set
            {
                this._White = value;
            }
        }
    }
}