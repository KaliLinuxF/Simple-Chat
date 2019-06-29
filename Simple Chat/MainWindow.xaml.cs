using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Simple_Chat.ServiceChat;
using System.Windows.Threading;

namespace Simple_Chat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {

        bool isConnected = false;
        ServiceChatClient client;
        int ID;
        string UserName;

      
       
        


        public MainWindow(string UName)
        {
            InitializeComponent();
            UserName = UName;
            ConnectUser();
        }



        public void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(UserName);
                isConnected = true;
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                isConnected = false;
            }
        }

        

        public void MsgCallBack(string msg)
        {
            lbchatText.Items.Insert(0, msg);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
            Application.Current.Shutdown();
        }

        private void TbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMsg(tbSendMsg.Text, ID);
                    tbSendMsg.Text = string.Empty;
                }
            }
        }

    }
}
