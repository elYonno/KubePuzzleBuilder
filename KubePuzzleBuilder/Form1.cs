using KubePuzzleBuilder.Properties;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.Security.Policy;

namespace KubePuzzleBuilder
{
    public partial class Kube : Form
    {
        private TileWorker tileWorker;

        private int chosenPictureIndex = 0;
        private bool itemSelect = false;
        private ItemType chosenItemType = ItemType.NONE;

        public Kube()
        {
            InitializeComponent();
            tileWorker = new TileWorker();

            // allow drop
            {
                picture11.AllowDrop = true;
                picture12.AllowDrop = true;
                picture13.AllowDrop = true;
                picture14.AllowDrop = true;
                picture15.AllowDrop = true;
                picture16.AllowDrop = true;
                picture17.AllowDrop = true;
                picture18.AllowDrop = true;
                picture19.AllowDrop = true;

                picture21.AllowDrop = true;
                picture22.AllowDrop = true;
                picture23.AllowDrop = true;
                picture24.AllowDrop = true;
                picture25.AllowDrop = true;
                picture26.AllowDrop = true;
                picture27.AllowDrop = true;
                picture28.AllowDrop = true;
                picture29.AllowDrop = true;

                picture31.AllowDrop = true;
                picture32.AllowDrop = true;
                picture33.AllowDrop = true;
                picture34.AllowDrop = true;
                picture35.AllowDrop = true;
                picture36.AllowDrop = true;
                picture37.AllowDrop = true;
                picture38.AllowDrop = true;
                picture39.AllowDrop = true;

                picture41.AllowDrop = true;
                picture42.AllowDrop = true;
                picture43.AllowDrop = true;
                picture44.AllowDrop = true;
                picture45.AllowDrop = true;
                picture46.AllowDrop = true;
                picture47.AllowDrop = true;
                picture48.AllowDrop = true;
                picture49.AllowDrop = true;

                picture51.AllowDrop = true;
                picture52.AllowDrop = true;
                picture53.AllowDrop = true;
                picture54.AllowDrop = true;
                picture55.AllowDrop = true;
                picture56.AllowDrop = true;
                picture57.AllowDrop = true;
                picture58.AllowDrop = true;
                picture59.AllowDrop = true;

                picture61.AllowDrop = true;
                picture62.AllowDrop = true;
                picture63.AllowDrop = true;
                picture64.AllowDrop = true;
                picture65.AllowDrop = true;
                picture66.AllowDrop = true;
                picture67.AllowDrop = true;
                picture68.AllowDrop = true;
                picture69.AllowDrop = true;
            }
        }

        private void Kube_Load(object sender, EventArgs e)
        {
            keySelect.SelectedIndex = 0;
            lockSelect.SelectedIndex = 0;
        }

        private void resetSelection()
        {
            itemSelect = false;
            chosenItemType = ItemType.NONE;
            chosenPictureIndex = 0;
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

        private void keySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (keySelect.SelectedIndex == 0)
            {
                pictureKey.Image = Resources.GreenKey;
                pictureKey.Tag = 1;
            }
            else
            {
                pictureKey.Image = Resources.YellowKey;
                pictureKey.Tag = 2;
            }
        }

