using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Image_and_Base64
{
    public partial class Form1 : Form
    {
        private string fileImg = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void bImage_Click(object sender, EventArgs e)
        {
            //MessageBoxButtons.OKCancel("please choose a low resolution image");
            
            if (MessageBox.Show("Please choose a low resolution image", "Attention", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                OpenFileDialog openImage = new OpenFileDialog();
                if (openImage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    fileImg = openImage.FileName;
                    picboxImage_1.Image = VaryQualityLevel(Image.FromFile(fileImg));
                    picboxImage_1.SizeMode = PictureBoxSizeMode.StretchImage;
                    txtBase64_1.Text = "";

                }
            }

        }

        private void bConvert64_Click(object sender, EventArgs e)
        {
            string base64_1 = "";
            if (picboxImage_1 != null)
            {
                base64_1 = base64Encode(picboxImage_1.Image);
                txtBase64_1.Text = base64_1;
                MessageBox.Show("Convert to Base 64 done");
            }
            else
            {
                MessageBox.Show("Please insert your image !");
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            txtBase64_2.Text = "";
        }


        private void bConvertImg_Click(object sender, EventArgs e)
        {
            string base64String = "";
            base64String = txtBase64_2.Text;
            if (base64String != "")
            {
                picboxImage_2.Image = imageEncode(base64String);
                picboxImage_2.SizeMode = PictureBoxSizeMode.StretchImage;
                MessageBox.Show("Convert to Image done");
            }
            else
            {
                MessageBox.Show("Please insert your base 64 !");
            }
        }


        private string base64Encode(Image img)
        {
            String base64 = String.Empty;
            if (img != null)
            {
                // Convert the Image to byte[]
                Bitmap bitmap = new Bitmap(img);
                var stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Jpeg);
                var imageBytes = stream.ToArray();


                // Convert byte[] to Base 64 String
                base64 = Convert.ToBase64String(imageBytes);
                
            }
            return base64;

        }

        private Image imageEncode(string base64)
        {
            Image img = null;
            try
            {

                //int modBase64 = base64.Length % 4;
                //if (modBase64 > 0)
                //{
                //    base64 += new string('=', 4 - modBase64);
                //}

                // Convert Base 64 String to byte[]
                //byte[] imageBytes = Convert.FromBase64String(base64);


                //// Convert byte[] to Image
                //using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                //{
                //    img = Image.FromStream(ms, true);
                //}

                // Convert Base64 String to byte[]
                byte[] imageBytes = Convert.FromBase64String(base64);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                img = Image.FromStream(ms, true);


            }
            catch (Exception e)
            {
                MessageBox.Show("Please check your base 64 string !");
        
            }
            return img;
        }


        private Image VaryQualityLevel(Image img)
        {
            Bitmap bmp1 = new Bitmap(img);
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 40L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            return bmp1;

        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //backgroundWorker1.WorkerReportsProgress = true;
            //backgroundWorker1.RunWorkerAsync();
        }
    }

}

