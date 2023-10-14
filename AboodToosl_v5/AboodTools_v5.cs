using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AboodTools_v5
{
    public class AboodPictureBox : PictureBox
    {
        //Fields
        private int borderSize = 2;
        private Color borderColor1 = Color.RoyalBlue;
        private Color borderColor2 = Color.HotPink;
        private DashStyle borderLineStyle = DashStyle.Solid;
        private DashCap borderCapStyle = DashCap.Flat;
        private float gradiantAngle = 50F;

        //Constractore
        public AboodPictureBox()
        {
            this.Size = new Size(100, 100);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        //Properties
        [Category("AboodPictureBox")]
        public int BorderSize
        {
            get
            {
                return borderSize;
            }

            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        [Category("AboodPictureBox")]
        public Color BorderColor1
        {
            get
            {
                return borderColor1;
            }

            set
            {
                borderColor1 = value;
                this.Invalidate();
            }
        }
        [Category("AboodPictureBox")]
        public Color BorderColor2
        {
            get
            {
                return borderColor2;
            }

            set
            {
                borderColor2 = value;
                this.Invalidate();
            }
        }
        [Category("AboodPictureBox")]
        public DashStyle BorderLineStyle
        {
            get
            {
                return borderLineStyle;
            }

            set
            {
                borderLineStyle = value;
                this.Invalidate();
            }
        }
        [Category("AboodPictureBox")]
        public DashCap BorderCapStyle
        {
            get
            {
                return borderCapStyle;
            }

            set
            {
                borderCapStyle = value;
                this.Invalidate();
            }
        }
        [Category("AboodPictureBox")]
        public float GradiantAngle
        {
            get
            {
                return gradiantAngle;
            }

            set
            {
                gradiantAngle = value;
                this.Invalidate();
            }
        }
        //Methids
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = new Size(this.Width, this.Height);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            //Fields
            var graph = pe.Graphics;
            var rectCounterSmoth = Rectangle.Inflate(this.ClientRectangle, -1, -1);
            var rectBorder = Rectangle.Inflate(rectCounterSmoth, -borderSize, -borderSize);
            var smothingSize = borderSize > 0 ? borderSize * 3 : 1;
            using (var borderGColor = new LinearGradientBrush(rectBorder, borderColor1, borderColor2, gradiantAngle))
            using (var pathRegion = new GraphicsPath())
            using (var penSmoth = new Pen(this.Parent.BackColor, smothingSize))
            using (var penBorder = new Pen(borderGColor, borderSize))
            {
                penBorder.DashStyle = borderLineStyle;
                penBorder.DashCap = borderCapStyle;
                pathRegion.AddEllipse(rectCounterSmoth);
                this.Region = new Region(pathRegion);
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                //Drawing
                graph.DrawEllipse(penSmoth, rectCounterSmoth);
                if (borderSize > 0)
                    graph.DrawEllipse(penBorder, rectBorder);
            }
        }
    }
    public class AboodToggleButton : CheckBox
    {
        //Fields
        private Color onBackColor = Color.RoyalBlue;
        private Color onToggleColor = Color.WhiteSmoke;
        private Color offBackColor = Color.Gray;
        private Color offToggleColor = Color.Gainsboro;
        private bool solidStyle = true;

        [Category("AboodToggleButton")]
        public Color OnBackColor
        {
            get
            {
                return onBackColor;
            }

            set
            {
                onBackColor = value;
                this.Invalidate();
            }
        }
        [Category("AboodToggleButton")]
        public Color OnToggleColor
        {
            get
            {
                return onToggleColor;
            }

            set
            {
                onToggleColor = value;
                this.Invalidate();
            }
        }
        [Category("AboodToggleButton")]
        public Color OffBackColor
        {
            get
            {
                return offBackColor;
            }

            set
            {
                offBackColor = value;
                this.Invalidate();
            }
        }
        [Category("AboodToggleButton")]
        public Color OffToggleColor
        {
            get
            {
                return offToggleColor;
            }

            set
            {
                offToggleColor = value;
                this.Invalidate();
            }
        }
        [Category("AboodToggleButton")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {

            }
        }
        [DefaultValue(true)]
        [Category("AboodToggleButton")]
        public bool SolidStyle
        {
            get
            {
                return solidStyle;
            }

            set
            {
                solidStyle = value;
                this.Invalidate();
            }
        }

        //Countsructor
        public AboodToggleButton()
        {
            this.MinimumSize = new Size(45, 22);
        }
        //methods 
        private GraphicsPath GetFigurePath()
        {
            int arcSize = this.Height - 1;
            Rectangle leftarc = new Rectangle(0, 0, arcSize, arcSize);
            Rectangle rightarc = new Rectangle(this.Width - arcSize - 2, 0, arcSize, arcSize);
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(leftarc, 90, 180);
            path.AddArc(rightarc, 270, 180);
            path.CloseAllFigures();
            return path;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            int toggleSize = this.Height - 5;
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.Clear(this.Parent.BackColor);
            if (this.Checked)//ON
            {
                //Draw The Control Surface
                if (solidStyle)
                    pevent.Graphics.FillPath(new SolidBrush(onBackColor), GetFigurePath());
                else pevent.Graphics.DrawPath(new Pen(onBackColor), GetFigurePath());
                //Draw THe Toggle
                pevent.Graphics.FillEllipse(new SolidBrush(onToggleColor),
                    new Rectangle(this.Width - this.Height + 1, 2, toggleSize, toggleSize));
            }
            else//OFF
            {
                //Draw The Control Surface
                if (solidStyle)
                    pevent.Graphics.FillPath(new SolidBrush(offBackColor), GetFigurePath());
                else pevent.Graphics.DrawPath(new Pen(offBackColor), GetFigurePath());
                //Draw THe Toggle
                pevent.Graphics.FillEllipse(new SolidBrush(offToggleColor),
                    new Rectangle(2, 2, toggleSize, toggleSize));
            }

        }
    }
    public class AboodButton : Button
    {
        //Fildes
        private int borderSize = 0;
        private int borderRadius = 40;
        private Color borderColor = Color.PaleVioletRed;

        //Properties
        [Category("AboodButton")]
        public int BorderSize
        {
            get
            {
                return borderSize;
            }

            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        [Category("AboodButton")]
        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }

            set
            {
                if (borderRadius <= this.Height)
                    borderRadius = value;
                else borderRadius = this.Height;
                Invalidate();
            }
        }
        [Category("AboodButton")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }

            set
            {
                borderColor = value;
                Invalidate();
            }
        }
        [Category("AboodButton")]
        public Color BackGraundColor
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
        [Category("AboodButton")]
        public Color TextColor
        {
            get
            {
                return this.ForeColor;
            }
            set
            {
                this.ForeColor = value;
            }
        }

        //Constractor
        public AboodButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Size = new Size(150, 40);
            BackColor = Color.MediumSlateBlue;
            ForeColor = Color.White;
            this.Resize += new EventHandler(Button_Resize);
        }
        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(0, 0, this.Width, this.Height);
            if (borderRadius > 2)  //Rounded Button
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - 1))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    //Button Surface
                    this.Region = new Region(pathSurface);
                    //Draw Surface Border For HD Result
                    pevent.Graphics.DrawPath(penSurface, pathSurface);
                    //Button Border
                    if (borderSize >= 1)
                        //Draw Control Border
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else    //Normal Button
            {
                //Button Surface
                this.Region = new Region(rectSurface);
                //Button Border
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width, this.Height);

                    }
                }
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Containar_BackColorChanged);
        }

        private void Containar_BackColorChanged(object sender, EventArgs e)
        {
            if (this.DesignMode)
                this.Invalidate();
        }
        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                BorderRadius = this.Height;
        }
    }
    public class AboodProgressBar : ProgressBar
    {
        //Constractor
        public AboodProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.ForeColor = Color.White;
        }
        //feilds
        private Color channelColor = Color.LightSteelBlue;
        private Color sliderColor = Color.RoyalBlue;
        private Color forebackColor = Color.RoyalBlue;
        private int channelHeight = 6;
        private int sliderHeight = 6;
        private TextPosition showValue = TextPosition.Right;
        private string symbolBefor = "";
        private string symbolAfter = "";
        private bool showMaximum = false;
        public enum TextPosition
        {
            Left,
            Right,
            Center,
            Sliding,
            None
        }
        //Others
        private bool paintBack = false;
        private bool stopPainting = false;

        //Properties
        [Category("AboodProgressBar")]
        public Color ChannelColor
        {
            get
            {
                return channelColor;
            }

            set
            {
                channelColor = value;
                this.Invalidate();
            }
        }
        [Category("AboodProgressBar")]
        public Color SliderColor
        {
            get
            {
                return sliderColor;
            }

            set
            {
                sliderColor = value;
                this.Invalidate();
            }
        }
        [Category("AboodProgressBar")]
        public Color ForeBackColor
        {
            get
            {
                return forebackColor;
            }

            set
            {
                forebackColor = value;
                this.Invalidate();
            }
        }
        [Category("AboodProgressBar")]
        public int ChannelHeight
        {
            get
            {
                return channelHeight;
            }

            set
            {
                channelHeight = value;
                this.Invalidate();
            }
        }
        [Category("AboodProgressBar")]
        public int Sliderheight
        {
            get
            {
                return sliderHeight;
            }

            set
            {
                sliderHeight = value;
                this.Invalidate();
            }
        }
        [Category("AboodProgressBar")]
        public TextPosition ShowValue
        {
            get
            {
                return showValue;
            }

            set
            {
                showValue = value;
                this.Invalidate();
            }
        }
        [Category("AboodProgressBar")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                base.Font = value;
                this.Invalidate();
            }
        }
        [Category("AboodProgressBar")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {
                base.ForeColor = value;
                this.Invalidate();
            }
        }
        [Category("AboodProgressBar")]
        public string SymbolBefor
        {
            get
            {
                return symbolBefor;
            }

            set
            {
                symbolBefor = value;
                this.Invalidate();
            }
        }
        [Category("AboodProgressBar")]
        public string SymbolAfter
        {
            get
            {
                return symbolAfter;
            }

            set
            {
                symbolAfter = value;
                this.Invalidate();
            }
        }

        [Category("AboodProgressBar")]
        public bool ShowMaximum
        {
            get
            {
                return showMaximum;
            }

            set
            {
                showMaximum = value;
                this.Invalidate();
            }
        }

        //Paint the BackGroung &Channel
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (stopPainting == false)
            {
                if (paintBack == false)
                {
                    //Feilds
                    Graphics graphic = pevent.Graphics;
                    Rectangle rectChannel = new Rectangle(0, 0, this.Width, channelHeight);
                    using (var brushChannel = new SolidBrush(ChannelColor))
                    {
                        if (channelHeight >= sliderHeight)
                            rectChannel.Y = this.Height - channelHeight;
                        else rectChannel.Y = this.Height - ((channelHeight + sliderHeight) / 2);

                        //Painting
                        graphic.Clear(this.Parent.BackColor);
                        graphic.FillRectangle(brushChannel, rectChannel);

                        //Stop Painting
                        if (this.DesignMode == false)
                            paintBack = false;
                    }
                }
                if (this.Value == this.Maximum || this.Value == this.Minimum)
                    paintBack = false;
            }
        }
        //PrintSlider
        protected override void OnPaint(PaintEventArgs e)
        {
            if (stopPainting == false)
            {
                //Fields
                Graphics graphic = e.Graphics;
                double scaleFactor = (((double)this.Value - this.Minimum) / ((double)this.Maximum - this.Minimum));
                int sliderWidth = (int)(this.Width * scaleFactor);
                Rectangle rectSlider = new Rectangle(0, 0, sliderWidth, sliderHeight);
                using (var brushSlider = new SolidBrush(sliderColor))
                {
                    if (sliderHeight >= channelHeight)
                        rectSlider.Y = this.Height - sliderHeight;
                    else rectSlider.Y = this.Height - ((sliderHeight + channelHeight) / 2);

                    //Painting
                    if (sliderWidth > 1)
                        graphic.FillRectangle(brushSlider, rectSlider);
                    if (showValue != TextPosition.None)
                        DrawValueText(graphic, sliderWidth, rectSlider);
                }
            }
            if (this.Value == this.Maximum) stopPainting = true;
            else stopPainting = false;
        }
        //Paint Value Text
        private void DrawValueText(Graphics graphic, int sliderWidth, Rectangle rectSlider)
        {
            string text = symbolBefor + this.Value.ToString() + symbolAfter;
            if (showMaximum) text = text + "/" + SymbolBefor + this.Maximum.ToString() + symbolAfter;
            var textSize = TextRenderer.MeasureText(text, this.Font);
            var rectText = new Rectangle(0, 0, textSize.Width, textSize.Height + 2);
            using (var brushText = new SolidBrush(this.ForeColor))
            using (var brushTextBack = new SolidBrush(forebackColor))
            using (var textFormat = new StringFormat())
            {
                switch (showValue)
                {
                    case TextPosition.Left:
                        rectText.X = 0;
                        textFormat.Alignment = StringAlignment.Near;
                        break;
                    case TextPosition.Right:
                        rectText.X = this.Width - textSize.Width;
                        textFormat.Alignment = StringAlignment.Far;
                        break;
                    case TextPosition.Center:
                        rectText.X = (this.Width - textSize.Width) / 2;
                        textFormat.Alignment = StringAlignment.Center;
                        break;
                    case TextPosition.Sliding:
                        rectText.X = sliderWidth - textSize.Width;
                        textFormat.Alignment = StringAlignment.Center;
                        //Clean The Previous Text Surface
                        using (var brushClear = new SolidBrush(this.Parent.BackColor))
                        {
                            var rect = rectSlider;
                            rect.Y = rectText.Y;
                            rect.Height = rectText.Height;
                            graphic.FillRectangle(brushClear, rect);
                        }
                        break;
                }
                //Painting 
                graphic.FillRectangle(brushTextBack, rectText);
                graphic.DrawString(text, this.Font, brushText, rectText, textFormat);
            }
        }
    }

    [DefaultEvent("_TextChanged")]
    public class AboodTextBox : UserControl
    {
        //Fields
        private Color borderColor = Color.MediumSlateBlue;
        private int borderSize = 2;
        private bool underLinedStyle = false;
        private Color focusColor = Color.HotPink;
        TextBox textBox1;
        private bool isFocused = false;
        public event EventHandler _TextChanged;
        //Constructor
        public AboodTextBox()
        {
            InitializeComponent();
        }
        //Events
        public static explicit operator TextBox(AboodTextBox txt)
        {
            return new TextBox();
        }
        //propertie 
        #region Properties
        [Category("AboodTextBox")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }
        [Category("AboodTextBox")]
        public int BorderSize
        {
            get
            {
                return borderSize;
            }

            set
            {
                borderSize = value;
                Invalidate();
            }
        }
        [Category("AboodTextBox")]
        public bool UnderLinedStyle
        {
            get
            {
                return underLinedStyle;
            }

            set
            {
                underLinedStyle = value;
                Invalidate();
            }
        }
        [Category("AboodTextBox")]
        public bool PasswordChar
        {
            get
            {
                return textBox1.UseSystemPasswordChar;
            }
            set
            {
                textBox1.UseSystemPasswordChar = value;
            }
        }
        [Category("AboodTextBox")]
        public bool MultiLine
        {
            get
            {
                return textBox1.Multiline;
            }
            set
            {
                textBox1.Multiline = value;
            }
        }
        [Category("AboodTextBox")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                base.BackColor = value;
                textBox1.BackColor = value;
            }
        }
        [Category("AboodTextBox")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {
                base.ForeColor = value;
                textBox1.ForeColor = value;
            }
        }
        [Category("AboodTextBox")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                base.Font = value;
                textBox1.Font = value;
                if (DesignMode)
                    UpDateControleHeight();
            }
        }
        [Category("AboodTextBox")]
        public string _Text
        {
            get
            {
                return textBox1.Text;
            }

            set
            {
                textBox1.Text = value;
            }
        }
        [Category("AboodTextBox")]
        public Color FocusColor
        {
            get
            {
                return focusColor;
            }

            set
            {
                focusColor = value;
            }
        }
        [Category("AboodTextBox")]
        public AutoCompleteSource AutoCompleteSource
        {
            get
            {
                return textBox1.AutoCompleteSource;
            }
            set
            {
                textBox1.AutoCompleteSource = value;
            }
        }
        [Category("AboodTextBox")]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get
            {
                return textBox1.AutoCompleteCustomSource;
            }
            set
            {
                textBox1.AutoCompleteCustomSource = value;
            }
        }
        [Category("AboodTextBox")]
        public AutoCompleteMode AutoCompleteMode
        {
            get
            {
                return textBox1.AutoCompleteMode;
            }
            set
            {
                textBox1.AutoCompleteMode = value;
            }
        }


        #endregion
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphic = e.Graphics;

            //Draw Border 
            using (Pen penBorder = new Pen(borderColor, borderSize))
            {
                penBorder.Alignment = PenAlignment.Inset;

                if (!isFocused)
                {
                    if (underLinedStyle)//line Style
                        graphic.DrawLine(penBorder, 0, this.Height, this.Width, this.Height);
                    else //Normal Style
                        graphic.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                }
                else
                {
                    penBorder.Color = focusColor;
                    if (underLinedStyle)//line Style
                        graphic.DrawLine(penBorder, 0, this.Height, this.Width, this.Height);
                    else //Normal Style
                        graphic.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                }
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DesignMode)
                UpDateControleHeight();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpDateControleHeight();
        }
        private void UpDateControleHeight()
        {
            if (textBox1.Multiline == false)
            {
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1;
                textBox1.Multiline = true;
                textBox1.MinimumSize = new Size(0, txtHeight);
                textBox1.Multiline = false;
                this.Height = textBox1.Height + this.Padding.Top + this.Padding.Bottom;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_TextChanged != null)
                _TextChanged.Invoke(sender, e);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            isFocused = true;
            this.Invalidate();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            isFocused = false;
            this.Invalidate();
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(7, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(210, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            this.textBox1.MouseEnter += new System.EventHandler(this.textBox1_MouseEnter);
            this.textBox1.MouseHover += new System.EventHandler(this.textBox1_MouseHover);
            // 
            // TextBoxe
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TextBoxe";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(224, 37);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
