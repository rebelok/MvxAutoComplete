using System;
using System.Collections;

namespace MvxAutoComplete.Core.Interfaces
{
    public interface ISearcher
    {
        void Search(
            string term, 
            int take,
            Action<IEnumerable> success,
            Action<Exception> error);

    }
}
