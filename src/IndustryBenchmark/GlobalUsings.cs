﻿global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
global using System.Diagnostics;
global using System.Net.Mime;
global using Microsoft.AspNetCore;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.HttpsPolicy;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.EntityFrameworkCore;
global using IntegrationEvents;
global using EventBus;
global using EventBus.Abstractions;
global using EventBus.Events;
global using EventBus.Extensions;
global using EventBusRabbitMQ;
global using Autofac;
global using Autofac.Extensions.DependencyInjection;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Text.Json.Serialization;