using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace OIM.PS.SyncProject.Common
{



    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]

    public partial class PowershellConnectorDefinition
    {

        private List<PCDefPluginAssembly> pluginAssembliesField;

        private List<PCDefConnectionParameter> connectionParametersField;

        private PCDefInitialization initializationField;

        private List<PCDefClass> schemaField;

        private string idField;

        private string versionField;

        private string descriptionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Assembly", IsNullable = false)]
        public List<PCDefPluginAssembly> PluginAssemblies
        {
            get
            {
                return this.pluginAssembliesField;
            }
            set
            {
                this.pluginAssembliesField = value;
            }
        }

        [XmlAnyElement("ConnectionParameterComment")]
        public XmlComment ConnectionParameterComment
        {
            get
            {
                return new XmlDocument().CreateComment("======Connection Parameter================" + Environment.NewLine);
            }
            set { }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ConnectionParameter", IsNullable = false)]
        public List<PCDefConnectionParameter> ConnectionParameters
        {
            get
            {
                return this.connectionParametersField;
            }
            set
            {
                this.connectionParametersField = value;
            }
        }

        [XmlAnyElement("InitializationComment")]
        public XmlComment InitializationComment
        {
            get
            {
                return new XmlDocument().CreateComment("======Initialization================" + Environment.NewLine);
            }
            set { }
        }

        /// <remarks/>
        public PCDefInitialization Initialization
        {
            get
            {
                return this.initializationField;
            }
            set
            {
                this.initializationField = value;
            }
        }

        [XmlAnyElement("ClassesComment")]
        public XmlComment ClassesComment
        {
            get
            {
                return new XmlDocument().CreateComment("======Classes Parameter================" + Environment.NewLine);
            }
            set { }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Class", IsNullable = false)]
        public List<PCDefClass> Schema
        {
            get
            {
                return this.schemaField;
            }
            set
            {
                this.schemaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefPluginAssembly
    {
        private string pathField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Path
        {
            get
            {
                return this.pathField;
            }
            set
            {
                this.pathField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefConnectionParameter
    {

        private string nameField;

        private string descriptionField;

        private bool isSensibleDataField;

        private bool isSensibleDataFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsSensibleData
        {
            get
            {
                return this.isSensibleDataField;
            }
            set
            {
                this.isSensibleDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IsSensibleDataSpecified
        {
            get
            {
                return this.isSensibleDataFieldSpecified;
            }
            set
            {
                this.isSensibleDataFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefInitialization
    {

        private PCDefInitializationEnvironmentInitialization environmentInitializationField;

        private List<PCDefInitializationCommand> predefinedCommandsField;

        private List<PCDefInitializationCustomCommand> customCommandsField;



        [XmlAnyElement("CustomCommandsComment")]
        public XmlComment CustomCommandsComment
        {
            get
            {
                return new XmlDocument().CreateComment("======Custom Commands=================================" + Environment.NewLine);
            }
            set { }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CustomCommand", IsNullable = false)]
        /// <remarks/>
        public List<PCDefInitializationCustomCommand> CustomCommands
        {
            get
            {
                return this.customCommandsField;
            }
            set
            {
                this.customCommandsField = value;
            }
        }

        [XmlAnyElement("PredefinedCommandsComment")]
        public XmlComment PredefinedCommandsComment
        {
            get
            {
                return new XmlDocument().CreateComment("======Predefined Commands================" + Environment.NewLine);
            }
            set { }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Command", IsNullable = false)]
        public List<PCDefInitializationCommand> PredefinedCommands
        {
            get
            {
                return this.predefinedCommandsField;
            }
            set
            {
                this.predefinedCommandsField = value;
            }
        }

        [XmlAnyElement("EnvironmentInitializationComment")]
        public XmlComment EnvironmentInitializationComment
        {
            get
            {
                return new XmlDocument().CreateComment("======Environment Initialization================" + Environment.NewLine);
            }
            set { }
        }
        /// <remarks/>
        public PCDefInitializationEnvironmentInitialization EnvironmentInitialization
        {
            get
            {
                return this.environmentInitializationField;
            }
            set
            {
                this.environmentInitializationField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefInitializationCommand
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefInitializationCustomCommand
    {

        private string nameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefInitializationEnvironmentInitialization
    {

        private PCDefInitializationEnvironmentInitializationConnect connectField;

        private PCDefInitializationEnvironmentInitializationDisconnect disconnectField;

        [XmlAnyElement("ConnectComment")]
        public XmlComment ConnectComment
        {
            get
            {
                return new XmlDocument().CreateComment("======Connect================" + Environment.NewLine);
            }
            set { }
        }

        /// <remarks/>
        public PCDefInitializationEnvironmentInitializationConnect Connect
        {
            get
            {
                return this.connectField;
            }
            set
            {
                this.connectField = value;
            }
        }

        [XmlAnyElement("DisconnectComment")]
        public XmlComment DisconnectComment
        {
            get
            {
                return new XmlDocument().CreateComment("======Disconnect================" + Environment.NewLine);
            }
            set { }
        }

        /// <remarks/>
        public PCDefInitializationEnvironmentInitializationDisconnect Disconnect
        {
            get
            {
                return this.disconnectField;
            }
            set
            {
                this.disconnectField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefInitializationEnvironmentInitializationConnect
    {

        private PCDefInitializationEnvironmentInitializationConnectCommandSequence commandSequenceField;

        /// <remarks/>
        public PCDefInitializationEnvironmentInitializationConnectCommandSequence CommandSequence
        {
            get
            {
                return this.commandSequenceField;
            }
            set
            {
                this.commandSequenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefInitializationEnvironmentInitializationConnectCommandSequence
    {

        private PCDefInitializationEnvironmentInitializationConnectCommandSequenceItem itemField;

        /// <remarks/>
        public PCDefInitializationEnvironmentInitializationConnectCommandSequenceItem Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefInitializationEnvironmentInitializationConnectCommandSequenceItem
    {

        private List<PCDefInitializationEnvironmentInitializationConnectCommandSequenceItemSetParameter> setParameterField;

        private string commandField;

        private byte orderField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SetParameter")]
        public List<PCDefInitializationEnvironmentInitializationConnectCommandSequenceItemSetParameter> SetParameter
        {
            get
            {
                return this.setParameterField;
            }
            set
            {
                this.setParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Command
        {
            get
            {
                return this.commandField;
            }
            set
            {
                this.commandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Order
        {
            get
            {
                return this.orderField;
            }
            set
            {
                this.orderField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefInitializationEnvironmentInitializationConnectCommandSequenceItemSetParameter
    {

        private string paramField;

        private string sourceField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Param
        {
            get
            {
                return this.paramField;
            }
            set
            {
                this.paramField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefInitializationEnvironmentInitializationDisconnect
    {

        private PCDefInitializationEnvironmentInitializationDisconnectCommandSequence commandSequenceField;

        /// <remarks/>
        public PCDefInitializationEnvironmentInitializationDisconnectCommandSequence CommandSequence
        {
            get
            {
                return this.commandSequenceField;
            }
            set
            {
                this.commandSequenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefInitializationEnvironmentInitializationDisconnectCommandSequence
    {

        private PCDefInitializationEnvironmentInitializationDisconnectCommandSequenceItem itemField;

        /// <remarks/>
        public PCDefInitializationEnvironmentInitializationDisconnectCommandSequenceItem Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefInitializationEnvironmentInitializationDisconnectCommandSequenceItem
    {

        private string commandField;

        private byte orderField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Command
        {
            get
            {
                return this.commandField;
            }
            set
            {
                this.commandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Order
        {
            get
            {
                return this.orderField;
            }
            set
            {
                this.orderField = value;
            }
        }

        private List<GenericItemSetParameter> fieldSetParameter;

        [System.Xml.Serialization.XmlElementAttribute("SetParameter")]
        public List<GenericItemSetParameter> SetParameter
        {
            get
            {
                return fieldSetParameter;
            }
            set
            {
                this.fieldSetParameter = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClass
    {
        private List<PCDefClassProperty> propertiesField;

        private PCDefClassReadConfiguration readConfigurationField;

        private List<PCDefClassMethod> methodConfigurationField;

        private string nameField;

        [XmlAnyElement("PropertiesComment")]
        public XmlComment PropertiesComment
        {
            get
            {
                return new XmlDocument().CreateComment("======Properties================" + Environment.NewLine);
            }
            set { }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Property", IsNullable = false)]
        public List<PCDefClassProperty> Properties
        {
            get
            {
                return this.propertiesField;
            }
            set
            {
                this.propertiesField = value;
            }
        }

        [XmlAnyElement("ReadConfigurationComment")]
        public XmlComment ReadConfigurationComment
        {
            get
            {
                return new XmlDocument().CreateComment("======ReadConfiguration================" + Environment.NewLine);
            }
            set { }
        }
        /// <remarks/>
        public PCDefClassReadConfiguration ReadConfiguration
        {
            get
            {
                return this.readConfigurationField;
            }
            set
            {
                this.readConfigurationField = value;
            }
        }

        [XmlAnyElement("MethodConfigurationComment")]
        public XmlComment MethodConfigurationComment
        {
            get
            {
                return new XmlDocument().CreateComment("======Method Configuration================" + Environment.NewLine);
            }
            set { }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Method", IsNullable = false)]
        public List<PCDefClassMethod> MethodConfiguration
        {
            get
            {
                return this.methodConfigurationField;
            }
            set
            {
                this.methodConfigurationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassProperty
    {

        private List<object> itemsField;

        private string nameField;

        private string dataTypeField;

        private bool isUniqueKeyField;

        private bool isUniqueKeyFieldSpecified;

        private bool isMandatoryField;

        private bool isAutoFillField;

        private bool isAutoFillFieldSpecified;

        private bool isDisplayField;
        private bool isMultiValueField;
        private bool isRevisionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CommandMappings", typeof(PCDefClassPropertyCommandMappings))]
        [System.Xml.Serialization.XmlElementAttribute("ModifiedBy", typeof(PCDefClassPropertyModifiedBy))]
        [System.Xml.Serialization.XmlElementAttribute("ReturnBindings", typeof(PCDefClassPropertyReturnBindings))]
        public List<object> Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DataType
        {
            get
            {
                return this.dataTypeField;
            }
            set
            {
                this.dataTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsUniqueKey
        {
            get
            {
                return this.isUniqueKeyField;
            }
            set
            {
                this.isUniqueKeyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IsUniqueKeySpecified
        {
            get
            {
                return this.isUniqueKeyFieldSpecified;
            }
            set
            {
                this.isUniqueKeyFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsMandatory
        {
            get
            {
                return this.isMandatoryField;
            }
            set
            {
                this.isMandatoryField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsMultivalue
        {
            get
            {
                return this.isMultiValueField;
            }
            set
            {
                this.isMultiValueField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsDisplay
        {
            get
            {
                return this.isDisplayField;
            }
            set
            {
                this.isDisplayField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsRevision
        {
            get
            {
                return this.isRevisionField;
            }
            set
            {
                this.isRevisionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsAutoFill
        {
            get
            {
                return this.isAutoFillField;
            }
            set
            {
                this.isAutoFillField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IsAutoFillSpecified
        {
            get
            {
                return this.isAutoFillFieldSpecified;
            }
            set
            {
                this.isAutoFillFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassPropertyCommandMappings
    {

        private List<PCDefClassPropertyCommandMappingsMap> mapField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Map")]
        public List<PCDefClassPropertyCommandMappingsMap> Map
        {
            get
            {
                return this.mapField;
            }
            set
            {
                this.mapField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassPropertyCommandMappingsMap
    {

        private string toCommandField;

        private string parameterField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ToCommand
        {
            get
            {
                return this.toCommandField;
            }
            set
            {
                this.toCommandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Parameter
        {
            get
            {
                return this.parameterField;
            }
            set
            {
                this.parameterField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassPropertyModifiedBy
    {

        private List<PCDefClassPropertyModifiedByModBy> modByField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ModBy")]
        public List<PCDefClassPropertyModifiedByModBy> ModBy
        {
            get
            {
                return this.modByField;
            }
            set
            {
                this.modByField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassPropertyModifiedByModBy
    {

        private string commandField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Command
        {
            get
            {
                return this.commandField;
            }
            set
            {
                this.commandField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassPropertyReturnBindings
    {

        private List<PCDefClassPropertyReturnBindingsBind> bindField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Bind")]
        public List<PCDefClassPropertyReturnBindingsBind> Bind
        {
            get
            {
                return this.bindField;
            }
            set
            {
                this.bindField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassPropertyReturnBindingsBind
    {

        private string commandResultOfField;

        private string pathField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CommandResultOf
        {
            get
            {
                return this.commandResultOfField;
            }
            set
            {
                this.commandResultOfField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Path
        {
            get
            {
                return this.pathField;
            }
            set
            {
                this.pathField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassReadConfiguration
    {
        private PCDefClassReadConfigurationListingCommand listingCommandField;

        private PCDefClassReadConfigurationCommandSequence commandSequenceField;

        /// <remarks/>
        public PCDefClassReadConfigurationListingCommand ListingCommand
        {
            get
            {
                return this.listingCommandField;
            }
            set
            {
                this.listingCommandField = value;
            }
        }

        /// <remarks/>
        public PCDefClassReadConfigurationCommandSequence CommandSequence
        {
            get
            {
                return this.commandSequenceField;
            }
            set
            {
                this.commandSequenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassReadConfigurationListingCommand
    {

        private string commandField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Command
        {
            get
            {
                return this.commandField;
            }
            set
            {
                this.commandField = value;
            }
        }

        private List<GenericItemSetParameter> fieldSetParameter;

        [System.Xml.Serialization.XmlElementAttribute("SetParameter")]
        public List<GenericItemSetParameter> setParameter
        {
            get
            {
                return fieldSetParameter;
            }
            set
            {
                this.fieldSetParameter = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassReadConfigurationCommandSequence
    {

        private List<PCDefClassReadConfigurationCommandSequenceItem> itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Item")]
        public List<PCDefClassReadConfigurationCommandSequenceItem> Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassReadConfigurationCommandSequenceItem
    {

        private string commandField;

        private byte orderField;

        private List<GenericItemSetParameter> fieldSetParameter;

        [System.Xml.Serialization.XmlElementAttribute("SetParameter")]
        public List<GenericItemSetParameter> setParameter
        {
            get
            {
                return fieldSetParameter;
            }
            set
            {
                this.fieldSetParameter = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Command
        {
            get
            {
                return this.commandField;
            }
            set
            {
                this.commandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Order
        {
            get
            {
                return this.orderField;
            }
            set
            {
                this.orderField = value;
            }
        }
    }

    //=======================================================================
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GenericItemSetParameter
    {

        private string paramField;

        private string sourceField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Param
        {
            get
            {
                return this.paramField;
            }
            set
            {
                this.paramField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    //=================================================================================================

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassMethod
    {
        private PCDefClassMethodCommandSequence commandSequenceField;

        private string nameField;

        [XmlAnyElement("PCDefClassMethodComment")]
        public XmlComment PCDefClassMethodComment
        {
            get
            {
                return new XmlDocument().CreateComment("              ");
            }
            set { }
        }

        /// <remarks/>
        public PCDefClassMethodCommandSequence CommandSequence
        {
            get
            {
                return this.commandSequenceField;
            }
            set
            {
                this.commandSequenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassMethodCommandSequence
    {

        private List<PCDefClassMethodCommandSequenceItem> itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Item")]
        public List<PCDefClassMethodCommandSequenceItem> Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PCDefClassMethodCommandSequenceItem
    {

        private string commandField;

        private byte orderField;

        private string conditionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Command
        {
            get
            {
                return this.commandField;
            }
            set
            {
                this.commandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Order
        {
            get
            {
                return this.orderField;
            }
            set
            {
                this.orderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Condition
        {
            get
            {
                return this.conditionField;
            }
            set
            {
                this.conditionField = value;
            }
        }

        private List<GenericItemSetParameter> fieldSetParameter;

        [System.Xml.Serialization.XmlElementAttribute("SetParameter")]
        public List<GenericItemSetParameter> SetParameter
        {
            get
            {
                return fieldSetParameter;
            }
            set
            {
                this.fieldSetParameter = value;
            }
        }
    }



}
