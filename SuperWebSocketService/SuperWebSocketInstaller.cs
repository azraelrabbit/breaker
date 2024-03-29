﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Configuration;


namespace SuperWebSocket.Service
{
    [RunInstaller(true)]
    public partial class SuperWebSocketInstaller : System.Configuration.Install.Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller processInstaller;

        public SuperWebSocketInstaller()
        {
            InitializeComponent();

            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = ConfigurationManager.AppSettings["ServiceName"];

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }
}