        private void lockSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lockSelect.SelectedIndex == 0)
            {
                pictureLock.Image = Resources.GreenLock;
                pictureLock.Tag = 3;
            }
            else
            {
                pictureLock.Image = Resources.YellowLock;
                pictureLock.Tag = 4;
            }
        }

        // --------------------------template--------------------------
        private void picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(PictureBox))
            {
                PictureBox picture = getPicture(sender);

                if (e.Button == MouseButtons.Left)
                    picture.DoDragDrop(picture.Image, DragDropEffects.All);
                    
                string? tag = picture.Tag.ToString();
                if (tag == null) throw new SystemException("Missing tag");
                chosenPictureIndex = int.Parse(tag);
                itemSelect = false;
            }
        }

        private void item_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(PictureBox))
            {
                PictureBox picture = getPicture(sender);

                if (e.Button == MouseButtons.Left)
                    picture.DoDragDrop(picture.Image, DragDropEffects.All);
                
                string? tag = picture.Tag.ToString();
                if (tag == null) throw new SystemException("Missing tag");
                chosenItemType = (ItemType)int.Parse(tag);
                itemSelect = true;
            }
        }

        // --------------------------tile functions--------------------------
        private void picture_DragDrop(object sender, DragEventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(PictureBox))
            {
                (PictureBox picture, Label label) = getPictureAndLabel(sender);

                if (e.Data != null && e.Data.GetDataPresent(DataFormats.Bitmap))
                {
                    var bmp = (Bitmap)e.Data.GetData(DataFormats.Bitmap);

                    if (itemSelect)
                    {
                        int tag = 0;
                        if (picture.Tag != null)
                        {
                            string? temp = picture.Tag.ToString();
                            if (temp != null)
                                tag = int.Parse(temp);
                        }

                        if (tag != 0)
                        {
                            picture.Controls.Clear();
                            if (chosenItemType != ItemType.NONE)
                            {
                                PictureBox itemBox = new PictureBox
                                {
                                    Image = (Bitmap)bmp.Clone(),
                                    Tag = chosenItemType,
                                    BackColor = Color.Transparent
                                };
                                                              
                                itemBox.MouseClick += new MouseEventHandler(item_Click);

                                picture.Controls.Add(itemBox);
                                itemBox.BringToFront();

                                if (chosenItemType == ItemType.CONVEYOR)
                                {
                                    int orientation = tileWorker.getOrientation(picture.Name);
                                    switch (orientation)
                                    {
                                        case 2: itemBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                                        case 3: itemBox.Image.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                                        case 4: itemBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
                                    }
                                }
                            }
                            label.Text = tileWorker.updateTileItem(picture.Name, chosenItemType);
                        }
                    }
                    else
                    {
                        picture.Image = (Image)bmp.Clone();
                        picture.Controls.Clear();
                        picture.Tag = chosenPictureIndex;
                        label.Text = tileWorker.updateTileImage(picture.Name, chosenPictureIndex);
                        label.Text = tileWorker.updateTileItem(picture.Name, ItemType.NONE);
                    }

                    resetSelection();
                }
            }
        }

        private void item_Click(object? sender, EventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(PictureBox))
            {
                PictureBox itemBox = getPicture(sender);
                PictureBox picture = (PictureBox)itemBox.Parent;

                picture_Click(picture, e);

                if (picture.Tag != null)
                {
                    string? tag = itemBox.Tag.ToString();
                    if (tag!= null && tag.Equals(ItemType.CONVEYOR.ToString()))
                    {
                        MouseEventArgs me = (MouseEventArgs)e;
                        Image bmp = (Image)itemBox.Image.Clone();

                        if (me.Button == MouseButtons.Left)
                        {
                            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        }
                        else if (me.Button == MouseButtons.Right)
                        {
                            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        }
                        else return;

                        itemBox.Image = bmp;
                    }
                }
            }
        }

        private void picture_Click(object sender, EventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(PictureBox))
            {
                (PictureBox picture, Label label) = getPictureAndLabel(sender);

                MouseEventArgs me = (MouseEventArgs)e;
                Image bmp = (Image)picture.Image.Clone();

                if (me.Button == MouseButtons.Left)
                {
                    bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    label.Text = tileWorker.rotateTile(picture.Name, false);
                }
                else if (me.Button == MouseButtons.Right)
                {
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    label.Text = tileWorker.rotateTile(picture.Name, true);
                }
                else return;

                picture.Image = bmp;
            }
        }

        private void picture_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null)
                if (e.Data.GetDataPresent(DataFormats.Bitmap))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
        }

        // --------------------------get tools--------------------------
        private PictureBox getPicture(object sender)
        {
            PictureBox? picture = sender as PictureBox;
            if (picture == null) throw new SystemException("Cannot find picture");
            return picture;
        }

        private (PictureBox, Label) getPictureAndLabel(object sender)
        {
            PictureBox pictureBox = getPicture(sender);
            string labelName = "label" + pictureBox.Name.Substring(pictureBox.Name.Length - 2);
            Label? label = Controls.Find(labelName, true).FirstOrDefault() as Label;
            if (label == null) throw new SystemException("Cannot find label");

            return (pictureBox, label);
        }
    }
}