using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace MovieApp
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        ViewModel _viewModel;


        public frmMain()
        {
            InitializeComponent();

            _viewModel = new ViewModel();
            _viewModel.LoadTopRatedMovies();
            _viewModel.LoadGenres();
            loadGenreComboBox();
            loadLookupBox();
            fillInLabels();
        }

        private void loadLookupBox()
        {
            lookUpEdit1.Properties.DataSource = _viewModel.Movies;
            lookUpEdit1.Properties.DisplayMember = "Title";
            lookUpEdit1.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Title"));
            lookUpEdit1.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ReleaseDate", "Release Date"));
            lookUpEdit1.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VoteAverage", "Score"));

        }

        private void loadGenreComboBox()
        {
            comboBoxEdit1.Properties.Items.Add("Top Rated");
            foreach (var item in _viewModel.Genres)
            {
                comboBoxEdit1.Properties.Items.Add(item.Name);
            }
            comboBoxEdit1.SelectedItem = "Top Rated";
        }

        public void fillInLabels()
        {
            lbl_name.Text = $"<color=255,0,0><size = 25>{_viewModel.CurrentMovie.Title}</color></size>";
            lbl_tagline.Text = _viewModel.CurrentMovie.Tagline;
            lbl_link.Text = $"<image=IMDB.png><href>https://www.imdb.com/title/{_viewModel.CurrentMovie.ImdbId}</href>";
            cbox_favorite.Checked = _viewModel.FavoriteMovies.Contains(_viewModel.CurrentMovie.Title);
            pictureEdit1.LoadAsync($"https://image.tmdb.org/t/p/w600_and_h900_bestv2{_viewModel.CurrentMovie.PosterPath}");
        }

        private void btn_previousMovie_Click(object sender, EventArgs e)
        {
            _viewModel.LoadPreviousMovie();
            fillInLabels();
        }

        private void btn_nextMovie_Click(object sender, EventArgs e)
        {
            _viewModel.LoadNextMovie();
            fillInLabels();
        }

        private void cbox_favorite_Click(object sender, EventArgs e)
        {
            if (_viewModel.FavoriteMovies.Contains(_viewModel.CurrentMovie.Title))
            {
                _viewModel.FavoriteMovies.Remove(_viewModel.CurrentMovie.Title);
            }
            else
            {
                _viewModel.FavoriteMovies.Add(_viewModel.CurrentMovie.Title);
            }
        }

        private void btn_newsletter_Click(object sender, EventArgs e)
        {
            MessageBox.Show("congrats, asshat, now you'll get spammed");
            tbox_email.Text = "";
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.SelectedItem.ToString() == "Top Rated")
            {
                _viewModel.LoadTopRatedMovies();
            }
            else
            {
                _viewModel.LoadGenreMovies(comboBoxEdit1.SelectedText);
            }
            fillInLabels(); 
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString()=="Folder")
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog()==DialogResult.OK)
                {
                    buttonEdit1.Text = fbd.SelectedPath;
                }
            }

            if (e.Button.Tag.ToString() == "Save")
            {
                if (buttonEdit1.Text != String.Empty)
                {
                    _viewModel.SaveToFile(buttonEdit1.Text);
                }
            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            _viewModel.LoadCurrentMovieDetail(_viewModel.Movies.First(c => c.Title == lookUpEdit1.Text).Id);
            fillInLabels();
        }
    }
}