

using System;
using System.Collections;
using MvxAutoComplete.Core.Interfaces;

namespace MvxAutoComplete.TestApp.Core.Models
{
    public class NYTimesSearcher : ISearcher
    {
        public void Search(string term, int take, Action<IEnumerable> success, Action<Exception> error)
        {
            NYTimesSearch.StartAsyncSearch(term, take, success, error);
        }
    }
}
