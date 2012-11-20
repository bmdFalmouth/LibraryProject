
using FileHelpers;
using LibraryProject.App.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace LibraryProject.App
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            CreateDatabase();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
                Window.Current.Activate();
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        /// <summary>
        /// 
        /// </summary>
        private async void CreateDatabase()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("books");
            await conn.CreateTableAsync<Book>();

            List<Book> books = await conn.Table<Book>().ToListAsync();
            if (books.Count == 0)
            {
                var installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                var assetFolder = await installFolder.GetFolderAsync("Assets");
                var file = await assetFolder.GetFileAsync("MockData.txt");

                FileHelperEngine engine = new FileHelperEngine(typeof(BookTemp));
                BookTemp[] result = engine.ReadFile(file.Path) as BookTemp[];

                List<Book> booksList = new List<Book>();
                
                for (int i = 0; i < result.Length; i++)
                {
                    Book book = new Book();
                    book.Title = result[i].Title;
                    book.Author = result[i].Author;
                    book.ISBN = result[i].ISBN;
                    book.Keywords = result[i].Keywords;
                    book.Description = result[i].Description;
                    book.Save = false;
                    booksList.Add(book);
                }
                foreach (var b in booksList)
                {
                    await conn.InsertAsync(b);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        public async void CreateBook(Book book)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("books");
            await conn.InsertAsync(book);
        }
    }
}
