﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4952
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Health.Direct.Config.Client.DomainManager {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:directproject:config/store/082010", ConfigurationName="DomainManager.IAddressManager")]
    public interface IAddressManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAddressManager/AddAddress", ReplyAction="urn:directproject:config/store/082010/IAddressManager/AddAddressResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAddressManager/AddAddressConfigStoreFaultF" +
            "ault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Address AddAddress(Health.Direct.Config.Store.Address address);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAddressManager/AddAddresses", ReplyAction="urn:directproject:config/store/082010/IAddressManager/AddAddressesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAddressManager/AddAddressesConfigStoreFaul" +
            "tFault", Name="ConfigStoreFault")]
        void AddAddresses(Health.Direct.Config.Store.Address[] addresses);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAddressManager/UpdateAddresses", ReplyAction="urn:directproject:config/store/082010/IAddressManager/UpdateAddressesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAddressManager/UpdateAddressesConfigStoreF" +
            "aultFault", Name="ConfigStoreFault")]
        void UpdateAddresses(Health.Direct.Config.Store.Address[] address);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAddressManager/GetAddresses", ReplyAction="urn:directproject:config/store/082010/IAddressManager/GetAddressesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAddressManager/GetAddressesConfigStoreFaul" +
            "tFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Address[] GetAddresses(string[] emailAddresses, System.Nullable<Health.Direct.Config.Store.EntityStatus> status);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAddressManager/GetAddressesByID", ReplyAction="urn:directproject:config/store/082010/IAddressManager/GetAddressesByIDResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAddressManager/GetAddressesByIDConfigStore" +
            "FaultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Address[] GetAddressesByID(long[] addressIDs, System.Nullable<Health.Direct.Config.Store.EntityStatus> status);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAddressManager/RemoveAddresses", ReplyAction="urn:directproject:config/store/082010/IAddressManager/RemoveAddressesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAddressManager/RemoveAddressesConfigStoreF" +
            "aultFault", Name="ConfigStoreFault")]
        void RemoveAddresses(string[] emailAddresses);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAddressManager/RemoveDomainAddresses", ReplyAction="urn:directproject:config/store/082010/IAddressManager/RemoveDomainAddressesRespon" +
            "se")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAddressManager/RemoveDomainAddressesConfig" +
            "StoreFaultFault", Name="ConfigStoreFault")]
        void RemoveDomainAddresses(long domainID);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAddressManager/SetDomainAddressesStatus", ReplyAction="urn:directproject:config/store/082010/IAddressManager/SetDomainAddressesStatusRes" +
            "ponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAddressManager/SetDomainAddressesStatusCon" +
            "figStoreFaultFault", Name="ConfigStoreFault")]
        void SetDomainAddressesStatus(long domainID, Health.Direct.Config.Store.EntityStatus status);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAddressManager/GetAddressCount", ReplyAction="urn:directproject:config/store/082010/IAddressManager/GetAddressCountResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAddressManager/GetAddressCountConfigStoreF" +
            "aultFault", Name="ConfigStoreFault")]
        int GetAddressCount(string domainName);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAddressManager/EnumerateDomainAddresses", ReplyAction="urn:directproject:config/store/082010/IAddressManager/EnumerateDomainAddressesRes" +
            "ponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAddressManager/EnumerateDomainAddressesCon" +
            "figStoreFaultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Address[] EnumerateDomainAddresses(string domainName, string lastAddress, int maxResults);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAddressManager/EnumerateAddresses", ReplyAction="urn:directproject:config/store/082010/IAddressManager/EnumerateAddressesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAddressManager/EnumerateAddressesConfigSto" +
            "reFaultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Address[] EnumerateAddresses(string lastAddress, int maxResults);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IAddressManagerChannel : Health.Direct.Config.Client.DomainManager.IAddressManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class AddressManagerClient : System.ServiceModel.ClientBase<Health.Direct.Config.Client.DomainManager.IAddressManager>, Health.Direct.Config.Client.DomainManager.IAddressManager {
        
        public AddressManagerClient() {
        }
        
        public AddressManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AddressManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AddressManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AddressManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Health.Direct.Config.Store.Address AddAddress(Health.Direct.Config.Store.Address address) {
            return base.Channel.AddAddress(address);
        }
        
        public void AddAddresses(Health.Direct.Config.Store.Address[] addresses) {
            base.Channel.AddAddresses(addresses);
        }
        
        public void UpdateAddresses(Health.Direct.Config.Store.Address[] address) {
            base.Channel.UpdateAddresses(address);
        }
        
        public Health.Direct.Config.Store.Address[] GetAddresses(string[] emailAddresses, System.Nullable<Health.Direct.Config.Store.EntityStatus> status) {
            return base.Channel.GetAddresses(emailAddresses, status);
        }
        
        public Health.Direct.Config.Store.Address[] GetAddressesByID(long[] addressIDs, System.Nullable<Health.Direct.Config.Store.EntityStatus> status) {
            return base.Channel.GetAddressesByID(addressIDs, status);
        }
        
        public void RemoveAddresses(string[] emailAddresses) {
            base.Channel.RemoveAddresses(emailAddresses);
        }
        
        public void RemoveDomainAddresses(long domainID) {
            base.Channel.RemoveDomainAddresses(domainID);
        }
        
        public void SetDomainAddressesStatus(long domainID, Health.Direct.Config.Store.EntityStatus status) {
            base.Channel.SetDomainAddressesStatus(domainID, status);
        }
        
        public int GetAddressCount(string domainName) {
            return base.Channel.GetAddressCount(domainName);
        }
        
        public Health.Direct.Config.Store.Address[] EnumerateDomainAddresses(string domainName, string lastAddress, int maxResults) {
            return base.Channel.EnumerateDomainAddresses(domainName, lastAddress, maxResults);
        }
        
        public Health.Direct.Config.Store.Address[] EnumerateAddresses(string lastAddress, int maxResults) {
            return base.Channel.EnumerateAddresses(lastAddress, maxResults);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:directproject:config/store/082010", ConfigurationName="DomainManager.IDomainManager")]
    public interface IDomainManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDomainManager/AddDomain", ReplyAction="urn:directproject:config/store/082010/IDomainManager/AddDomainResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDomainManager/AddDomainConfigStoreFaultFau" +
            "lt", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Domain AddDomain(Health.Direct.Config.Store.Domain domain);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDomainManager/UpdateDomain", ReplyAction="urn:directproject:config/store/082010/IDomainManager/UpdateDomainResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDomainManager/UpdateDomainConfigStoreFault" +
            "Fault", Name="ConfigStoreFault")]
        void UpdateDomain(Health.Direct.Config.Store.Domain domain);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDomainManager/GetDomain", ReplyAction="urn:directproject:config/store/082010/IDomainManager/GetDomainResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDomainManager/GetDomainConfigStoreFaultFau" +
            "lt", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Domain GetDomain(long id);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDomainManager/GetDomainCount", ReplyAction="urn:directproject:config/store/082010/IDomainManager/GetDomainCountResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDomainManager/GetDomainCountConfigStoreFau" +
            "ltFault", Name="ConfigStoreFault")]
        int GetDomainCount();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDomainManager/GetDomains", ReplyAction="urn:directproject:config/store/082010/IDomainManager/GetDomainsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDomainManager/GetDomainsConfigStoreFaultFa" +
            "ult", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Domain[] GetDomains(string[] domainNames, System.Nullable<Health.Direct.Config.Store.EntityStatus> status);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDomainManager/RemoveDomain", ReplyAction="urn:directproject:config/store/082010/IDomainManager/RemoveDomainResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDomainManager/RemoveDomainConfigStoreFault" +
            "Fault", Name="ConfigStoreFault")]
        void RemoveDomain(string domainName);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDomainManager/EnumerateDomains", ReplyAction="urn:directproject:config/store/082010/IDomainManager/EnumerateDomainsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDomainManager/EnumerateDomainsConfigStoreF" +
            "aultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Domain[] EnumerateDomains(string lastDomainName, int maxResults);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IDomainManagerChannel : Health.Direct.Config.Client.DomainManager.IDomainManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class DomainManagerClient : System.ServiceModel.ClientBase<Health.Direct.Config.Client.DomainManager.IDomainManager>, Health.Direct.Config.Client.DomainManager.IDomainManager {
        
        public DomainManagerClient() {
        }
        
        public DomainManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DomainManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DomainManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DomainManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Health.Direct.Config.Store.Domain AddDomain(Health.Direct.Config.Store.Domain domain) {
            return base.Channel.AddDomain(domain);
        }
        
        public void UpdateDomain(Health.Direct.Config.Store.Domain domain) {
            base.Channel.UpdateDomain(domain);
        }
        
        public Health.Direct.Config.Store.Domain GetDomain(long id) {
            return base.Channel.GetDomain(id);
        }
        
        public int GetDomainCount() {
            return base.Channel.GetDomainCount();
        }
        
        public Health.Direct.Config.Store.Domain[] GetDomains(string[] domainNames, System.Nullable<Health.Direct.Config.Store.EntityStatus> status) {
            return base.Channel.GetDomains(domainNames, status);
        }
        
        public void RemoveDomain(string domainName) {
            base.Channel.RemoveDomain(domainName);
        }
        
        public Health.Direct.Config.Store.Domain[] EnumerateDomains(string lastDomainName, int maxResults) {
            return base.Channel.EnumerateDomains(lastDomainName, maxResults);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:directproject:config/store/082010", ConfigurationName="DomainManager.IDnsRecordManager")]
    public interface IDnsRecordManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDnsRecordManager/AddDnsRecords", ReplyAction="urn:directproject:config/store/082010/IDnsRecordManager/AddDnsRecordsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDnsRecordManager/AddDnsRecordsConfigStoreF" +
            "aultFault", Name="ConfigStoreFault")]
        void AddDnsRecords(Health.Direct.Config.Store.DnsRecord[] dnsRecords);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDnsRecordManager/AddDnsRecord", ReplyAction="urn:directproject:config/store/082010/IDnsRecordManager/AddDnsRecordResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDnsRecordManager/AddDnsRecordConfigStoreFa" +
            "ultFault", Name="ConfigStoreFault")]
        void AddDnsRecord(Health.Direct.Config.Store.DnsRecord record);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDnsRecordManager/Count", ReplyAction="urn:directproject:config/store/082010/IDnsRecordManager/CountResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDnsRecordManager/CountConfigStoreFaultFaul" +
            "t", Name="ConfigStoreFault")]
        int Count(System.Nullable<Health.Direct.Common.DnsResolver.DnsStandard.RecordType> recordType);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDnsRecordManager/GetLastDnsRecords", ReplyAction="urn:directproject:config/store/082010/IDnsRecordManager/GetLastDnsRecordsResponse" +
            "")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDnsRecordManager/GetLastDnsRecordsConfigSt" +
            "oreFaultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.DnsRecord[] GetLastDnsRecords(long lastRecordID, int maxResults, Health.Direct.Common.DnsResolver.DnsStandard.RecordType typeID);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDnsRecordManager/GetDnsRecord", ReplyAction="urn:directproject:config/store/082010/IDnsRecordManager/GetDnsRecordResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDnsRecordManager/GetDnsRecordConfigStoreFa" +
            "ultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.DnsRecord GetDnsRecord(long recordID);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDnsRecordManager/GetDnsRecords", ReplyAction="urn:directproject:config/store/082010/IDnsRecordManager/GetDnsRecordsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDnsRecordManager/GetDnsRecordsConfigStoreF" +
            "aultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.DnsRecord[] GetDnsRecords(long[] recordIDs);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDnsRecordManager/GetMatchingDnsRecords", ReplyAction="urn:directproject:config/store/082010/IDnsRecordManager/GetMatchingDnsRecordsResp" +
            "onse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDnsRecordManager/GetMatchingDnsRecordsConf" +
            "igStoreFaultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.DnsRecord[] GetMatchingDnsRecords(string domainName);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDnsRecordManager/GetMatchingDnsRecordsByTy" +
            "pe", ReplyAction="urn:directproject:config/store/082010/IDnsRecordManager/GetMatchingDnsRecordsByTy" +
            "peResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDnsRecordManager/GetMatchingDnsRecordsByTy" +
            "peConfigStoreFaultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.DnsRecord[] GetMatchingDnsRecordsByType(string domainName, Health.Direct.Common.DnsResolver.DnsStandard.RecordType typeID);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDnsRecordManager/RemoveDnsRecord", ReplyAction="urn:directproject:config/store/082010/IDnsRecordManager/RemoveDnsRecordResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDnsRecordManager/RemoveDnsRecordConfigStor" +
            "eFaultFault", Name="ConfigStoreFault")]
        void RemoveDnsRecord(Health.Direct.Config.Store.DnsRecord dnsRecord);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDnsRecordManager/RemoveDnsRecordByID", ReplyAction="urn:directproject:config/store/082010/IDnsRecordManager/RemoveDnsRecordByIDRespon" +
            "se")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDnsRecordManager/RemoveDnsRecordByIDConfig" +
            "StoreFaultFault", Name="ConfigStoreFault")]
        void RemoveDnsRecordByID(long recordID);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDnsRecordManager/UpdateDnsRecord", ReplyAction="urn:directproject:config/store/082010/IDnsRecordManager/UpdateDnsRecordResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDnsRecordManager/UpdateDnsRecordConfigStor" +
            "eFaultFault", Name="ConfigStoreFault")]
        void UpdateDnsRecord(Health.Direct.Config.Store.DnsRecord dnsRecord);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IDnsRecordManager/UpdateDnsRecords", ReplyAction="urn:directproject:config/store/082010/IDnsRecordManager/UpdateDnsRecordsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IDnsRecordManager/UpdateDnsRecordsConfigSto" +
            "reFaultFault", Name="ConfigStoreFault")]
        void UpdateDnsRecords(Health.Direct.Config.Store.DnsRecord[] dnsRecords);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IDnsRecordManagerChannel : Health.Direct.Config.Client.DomainManager.IDnsRecordManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class DnsRecordManagerClient : System.ServiceModel.ClientBase<Health.Direct.Config.Client.DomainManager.IDnsRecordManager>, Health.Direct.Config.Client.DomainManager.IDnsRecordManager {
        
        public DnsRecordManagerClient() {
        }
        
        public DnsRecordManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DnsRecordManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DnsRecordManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DnsRecordManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddDnsRecords(Health.Direct.Config.Store.DnsRecord[] dnsRecords) {
            base.Channel.AddDnsRecords(dnsRecords);
        }
        
        public void AddDnsRecord(Health.Direct.Config.Store.DnsRecord record) {
            base.Channel.AddDnsRecord(record);
        }
        
        public int Count(System.Nullable<Health.Direct.Common.DnsResolver.DnsStandard.RecordType> recordType) {
            return base.Channel.Count(recordType);
        }
        
        public Health.Direct.Config.Store.DnsRecord[] GetLastDnsRecords(long lastRecordID, int maxResults, Health.Direct.Common.DnsResolver.DnsStandard.RecordType typeID) {
            return base.Channel.GetLastDnsRecords(lastRecordID, maxResults, typeID);
        }
        
        public Health.Direct.Config.Store.DnsRecord GetDnsRecord(long recordID) {
            return base.Channel.GetDnsRecord(recordID);
        }
        
        public Health.Direct.Config.Store.DnsRecord[] GetDnsRecords(long[] recordIDs) {
            return base.Channel.GetDnsRecords(recordIDs);
        }
        
        public Health.Direct.Config.Store.DnsRecord[] GetMatchingDnsRecords(string domainName) {
            return base.Channel.GetMatchingDnsRecords(domainName);
        }
        
        public Health.Direct.Config.Store.DnsRecord[] GetMatchingDnsRecordsByType(string domainName, Health.Direct.Common.DnsResolver.DnsStandard.RecordType typeID) {
            return base.Channel.GetMatchingDnsRecordsByType(domainName, typeID);
        }
        
        public void RemoveDnsRecord(Health.Direct.Config.Store.DnsRecord dnsRecord) {
            base.Channel.RemoveDnsRecord(dnsRecord);
        }
        
        public void RemoveDnsRecordByID(long recordID) {
            base.Channel.RemoveDnsRecordByID(recordID);
        }
        
        public void UpdateDnsRecord(Health.Direct.Config.Store.DnsRecord dnsRecord) {
            base.Channel.UpdateDnsRecord(dnsRecord);
        }
        
        public void UpdateDnsRecords(Health.Direct.Config.Store.DnsRecord[] dnsRecords) {
            base.Channel.UpdateDnsRecords(dnsRecords);
        }
    }
}
