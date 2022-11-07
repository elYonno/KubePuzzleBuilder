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
        private void onPictureDrop(DragEventArgs e, PictureBox picture, Label label)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                var bmp = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
                picture.Image = bmp;
                tileWorker.updateTileImage(picture.Name, chosenPictureIndex, label);
                chosenPictureIndex = 0;
            }
        }

        private void onPictureClick(EventArgs e, PictureBox picture, Label label)
        {
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

        private void eventDragEnter(DragEventArgs e)
        {
            if (e.Data != null)
                if (e.Data.GetDataPresent(DataFormats.Bitmap))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
        }

        // --------------------------tile events--------------------------

        // -------------top-------------
        // picture 11
        private void picture11_DragEnter(object sender, DragEventArgs e)
        {
            eventDragEnter(e);
        }

        private void picture11_DragDrop(object sender, DragEventArgs e)
        {
            onPictureDrop(e, picture11, label11);
        }

        private void picture11_Click(object sender, EventArgs e)
        {
            onPictureClick(e, picture11, label11);
        }

        // picture12
        private void picture12_DragEnter(object sender, DragEventArgs e)
        {
            eventDragEnter(e);
        }

        private void picture12_DragDrop(object sender, DragEventArgs e)
        {
            onPictureDrop(e, picture12, label12);
        }

        private void picture12_Click(object sender, EventArgs e)
        {
            onPictureClick(e, picture12, label12);
        }

        // picture13
        private void picture13_DragEnter(object sender, DragEventArgs e)
        {
            eventDragEnter(e);
        }

        private void picture13_DragDrop(object sender, DragEventArgs e)
        {
            onPictureDrop(e, picture13, label13);
        }

        private void picture13_Click(object sender, EventArgs e)
        {
            onPictureClick(e, picture13, label13);
        }

        // picture14
        private void picture14_DragEnter(object sender, DragEventArgs e)
        {
            eventDragEnter(e);
        }

        private void picture14_DragDrop(object sender, DragEventArgs e)
        {
            onPictureDrop(e, picture14, label14);
        }

        private void picture14_Click(object sender, EventArgs e)
        {
            onPictureClick(e, picture14, label14);
        }

        // picture15
        private void picture15_DragEnter(object sender, DragEventArgs e)
        {
            eventDragEnter(e);
        }

        private void picture15_DragDrop(object sender, DragEventArgs e)
        {
            onPictureDrop(e, picture15, label15);
        }

        private void picture15_Click(object sender, EventArgs e)
        {
            onPictureClick(e, picture15, label15);
        }

        // picture16
        private void picture16_DragEnter(object sender, DragEventArgs e)
        {
            eventDragEnter(e);
        }

        private void picture16_DragDrop(object sender, DragEventArgs e)
        {
            onPictureDrop(e, picture16, label16);
        }

        private void picture16_Click(object sender, EventArgs e)
        {
            onPictureClick(e, picture16, label16);
        }

        // picture17
        private void picture17_DragEnter(object sender, DragEventArgs e)
        {
            eventDragEnter(e);
        }

        private void picture17_DragDrop(object sender, DragEventArgs e)
        {
            onPictureDrop(e, picture17, label17);
        }

        private void picture17_Click(object sender, EventArgs e)
        {
            onPictureClick(e, picture17, label17);
        }

        // picture18
        private void picture18_DragEnter(object sender, DragEventArgs e)
        {
            eventDragEnter(e);
        }

        private void picture18_DragDrop(object sender, DragEventArgs e)
        {
            onPictureDrop(e, picture18, label18);
        }

        private void picture18_Click(object sender, EventArgs e)
        {
            onPictureClick(e, picture18, label18);
        }

        // picture19
        private void picture19_DragEnter(object sender, DragEventArgs e)
        {
            eventDragEnter(e);
        }

        private void picture19_DragDrop(object sender, DragEventArgs e)
        {
            onPictureDrop(e, picture19, label19);
        }

        private void picture19_Click(object sender, EventArgs e)
        {
            onPictureClick(e, picture19, label19);
        }
    }
}