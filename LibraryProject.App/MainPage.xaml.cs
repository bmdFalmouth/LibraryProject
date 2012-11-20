using LibraryProject.App.Pages;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace LibraryProject.App
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : LibraryProject.App.Common.LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
            
            AddKeywords();
            AddCategories();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            base.LoadState(navigationParameter, pageState);
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            base.SaveState(pageState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Book book = new Book
            {
                Title = "A woman from sand hills",
                Author = "Abe Kobo"
            };

            (App.Current as App).CreateBook(book);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("books");

            var query = conn.Table<Book>();
            var result = await query.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSearch_click(object sender, RoutedEventArgs e)
        {
            string searchPhrase = txtSearch.Text;
            string category = ddlCategory.SelectedItem.ToString();
            string keyword;
            if (ddlKeyword.SelectedIndex > 0)
            {
                keyword = ddlKeyword.SelectedItem.ToString();
            }
            else
            {
                keyword = "";
            }

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("books");

            var query = conn.Table<Book>().Where(x => (x.Author.Contains(searchPhrase) || x.Title.Contains(searchPhrase) || x.Description.Contains(searchPhrase)) && x.Description.Contains(keyword));

            switch (category)
            {
                case("Author"):
                query = conn.Table<Book>().Where(x => x.Author.Contains(searchPhrase) && x.Keywords.Contains(keyword));
                break;
                case("Title"):
                query = conn.Table<Book>().Where(x => x.Title.Contains(searchPhrase) && x.Keywords.Contains(keyword));
                break;
                case("Description"):
                query = conn.Table<Book>().Where(x => x.Description.Contains(searchPhrase) && x.Keywords.Contains(keyword));
                break;
            }

            List<Book> results = await query.ToListAsync();
            
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BookDetails), results);
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchPage_click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BookDetails));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async void AddKeywords()
        {
            List<string> bkw = new List<string>();
            
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("books");

            var query = conn.Table<Book>();
            var result = await query.ToListAsync();

            foreach (Book b in result)
            {
                string[] keywords = b.Keywords.Split(' ');
                for (int i = 0; i < keywords.Length; i++)
                {
                    bkw.Add(keywords[i]);
                }
            }

            List<string> bkwNoDupes = bkw.Distinct().ToList();
            bkwNoDupes.Sort();

            ddlKeyword.ItemsSource = bkwNoDupes;
            ddlKeyword.SelectedIndex = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddCategories()
        {
            List<string> categories = new List<string> { "Any", "Title", "Author", "Description" };
            ddlCategory.ItemsSource = categories;
            ddlCategory.SelectedIndex = 0;
        }
    }
}
