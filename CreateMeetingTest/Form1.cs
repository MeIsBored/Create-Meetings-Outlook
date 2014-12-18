using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// Add Microsoft.Office.Interop.Outlook
using Outlook = Microsoft.Office.Interop.Outlook;

namespace CreateMeetingTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Create an outlook application
            Outlook.Application App = new Outlook.Application();

            // Create an Appointment item, this is the way outlook manages meetings
            Outlook.AppointmentItem Meeting = App.CreateItem(Outlook.OlItemType.olAppointmentItem) as Outlook.AppointmentItem;
            
            // Set meeting's subject
            Meeting.Subject = txtSubject.Text;

            // Set Body
            Meeting.Body = txtMessage.Text;

            // Set Show As Status
            Meeting.BusyStatus = Outlook.OlBusyStatus.olFree;

            // Set to not request responses
            Meeting.ResponseRequested = false;

            // Set item as Meeting
            Meeting.MeetingStatus = Outlook.OlMeetingStatus.olMeeting;

            // Set Location
            Meeting.Location = txtLocation.Text;

            // Set Starting datetime
            Meeting.Start = dtpStart.Value;

            // Set Ending datetime
            Meeting.End = dtpEnd.Value;

            // Set required recipients
            Outlook.Recipient RecipReq = Meeting.Recipients.Add(txtRecipients.Text);

            // Resolve recipients
            Meeting.Recipients.ResolveAll();

            // Don't display the meeting's information
            Meeting.Display(true);

            // Send it!!!
            Meeting.Send();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // This is to set time without minutes and seconds, minutes can be changed on runtime
            dtpStart.Value = dtpStart.Value.AddSeconds(-dtpStart.Value.Second);
            dtpStart.Value = dtpStart.Value.AddMinutes(-dtpStart.Value.Minute);
            dtpEnd.Value = dtpStart.Value.AddHours(1);
            
        }
    }
}
