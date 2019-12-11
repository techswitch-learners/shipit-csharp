using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Mono.Security.Authenticode;
using ShipIt.Models.DataModels;

namespace ShipIt.Models.ApiModels
{
    public enum EmployeeRole
    {
        OPERATIONS_MANAGER,
        PICKER,
        CLEANER,
        MANAGER
    }

    public static class DataBaseRoles
    {
        public const string OperationsManager = "operations manager";
        public const string Picker = "picker";
        public const string Cleaner = "cleaner";
        public const string Manager = "manager";
    }

    public class EmployeeApiModel
    {
        public string Name { get; set; }
        public int WarehouseId { get; set; }
        public EmployeeRole role { get; set; }
        public string ext { get; set; }

        public EmployeeApiModel(EmployeeDataModel dataModel)
        {
            Name = dataModel.Name;
            WarehouseId = dataModel.WarehouseId;
            role = MapDatabaseRoleToApiRole(dataModel.Role);
            ext = dataModel.Ext;
        }

        private EmployeeRole MapDatabaseRoleToApiRole(string databaseRole)
        {
            if (databaseRole == DataBaseRoles.Cleaner) return EmployeeRole.CLEANER;
            if (databaseRole == DataBaseRoles.Manager) return EmployeeRole.MANAGER;
            if (databaseRole == DataBaseRoles.OperationsManager) return EmployeeRole.OPERATIONS_MANAGER;
            if (databaseRole == DataBaseRoles.Picker) return EmployeeRole.PICKER;
            throw new ArgumentOutOfRangeException("DatabaseRole");
        }

        //Empty constructor needed for Xml serialization
        public EmployeeApiModel()
        {
        }
    }
}