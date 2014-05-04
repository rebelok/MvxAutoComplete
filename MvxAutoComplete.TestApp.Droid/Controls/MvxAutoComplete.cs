namespace MvxAutoComplete.TestApp.Droid.Controls
{
    /* public class MvxAutoComplete : RelativeLayout
     {
         private MvxListView _resultListView;
         private EditText _searchBox;
         private ISearcher _searcher;
         private IHistory _history;
         private int _take = 10;
         private string _searchTerm;

         public MvxAutoComplete(Context context)
             : base(context)
         {
             Initialize();
         }

         public MvxAutoComplete(Context context, IAttributeSet attrs)
             : base(context, attrs)
         {
             Initialize();
         }


         private void Initialize()
         {
             _resultListView = new MvxListView(Context, null);
             AddSearchBox();
             AddResultView();
             _searcher = Mvx.Resolve<ISearcher>();
             _history = Mvx.Resolve<IHistory>();
         }

         private void AddSearchBox()
         {
             _searchBox = new EditText(Context) { Id = this.GetGeneratedId() };
             var layout = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
             {
                 AlignWithParent = true,
                 BottomMargin = 6
             };
             layout.AddRule(LayoutRules.AlignParentTop);
             _searchBox.LayoutParameters = layout;
             _searchBox.SetPadding(0, 0, 0, 6);
             AddView(_searchBox);
             _searchBox.TextChanged += OnTextChanged;
         }

         private void AddResultView()
         {
             _resultListView = new MvxListView(Context, null);
             var layout = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
             layout.AddRule(LayoutRules.Below, _searchBox.Id);
             _resultListView.LayoutParameters = layout;
             _resultListView.ItemTemplateId = 0;
             _resultListView.ItemsSource = null;
             _resultListView.ItemClick = WrappedItemClickCommand;
             AddView(_resultListView);
         }

         private void OnTextChanged(object sender, Android.Text.TextChangedEventArgs e)
         {
             _searchTerm = e.Text.ToString();

             if (e.AfterCount > 0)
             {
                 StartSearch();
             }
         }

         private void StartSearch()
         {
             IsSearching = true;
             _searcher.Search(_searchTerm, Take, OnSearchSuccess, OnSearchError);
         }

         private void OnSearchError(Exception exception)
         {
             IsSearching = false;

         }

         private void OnSearchSuccess(IEnumerable enumerable)
         {
             IsSearching = false;
             _resultListView.ItemsSource = enumerable;
         }


         public string SearchTerm
         {
             get { return _searchTerm; }
             set { _searchTerm = value; StartSearch(); }
         }

         bool IsSearching { get; set; }



         public int ResultItemTemplateId
         {
             get { return _resultListView.ItemTemplateId; }
             set { _resultListView.ItemTemplateId = value; }
         }

         public ICommand ItemClickCommand
         {
             get { return _resultListView.ItemClick; }
             set { _resultListView.ItemClick = value; }
         }

         private ICommand _wrappedItemClickCommand;
         private ICommand WrappedItemClickCommand
         {
             get { return _wrappedItemClickCommand ?? (_wrappedItemClickCommand = new MvxCommand(someMethod)); }
         }

         private void someMethod()
         {
             throw new NotImplementedException();
         }

         public int Take
         {
             get { return _take; }
             set { _take = value; }
         }

         protected override void OnLayout(bool changed, int l, int t, int r, int b)
         {

         }
     }*/
}