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
        int lr, lg, lb, dr, dg, db;
        List<square> board = new List<square>();
        bool colorChange = false;
        public Form1()
        {
            InitializeComponent();


        }
        public List<Bitmap> images;
       
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
                if (!of.FileName.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase))
                {
                    MessageBox.Show("File must be a gif!", "Error: File type mismatch");
                }
                else
                {
                    
                    lsquareghost = Color.FromArgb(255, !hex?(int)LSR.Value:lr, !hex?(int)LSG.Value:lg, !hex?(int)LSB.Value:lb);
                    dsquareghost = Color.FromArgb(255, !hex?(int)RSR.Value:dr, !hex?(int)RSG.Value:dg, !hex?(int)RSB.Value:db);

                    Game = new ChessBoard();


                    board.Clear();
                    int[] ranks = { 1, 2, 3, 4, 5, 6, 7, 8 };
                    if (!whitebottom)
                        ranks = ranks.Reverse().ToArray();
                    char[] f = (whitebottom ? new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' } : new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' }.Reverse().ToArray());
                    for (int i = 1; i < 9; i++)
                    {
                        int sx = (int)sizeud.Value / 8;
                        int sy = (int)sizeud.Value / 8;
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



                    }
                    string fl = of.FileName;
                    images = new List<Bitmap>();
                    AnimatedGif gif = new AnimatedGif(fl, images);

                    foreach (square s in board)
                    {
                        if (s.name == "e1")
                        {

                            wte = PictureAnalysis.GetMostUsedColorList(images[0].Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0), 3);
                        }
                        if (s.name == "e8")
                        {
                            blk = PictureAnalysis.GetMostUsedColorList(images[0].Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0), 3);
                        }
                        if (s.name == "e5")
                        {
                            dsquare = PictureAnalysis.GetMostUsedColor(images[0].Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0));
                        }
                        if (s.name == "d5")
                        {
                            lsquare = PictureAnalysis.GetMostUsedColor(images[0].Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0));
                        }
                    }









                    readGame(images);
                }

            }
            void readGame(List<Bitmap> ims)
            {

                int count = 0;
                string pgn = "";
                int num = 0;
                bool t1 = true;

                foreach (Bitmap b in ims)
                {
                    if (Game.Moves().Length == 0)
                        goto endedGame;
                    string start = "";
                    string dest = "";
                    count++;
                    Color most = new Color();

                    if (t1)
                    {
                        //here will be more complex code comparing for turn one specific stuff
                        foreach (square s in board)
                        {

                            if (s.rank == 2)
                            {

                                Bitmap cur = b.Clone(new RectangleF(new PointF(s.topleft[0], s.topleft[1]), new SizeF(s.width, s.height)), 0);
                                most = PictureAnalysis.GetMostUsedColor(cur);
                                s.cur_empty = tolCheck(most, 5, !colorChange ? lsquare : lsquareghost) || tolCheck(most, 5, !colorChange ? dsquare : dsquareghost);

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
                            if ((s.name == "e9" || s.name == "a9")||count == 19)
                            {
                                st = Game.ToAscii();
                            }
                            if (!tolCheck(curcol, 10, prevcol))
                            {
                                // pictureBox2.Image = cur;
                                //pictureBox3.Image = prev;
                                // goto endedGame;
                                // s.changed = true;
                                if (colorChange && (tolCheck(curcol, 5, lsquare) || tolCheck(curcol, 5, dsquare)))
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
                                else if (tolCheck(curcol, 7, !colorChange ? lsquare : lsquareghost) || tolCheck(curcol, 7, !colorChange ? dsquare : dsquareghost))
                                {
                                    start = s.name;
                                    s.cur_empty = true;
                                }
                                else
                                {
                                    dest = s.name;
                                }
                               
                                 if (moves == 2)
                                {
                                    foreach (square sss in castle)
                                    {
                                        if (sss.file == 'g')
                                        {
                                            try
                                            {
                                                Game.Move("O-O");
                                                goto Done;
                                            }
                                            catch
                                           {
                                                goto endedGame;
                                           }
                                        }
                                        if (sss.file == 'c')
                                        {
                                            try
                                            {
                                                Game.Move("O-O-O");
                                                goto Done;
                                            }
                                            catch
                                            {
                                                goto endedGame;
                                            }
                                        }
                                    }

                                }
                                else if (starts == 2)
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
                            }
                        EmpChng:
                            Console.WriteLine();
                        } //this last
                        st = Game.ToAscii();
                        Console.WriteLine(st);
                        //Console.Read();

                        try { Game.Move(new Move(start, dest));
                           
                                    }
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
                pgn = Game.ToPgn();
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
       

        
      
        bool hex = false;
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!colorChange)
            {
                colorChange = true;
                colswit.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
               
                if (hex)
                {
                    label2.Visible = true;
                    label5.Visible = true;
                    lhbox.Visible = true;
                    dhbox.Visible = true;
                    LSR.Visible = false;
                    LSG.Visible = false;
                    LSB.Visible = false;
                    RSR.Visible = false;
                    RSG.Visible = false;
                    RSB.Visible = false;
                }
                else
                {

                    lhbox.Visible = false;
                    dhbox.Visible = false;
                    LSR.Visible = true;
                    LSG.Visible = true;
                    LSB.Visible = true;
                    RSR.Visible = true;
                    RSG.Visible = true;
                    RSB.Visible = true;
                    label2.Visible = false;
                    label5.Visible = false;
                }
            }
            else
            {
                label3.Visible = false;
                label4.Visible = false;
                label2.Visible = false;
                label5.Visible = false;
                colorChange = false;
                colswit.Visible = false;
                lhbox.Visible = false;
                dhbox.Visible = false;
                LSR.Visible = false;
                LSG.Visible = false;
                LSB.Visible = false;
                RSR.Visible = false;
                RSG.Visible = false;
                RSB.Visible = false;
            }
        }

    

        private void colswit_Click(object sender, EventArgs e)
        {
            if (colorChange)
                if (!hex)
                {
                    hex = true;
                    lhbox.Visible = true;
                    dhbox.Visible = true;
                    LSR.Visible = false;
                    LSG.Visible = false;
                    LSB.Visible = false;
                    RSR.Visible = false;
                    RSG.Visible = false;
                    RSB.Visible = false;
                    colswit.Text = "HEX";
                }
                else
                {
                    hex = false;
                    lhbox.Visible = !true;
                    dhbox.Visible = !true;
                    LSR.Visible = !false;
                    LSG.Visible = !false;
                    LSB.Visible = !false;
                    RSR.Visible = !false;
                    RSG.Visible = !false;
                    RSB.Visible = !false;
                    colswit.Text = "RGB";
                }

        }

        private void lhbox_Leave(object sender, EventArgs e)
        {

            string lht = lhbox.Text; try
            {
                
            string add = "";
            while (lht.Length != 6)
            {
                lht += "0";
                add += "0";
            }
            lhbox.Text += add;
           
                lr = Convert.ToInt32(lht[0] + "" + lht[1], 16);
                lg = Convert.ToInt32(lht[2] + "" + lht[3], 16);
                lb = Convert.ToInt32(lht[4] + "" + lht[5], 16);

                if (lr < 0 || lr > 255 || lg < 0 || lg > 255 || lb < 0 || lb > 255)
                {
                    lhbox.Text = "FFFFFF";
                    lht = lhbox.Text;
                    lr = Convert.ToInt32(lht[0] + "" + lht[1], 16);
                    lg = Convert.ToInt32(lht[2] + "" + lht[3], 16);
                    lb = Convert.ToInt32(lht[4] + "" + lht[5], 16);
                }
            }
            catch
            {
                lhbox.Text = "FFFFFF";
                lht = lhbox.Text;
                lr = Convert.ToInt32(lht[0] + "" + lht[1], 16);
                lg = Convert.ToInt32(lht[2] + "" + lht[3], 16);
                lb = Convert.ToInt32(lht[4] + "" + lht[5], 16);
            }

        }

        private void dhbox_Leave(object sender, EventArgs e)
        {
            string dht = dhbox.Text; try
            {
               
                string add = "";
            while (dht.Length != 6)
            {
                dht += "0";
                add += "0";
            }
            dhbox.Text += add;

           
                dr = Convert.ToInt32(dht[0] + "" + dht[1], 16);
                dg = Convert.ToInt32(dht[2] + "" + dht[3], 16);
                db = Convert.ToInt32(dht[4] + "" + dht[5], 16);

                if (lr < 0 || lr > 255 || lg < 0 || lg > 255 || lb < 0 || lb > 255)
                {
                    dhbox.Text = "FFFFFF";
                    dht = lhbox.Text;
                    dr = Convert.ToInt32(dht[0] + "" + dht[1], 16);
                    dg = Convert.ToInt32(dht[2] + "" + dht[3], 16);
                    db = Convert.ToInt32(dht[4] + "" + dht[5], 16);
                }
            }
            catch
            {
                dhbox.Text = "FFFFFF";
                dht = lhbox.Text;
                dr = Convert.ToInt32(dht[0] + "" + dht[1], 16);
                dg = Convert.ToInt32(dht[2] + "" + dht[3], 16);
                db = Convert.ToInt32(dht[4] + "" + dht[5], 16);
            }

        }

        private void LSR_ValueChanged(object sender, EventArgs e)
        {
            LSR.Value = (int)LSR.Value;
        }

        private void LSG_ValueChanged(object sender, EventArgs e)
        {
            LSG.Value = (int)LSG.Value;
        }

        private void LSB_ValueChanged(object sender, EventArgs e)
        {
            LSB.Value = (int)LSB.Value;
        }

        private void RSR_ValueChanged(object sender, EventArgs e)
        {
            RSR.Value = (int)RSR.Value;
        }

        private void RSG_ValueChanged(object sender, EventArgs e)
        {
            RSG.Value = (int)RSG.Value;

        }

        private void RSB_ValueChanged(object sender, EventArgs e)
        {
            RSB.Value = (int)RSB.Value;

        }
    }
}
