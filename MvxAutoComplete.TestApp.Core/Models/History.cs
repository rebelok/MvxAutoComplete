using System.Collections;
using MvxAutoComplete.Core.Interfaces;

namespace MvxAutoComplete.TestApp.Core.Models
{
    public class History : IHistory
    {
        public void Store(object itemToStore)
        {
            //todo: implement
        }

        public IEnumerable GetRecent(int take)
        {
            return null;
        }
    }
}
