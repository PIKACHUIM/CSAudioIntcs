/****************************************************************************** 
/* Module: Example
/* Description: Shows how to generate Trial Extension keys in C# language
/*
/* Authors: Rafael Ahucha  
/* (c) 2004 Winlicense Professional
/*****************************************************************************/ 


/****************************************************************************** 
/*                   Libraries used by this module
/*****************************************************************************/ 

using System;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;


/****************************************************************************** 
/* Class: App
/*
/* Description: Main application class
/*
/******************************************************************************/

class App{
    
   public static void Main(string[] args){

        string TrialHash = "dsf7987fgvb87vc89n32d6z4c8b9s6a5d8g623n334jgt1i23324gvfa2334jc87634h";
        byte[] ExtendedKey = new byte [100];
        int    SizeKey;

        // Create a key extending days to 3 and executions to 5
        
        SizeKey = WinlicenseSDK.WLGenTrialExtensionFileKey(TrialHash,0,3,5,0,0,0, ExtendedKey);

        // Create file to store the generated key

        FileStream fs = new FileStream("extendkey.dat", FileMode.Create);
        
        BinaryWriter w = new BinaryWriter(fs);
        
        // Write data to file

        for (int i = 0; i < SizeKey; i++)
        {
            w.Write((byte)ExtendedKey[i]);
        }

        // Close file
        
        w.Close();
        fs.Close();

        MessageBox.Show("A trial extension key has been generated", "Trial Extension SDK");
        
}


    /****************************************************************************** 
    /* Class: WinlicenseSDK
    /*
    /* Description: Wrapper for the Winlicense SDK APIs
    /*
    /******************************************************************************/

    class WinlicenseSDK
    {
        [DllImport( "WinlicenseSDK.dll", EntryPoint="WLGenTrialExtensionFileKey", CallingConvention = CallingConvention.StdCall )]
        public static extern int WLGenTrialExtensionFileKey(string TrialHash, int Level, int NumDays, int NumExec, int NewDate, int NumMinutes, int TimeRuntime, byte[] BufferOut);
    }
    
}
