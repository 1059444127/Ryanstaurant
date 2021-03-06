﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ryanstaurant.UMS.DataContract.Utility
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ItemContent", Namespace="http://schemas.datacontract.org/2004/07/Ryanstaurant.UMS.DataContract.Utility")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Ryanstaurant.UMS.DataContract.Employee))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Ryanstaurant.UMS.DataContract.Authority))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Ryanstaurant.UMS.DataContract.Role))]
    public partial class ItemContent : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private Ryanstaurant.UMS.DataContract.Utility.RequestContent RequestInfoField;
        
        private Ryanstaurant.UMS.DataContract.Utility.ResultContent ResultInfoField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Ryanstaurant.UMS.DataContract.Utility.RequestContent RequestInfo
        {
            get
            {
                return this.RequestInfoField;
            }
            set
            {
                this.RequestInfoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Ryanstaurant.UMS.DataContract.Utility.ResultContent ResultInfo
        {
            get
            {
                return this.ResultInfoField;
            }
            set
            {
                this.ResultInfoField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RequestContent", Namespace="http://schemas.datacontract.org/2004/07/Ryanstaurant.UMS.DataContract.Utility")]
    public partial class RequestContent : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private Ryanstaurant.UMS.DataContract.Utility.RequestOperation OperationField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Ryanstaurant.UMS.DataContract.Utility.RequestOperation Operation
        {
            get
            {
                return this.OperationField;
            }
            set
            {
                this.OperationField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResultContent", Namespace="http://schemas.datacontract.org/2004/07/Ryanstaurant.UMS.DataContract.Utility")]
    public partial class ResultContent : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string ExceptionField;
        
        private string InnerErrorMessageField;
        
        private Ryanstaurant.UMS.DataContract.Utility.ResultState StateField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Exception
        {
            get
            {
                return this.ExceptionField;
            }
            set
            {
                this.ExceptionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string InnerErrorMessage
        {
            get
            {
                return this.InnerErrorMessageField;
            }
            set
            {
                this.InnerErrorMessageField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Ryanstaurant.UMS.DataContract.Utility.ResultState State
        {
            get
            {
                return this.StateField;
            }
            set
            {
                this.StateField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RequestOperation", Namespace="http://schemas.datacontract.org/2004/07/Ryanstaurant.UMS.DataContract.Utility")]
    public enum RequestOperation : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Query = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Add = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Modify = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Delete = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResultState", Namespace="http://schemas.datacontract.org/2004/07/Ryanstaurant.UMS.DataContract.Utility")]
    public enum ResultState : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Success = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Fail = 0,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResultEntity", Namespace="http://schemas.datacontract.org/2004/07/Ryanstaurant.UMS.DataContract.Utility")]
    public partial class ResultEntity : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string ErrorMessageField;
        
        private string InnerErrorMessageField;
        
        private Ryanstaurant.UMS.DataContract.Utility.ItemContent[] ResultObjectField;
        
        private Ryanstaurant.UMS.DataContract.Utility.ResultState StateField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorMessage
        {
            get
            {
                return this.ErrorMessageField;
            }
            set
            {
                this.ErrorMessageField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string InnerErrorMessage
        {
            get
            {
                return this.InnerErrorMessageField;
            }
            set
            {
                this.InnerErrorMessageField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Ryanstaurant.UMS.DataContract.Utility.ItemContent[] ResultObject
        {
            get
            {
                return this.ResultObjectField;
            }
            set
            {
                this.ResultObjectField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Ryanstaurant.UMS.DataContract.Utility.ResultState State
        {
            get
            {
                return this.StateField;
            }
            set
            {
                this.StateField = value;
            }
        }
    }
}
namespace Ryanstaurant.UMS.DataContract
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Employee", Namespace="http://schemas.datacontract.org/2004/07/Ryanstaurant.UMS.DataContract")]
    public partial class Employee : Ryanstaurant.UMS.DataContract.Utility.ItemContent
    {
        
        private Ryanstaurant.UMS.DataContract.Authority[] AuthoritiesField;
        
        private string DescriptionField;
        
        private string ExceptionField;
        
        private int IDField;
        
        private string LoginNameField;
        
        private string NameField;
        
        private string PasswordField;
        
        private Ryanstaurant.UMS.DataContract.Role[] RolesField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Ryanstaurant.UMS.DataContract.Authority[] Authorities
        {
            get
            {
                return this.AuthoritiesField;
            }
            set
            {
                this.AuthoritiesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description
        {
            get
            {
                return this.DescriptionField;
            }
            set
            {
                this.DescriptionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Exception
        {
            get
            {
                return this.ExceptionField;
            }
            set
            {
                this.ExceptionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LoginName
        {
            get
            {
                return this.LoginNameField;
            }
            set
            {
                this.LoginNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password
        {
            get
            {
                return this.PasswordField;
            }
            set
            {
                this.PasswordField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Ryanstaurant.UMS.DataContract.Role[] Roles
        {
            get
            {
                return this.RolesField;
            }
            set
            {
                this.RolesField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Authority", Namespace="http://schemas.datacontract.org/2004/07/Ryanstaurant.UMS.DataContract")]
    public partial class Authority : Ryanstaurant.UMS.DataContract.Utility.ItemContent
    {
        
        private string DescriptionField;
        
        private int IDField;
        
        private int KeyCodeField;
        
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description
        {
            get
            {
                return this.DescriptionField;
            }
            set
            {
                this.DescriptionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int KeyCode
        {
            get
            {
                return this.KeyCodeField;
            }
            set
            {
                this.KeyCodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Role", Namespace="http://schemas.datacontract.org/2004/07/Ryanstaurant.UMS.DataContract")]
    public partial class Role : Ryanstaurant.UMS.DataContract.Utility.ItemContent
    {
        
        private Ryanstaurant.UMS.DataContract.Authority[] AuthoritiesField;
        
        private string DescriptionField;
        
        private int IDField;
        
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Ryanstaurant.UMS.DataContract.Authority[] Authorities
        {
            get
            {
                return this.AuthoritiesField;
            }
            set
            {
                this.AuthoritiesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description
        {
            get
            {
                return this.DescriptionField;
            }
            set
            {
                this.DescriptionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IUMSService")]
public interface IUMSService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUMSService/Execute", ReplyAction="http://tempuri.org/IUMSService/ExecuteResponse")]
    Ryanstaurant.UMS.DataContract.Utility.ResultEntity Execute(Ryanstaurant.UMS.DataContract.Utility.ItemContent[] requestEntitiy);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUMSService/Execute", ReplyAction="http://tempuri.org/IUMSService/ExecuteResponse")]
    System.Threading.Tasks.Task<Ryanstaurant.UMS.DataContract.Utility.ResultEntity> ExecuteAsync(Ryanstaurant.UMS.DataContract.Utility.ItemContent[] requestEntitiy);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUMSService/Query", ReplyAction="http://tempuri.org/IUMSService/QueryResponse")]
    Ryanstaurant.UMS.DataContract.Utility.ResultEntity Query(Ryanstaurant.UMS.DataContract.Utility.ItemContent[] requestEntitiy);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUMSService/Query", ReplyAction="http://tempuri.org/IUMSService/QueryResponse")]
    System.Threading.Tasks.Task<Ryanstaurant.UMS.DataContract.Utility.ResultEntity> QueryAsync(Ryanstaurant.UMS.DataContract.Utility.ItemContent[] requestEntitiy);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IUMSServiceChannel : IUMSService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class UMSServiceClient : System.ServiceModel.ClientBase<IUMSService>, IUMSService
{
    
    public UMSServiceClient()
    {
    }
    
    public UMSServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public UMSServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public UMSServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public UMSServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public Ryanstaurant.UMS.DataContract.Utility.ResultEntity Execute(Ryanstaurant.UMS.DataContract.Utility.ItemContent[] requestEntitiy)
    {
        return base.Channel.Execute(requestEntitiy);
    }
    
    public System.Threading.Tasks.Task<Ryanstaurant.UMS.DataContract.Utility.ResultEntity> ExecuteAsync(Ryanstaurant.UMS.DataContract.Utility.ItemContent[] requestEntitiy)
    {
        return base.Channel.ExecuteAsync(requestEntitiy);
    }
    
    public Ryanstaurant.UMS.DataContract.Utility.ResultEntity Query(Ryanstaurant.UMS.DataContract.Utility.ItemContent[] requestEntitiy)
    {
        return base.Channel.Query(requestEntitiy);
    }
    
    public System.Threading.Tasks.Task<Ryanstaurant.UMS.DataContract.Utility.ResultEntity> QueryAsync(Ryanstaurant.UMS.DataContract.Utility.ItemContent[] requestEntitiy)
    {
        return base.Channel.QueryAsync(requestEntitiy);
    }
}
