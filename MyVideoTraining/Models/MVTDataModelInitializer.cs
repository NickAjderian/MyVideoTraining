using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyVideoTraining.Models
{
    public class MVTDataModelInitializer:System.Data.Entity.MigrateDatabaseToLatestVersion<MVTDataModel,MyVideoTraining.Migrations.Configuration>
    {
        
    }
}