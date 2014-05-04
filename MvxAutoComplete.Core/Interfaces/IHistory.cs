using System.Collections;

namespace MvxAutoComplete.Core.Interfaces
{
    public interface IHistory
    {
        void Store(object itemToStore);

        IEnumerable GetRecent(int take);
    }
}
