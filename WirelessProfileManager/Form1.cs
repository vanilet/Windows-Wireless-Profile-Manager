using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NativeWifi;

namespace WirelessProfileManager
{
    public partial class Form1 : Form
    {
        #region Field
        private WlanClient wlanClient;

        private List<String> interfaceList;
        #endregion Field

        #region Constant
        private const String KEY_SSID = "SSID";
        private const String KEY_PHY_TYPE = "PHY_TYPE";
        private const String KEY_RSSI = "RSSI";
        private const String KEY_BSSID = "BSSID";
        #endregion Constant

        public Form1()
        {
            InitializeComponent();

            setWaitCursor();

            wlanClient = new WlanClient();

            interfaceList = new List<String>();

            dataGridView_ScannedBssList.ColumnCount = 4;
            dataGridView_ScannedBssList.Columns[0].Name = KEY_SSID;
            dataGridView_ScannedBssList.Columns[1].Name = KEY_PHY_TYPE;
            dataGridView_ScannedBssList.Columns[2].Name = KEY_BSSID;
            dataGridView_ScannedBssList.Columns[3].Name = KEY_RSSI;

            initInterfaces();

            setDefaultCursor();
        }

        private void initInterfaces()
        {
            foreach(var item in wlanClient.Interfaces)
                interfaceList.Add(item.InterfaceName);

            cbxInterface.DataSource = interfaceList;
        }

        private void setWaitCursor()
        {
            UseWaitCursor = true;

            Cursor = Cursors.WaitCursor;
        }

        private void setDefaultCursor()
        {
            UseWaitCursor = false;

            Cursor = Cursors.Default;
        }

        private void cbxInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(interfaceList.Count() > 0)
            {
                setWaitCursor();

                var currentInterface = wlanClient.Interfaces[cbxInterface.SelectedIndex];
                
                currentInterface.Scan();

                dataGridView_ScannedBssList.Rows.Clear();

                foreach(var item in currentInterface.GetNetworkBssList())
                {
                    String[] entity = {
                                          System.Text.Encoding.UTF8.GetString(item.dot11Ssid.SSID),
                                          String.Format("802.11{0}", item.dot11BssPhyType.ToString()),
                                          String.Format("{0:x}:{1:x}:{2:x}:{3:x}:{4:x}:{5:x}",
                                                item.dot11Bssid[0], item.dot11Bssid[1],item.dot11Bssid[2],
                                                item.dot11Bssid[3], item.dot11Bssid[4], item.dot11Bssid[5]),
                                          String.Format("{0}dbm", item.rssi.ToString())
                                      };

                    dataGridView_ScannedBssList.Rows.Add(entity);
                }

                dataGridView_ScannedBssList.Update();

                setDefaultCursor();
            }
        }
    }
}
