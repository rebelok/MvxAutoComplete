using System;
using System.Collections;
using System.Windows.Input;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.ViewModels;
using MvxAutoComplete.Core.Interfaces;


namespace MvxAutoComplete.Droid
{
    public class MvxAutoComplete : RelativeLayout
    {
        private MvxListView _resultListView;
        private EditText _searchBox;
        private ISearcher _searcher;
        private IHistory _history;
        private int _take = 10;
        private string _searchTerm;
        private readonly IAttributeSet _attrs;
        private ICommand _itemClick;

        public MvxAutoComplete(Context context)
            : base(context)
        {
            Initialize();
        }

        public MvxAutoComplete(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            _attrs = attrs;
            Initialize();
        }
        
        public string SearchTerm
        {
            get { return _searchTerm; }
            set { _searchTerm = value; StartSearch(); }
        }

        public bool IsSearching { get; set; }

        public int ResultItemTemplateId
        {
            get { return _resultListView.ItemTemplateId; }
            set { _resultListView.ItemTemplateId = value; }
        }

        public ICommand ItemClick
        {
            get { return _itemClick; }
            set { _itemClick = value; }
        }

        public int Take
        {
            get { return _take; }
            set { _take = value; }
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
            _resultListView = new MvxListView(Context, _attrs);
            var layout = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            layout.AddRule(LayoutRules.Below, _searchBox.Id);
            _resultListView.LayoutParameters = layout;
            _resultListView.ItemClick = WrappedItemClickCommand;
            AddView(_resultListView);
            
        }
        
        private void OnTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _searchTerm = e.Text.ToString();

            if (e.AfterCount > 2)
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
            //todo: show error message
        }

        private void OnSearchSuccess(IEnumerable enumerable)
        {
           Post(() => ShowResult(enumerable));
        }

        private void ShowResult(IEnumerable enumerable)
        {
            IsSearching = false;
            _resultListView.Adapter.ItemsSource = enumerable;
        }
        private ICommand _wrappedItemClickCommand;

        private ICommand WrappedItemClickCommand
        {
            get { return _wrappedItemClickCommand ?? (_wrappedItemClickCommand = new MvxCommand<object>(WrappedItemClick)); }
        }

        private void WrappedItemClick(object clickedItem)
        {
            _history.Store(clickedItem);
          
            if (ItemClick!= null)
            {
                ItemClick.Execute(clickedItem);
            }
        }
    }
}