// Copyright (C) 2016 to the present, Crestron Electronics, Inc.
// All rights reserved.
// No part of this software may be reproduced in any form, machine
// or natural, without the express written consent of Crestron Electronics.

#define LOG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using TnClient.Properties;


 
namespace TnClient
{

    public partial class Form1 : Form
    {

        String Hostname = String.Empty;
        int Port = 23;
        int DeviceID = 0x55;
        Boolean SleepyDev = false;
        static String port_str = String.Empty;
        static String dev_id_str = String.Empty;
        String log_file_path_str = String.Empty;
        private Boolean log_file_created = false;
        byte seq_number = 0;
        public const int SEQ_NUMBER_MODULE  = 0x1f;
        public const int DATA_BLOCK_SIZE = 128;

        enum Opcode { Fw_ImageNotify = 0xE0, FW_ImageBlockRequest = 0xE1, FW_WriteBlock = 0xE2, FW_ImageRequestForSleepyDevice = 0xE3,
        FW_LoadTarget = 0xE5, FW_Sleepy_TargetList = 0xE7, FW_ImageCancel = 0xE9 
        };
        enum states {Notify, Load, Complete, LoadSuccess, LoadTarget, Abort, TargetProgramSuccess, TargetProgramFail};
        private byte[] input = new byte[2000];          //buffer for file reading
        private byte[] txbuffer = new byte[2000];        //transmit buffer for tcp
        private byte[] rxbuffer = new byte[5000];         //receive buffer for tcp
        private String tcp_response_string = String.Empty;
        private String tcp_request_string = String.Empty;
        private String ImageNotifyHeader = "custom lbi send_data 0xE0 ";
        private String WriteBlockHeader = "custom lbi send_data 0xE2 ";

        Int16 identity_timeout = 0;
        Int16 device_short_address = 0;
        string identity_timeout_str = "";
        string device_short_address_str = "";
        private String SendStringToBridgeHeader = "custom lbi send_data";
        private String LBI_DEVICE_DISCOVERY = "07";
        private String LBI_DEVICE_INFO = "08";
        private String LBI_PERMIT_JOIN = "0B";
        private String LBI_IDENTIFY = "19";
        private String LBI_ISALIVE = "24";
        private String LBI_DISABLE_JOIN = "25";
        private String LBI_GET_BCD = "26";
        private String LBI_FORM_NETWORK = "28";
        private String LBI_JOIN_NETWORK = "29";
        private String LBI_STOP_IDENTIFY = "2C";


        private String ImageCancel = "custom lbi send_data 0xE9 0 {E9}\r\n";
        private String LoadTargetHeader = "custom lbi send_data 0xE5 ";
        private String FW_id_str = "12345".PadLeft(24, '0');
        private String FW_version_str = "6789".PadLeft(20,'0');                                                                                                                                                            
        int State = (int)states.Notify;
        int block_offset = 0;
        System.IO.StreamWriter logfile;
        private System.IO.BinaryReader br = null;
        private System.IO.Stream sr = null;
 

