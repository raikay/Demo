using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsClientDemo
{
    public partial class Form1 : Form
    {
        private HubConnection _hubConnection;
        private static int count=0;
        public Form1()
        {
            InitializeComponent();
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/myhub")
                .WithAutomaticReconnect()//wait 0, 2, 10, and 30 seconds
                .Build();
            _hubConnection.On<MsgInfo>("ShowMsg", (msg) =>
            {
                this.txtMsg.AppendText($"{msg.Title}:{msg.MsgContent}\r\n");
            });
            await _hubConnection.StartAsync();
        }

        private async void btnUpdateData_Click(object sender, EventArgs e)
        {
            count++;
            txtStatus1.Text = txtStatus1.Text + count;
            txtStatus2.Text = txtStatus2.Text + count;
            await _hubConnection.InvokeAsync("UpdateDataServer", 
                new DataStatus { Status1 = txtStatus1.Text, Status2 = txtStatus2.Text });
        }
    }
    public class DataStatus
    {
        public string Status1 { get; set; }
        public string Status2 { get; set; }
    }
    public class MsgInfo
    {
        public string Title { get; set; }
        public string MsgContent { get; set; }
    }
}
