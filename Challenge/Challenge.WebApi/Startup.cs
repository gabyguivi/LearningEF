﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Challenge.WebApi.Startup))]

namespace Challenge.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {           
        }
    }
}