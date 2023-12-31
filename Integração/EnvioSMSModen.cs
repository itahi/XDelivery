﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace DexComanda.Integração
{
  public  class EnvioSMSModen
    {
      private static bool portaaberta = false;
      public static bool EnviaSMS(int iVelocidade, string iPorta, string iTelefone, string iMensagem)
      {
         SerialPort _serialPort = new SerialPort();
         bool isSend = false;
          
        var receiveNow = new AutoResetEvent(false);
         try
         {
             _serialPort.PortName = iPorta;
             _serialPort.BaudRate = iVelocidade;
             _serialPort.Parity = Parity.None;
             _serialPort.DataBits = 8;
             _serialPort.StopBits = StopBits.One;
             _serialPort.ReadTimeout = 300;
             _serialPort.Encoding = Encoding.GetEncoding("iso-8859-1");
             _serialPort.Handshake = Handshake.RequestToSend; // define a forma com é feito o HandShake
             _serialPort.DtrEnable = true; //activa o DTR
             _serialPort.RtsEnable = true; //activa o RTS
             _serialPort.NewLine = System.Environment.NewLine;
  
                 _serialPort.Open();

             if (_serialPort.IsOpen)
             {
                  portaaberta = true;
                 _serialPort.WriteLine("AT"+ (char)(13));
                 _serialPort.WriteLine("AT+CMGF=1" + (char)(13));
                 _serialPort.WriteLine("AT+CMGS=\"" + iTelefone + "\"");
                 _serialPort.WriteLine(">" + iMensagem + (char)(26));
                // _serialPort.WriteLine("RX");
                 Thread.Sleep(15);
                // var stri = _serialPort.ReadLine();
                 isSend = true;
             }
             else
             {
                 isSend = false;
                 MessageBox.Show("Não foi possivel enviar o SMS", "Dex - Erro");
                 Environment.Exit(1);
             }
        
            
         }
         catch (Exception e)
         {

             MessageBox.Show("Não foi possivel enviar o SMS pois "+e.Message,"Dex Aviso");
         }
         _serialPort.Close();
         return isSend;
      }
    }
}