 /*    Diagnostic log file */

/********************************************************/
        TcpClient client;
        NetworkStream tcp_stream;
        delegate void SetTextCallback(string text);
        public Form1()
        {
            InitializeComponent();
            TnClient.Properties.Settings.Default.Reload();
            Hostname = TnClient.Properties.Settings.Default.Hostname;
            Port = TnClient.Properties.Settings.Default.Port;
            log_file_path_str = TnClient.Properties.Settings.Default.Logfile;
            log_file.Text = log_file_path_str;
            hostname.Text = Hostname;
            port.Text = ""+Port;
            fw_id.Text = FW_id_str;
            DevID.Text = "" + DeviceID;
            fw_version.Text = "" + FW_version_str;
            AppDomain.CurrentDomain.UnhandledException += AllUnhandledExceptions;
        }
        private  void AllUnhandledExceptions(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            // Display or log ex.ToString()
            //...
            SetDiagText("dddd");
            Environment.Exit(System.Runtime.InteropServices.Marshal.GetHRForException(ex));
        }
        
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        private void SetDiagText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.DiagLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetDiagText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.DiagLabel.Text = text;
            }
            DiagLabel.Update();
        }   
        
        private void FileOpen_Click(object sender, EventArgs e)
        {
            string File_to_prog = String.Empty;
            bool ChkFile = false;
 
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File_to_prog = openFileDialog1.FileName;
                ChkFile = true;
             }
            if(ChkFile)
            {
                System.IO.FileInfo fInfo = new System.IO.FileInfo(File_to_prog);
                Path.Text = "" + File_to_prog;
                try
                {
                    sr = System.IO.File.Open(File_to_prog, System.IO.FileMode.Open);
                }
                catch (System.IO.IOException)
                {

                    SetDiagText("File is being used by another process.");
                }
                catch (System.ArgumentOutOfRangeException e1)
                {
                    SetDiagText("DD" +e1.Message + e1.StackTrace);

                }
                catch (System.ArgumentException e2)
                {
                    SetDiagText("EE"+ e2.Message + e2.StackTrace);
                }
 
                try
                {
                     br = new System.IO.BinaryReader(sr);
                 }
                catch (Exception ee)
                {
                     SetDiagText("CC"+ ee.Message + ee.StackTrace);
                }
            }

        }

        private void hostname_TextChanged(object sender, EventArgs e)
        {
            Hostname = hostname.Text;
        }

        private void port_TextChanged(object sender, EventArgs e)
        {
            port_str = port.Text;
            Port = Convert.ToInt16(port_str, 10); 
        }


        private string ByteArrayToHexString(byte[] data, ushort index, int length)
        {
            ushort i = index;
            StringBuilder sb = new StringBuilder(data.Length * 3);
            while (i < length)
            {
                sb.Append(Convert.ToString(data[i], 16).PadLeft(2, '0'));
                i++;
            }
            return sb.ToString().ToUpper();
        }

        private int WaitForResponseFromSlave()
        {
            int nTry = 1000;
            Int32 res = -1;
            while (!tcp_stream.DataAvailable)
            {
                Thread.Sleep(5);
                if(--nTry == 0)
                {
                    break;
                }
            }
            if (nTry > 0)
            {
                res = 0;
            }
            return res;
        }
        
        private void DiagLabel_Click(object sender, EventArgs e)
        {

        }

        private void OpenTCP_Click(object sender, EventArgs e)
        {
            
            if ((String.IsNullOrEmpty(Hostname)) || (Port == 0))
            {
                SetDiagText("No host name or Port defined");
                return;
            }
            try
            {
                if (IsValidIp(Hostname))
                {
                    IPAddress ip_address = IPAddress.Parse(Hostname);
                    IPEndPoint ipLocalEndPoint = new IPEndPoint(ip_address, Port);
                    client = new TcpClient();
                    client.Connect(ipLocalEndPoint);

                }
                else
                {
                    IPHostEntry hostInfo = Dns.GetHostEntry(Hostname);
                    client = new TcpClient(hostInfo.HostName, Port);

                }
            }
            catch (SocketException e4)
            {
                //               DiagLabel.Text = "SocketException: " + e4;
                SetDiagText("Failure to connect to TCP" + e4);
            }
            if (client.Connected)
            {
                tcp_stream = client.GetStream();
                SetDiagText("Connected to TCP");
                TnClient.Properties.Settings.Default.Hostname = Hostname;
                TnClient.Properties.Settings.Default.Port = Port;
                TnClient.Properties.Settings.Default.Save();
            }
        }

        private void Form1Closing(object sender, FormClosingEventArgs e)
        {
            // The form is closing, save the user's preferences
            // Close everything.
            TnClient.Properties.Settings.Default.Hostname = Hostname;
            TnClient.Properties.Settings.Default.Port = Port;
            TnClient.Properties.Settings.Default.Logfile = log_file_path_str;

            tcp_stream.Close();
            client.Close();
            br.Close();
            sr.Close();
#if LOG
            if (log_file_created) { logfile.Close(); }
#endif
        }

        private void  SendStringToTCP(ref String s_tx, ref String s_rx, int timeout = 150/*miliseconds*/)
        {
            if ((s_rx == null) || (s_tx == null)) { return; }
            int index = 0;
            int tcp_rx_bytes = 0;
            s_rx = String.Empty;
 #if LOG
            if (log_file_created) { logfile.WriteLine(s_tx); }
#endif
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(s_tx);
            tcp_stream.Write(data, 0, data.Length);
            if (timeout > 0)
            {
                Thread.Sleep(timeout);
                if (WaitForResponseFromSlave() == 0)      //if there is a response
                {
                    do
                    {
                        tcp_rx_bytes += tcp_stream.Read(rxbuffer, index, rxbuffer.Length);
                        index = tcp_rx_bytes;
                        if (tcp_rx_bytes >= rxbuffer.Length) break;
                    }
                    while (tcp_stream.DataAvailable);
                    s_rx = System.Text.Encoding.ASCII.GetString(rxbuffer, 0, tcp_rx_bytes);
#if LOG
                    if (log_file_created) { logfile.WriteLine(s_rx); }
                    Thread.Sleep(10);
#endif
                }
            }
         }

        private void MakeFW_writeblock(byte[] src, ref String dst, int src_len, int block_offset, bool last_block)
        {
            if((src == null)||(dst == null)) return;
            String s_fw = String.Empty;
 
 //           if(++seq_number > SEQ_NUMBER_MODULE) seq_number = 0x01;
            seq_number = 0x01;
            s_fw += WriteBlockHeader;

            String seq_num_str = seq_number.ToString("X2");
            seq_num_str = "0x"+ seq_num_str.ToUpper();

            //String block_num_str = block_offset.ToString("X6");
            //block_num_str = block_num_str.ToUpper();

            s_fw += seq_num_str;
            s_fw += " {";
            String payload = ByteArrayToHexString(src, 0, src_len);
            s_fw += payload;
            s_fw += "}\r\n";
            dst = s_fw;
        }
        private void MakeTargetLoad(ref String dst )
        {
            if (dst == null) return;
            String s_fw = String.Empty;
            s_fw += LoadTargetHeader;
//            if (++seq_number > SEQ_NUMBER_MODULE) seq_number = 0x01;
            seq_number = 0x01;
             String seq_num_str = seq_number.ToString("X2");
            seq_num_str = "0x" + seq_num_str.ToUpper();

            String dev_addr_str = DeviceID.ToString("X2");
            dev_addr_str = dev_addr_str.ToUpper();

            s_fw += seq_num_str;
            s_fw += " {";
            s_fw += dev_addr_str;
            s_fw += FW_id_str;
            s_fw += "}\r\n";
            dst = s_fw;
        }

        private void MakeImageNotify(ref String dst, long fw_size)
        {
            if (dst == null) return;
            String s_fw = String.Empty;
            s_fw += ImageNotifyHeader;
//            if (++seq_number > SEQ_NUMBER_MODULE) seq_number = 0x01;
            seq_number = 0x01;
            String seq_num_str = seq_number.ToString("X2");
            seq_num_str = "0x" + seq_num_str.ToUpper();
            s_fw += seq_num_str;
 

            String fw_size_str = fw_size.ToString("X6");
            fw_size_str = fw_size_str.ToUpper();
            String reserved_str = "0000";
            s_fw += " {";
            s_fw += FW_id_str;
            s_fw += FW_version_str;
            s_fw += fw_size_str;
            s_fw += reserved_str;
            s_fw += "}\r\n";
            dst = s_fw;
        }
        private bool IsValidIp(string addr)
        {
            IPAddress ip;
            bool valid = !string.IsNullOrEmpty(addr) && IPAddress.TryParse(addr, out ip);
            return valid;
        }
        
        private Boolean ParseE0TCPResponse(String s)
        {
            int index_cmd = s.IndexOf("0xE0");
            if ((index_cmd < 0x200) && (index_cmd > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
 
        private int ParseE1TCPResponse(String s)
        {
            int index_cmd = s.IndexOf("0xE1");
            if ((index_cmd > 0x200) || (index_cmd < 0))  //string will not be longer than 0x200
            {
                return -1;
            }
            String e1_str = s.Substring(index_cmd, (s.Length - index_cmd) );
           
            index_cmd = e1_str.IndexOf("Payload: ") +"Payload:".Length;
            if ((index_cmd > 0x200) || (index_cmd < 0))         //string will not be longer than 0x200
            {
                return -1;
            }
            else
            {
                String block_num_str = e1_str.Substring(index_cmd, 9);
                block_num_str = block_num_str.Replace(" ", "");
#if LOG
                if (log_file_created) { logfile.WriteLine("Block number: " + block_num_str); }
#endif
                int block_num = Convert.ToInt32(block_num_str, 16);
                return (block_num);
            }
 
        }

        private Boolean ParseE2TCPResponse(String s)
        {
            int index_cmd = s.IndexOf("0xE2");
            if ((index_cmd < 0x200) && (index_cmd > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int WaitForE5TCPResponse()
        {
            int tcp_rx_bytes = 0;
            int index = 0;
            String s_rx = String.Empty;
            if (WaitForResponseFromSlave() == 0)      //if there is a response
            {
                do
                {
                    tcp_rx_bytes += tcp_stream.Read(rxbuffer, index, rxbuffer.Length);
                    index = tcp_rx_bytes;
                    if (tcp_rx_bytes >= rxbuffer.Length) break;
                }
                while (tcp_stream.DataAvailable);
                s_rx = System.Text.Encoding.ASCII.GetString(rxbuffer, 0, tcp_rx_bytes);
#if LOG
                if (log_file_created) { logfile.WriteLine(s_rx); }
                Thread.Sleep(10);
#endif
            }
            else
            {
                return -1;
            }
            int index_cmd = s_rx.IndexOf("0xE5");
            if ((index_cmd > 0x200) || (index_cmd < 0))  //string will not be longer than 0x200
            {
                return -1;
            }
            String e5_str = s_rx.Substring(index_cmd, (s_rx.Length - index_cmd));

            e5_str = e5_str.Replace(" ", "");
#if LOG
            if (log_file_created)
            {
                logfile.WriteLine("E5 resp: " + e5_str);
            }
#endif
            index_cmd = e5_str.IndexOf("Payload:");
            if ((index_cmd > 0x200) || (index_cmd < 0))         //string will not be longer than 0x200
            {
                return -1;
            }
            else
            {
                index_cmd += "Payload:".Length;
                String dev_short_addr_str = e5_str.Substring(index_cmd, 4);
                String load_status_str = e5_str.Substring(index_cmd + 4, 2);
#if LOG
                if (log_file_created)
                {
                    logfile.WriteLine("Index of payload: " + index_cmd);
                    logfile.WriteLine("FW load address: " + dev_short_addr_str);
                    logfile.WriteLine("FW load status: " + load_status_str);                
                }
#endif
                try { 
                    if (IsDigits(dev_short_addr_str))
                    {
                        int short_addr = Convert.ToInt16(dev_short_addr_str, 16);
                        int load_status = Convert.ToByte(load_status_str, 16);
                        return ((int)load_status);
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch(Exception e7)
                {
                    SetDiagText("" + e7);
                    return -1;

                }
                
            }
        }

        //private Boolean IsKeepAlivePacket(String s)
        //{
        //    int index_keep_alive = s.IndexOf("0x24Sequence");
        //    if (index_keep_alive > 0)
        //    {
        //        return true;
        //    }
        //    else{
        //        return false;
        //    }
        //}
        
        private Boolean IsDigits(String s)
        {

            return (s.All(char.IsDigit));
        }
        private void LoadStart_Click(object sender, EventArgs e)
        {
            long l = 0;
            int file_rx_bytes = 0;
            int bytes_from_file = DATA_BLOCK_SIZE;
            try
            {
                SetDiagText("Loading");

                if ((client.Connected) && (sr.CanRead) && (sr.CanSeek) && (sr != null) && (br != null))
                {
                    sr.Seek(0, SeekOrigin.Begin);
                    l = sr.Length;
                    progressBar1.Maximum = (int)l;
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 0;
                    MakeImageNotify(ref tcp_request_string, l);
                    SendStringToTCP(ref tcp_request_string, ref tcp_response_string);
                    if (ParseE0TCPResponse(tcp_response_string))
                    {
                        block_offset = ParseE1TCPResponse(tcp_response_string);
                        if (block_offset != -1)
                        {
                            State = (int)states.Load;
                            bytes_from_file = DATA_BLOCK_SIZE;
                        }
                        else {
                            State = (int)states.Abort;
                            SetDiagText("Aborting");
                        }
                    }
                    else
                    {
                        State = (int)states.Abort;
                        SetDiagText("Aborting");
                    }

                    while ((l > 0) && ((State == (int)states.Load) || (State == (int)states.Complete)))
                    {
                           
                        /*  Reading from file */
                            
                        try
                        {
                            file_rx_bytes = br.Read(input, 0, bytes_from_file);
                        }
                        catch(Exception e1)
                        {
                            SetDiagText("" + e1);
                            State = (int)states.Abort;
                            break;

                        }
                        l -= file_rx_bytes;
                        if (progressBar1.Value < progressBar1.Maximum)
                        {
                            progressBar1.Increment(file_rx_bytes);
                        }
/*
                            catch (System.ArgumentException e1)
                            {
                                SetDiagText("" + e1);
                            }
                            catch (System.IO.IOException e2)
                            {
                                SetDiagText("" + e2);
                            }
                            catch (System.ObjectDisposedException)
                            {
                                SetDiagText("File is not open");
                            }
*/
                            /*  Making console command */
                        if (State == (int)states.Load)
                        {
                            if (l != 0)
                            {
                                MakeFW_writeblock(input, ref tcp_request_string, bytes_from_file, block_offset, false);
                            }
                            else
                            {
                                MakeFW_writeblock(input, ref tcp_request_string, bytes_from_file, block_offset, true);
                                State = (int)states.Complete;
                            }
                        }
                        else if (State == (int)states.Complete)
                        {
                            MakeFW_writeblock(input, ref tcp_request_string, bytes_from_file, block_offset, true);
                        }

                        /*  Sending console command to bridge */
                        SendStringToTCP(ref tcp_request_string, ref tcp_response_string);

                        /*  If response is valid - proceed */
                        if (tcp_response_string != String.Empty)
                        {
                            if (State == (int)states.Load)
                            {
                                block_offset = ParseE1TCPResponse(tcp_response_string);
                            }
                            else if (State == (int)states.Complete)
                            {
                                if (ParseE2TCPResponse(tcp_response_string))
                                {
                                    State = (int)states.LoadSuccess;
                                }
                                else
                                {
                                    State = (int)states.Abort;
                                }
                                break;
                            }

                        }
                        /*  If response is invalid - abort */
                        else
                        {
                            State = (int)states.Abort;
                            break;
                        }

                        if ((State == (int)states.Load) && (l < DATA_BLOCK_SIZE))
                        {
                            State = (int)states.Complete;
                            bytes_from_file = (int)l;
                        }

 
                        SetDiagText("L = " + l);
 
                        /*  If it is going to be the last block - change state to Complete */


                            Thread.Sleep(5);
                            Application.DoEvents();
                    }
 
                    if (State == (int)states.LoadSuccess)
                    {
                        SetDiagText("Load completed");
                        progressBar1.Value = 0;
                    }

                    if (State == (int)states.Abort)
                    {
                        try
                        {
                            SendStringToTCP(ref ImageCancel, ref tcp_response_string);
                            SetDiagText("Closing in Abort");
#if LOG
                            if (log_file_created)
                            {
                                Thread.Sleep(1500);//time to complete writing to logfile
                                logfile.Close();
                            }
#endif
                        }
                        catch (Exception e6)
                        {
                            SetDiagText("Abort Exception:" + e6.Message);

                        }

                        //                        Environment.Exit(1);
                    }
                }
                else
                {
                    if (!client.Connected)
                    {
                        SetDiagText("No TCP connection");
#if LOG
                        if (log_file_created)
                        {
                            Thread.Sleep(500);//time to complete writing to logfile
                            logfile.Close();
                        }
#endif
                        if (sr != null)
                        {
                            br.Close();
                            sr.Close();

                        }
                    }
                    if ((sr == null) || (!sr.CanSeek) || (!sr.CanRead))
                    {
                        SetDiagText("File is not open");
                    }

                }

            }
            catch (Exception e5)
            {
                SetDiagText("Load Start Exception:" + e5.Message);

            }
         }

        private void DevID_TextChanged(object sender, EventArgs e)
        {
            dev_id_str = DevID.Text;
            DeviceID = Convert.ToInt16(dev_id_str, 10);
            if (DeviceID == 0) DeviceID = 0x55;
        }

        private void SleepyDevice_CheckedChanged(object sender, EventArgs e)
        {
            if(SleepyDevice.Checked == true)
            {
                SleepyDev = true;
            }
            else
            {
                SleepyDev = false;    
            }
        }

        private void ProgramDevice_Click(object sender, EventArgs e)
        {
            if(State == (int)states.LoadSuccess)
            {
                int nTry = 100;
                int prog_state = -1;
                MakeTargetLoad(ref tcp_request_string);
                SendStringToTCP(ref tcp_request_string, ref tcp_response_string, 0);
                while((nTry--) >0)
                {
                    prog_state = WaitForE5TCPResponse();
                    if(prog_state >0)
                    {
                        break;
                    }
                    Thread.Sleep(100);

                }
                if(prog_state == 0xAA)
                {
                    State = (int)states.TargetProgramSuccess;
                    SetDiagText("Target programmed");
                }
                else
                {
                    State = (int)states.TargetProgramFail;
                    SetDiagText("Target programming failed");
                }
            }
        }

        private void fw_id_TextChanged(object sender, EventArgs e)
        {
            FW_id_str = fw_id.Text.PadLeft(24,'0');
        }

        private void fw_version_TextChanged(object sender, EventArgs e)
        {
            FW_version_str = fw_version.Text.PadLeft(20,'0');
        }

        private void log_file_TextChanged(object sender, EventArgs e)
        {
            log_file_path_str = log_file.Text;
        }

        private void log_Click(object sender, EventArgs e)
        {
#if LOG
            try
            {
                if (log_file_path_str != String.Empty)
                {
                    logfile = new System.IO.StreamWriter(log_file_path_str);
                    if (logfile != null) {
                        log_file_created = true;
                        TnClient.Properties.Settings.Default.Logfile = log_file_path_str;
                        TnClient.Properties.Settings.Default.Save();
                        logfile.WriteLine("\r\nLogging"); 
                    }
                }
            }
            catch (Exception e6)
            {
                SetDiagText("Log file open exceptiom:" + e6.Message);

            }

#endif
        }


        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void GetBCD_CheckedChanged(object sender, EventArgs e)
        {
            tcp_request_string = SendStringToBridgeHeader;              //set header
            tcp_request_string += " 0x" + LBI_GET_BCD;                  //add command
            tcp_request_string += " 0x" + "00";                         //add caller 0
            tcp_request_string += " {" + LBI_GET_BCD + "}\r\n";             //add payload

            SendStringToTCP(ref tcp_request_string, ref tcp_response_string, 150);
        }

        private void PermitJoin_CheckedChanged(object sender, EventArgs e)
        {
            tcp_request_string = SendStringToBridgeHeader;              //set header
            tcp_request_string += " 0x" + LBI_PERMIT_JOIN;              //add command
            tcp_request_string += " 0x" + "00";                         //add caller 0
            tcp_request_string += " {" + LBI_PERMIT_JOIN + "}\r\n";         //add payload

            SendStringToTCP(ref tcp_request_string, ref tcp_response_string, 150);
        }

        private void DisableJoin_CheckedChanged(object sender, EventArgs e)
        {
            tcp_request_string = SendStringToBridgeHeader;              //set header
            tcp_request_string += " 0x" + LBI_DISABLE_JOIN;             //add command
            tcp_request_string += " 0x" + "00";                         //add caller 0
            tcp_request_string += " {" + LBI_DISABLE_JOIN + "}\r\n";        //add payload

            SendStringToTCP(ref tcp_request_string, ref tcp_response_string, 150);
        }

        private void DeviceDiscovery_CheckedChanged(object sender, EventArgs e)
        {
            tcp_request_string = SendStringToBridgeHeader;              //set header
            tcp_request_string += " 0x" + LBI_DEVICE_DISCOVERY;         //add command
            tcp_request_string += " 0x" + "00";                         //add caller 0
            tcp_request_string += " {" + LBI_DEVICE_DISCOVERY + "}\r\n";    //add payload

            SendStringToTCP(ref tcp_request_string, ref tcp_response_string, 150);
        }

        private void DeviceIdentity_CheckedChanged(object sender, EventArgs e)
        {
            tcp_request_string = SendStringToBridgeHeader;              //set header
            tcp_request_string += " 0x" + LBI_IDENTIFY;                 //add command
            tcp_request_string += " 0x" + "00";                         //add caller 0
            tcp_request_string += " {" + device_short_address_str + identity_timeout_str + "}\r\n";//add payload

            SendStringToTCP(ref tcp_request_string, ref tcp_response_string, 150);
        }

        private void DeviceStopIdentity_CheckedChanged(object sender, EventArgs e)
        {
            tcp_request_string = SendStringToBridgeHeader;              //set header
            tcp_request_string += " 0x" + LBI_STOP_IDENTIFY;            //add command
            tcp_request_string += " 0x" + "00";                         //add caller 0
            tcp_request_string += " {" + device_short_address_str + "}\r\n";//add payload

            SendStringToTCP(ref tcp_request_string, ref tcp_response_string, 150);
        }

        private void DeviceInfo_CheckedChanged(object sender, EventArgs e)
        {
            tcp_request_string = SendStringToBridgeHeader;              //set header
            tcp_request_string += " 0x" + LBI_DEVICE_INFO;              //add command
            tcp_request_string += " 0x" + "00";                         //add caller 0
            tcp_request_string += " {" + device_short_address_str + "}\r\n";//add payload

            SendStringToTCP(ref tcp_request_string, ref tcp_response_string, 150);
        }

        private void IsAlive_CheckedChanged(object sender, EventArgs e)
        {
            tcp_request_string = SendStringToBridgeHeader;              //set header
            tcp_request_string += " 0x" + LBI_ISALIVE;                  //add command
            tcp_request_string += " 0x" + "00";                         //add caller 0
            tcp_request_string += " {" + LBI_ISALIVE + "}\r\n";             //add payload

            SendStringToTCP(ref tcp_request_string, ref tcp_response_string, 150);
        }

        private void FormNetwork_CheckedChanged(object sender, EventArgs e)
        {
            tcp_request_string = SendStringToBridgeHeader;              //set header
            tcp_request_string += " 0x" + LBI_FORM_NETWORK;             //add command
            tcp_request_string += " 0x" + "00";                         //add caller 0
            tcp_request_string += " {" + LBI_FORM_NETWORK + "}\r\n";        //add payload

            SendStringToTCP(ref tcp_request_string, ref tcp_response_string, 150);
        }

        private void JoinNetwork_CheckedChanged(object sender, EventArgs e)
        {
            tcp_request_string = SendStringToBridgeHeader;              //set header
            tcp_request_string += " 0x" + LBI_JOIN_NETWORK;             //add command
            tcp_request_string += " 0x" + "00";                         //add caller 0
            tcp_request_string += " {" + LBI_JOIN_NETWORK + "}\r\n";        //add payload

            SendStringToTCP(ref tcp_request_string, ref tcp_response_string, 150);
        }

        private void Device_Short_Address_TextChanged(object sender, EventArgs e)
        {
            try
            {
                device_short_address = Convert.ToInt16(Device_Short_Address.Text, 10);
                device_short_address_str = device_short_address.ToString("X4");
                device_short_address_str = device_short_address_str.ToUpper();
                SetDiagText("");
            }
            catch (Exception e6)
            {
                SetDiagText("illegal char input:" + e6);

            }
        }

        private void Identify_Timeout_TextChanged(object sender, EventArgs e)
        {
            try
            {
                identity_timeout = Convert.ToInt16(Identify_Timeout.Text, 10);
                identity_timeout_str = identity_timeout.ToString("X4");
                identity_timeout_str = identity_timeout_str.ToUpper();
                SetDiagText("");
            }
            catch (Exception e7)
            {
                SetDiagText("illegal char input:" + e7);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}