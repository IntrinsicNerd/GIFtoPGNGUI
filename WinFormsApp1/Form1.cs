using Chess;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Documents.Serialization;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public ChessBoard Game;
        public static bool whitebottom = true;
        public static Random lolxd = new Random();
        public Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
            }
            return bitmap;
        }
        List<square> board = new List<square>();
        bool colorChange = false;
        public Form1()
        {
            InitializeComponent();


        }
        public List<Bitmap> images;
        Bitmap squarebm;
        #region code I'm copying
        public class AnimatedGif
        {
            private List<AnimatedGifFrame> mImages = new List<AnimatedGifFrame>();
            PropertyItem mTimes;
            public AnimatedGif(string path)
            {
                Image img = Image.FromFile(path);
                int frames = img.GetFrameCount(FrameDimension.Time);
                if (frames <= 1) throw new ArgumentException("Image not animated");
                byte[] times = img.GetPropertyItem(0x5100).Value;
                int frame = 0;
                for (; ; )
                {
                    int dur = BitConverter.ToInt32(times, 4 * frame);
                    mImages.Add(new AnimatedGifFrame(new Bitmap(img), dur));
                    if (++frame >= frames) break;
                    img.SelectActiveFrame(FrameDimension.Time, frame);
                }
                img.Dispose();
            }
            public AnimatedGif(string path, List<Bitmap> bm)
            {
                bm.Clear();
                Image img = Image.FromFile(path);
                int frames = img.GetFrameCount(FrameDimension.Time);
                if (frames <= 1) throw new ArgumentException("Image not animated");
                byte[] times = img.GetPropertyItem(0x5100).Value;
                int frame = 0;
                for (; ; )
                {
                    int dur = BitConverter.ToInt32(times, 4 * frame);
                    mImages.Add(new AnimatedGifFrame(new Bitmap(img), dur));
                    bm.Add(new Bitmap(new AnimatedGifFrame(new Bitmap(img), dur).Image, new Size(400, 400)));
                    if (++frame >= frames) break;
                    img.SelectActiveFrame(FrameDimension.Time, frame);
                }
                img.Dispose();
            }
            public List<AnimatedGifFrame> Images { get { return mImages; } }
        }

        public class AnimatedGifFrame
        {
            private int mDuration;
            private Image mImage;
            internal AnimatedGifFrame(Image img, int duration)
            {
                mImage = img; mDuration = duration;
            }
            public Image Image { get { return mImage; } }
            public int Duration { get { return mDuration; } }
        }


        class PictureAnalysis
        {
            public static List<Color> TenMostUsedColors { get; private set; }
            public static List<int> TenMostUsedColorIncidences { get; private set; }

            public static Color MostUsedColor { get; private set; }
            public static int MostUsedColorIncidence { get; private set; }

            private static int pixelColor;

            private static Dictionary<int, int> dctColorIncidence;

            public static Color GetMostUsedColor(Bitmap theBitMap, bool second = false)
            {
                TenMostUsedColors = new List<Color>();
                TenMostUsedColorIncidences = new List<int>();

                MostUsedColor = Color.Empty;
                MostUsedColorIncidence = 0;

                // does using Dictionary<int,int> here
                // really pay-off compared to using
                // Dictionary<Color, int> ?

                // would using a SortedDictionary be much slower, or ?

                dctColorIncidence = new Dictionary<int, int>();

                // this is what you want to speed up with unmanaged code
                for (int row = 0; row < theBitMap.Size.Width; row++)
                {
                    for (int col = 0; col < theBitMap.Size.Height; col++)
                    {
                        pixelColor = theBitMap.GetPixel(row, col).ToArgb();

                        if (dctColorIncidence.Keys.Contains(pixelColor))
                        {
                            dctColorIncidence[pixelColor]++;
                        }
                        else
                        {
                            dctColorIncidence.Add(pixelColor, 1);
                        }
                    }
                }

                // note that there are those who argue that a
                // .NET Generic Dictionary is never guaranteed
                // to be sorted by methods like this
                var dctSortedByValueHighToLow = dctColorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                // this should be replaced with some elegant Linq ?
                foreach (KeyValuePair<int, int> kvp in dctSortedByValueHighToLow.Take(10))
                {
                    TenMostUsedColors.Add(Color.FromArgb(kvp.Key));
                    TenMostUsedColorIncidences.Add(kvp.Value);
                }
                if (second)
                {
                    dctSortedByValueHighToLow.Remove(dctSortedByValueHighToLow.First().Key);


                }
                MostUsedColor = Color.FromArgb(dctSortedByValueHighToLow.First().Key);
                MostUsedColorIncidence = dctSortedByValueHighToLow.First().Value;


                return MostUsedColor;
            }

            public static List<Color> GetMostUsedColorList(Bitmap theBitMap, int length)
            {
                TenMostUsedColors = new List<Color>();
                TenMostUsedColorIncidences = new List<int>();

                MostUsedColor = Color.Empty;
                MostUsedColorIncidence = 0;

                // does using Dictionary<int,int> here
                // really pay-off compared to using
                // Dictionary<Color, int> ?

                // would using a SortedDictionary be much slower, or ?

                dctColorIncidence = new Dictionary<int, int>();

                // this is what you want to speed up with unmanaged code
                for (int row = 0; row < theBitMap.Size.Width; row++)
                {
                    for (int col = 0; col < theBitMap.Size.Height; col++)
                    {
                        pixelColor = theBitMap.GetPixel(row, col).ToArgb();

                        if (dctColorIncidence.Keys.Contains(pixelColor))
                        {
                            dctColorIncidence[pixelColor]++;
                        }
                        else
                        {
                            dctColorIncidence.Add(pixelColor, 1);
                        }
                    }
                }

                // note that there are those who argue that a
                // .NET Generic Dictionary is never guaranteed
                // to be sorted by methods like this
                var dctSortedByValueHighToLow = dctColorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                // this should be replaced with some elegant Linq ?
                foreach (KeyValuePair<int, int> kvp in dctSortedByValueHighToLow.Take(10))
                {
                    TenMostUsedColors.Add(Color.FromArgb(kvp.Key));
                    TenMostUsedColorIncidences.Add(kvp.Value);
                }
                List<Color> cols = new List<Color>();
                cols.Add(Color.FromArgb(dctSortedByValueHighToLow.First().Key));
                MostUsedColorIncidence = dctSortedByValueHighToLow.First().Value;
                while (cols.Count < length)
                {
                    dctSortedByValueHighToLow.Remove(dctSortedByValueHighToLow.First().Key);
                    cols.Add(Color.FromArgb(dctSortedByValueHighToLow.First().Key));
                }


                return cols;
            }

        }

        List<Color> wte = new List<Color>();
        List<Color> blk = new List<Color>();
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            PictureAnalysis pic = new PictureAnalysis();

            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "GIF files(*.gif)|";
            Color lsquare = new Color();
            Color dsquare = new Color();
            Color lsquareghost = new Color();
            Color dsquareghost = new Color();
            if (of.ShowDialog() == DialogResult.OK)
            {
                lsquareghost = Color.FromArgb(255, (int)LSR.Value,(int) LSG.Value, (int)LSB.Value);
                dsquareghost = Color.FromArgb(255, (int)RSR.Value, (int)RSG.Value, (int)RSB.Value);

                Game = new ChessBoard();
                timer1.Start();
                comboBox1.Items.Clear();
                board.Clear();
                int[] ranks = { 1, 2, 3, 4, 5, 6, 7, 8 };
                if (!whitebottom)
                    ranks = ranks.Reverse().ToArray();
                char[] f = (whitebottom ? new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' } : new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' }.Reverse().ToArray());
                for (int i = 1; i < 9; i++)
                {
                    int sx = pictureBox1.Width / 8;
                    int sy = pictureBox1.Height / 8;
                    for (int k = 0; k < 8; k++)
                    {
                        byte v = (byte)((((i + (k + 1)) % 2 == 0)) ? 1 : 0);
                        board.Add(
                            new square(sy / 4, sx / 6,
                            ranks[i - 1], f[k],
                            new int[] { (k * sy) + sy / 2, ((8 - i) * sx) + sx / 2 },
                            (i > 2 && i < 7) ? true : false, v));
                        //full squares
                        //board.Add(
                        //    new square(sy, sx,
                        //    ranks[i - 1], f[k],
                        //    new int[] { (k * sy), ((8 - i) * sx) },
                        //    (i > 2 && i < 7) ? true : false, v));
                    }

                    comboBox1.Items.Clear();
                    foreach (square s in board)
                    {
                        comboBox1.Items.Add(s.name);
                    }
                }
                string fl = of.FileName;
                //Stream imageStreamSource = new FileStream(fl, FileMode.Open, FileAccess.Read, FileShare.Read);
                //GifBitmapDecoder dec = new GifBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                images = new List<Bitmap>();
                AnimatedGif gif = new AnimatedGif(of.FileName, images);
                //foreach (BitmapSource b in dec.Frames)
                //{
                //    int fc = 0;

                //    images.Add(new Bitmap(BitmapFromSource(b), new Size(400, 400)));
                //}
                foreach (square s in board)
                {
                    if (s.name == "e1")
                    {
                        // wte = images[0].Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0).GetPixel((s.width / 2) - 5, (s.height * 2) / 4);

                        wte = PictureAnalysis.GetMostUsedColorList(images[0].Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0), 3);
                    }
                    if (s.name == "e8")
                    {
                        blk = PictureAnalysis.GetMostUsedColorList(images[0].Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0), 3);
                        //blk = images[0].Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0).GetPixel((s.width / 2) - 5, (s.height * 2) / 4);
                    }
                    if (s.name == "e5" )
                    {
                        dsquare = PictureAnalysis.GetMostUsedColor(images[0].Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0));
                    }
                    if (s.name == "d5" )
                    {
                        lsquare = PictureAnalysis.GetMostUsedColor(images[0].Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0));
                    }
                }


                pictureBox1.Image = images[0];
                square rand = board[lolxd.Next(board.Count)];
                label1.Text = rand.name + ", " + (rand.color == 0 ? "White" : "Black");
                squarebm = images[0].Clone(new RectangleF(new PointF(rand.topleft[0], rand.topleft[1]), new SizeF(rand.width, rand.height)), 0);
                pictureBox2.Image = squarebm;


                //implement timer code so that the color changes
                //whiteBox.BackColor = wte;
                //blackBox.BackColor = blk;
                if (chk)
                    readGame(images);
                List<Bitmap> LastTurn = new List<Bitmap>();

            }
            void readGame(List<Bitmap> ims)
            {

                int count = 0;
                string pgn = "";
                int num = 0;
                bool t1 = true;

                foreach (Bitmap b in ims)
                {
                    string start = "";
                    string dest = "";
                    count++;
                    Color most = new Color();
                    pictureBox1.Image = ims[num];
                    if (t1)
                    {
                        //here will be more complex code comparing for turn one specific stuff
                        foreach (square s in board)
                        {

                            if (s.rank == 2)
                            {
                               
                                Bitmap cur = b.Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0);
                                most = PictureAnalysis.GetMostUsedColor(cur);
                                s.cur_empty = tolCheck(most, 5, !colorChange?lsquare:lsquareghost) || tolCheck(most, 5, !colorChange?dsquare:dsquareghost);

                                if (s.cur_empty)
                                {
                                    Bitmap three = b.Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1] - s.height), new SizeF(s.width, s.height)), 0);
                                    bool emp = tolCheck(most, 5, !colorChange ? lsquare : lsquareghost) || tolCheck(most, 5, !colorChange ? dsquare : dsquareghost);
                                    string desty = s.file.ToString() + (!emp ? "3" : "4");
                                    Game.Move(desty);
                                    foreach (square ss in board)
                                    {
                                        if (ss.name == desty)
                                        {
                                            ss.cur_empty = false;
                                            string debug = Game.ToAscii();
                                            goto EndT1;
                                        }
                                    }


                                }
                            }
                            else if (s.rank == 1 && (s.file == 'b' || s.file == 'g'))
                            {

                                Bitmap cur = b.Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0);
                                most = PictureAnalysis.GetMostUsedColor(cur);
                                s.cur_empty = tolCheck(most, 5, !colorChange ? lsquare : lsquareghost) || tolCheck(most, 5, !colorChange ? dsquare : dsquareghost);
                                if (s.cur_empty)
                                {
                                    Bitmap three = b.Clone(new RectangleF(new PointF(s.topleft[0] - s.width * 12, s.topleft[1] - (s.height * 4)), new SizeF(s.width, s.height)), 0);
                                    most = PictureAnalysis.GetMostUsedColor(three);
                                    bool emp = tolCheck(most, 5, !colorChange ? lsquare : lsquareghost) || tolCheck(most, 5, !colorChange ? dsquare : dsquareghost);
                                    string desty = (!emp ? ((char)(s.file - 1)).ToString() : ((char)(s.file + 1)).ToString()) + "3";
                                    string mov = "N" + desty;
                                    Game.Move(mov);
                                    foreach (square ss in board)
                                    {
                                        if (ss.name == desty)
                                        {
                                            ss.cur_empty = false;
                                            goto EndT1;
                                        }
                                    }
                                    pictureBox3.Image = three;
                                    pictureBox2.Image = cur;

                                }
                            }


                        }
                    EndT1:
                        t1 = false;
                    }

                    //run code to see what move was made and add it to the pgn
                    else
                    {
                        String st = "";
                        List<square> castle = new List<square>();
                        List<square> ep = new List<square>();
                        byte moves = 0;
                        byte starts = 0;
                        foreach (square s in board)
                        {

                            
                            if (s.cur_empty)
                                s.was_empty = true;
                            else s.was_empty = false;
                          
                            Bitmap cur = b.Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0);
                            Bitmap prev = ims[ims.IndexOf(b) - 1].Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0);

                            Color curcol = PictureAnalysis.GetMostUsedColor(cur);
                            Color prevcol = PictureAnalysis.GetMostUsedColor(prev);
                            if ((s.name == "e5" || s.name == "d6") && count ==5)
                            {
                                st = Game.ToAscii();
                            }
                            if (!tolCheck(curcol, 10, prevcol) )
                            {
                                // pictureBox2.Image = cur;
                                //pictureBox3.Image = prev;
                                // goto endedGame;
                                // s.changed = true;
                                if (colorChange && (tolCheck(curcol,5,lsquare) || tolCheck(curcol,5,dsquare)))
                                {
                                    s.cur_empty = true;
                                    goto EmpChng;
                                }
                                if (s.was_empty)
                                {
                                    dest = s.name;
                                    s.cur_empty = false;
                                    moves++;
                                    castle.Add(s);
                                }
                                else if (tolCheck(curcol, 5, !colorChange?lsquare:lsquareghost) || tolCheck(curcol, 5, !colorChange?dsquare:dsquareghost))
                                {
                                    start = s.name;
                                    s.cur_empty = true;
                                }
                                else
                                {
                                    dest = s.name;
                                }
                                if (starts == 2)
                                {
                                    if (castle.Count == 1)
                                    {

                                        if (ep[0].file == castle[0].file)
                                        {
                                            Game.Move(new Move(ep[1].name, castle[0].name));
                                            goto Done;
                                        }
                                        else
                                        {
                                            Game.Move(new Move(ep[0].name, castle[0].name));
                                            goto Done;
                                        }
                                    }

                                }
                                else if (moves == 2)
                                {
                                    foreach (square sss in castle)
                                    {
                                        if (sss.file == 'g')
                                        {
                                            Game.Move("O-O");
                                            goto Done;
                                        }
                                        if (sss.file == 'c')
                                        {
                                            Game.Move("O-O-O");
                                            goto Done;
                                        }
                                    }

                                }
                            }
                        EmpChng:
                            Console.WriteLine();
                        } //this last
                        st = Game.ToAscii();
                        Console.WriteLine(st);
                        //Console.Read();
                       
                        try { Game.Move(new Move(start, dest)); }
                        catch { goto endedGame; }

                    Done:
                        Console.WriteLine();
                        // LastTurn.Clear();
                    }
                    pgn = Game.ToPgn();
                    richTextBox1.Text = Game.ToAscii();

                    num++;
                }
            endedGame:
                Console.WriteLine();
                richTextBox1.Text = pgn;


            }
            void comparesquare()
            {

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (whitebottom)
            {
                whitebottom = false;
                button2.Text = "Click for white on bottom";
            }
            else
            {
                whitebottom = true;
                button2.Text = "Click for black on bottom";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                square rand = board[lolxd.Next(board.Count)];
                label1.Text = rand.name + ", " + (rand.color == 0 ? "White" : "Black");
                squarebm = images[imnum].Clone(new RectangleF(new PointF(rand.topleft[0], rand.topleft[1]), new SizeF(rand.width, rand.height)), 0);
                pictureBox2.Image = squarebm;
            }
            catch { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            square s = board[comboBox1.SelectedIndex];
            label2.Text = s.color == 0 ? "White" : "Black";
            Bitmap b = images[imnum].Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0);
            pictureBox3.Image = b;
            selbox.BackColor = PictureAnalysis.GetMostUsedColor(b);

        }
        int imnum = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            if (!(imnum == images.Count - 1))
            {
                imnum++;
                pictureBox1.Image = images[imnum];

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!(imnum == 0))
            {
                imnum--;
                pictureBox1.Image = images[imnum];

            }
        }
        int timercount = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                whiteBox.BackColor = wte[timercount];
                blackBox.BackColor = blk[timercount];
                timercount++;
                if (timercount >= wte.Count)
                    timercount = 0;
            }
            catch
            {

            }

        }

        static bool tolCheck(Color c, int tol, List<Color> CompareList)
        {
            foreach (Color comp in CompareList)
            {
                for (int i = 0; i < tol; i++)
                {
                    for (int k = 0; k < tol; k++)
                    {

                        for (int m = 0; m < tol; m++)
                        {
                            if ((c.R + i == comp.R || c.R - i == comp.R) && (c.G + k == comp.G || c.G - k == comp.G) && (c.B - m == comp.B || c.B + m == comp.B))
                            {
                                return true;
                            }

                        }
                    }




                }
            }
            return false;
        }
        static bool tolCheck(Color c, int tol, Color comp)
        {

            for (int i = 0; i < tol; i++)
            {
                for (int k = 0; k < tol; k++)
                {

                    for (int m = 0; m < tol; m++)
                    {
                        if ((c.R + i == comp.R || c.R - i == comp.R) && (c.G + k == comp.G || c.G - k == comp.G) && (c.B - m == comp.B || c.B + m == comp.B))
                        {
                            return true;
                        }

                    }
                }




            }

            return false;
        }

        public class square
        {
            public string name { get; }
            public int height { get; }
            public int width { get; }
            public int rank { get; }
            public char file { get; }
            public int[] topleft { get; }
            public bool cur_empty { get; set; }
            public bool was_empty { get; set; }
            public bool changed { get; set; }
            public byte color { get; } //0 light square 1 dark square
            public square()
            {

            }
            public square(int h, int w, int r, char f, int[] tl, bool ce, byte col)
            {
                height = h;
                width = w;
                rank = r;
                file = f;
                name = file + "" + rank;
                topleft = tl;
                cur_empty = ce;
                was_empty = false;
                changed = false;
                color = col;
            }


        }
        bool chk = false;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                chk = true;
            else
                chk = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                colorChange = true;
            else
                colorChange = false;
            
        }
    }
}
