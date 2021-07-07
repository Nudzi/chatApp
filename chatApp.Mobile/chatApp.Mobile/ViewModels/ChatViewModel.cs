using chatApp.Mobile.Services;
using chatModel;
using chatModel.Requests.Friends;
using chatModel.Requests.Histories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace chatApp.Mobile.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        private readonly APIService _historiesService = new APIService("histories");
        public ObservableCollection<HistoryList> Histories { get; set; } = new ObservableCollection<HistoryList>();

        public ChatViewModel()
        {
        }
        public FriendList FriendList { get; set; }
        public string FriendlyName { get; set; }
        public byte[] byteImage { get; set; }
        public string AlbumPath { get; set; }
        public int listSize = 10;
        public async Task Init()
        {
            try
            {
                HistoriesSearchRequest historiesSearchRequest = new HistoriesSearchRequest
                {
                    UserIdSecondary = Global.LoggedUser.Id,
                    UserIdPrimary = FriendList.UserIdSecondary,
                    Status = false
                };
                var historyListUnseen = await _historiesService.Get<List<Histories>>(historiesSearchRequest);
                foreach (var item in historyListUnseen)
                {
                    item.Status = true;
                    await _historiesService.Update<Histories>(item.Id, item);
                }

                HistoriesSearchRequest historiesSearchRequestSize = new HistoriesSearchRequest
                {
                    ListSize = listSize
                };
                var historylist = await _historiesService.Get<List<Histories>>(historiesSearchRequestSize);
                var histories = new List<Histories>();
                foreach (var history in historylist)
                {
                    if (history.UserIdPrimary == Global.LoggedUser.Id || history.UserIdPrimary == FriendList.UserIdSecondary)
                    {
                        if (history.UserIdSecondary == Global.LoggedUser.Id || history.UserIdSecondary == FriendList.UserIdSecondary)
                        {
                            histories.Add(history);
                        }
                    }
                }

                Histories.Clear();
                foreach (var history in histories)
                {
                    HistoryList tmp = new HistoryList
                    {
                        Id = history.Id,
                        Image = history.Image,
                        ImageThumb = history.ImageThumb,
                        Message = history.Message,
                        ModifiedDate = history.ModifiedDate,
                        Status = history.Status,
                        UserIdPrimary = history.UserIdPrimary,
                        UserIdSecondary = history.UserIdSecondary,
                        isPrimary = false,
                        isVisibleImage = true,
                        isVisibleMessage = true,
                        AlbumPath = history.ImagePath
                    };
                    if (history.Image.Length == 0)
                        tmp.isVisibleImage = false;
                    if (history.Message == null)
                        tmp.isVisibleMessage = false;
                    if (tmp.UserIdPrimary == Global.LoggedUser.Id)
                        tmp.isPrimary = true;
                    Histories.Add(tmp);
                }

            }
            catch (Exception ex)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        public async Task SaveMessage(string message)
        {
            try
            {
                HistoriesUpsertRequest historiesUpsertRequest = new HistoriesUpsertRequest
                {
                    Image = byteImage,
                    ImageThumb = byteImage,
                    Message = message,
                    UserIdPrimary = Global.LoggedUser.Id,
                    UserIdSecondary = FriendList.UserIdSecondary,
                    ModifiedDate = DateTime.Now,
                    Status = false,
                    ImagePath = AlbumPath
                };
                await _historiesService.Insert<Histories>(historiesUpsertRequest);
                if (byteImage != null)
                {
                    var predicted = Connect("127.0.0.1", AlbumPath);
                    await TextToSpeech.SpeakAsync("You have send a: " + predicted);
                    byteImage = null;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        public string Connect(String server, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 2222;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
                return responseData;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
                return "";
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                return "";
            }
        }
        public async Task VoiceImage(HistoryList item)
        {
            if (item.Image.Length != 0)
            {
                var predicted = Connect("127.0.0.1", item.AlbumPath);
                await TextToSpeech.SpeakAsync("It is a: " + predicted);
            }
            else
            {
                await TextToSpeech.SpeakAsync(item.Message);
            }
        }
    }
}