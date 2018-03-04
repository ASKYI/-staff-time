using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using TestTree.Model;

namespace TestTree.ViewModel 
{
    public class FaveViewModel : BaseViewModel
    {
        public ObservableCollection<TreeNode> FavTasks { get; set; }
        private void Generate_FavTask()
        {
            if (TaskNodesDictionary == null)
                throw new Exception("Dictionary has not been generated");
            FavTasks = ConvertTasksIntoNodes(GetTasksByProp("Favorite", "True"));
        }

        public FaveViewModel() : base()
        {
            Generate_FavTask();
        }

    }
}
