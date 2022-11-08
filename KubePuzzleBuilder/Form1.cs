using System.Diagnostics;
using System.Security.Policy;

namespace KubePuzzleBuilder
{
    public partial class Kube : Form
    {
        private int chosenPictureIndex = 0;
        private TileWorker tileWorker;

        public Kube()
        {
            InitializeComponent();
            tileWorker = new TileWorker();

            // allow drop
            {
                this.picture11.AllowDrop = true;
                this.picture12.AllowDrop = true;
                this.picture13.AllowDrop = true;
                this.picture14.AllowDrop = true;
                this.picture15.AllowDrop = true;
                this.picture16.AllowDrop = true;
                this.picture17.AllowDrop = true;
                this.picture18.AllowDrop = true;
                this.picture19.AllowDrop = true;

                this.picture21.AllowDrop = true;
                this.picture22.AllowDrop = true;
                this.picture23.AllowDrop = true;
                this.picture24.AllowDrop = true;
                this.picture25.AllowDrop = true;
                this.picture26.AllowDrop = true;
                this.picture27.AllowDrop = true;
                this.picture28.AllowDrop = true;
                this.picture29.AllowDrop = true;

                this.picture31.AllowDrop = true;
                this.picture32.AllowDrop = true;
                this.picture33.AllowDrop = true;
                this.picture34.AllowDrop = true;
                this.picture35.AllowDrop = true;
                this.picture36.AllowDrop = true;
                this.picture37.AllowDrop = true;
                this.picture38.AllowDrop = true;
                this.picture39.AllowDrop = true;

                this.picture41.AllowDrop = true;
                this.picture42.AllowDrop = true;
                this.picture43.AllowDrop = true;
                this.picture44.AllowDrop = true;
                this.picture45.AllowDrop = true;
                this.picture46.AllowDrop = true;
                this.picture47.AllowDrop = true;
                this.picture48.AllowDrop = true;
                this.picture49.AllowDrop = true;

                this.picture51.AllowDrop = true;
                this.picture52.AllowDrop = true;
                this.picture53.AllowDrop = true;
                this.picture54.AllowDrop = true;
                this.picture55.AllowDrop = true;
                this.picture56.AllowDrop = true;
                this.picture57.AllowDrop = true;
                this.picture58.AllowDrop = true;
                this.picture59.AllowDrop = true;

                this.picture61.AllowDrop = true;
                this.picture62.AllowDrop = true;
                this.picture63.AllowDrop = true;
                this.picture64.AllowDrop = true;
                this.picture65.AllowDrop = true;
                this.picture66.AllowDrop = true;
                this.picture67.AllowDrop = true;
                this.picture68.AllowDrop = true;
                this.picture69.AllowDrop = true;
            }
        }

        private void Kube_Load(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON|*.json";
            saveFileDialog.Title = "Export as JSON";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                string output = tileWorker.export();

                try
                {
                    StreamWriter file = new StreamWriter(saveFileDialog.FileName);
                    file.WriteAsync(output);
                    file.Close();
                } catch (Exception exception)
                {
                    MessageBox.Show(
                        exception.Message,
                        "Export as JSON",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
            }
        }

        // --------------------------template--------------------------
        private void pictureBlank_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                pictureBlank.DoDragDrop(pictureBlank.Image, DragDropEffects.All);
            
            chosenPictureIndex = 0;
        }

        private void pictureStart_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                pictureStart.DoDragDrop(pictureStart.Image, DragDropEffects.All);
            chosenPictureIndex = 1;
            
        }

        private void pictureFinish_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                pictureFinish.DoDragDrop(pictureFinish.Image, DragDropEffects.All);
            chosenPictureIndex = 2;
        }

        private void pictureStraight_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                pictureStraight.DoDragDrop(pictureStraight.Image, DragDropEffects.All);
            chosenPictureIndex = 3;
        }

        private void pictureCorner_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                pictureCorner.DoDragDrop(pictureCorner.Image, DragDropEffects.All);
            chosenPictureIndex = 4;
        }

        // --------------------------tile functions--------------------------
        private void onPictureDrop(object sender, DragEventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(PictureBox))
            {
                PictureBox? picture = sender as PictureBox;
                if (picture == null) throw new SystemException("Cannot find picture");

                string labelName = "label" + picture.Name.Substring(picture.Name.Length - 2);
                Label? label = Controls.Find(labelName, true).FirstOrDefault() as Label;
                if (label == null) throw new SystemException("Cannot find label");

                if (e.Data != null && e.Data.GetDataPresent(DataFormats.Bitmap))
                {
                    var bmp = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
                    picture.Image = (Image)bmp.Clone();
                    tileWorker.updateTileImage(picture.Name, chosenPictureIndex, label);
                    chosenPictureIndex = 0;
                }
            }
        }

        private void onPictureClick(object sender, EventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(PictureBox))
            {
                PictureBox? picture = sender as PictureBox;
                if (picture == null) throw new SystemException("Cannot find picture");

                string labelName = "label" + picture.Name.Substring(picture.Name.Length - 2);
                Label? label = Controls.Find(labelName, true).FirstOrDefault() as Label;
                if (label == null) throw new SystemException("Cannot find label");

                MouseEventArgs me = (MouseEventArgs)e;
                Image bmp = (Image)picture.Image.Clone();

                if (me.Button == MouseButtons.Left)
                {
                    bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    tileWorker.rotateTile(picture.Name, false, label);
                }
                else if (me.Button == MouseButtons.Right)
                {
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    tileWorker.rotateTile(picture.Name, true, label);
                }
                else return;

                picture.Image = bmp;
            }
        }

        private void onPictureEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null)
                if (e.Data.GetDataPresent(DataFormats.Bitmap))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
        }
    }
}