using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Mp3Window.Resources.Style
{

    /**以下两个方法都会覆盖原有style 暂未找到解决方案
    public class BackgroundConverter : IValueConverter
    {
        #region IValueConverter  
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView; // Use the ItemsControl.ItemsContainerFromItemContainer(item) to get the ItemsControl.. and cast  

            // Get the index of a ListViewItem  
            int index = listView.ItemContainerGenerator.IndexFromContainer(item); // this is a state-of-art way to get the index of an Item from a ItemsControl  
            if (index % 2 == 0)
                return Brushes.LightBlue;
            else
            {
                return Brushes.Beige;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion IValueConverter  
    }
    ///暂时废弃
    /// <summary>
    /// StyleSelector 用来使得listview 隔行变色
    /// </summary>
    class ListViewItemStyleSelector:StyleSelector
    {
    
        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
        
             System.Windows.Style st= new System.Windows.Style();
            
            st.TargetType = typeof(ListViewItem);
            Setter backgroundSetter = new Setter();

            backgroundSetter.Property = ListViewItem.BackgroundProperty;
    
            ListView listView =ItemsControl.ItemsControlFromItemContainer(container) as ListView;
            int index = listView.ItemContainerGenerator.IndexFromContainer(container);
            st.BasedOn = listView.Style;
            if (index%2==0)
                backgroundSetter.Value = Brushes.White;
            else
                backgroundSetter.Value = Brushes.LightGray;

            st.Setters.Add(backgroundSetter);
            return st;
        }
    }*/
}
