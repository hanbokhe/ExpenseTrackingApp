using Xamarin.Forms;

namespace ExpenseTrackingApp.View
{
    public class AlternatingListView : ListView
    {
        public AlternatingListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
        }

        public AlternatingListView()
        {

        }

        protected override void SetupContent(Cell content, int index)
        {
            base.SetupContent(content, index);

            var viewCell = content as ViewCell;
            viewCell.View.BackgroundColor = index % 2 == 0 ? Color.White : Color.WhiteSmoke;
        }
    }
}