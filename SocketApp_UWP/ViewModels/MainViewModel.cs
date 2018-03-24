using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace SocketApp_UWP.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Models.Message> MessageCollection { get; set; }

        public MainViewModel()
        {
            MessageCollection = new ObservableCollection<Models.Message>();
        }

        public void InitializeSocket()
        {
            var io = IO.Socket(new Uri("http://lcw-chat.herokuapp.com/"));
            io.On("broadMessage", async (data) =>
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                 {
                     var message = JsonConvert.DeserializeObject<Models.Message>(data.ToString());
                     MessageCollection.Add(message);
                 });
            });
        }
    }
}
