using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using MvxAutoComplete.TestApp.Core.Models;

namespace MvxAutoComplete.TestApp.Core.ViewModels
{
    public class FirstViewModel 
		: MvxViewModel
    {
		private string _hello = "test me";
        public string Hello
		{ 
			get { return _hello; }
			set { _hello = value; RaisePropertyChanged(() => Hello); }
		}

        private MvxCommand<object> _clickCommand;

        public ICommand ClickCommand
        {
            get { return _clickCommand ?? (_clickCommand = new MvxCommand<object>(Click)); }
        }

        private void Click(object o)
        {
            Hello = ((Article)o).Snippet;
        }
    }
}
