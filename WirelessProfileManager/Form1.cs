using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NativeWifi;

namespace WirelessProfileManager
{
    public partial class Form1 : Form
    {
        #region Field
        private WlanClient wlanClient;

        private WlanClient.WlanInterface currentInterface;

        private List<String> interfaceList;
        #endregion Field

        #region Constant
        private enum BssColumnNames
        {
            SSID = 0,
            PHY_TYPE = 1,
            BSSID = 2,
            RSSI = 3,
            SECURITY = 4,
            AUTH = 5,
            CIPHER = 6
        }
        #endregion Constant

        public Form1()
        {
            InitializeComponent();

            setWaitCursor();

            wlanClient = new WlanClient();

            interfaceList = new List<String>();
            interfaceList.Add("List of 802.11 deivce");

            btnConnect.Enabled = false;

            dataGridView_ScannedBssList.ColumnCount = 7;
            dataGridView_ScannedBssList.Columns[BssColumnNames.SSID.GetHashCode()].Name = BssColumnNames.SSID.ToString();
            dataGridView_ScannedBssList.Columns[BssColumnNames.PHY_TYPE.GetHashCode()].Name = BssColumnNames.PHY_TYPE.ToString();
            dataGridView_ScannedBssList.Columns[BssColumnNames.BSSID.GetHashCode()].Name = BssColumnNames.BSSID.ToString();
            dataGridView_ScannedBssList.Columns[BssColumnNames.RSSI.GetHashCode()].Name = BssColumnNames.RSSI.ToString();
            dataGridView_ScannedBssList.Columns[BssColumnNames.SECURITY.GetHashCode()].Name = BssColumnNames.SECURITY.ToString();
            dataGridView_ScannedBssList.Columns[BssColumnNames.AUTH.GetHashCode()].Name = BssColumnNames.AUTH.ToString();
            dataGridView_ScannedBssList.Columns[BssColumnNames.CIPHER.GetHashCode()].Name = BssColumnNames.CIPHER.ToString();

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

        private void updateProfileList()
        {
            try
            {
                btnConnect.Enabled = false;

                setWaitCursor();

                dataGridView_ScannedBssList.Rows.Clear();

                foreach(var item in currentInterface.GetNetworkBssList())
                {
                    String[] entity = new String[dataGridView_ScannedBssList.ColumnCount];

                    entity[BssColumnNames.SSID.GetHashCode()] = System.Text.Encoding.UTF8.GetString(item.dot11Ssid.SSID);

                    entity[BssColumnNames.PHY_TYPE.GetHashCode()] = "802.11";

                    switch(item.dot11BssPhyType)
                    {
                        case Wlan.Dot11PhyType.OFDM:
                            entity[1] += "a";
                            break;

                        case Wlan.Dot11PhyType.HRDSSS:
                            entity[1] += "b";
                            break;

                        case Wlan.Dot11PhyType.ERP:
                            entity[1] += "g";
                            break;

                        case Wlan.Dot11PhyType.HT:
                            entity[1] += "n";
                            break;

                        case Wlan.Dot11PhyType.VHT:
                            entity[1] += "ac";
                            break;

                        default:
                            entity[1] += item.dot11BssPhyType.ToString();
                            break;
                    }

                    entity[BssColumnNames.BSSID.GetHashCode()] = String.Format("{0:x}:{1:x}:{2:x}:{3:x}:{4:x}:{5:x}",
                        item.dot11Bssid[0], item.dot11Bssid[1], item.dot11Bssid[2],
                        item.dot11Bssid[3], item.dot11Bssid[4], item.dot11Bssid[5]);

                    entity[BssColumnNames.RSSI.GetHashCode()] = String.Format("{0}dbm", item.rssi.ToString());
                    dataGridView_ScannedBssList.Rows.Add(entity);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dataGridView_ScannedBssList.Update();

                setDefaultCursor();

                cbxInterface.Enabled = true;
            }
        }

        private void cbxInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbxInterface.Enabled = false;
                btnConnect.Enabled = false;

                setWaitCursor();

                if(interfaceList.Count() > 1 && cbxInterface.SelectedIndex > 0)
                {
                    currentInterface = wlanClient.Interfaces[cbxInterface.SelectedIndex - 1];

                    currentInterface.WlanNotification += currentInterface_WlanNotification;
                    
                    currentInterface.Scan();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                setDefaultCursor();

                cbxInterface.Enabled = true;
            }
        }

        void currentInterface_WlanNotification(Wlan.WlanNotificationData notifyData)
        {
            Invoke(new Action(updateProfileList));
        }

        private void dataGridView_ScannedBssList_SelectionChanged(object sender, EventArgs e)
        {
            btnConnect.Enabled = true;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                cbxInterface.Enabled = false;
                dataGridView_ScannedBssList.Enabled = false;
                btnConnect.Enabled = false;

                setWaitCursor();

                var currentItem = dataGridView_ScannedBssList.CurrentRow;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cbxInterface.Enabled = true;
                dataGridView_ScannedBssList.Enabled = true;
                btnConnect.Enabled = true;

                setDefaultCursor();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            currentInterface = null;

            interfaceList.Clear();

            cbxInterface.Dispose();

            dataGridView_ScannedBssList.ClearSelection();
            dataGridView_ScannedBssList.Rows.Clear();

            dataGridView_ScannedBssList.Dispose();
        }
    }
}
