using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TestTool
{
    /// <summary>
    /// 图片切换效果
    /// </summary>
    class ImgsEffect
    {
        /// <summary>
        /// 水平百叶窗
        /// </summary>
        /// <param name="obmp"></param>
        /// <param name="bmp"></param>
        /// <param name="pic"></param>
        public void Effect_BaiYeH(Bitmap obmp, Bitmap bmp, PictureBox pic)
        {
            int step = 30;
            try
            {
                Bitmap bmp1 = (Bitmap)bmp.Clone();
                int height = bmp1.Height / step;
                int width = bmp1.Width;
                Graphics g = Graphics.FromImage(obmp);
                Point[] MyPoint = new Point[step];
                for (int y = 0; y < step; y++)
                {
                    MyPoint[y].X = 0;
                    MyPoint[y].Y = y * height;
                }
                Bitmap bitmap = new Bitmap(bmp.Width, bmp.Height);
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < step; j++)
                    {
                        for (int k = 0; k < width; k++)
                        {
                            bitmap.SetPixel(MyPoint[j].X + k, MyPoint[j].Y + i, bmp.GetPixel(MyPoint[j].X + k, MyPoint[j].Y + i));
                        }
                    }
                    pic.Refresh();
                    pic.Image = bitmap;

                    System.Threading.Thread.Sleep(20);
                }
                g.Dispose();
                bmp1.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }

        }

        /// <summary>
        /// 垂直百叶窗
        /// </summary>
        /// <param name="obmp"></param>
        /// <param name="bmp"></param>
        /// <param name="pic"></param>
        public void Effect_BaiYeV(Bitmap obmp, Bitmap bmp, PictureBox pic)
        {
            int step = 50;
            try
            {
                Bitmap bmp1 = (Bitmap)bmp.Clone();
                int dw = bmp1.Width / step;
                int dh = bmp1.Height;
                Graphics g = Graphics.FromImage(obmp);
                Point[] MyPoint = new Point[step];
                for (int x = 0; x < step; x++)
                {
                    MyPoint[x].Y = 0;
                    MyPoint[x].X = x * dw;
                }
                Bitmap bitmap = new Bitmap(bmp1.Width, bmp1.Height);
                for (int i = 0; i < dw; i++)
                {
                    for (int j = 0; j < step; j++)
                    {
                        for (int k = 0; k < dh; k++)
                        {
                            bitmap.SetPixel(MyPoint[j].X + i, MyPoint[j].Y + k, bmp1.GetPixel(MyPoint[j].X + i, MyPoint[j].Y + k));
                        }
                    }
                    pic.Refresh();
                    pic.Image = bitmap;

                    System.Threading.Thread.Sleep(20);
                }
                g.Dispose();
                bmp1.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 从上向下
        /// </summary>
        /// <param name="obmp"></param>
        /// <param name="bmp"></param>
        /// <param name="pic"></param>

        public void Effect_U2D(Bitmap obmp, Bitmap bmp, PictureBox pic)
        {
            try
            {
                int width = bmp.Width;
                int height = bmp.Height;

                Graphics g = pic.CreateGraphics();
                g.DrawImage(obmp, 0, 0, width, height);
                for (int y = 1; y <= height; y += 40)
                {
                    Bitmap bitmap = bmp.Clone(new Rectangle(0, 0, width, y), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    g.DrawImage(bitmap, 0, 0);
                    System.Threading.Thread.Sleep(100);
                }
                g.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }
        /// <summary>
        /// 从下向上
        /// </summary>
        /// <param name="obmp"></param>
        /// <param name="bmp"></param>
        /// <param name="pic"></param>
        public void Effect_D2U(Bitmap obmp, Bitmap bmp, PictureBox pic)
        {
            try
            {
                int width = bmp.Width;
                int height = bmp.Height;

                Graphics g = pic.CreateGraphics();
                g.DrawImage(obmp, 0, 0, width, height);

                for (int y = 1; y <= height; y += 40)
                {
                    Bitmap bitmap = bmp.Clone(new Rectangle(0, height - y, width, y), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    g.DrawImage(bitmap, 0, height - y);
                    System.Threading.Thread.Sleep(100);
                }
                g.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 从左向右
        /// </summary>
        /// <param name="obmp"></param>
        /// <param name="bmp"></param>
        /// <param name="pic"></param>
        public void Effect_L2R(Bitmap obmp, Bitmap bmp, PictureBox pic)
        {
            try
            {
                int width = bmp.Width;
                int height = bmp.Height;
                Graphics g = pic.CreateGraphics();
                g.DrawImage(obmp, 0, 0, width, height);
                for (int x = 10; x <= width; x+=10)
                {
                    Bitmap bitmap = bmp.Clone(new Rectangle(0, 0, x, height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    g.DrawImage(bitmap, 0, 0);
                    System.Threading.Thread.Sleep(1);
                }
                g.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 从右向左
        /// </summary>
        /// <param name="obmp"></param>
        /// <param name="bmp"></param>
        /// <param name="pic"></param>
        public void Effect_R2L(Bitmap obmp, Bitmap bmp, PictureBox pic)
        {
            try
            {
                int width = bmp.Width;
                int height = bmp.Height;
                Graphics g = pic.CreateGraphics();
                g.DrawImage(obmp, 0, 0, width, height);
                for (int x = 1; x <= width; x += 50)
                {
                    //----------------------------------------------w, 0,  0,  h  ||  w-x, 0, +x, h 
                    Bitmap bitmap = bmp.Clone(new Rectangle(width - x, 0, x, height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    g.DrawImage(bitmap, width - x, 0);
                    System.Threading.Thread.Sleep(100);
                }
                g.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 淡入效果
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public  void DanRu(Bitmap bmp, PictureBox picBox)
        {
            //淡入显示图像
            try
            {
                Graphics g = picBox.CreateGraphics();
                g.Clear(Color.Gray);
                int width = bmp.Width;
                int height = bmp.Height;
                ImageAttributes attributes = new ImageAttributes();
                ColorMatrix matrix = new ColorMatrix();
                //创建淡入颜色矩阵
                matrix.Matrix00 = (float)0.0;
                matrix.Matrix01 = (float)0.0;
                matrix.Matrix02 = (float)0.0;
                matrix.Matrix03 = (float)0.0;
                matrix.Matrix04 = (float)0.0;
                matrix.Matrix10 = (float)0.0;
                matrix.Matrix11 = (float)0.0;
                matrix.Matrix12 = (float)0.0;
                matrix.Matrix13 = (float)0.0;
                matrix.Matrix14 = (float)0.0;
                matrix.Matrix20 = (float)0.0;
                matrix.Matrix21 = (float)0.0;
                matrix.Matrix22 = (float)0.0;
                matrix.Matrix23 = (float)0.0;
                matrix.Matrix24 = (float)0.0;
                matrix.Matrix30 = (float)0.0;
                matrix.Matrix31 = (float)0.0;
                matrix.Matrix32 = (float)0.0;
                matrix.Matrix33 = (float)0.0;
                matrix.Matrix34 = (float)0.0;
                matrix.Matrix40 = (float)0.0;
                matrix.Matrix41 = (float)0.0;
                matrix.Matrix42 = (float)0.0;
                matrix.Matrix43 = (float)0.0;
                matrix.Matrix44 = (float)0.0;
                matrix.Matrix33 = (float)1.0;
                matrix.Matrix44 = (float)1.0;
                //从0到1进行修改色彩变换矩阵主对角线上的数值
                //使三种基准色的饱和度渐增
                Single count = (float)0.0;
                while (count < 1.0)
                {
                    matrix.Matrix00 = count;
                    matrix.Matrix11 = count;
                    matrix.Matrix22 = count;
                    matrix.Matrix33 = count;
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    g.DrawImage(bmp, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel, attributes);
                    System.Threading.Thread.Sleep(200);
                    count = (float)(count + 0.02);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
        }

    }
}
